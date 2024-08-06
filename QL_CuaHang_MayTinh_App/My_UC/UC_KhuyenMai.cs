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
    public partial class UC_KhuyenMai : UserControl
    {
        BUS_KhuyenMai bus_km = new BUS_KhuyenMai();
        private string HanhDong = "";
        public UC_KhuyenMai()
        {
            InitializeComponent();
            LoadData();
            this.dataGridView_km.SelectionChanged += DataGridView_km_SelectionChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btn_delete.Click += Btn_delete_Click;
            this.btn_update.Click += Btn_update_Click;
            this.btn_save.Click += Btn_save_Click;
            this.btn_Huy.Click += Btn_Huy_Click;
            this.btn_Search_TenKM.Click += Btn_Search_TenKM_Click;
        }

        private void Btn_Search_TenKM_Click(object sender, EventArgs e)
        {
            string text_km = txt_Search_tenKM.Text;
            //bus_cv.FindByName(text_chucvu);
            dataGridView_km.DataSource = bus_km.FindByName(text_km);
        }
        public bool KT_ThongTin_KM()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_KM.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox và không phải là txt_maKH
                if (control is TextBox textBox && textBox != txt_maKM)
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

        public void XoaText_ThongTin_KM()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_KM.Controls)
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
        //    txt_maKM.Text = "";
        //    txt_tenKM.Text = "";
        //    txt_TriGia.Text = "";
        //}

        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            XoaText_ThongTin_KM();
            btn_save.Enabled = false;
            btn_Huy.Enabled = false;
            btnThem.Enabled = true;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            HanhDong = "";
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            string MaKM_random = new Random().Next(100, 9999).ToString();
            string maKM = txt_maKM.Text;
            string tenKM = txt_tenKM.Text;
            string giatri = txt_TriGia.Text;
            int trigia = int.Parse(giatri);
            switch (HanhDong)
            {
                case "Them":
                    if(!KT_ThongTin_KM())
                    {
                        return;
                    }    
                    bus_km.ThemKhuyenMai(MaKM_random, tenKM, trigia);
                    MessageBox.Show("Bạn đã thêm khuyến mãi thành công rồi nha!!!");
                    LoadData();
                    break; 

                case "Sua":
                    if(string.IsNullOrEmpty(maKM))
                    {
                        MessageBox.Show("Vui lòng chọn mã khuyến mãi để sửa.");
                        break;
                    }    
                    bus_km.SuaKhuyenMai(maKM, tenKM, trigia);
                    MessageBox.Show("Sửa khuyến mãi thành công.");
                    LoadData();

                    break;

                case "Xoa":
                    if (string.IsNullOrEmpty(maKM))
                    {
                        MessageBox.Show("Vui lòng chọn mã khuyến mãi để xóa.");
                        break;
                    }
                    if (MessageBox.Show("Bạn có chắc muốn xóa khuyến mãi này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        khuyenmai km = bus_km.FindByID(maKM);
                        if (km != null)
                        {
                            bool kq = bus_km.XoaKhuyenMai(maKM);
                            if (kq == true)
                            {
                                MessageBox.Show("Xóa khuyến mãi thành công!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá khuyến mãi này!");
                            }
                        }

                    }
                    break;
                default:
                    MessageBox.Show("Không có hành động nào được chọn!");
                    break;
            }
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            txt_maKM.Enabled = false;

            HanhDong = "Sua";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btnThem.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            txt_maKM.Enabled = false;
            if (txt_maKM != null)
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
            XoaText_ThongTin_KM();
            HanhDong = "Them";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            txt_maKM.Enabled = false;
        }

        private void DataGridView_km_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_km.SelectedRows.Count > 0)
            {
                string makm = dataGridView_km.CurrentRow.Cells["MaKM"].Value.ToString();
                string tenkm = dataGridView_km.CurrentRow.Cells["TenKM"].Value.ToString();              
                string giatri = dataGridView_km.CurrentRow.Cells["GiaTri"].Value.ToString();

                txt_maKM.Text = makm;
                txt_tenKM.Text = tenkm;
                txt_TriGia.Text = giatri;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadData()
        {
            dataGridView_km.DataSource = bus_km.GetData();
        }
    }
}
