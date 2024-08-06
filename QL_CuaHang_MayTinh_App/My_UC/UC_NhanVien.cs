using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_NhanVien : UserControl
    {
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        BUS_KhachHang bus=new BUS_KhachHang();
        private string HanhDong = "";
        public UC_NhanVien()
        {
            InitializeComponent();
            LoadData();
            this.btnThem.Click += BtnThem_Click;
            this.btn_delete.Click += Btn_delete_Click;
            this.btn_Huy.Click += Btn_Huy_Click;
            this.btn_save.Click += Btn_save_Click;
            this.btn_update.Click += Btn_update_Click;
            this.btn_Search_tenNV.Click += Btn_Search_tenNV_Click;
            dataGridView_NV.SelectionChanged += DataGridView_NV_SelectionChanged;
        }

        private void DataGridView_NV_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_NV.SelectedRows.Count > 0)
            {
                string manv = dataGridView_NV.CurrentRow.Cells["MaNV"].Value.ToString();
                string tennv = dataGridView_NV.CurrentRow.Cells["TenNV"].Value.ToString();
                string diachi = dataGridView_NV.CurrentRow.Cells["DiaChi"].Value.ToString();
                string sdt = dataGridView_NV.CurrentRow.Cells["SDT"].Value.ToString();
                string email = dataGridView_NV.CurrentRow.Cells["Email"].Value.ToString();

                txt_maNV.Text = manv;
                txt_tenNV.Text = tennv;
                txt_diachiNV.Text = diachi;
                txt_sdtNV.Text = sdt;
                txt_emailNV.Text = email;
            }
        }
        public bool KT_ThongTin_NhanVien()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_NhanVien.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox và không phải là txt_maKH
                if (control is TextBox textBox && textBox != txt_maNV)
                {
                    // Kiểm tra nếu TextBox trống
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Hiển thị thông báo và trả về false nếu có TextBox trống
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin nha!!");
                        return false;
                    }
                }
            }
            // Trả về true nếu tất cả các TextBox (trừ txt_maKH) đều không trống
            return true;
        }

        public void XoaText_ThongTin_KhachHang()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_NhanVien.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox
                if (control is TextBox textBox)
                {
                    // Kiểm tra nếu TextBox không trống
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Xóa nội dung của TextBox
                        textBox.Text = string.Empty;
                    }
                }
            }


        }

        //public void XoaText()
        //{
        //    txt_maNV.Text = "";
        //    txt_tenNV.Text = "";
        //    txt_diachiNV.Text = "";
        //    txt_sdtNV.Text = "";
        //    txt_emailNV.Text = "";
        //    checkbox_nv.Checked = false;
        //}

        private void Btn_Search_tenNV_Click(object sender, EventArgs e)
        {
            string text_nv = txt_Search_tenSP.Text;
            //bus_cv.FindByName(text_chucvu);
            dataGridView_NV.DataSource = bus_nv.FindByName(text_nv);
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            txt_maNV.Enabled = false;

            HanhDong = "Sua";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btnThem.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            string MaNV_random = new Random().Next(100, 9999).ToString();
            string maNV = txt_maNV.Text;
            string tennv = txt_tenNV.Text;
            string diachi = txt_diachiNV.Text;
            string sdt = txt_sdtNV.Text;
            string email = txt_emailNV.Text;
            bool checkbox = checkbox_nv.Checked;

            switch (HanhDong)
            {
                case "Them":
                    if (!KT_ThongTin_NhanVien())
                    {
                        // Nếu thông tin không đầy đủ, dừng việc thực hiện
                        return;
                    }

                    // Kiểm tra định dạng email
                    if (!bus.KT_DinhDangEmail(email))
                    {
                        MessageBox.Show("Email không hợp lệ. Vui lòng nhập email đúng định dạng.");
                        return;
                    }

                    // Tìm khách hàng dựa trên số điện thoại
                    nhanvien khangSDT = bus_nv.TimSDTKNhanVien(sdt);
                    if (khangSDT != null)
                    {
                        // Nếu tìm thấy khách hàng với số điện thoại này, thông báo lỗi
                        MessageBox.Show("Số điện thoại này đã đăng ký rồi, chọn số điện thoại khác nhé!!!");
                        return;
                    }

                    // Tìm khách hàng dựa trên email
                    nhanvien khangEmail = bus_nv.TimEmailNhanVien(email);
                    if (khangEmail != null)
                    {
                        // Nếu tìm thấy khách hàng với email này, thông báo lỗi
                        MessageBox.Show("Email này đã đăng ký rồi, chọn email khác nhé!!!");
                        return;
                    }

                    bus_nv.Insert(MaNV_random, tennv, diachi, sdt, email);
                    MessageBox.Show("Bạn đã thêm nhân viên thành công rồi nha!!!");
                    LoadData();
                    XoaText_ThongTin_KhachHang();
                    break;

                case "Sua":
                    if (String.IsNullOrEmpty(maNV))
                    {
                        MessageBox.Show("Vui lòng chọn mã nhân viên để sửa!!!");
                        break;
                    }
                    bus_nv.Update(MaNV_random, tennv, diachi, sdt, checkbox);
                    MessageBox.Show("Sửa nhân viên thành công.");
                    LoadData();
                    XoaText_ThongTin_KhachHang();

                    break;

                case "Xoa":
                    if (String.IsNullOrEmpty(maNV))
                    {
                        MessageBox.Show("Vui lòng chọn mã nhân viên để xóa!!!");
                        break;
                    }
                    if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        nhanvien nv = bus_nv.FindByID(maNV);
                        if (nv != null)
                        {
                            bool kq = bus_nv.Delete(maNV);
                            if (kq == true)
                            {
                                MessageBox.Show("Xóa nhân viên thành công!");
                                LoadData();
                                XoaText_ThongTin_KhachHang();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá nhân viên này!");
                            }
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Không có hành động nào được chọn!");
                    break;
            }
        }

        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            XoaText_ThongTin_KhachHang();
            btn_save.Enabled = false;
            btn_Huy.Enabled = false;
            btnThem.Enabled = true;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            HanhDong = "";
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            txt_maNV.Enabled = false;
            if (txt_maNV != null)
            {
                HanhDong = "Xoa";
                btn_save.Enabled = true;
                btn_Huy.Enabled = true;
                btnThem.Enabled = false;
                btn_update.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui Lòng nhập dữ liệu đầy đủ !!");
                return;
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            XoaText_ThongTin_KhachHang();
            HanhDong = "Them";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            txt_maNV.Enabled = false;
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            dataGridView_NV.DataSource = bus_nv.GetData();
        }
    }
}
