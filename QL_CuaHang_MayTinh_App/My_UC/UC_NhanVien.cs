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

        public void XoaText()
        {
            txt_maNV.Text = "";
            txt_tenNV.Text = "";
            txt_diachiNV.Text = "";
            txt_sdtNV.Text = "";
            txt_emailNV.Text = "";
            checkbox_nv.Checked = false;
        }

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
                    bus_nv.Insert(MaNV_random, tennv, diachi, sdt, email);
                    MessageBox.Show("Bạn đã thêm nhân viên thành công rồi nha!!!");
                    LoadData();
                    break;

                case "Sua":
                    bus_nv.Update(MaNV_random, tennv, diachi, sdt, checkbox);
                    MessageBox.Show("Sửa nhân viên thành công.");
                    LoadData();

                    break;

                case "Xoa":
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
            XoaText();
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
            XoaText();
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
