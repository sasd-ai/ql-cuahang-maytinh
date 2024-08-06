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
    public partial class UC_LoaiSanPham : UserControl
    {
        BUS_LoaiSanPham bus_lsp = new BUS_LoaiSanPham();
        private string HanhDong = "";
        public UC_LoaiSanPham()
        {
            InitializeComponent();
            LoadData();
            dataGridView_LoaiSP.SelectionChanged += DataGridView_LoaiSP_SelectionChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btn_delete.Click += Btn_delete_Click;
            this.btn_Huy.Click += Btn_Huy_Click;
            this.btn_save.Click += Btn_save_Click;
            this.btn_Search_TenLoaiSP.Click += Btn_Search_TenLoaiSP_Click;
            this.btn_update.Click += Btn_update_Click;
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            txt_maLSP.Enabled = false;

            HanhDong = "Sua";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btnThem.Enabled = false;
            btn_delete.Enabled = false;
        }
        public bool KT_ThongTin_LSP()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_LSP.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox và không phải là txt_maKH
                if (control is TextBox textBox && textBox != txt_maLSP)
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

        public void XoaText_ThongTin_LSP()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_LSP.Controls)
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

        //private void XoaText()
        //{
        //    txt_maLSP.Text = "";
        //    txt_tenLSP.Text = "";
        //}
        private void DataGridView_LoaiSP_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_LoaiSP.SelectedRows.Count > 0)
            {
                string maLSP = dataGridView_LoaiSP.CurrentRow.Cells["MaLoai"].Value.ToString();
                string tenLSP = dataGridView_LoaiSP.CurrentRow.Cells["TenLoaiSP"].Value.ToString();

                txt_maLSP.Text = maLSP;
                txt_tenLSP.Text = tenLSP;
            }
        }

        private void Btn_Search_TenLoaiSP_Click(object sender, EventArgs e)
        {
            string text_lsp = txt_Search_tenLOAISP.Text;
            //bus_cv.FindByName(text_chucvu);
            dataGridView_LoaiSP.DataSource = bus_lsp.FindByName(text_lsp);
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            string MaLSP_random = new Random().Next(100, 9999).ToString();
            string maLSP = txt_maLSP.Text;
            string tenLSP = txt_tenLSP.Text;
            switch (HanhDong)
            {
                case "Them":
                    if(!KT_ThongTin_LSP())
                    {
                        return;
                    }    
                    bus_lsp.ThemLoaiSanPham(MaLSP_random, tenLSP);
                    MessageBox.Show("Bạn đã thêm loại sản phẩm thành công rồi nha!!!");
                    LoadData();
                    break;

                case "Sua":
                    if (String.IsNullOrEmpty(maLSP))
                    {
                        MessageBox.Show("Vui lòng chọn mã loại sản phẩm để sửa!!!");
                        break;
                    }
                    bus_lsp.SuaLoaiSanPham(maLSP, tenLSP);
                    MessageBox.Show("Sửa loại sản phẩm thành công.");
                    LoadData();

                    break;

                case "Xoa":
                    if (String.IsNullOrEmpty(maLSP))
                    {
                        MessageBox.Show("Vui lòng chọn mã loại sản phẩm để xóa!!!");
                        break;
                    }
                    if (MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        loaisp lsp = bus_lsp.FindByID(maLSP);
                        if (lsp != null)
                        {
                            bool kq = bus_lsp.XoaLoaiSanPham(maLSP);
                            if (kq == true)
                            {
                                MessageBox.Show("Xóa loại sản phẩm thành công!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá loại sản phẩm này!");
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
            XoaText_ThongTin_LSP();
            btn_save.Enabled = false;
            btn_Huy.Enabled = false;
            btnThem.Enabled = true;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            HanhDong = "";
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            txt_maLSP.Enabled = false;
            if (txt_maLSP != null)
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
            XoaText_ThongTin_LSP();
            HanhDong = "Them";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            txt_maLSP.Enabled = false;
        }

        public void LoadData()
        {
            dataGridView_LoaiSP.DataSource = bus_lsp.GetData();
        }
    }
}
