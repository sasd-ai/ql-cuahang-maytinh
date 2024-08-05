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
    public partial class UC_KhachHang : UserControl
    {
        BUS_KhachHang bus_kh = new BUS_KhachHang();
        private string HanhDong = "";


        public UC_KhachHang()
        {
            InitializeComponent();
            LoadData();
            this.dataGridView_KhachHang.SelectionChanged += DataGridView_KhachHang_SelectionChanged;
            this.btnThem.Click += btnThem_Click;
            this.btn_delete.Click += btn_delete_Click;
            this.btn_update.Click += btn_update_Click;
            this.btn_save.Click += btn_save_Click;
            this.btn_Huy.Click += btn_Huy_Click;
            this.btn_Search_TenKH.Click += Btn_Search_TenKH_Click;
        }

        private void Btn_Search_TenKH_Click(object sender, EventArgs e)
        {
                string text_khachhang = txt_Search_tenKH.Text;
                //bus_cv.FindByName(text_chucvu);
                dataGridView_KhachHang.DataSource = bus_kh.FindByName(text_khachhang);
        }

        private void DataGridView_KhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_KhachHang.SelectedRows.Count > 0)
            {
                string makh = dataGridView_KhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
                string tenkh = dataGridView_KhachHang.CurrentRow.Cells["tenKH"].Value.ToString();
                string sdt = "";
                if (dataGridView_KhachHang.CurrentRow.Cells["sdt"].Value != null)
                     sdt = dataGridView_KhachHang.CurrentRow.Cells["sdt"].Value.ToString(); 
                string email = dataGridView_KhachHang.CurrentRow.Cells["email"].Value.ToString();

                txt_maKH.Text = makh;
                txt_tenKH.Text = tenkh;
                txt_SDT.Text = sdt;
                txt_EMAIL.Text = email;
            }
        }

        private void UC_KhachHang_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void XoaText()
        {
            txt_maKH.Text = "";
            txt_tenKH.Text = "";
            txt_SDT.Text = "";
            txt_EMAIL.Text = "";

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaText();
            HanhDong = "Them";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            txt_maKH.Enabled = false;

        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            XoaText();
            btn_save.Enabled = false;
            btn_Huy.Enabled = false;
            btnThem.Enabled = true;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            HanhDong = "";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            txt_maKH.Enabled = false;

            HanhDong = "Sua";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btnThem.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            txt_maKH.Enabled = false;
            if (txt_maKH != null)
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            string MaKH_random = new Random().Next(100, 9999).ToString();
            string maKH = txt_maKH.Text;
            string tenKH = txt_tenKH.Text;
            string SDT = txt_SDT.Text;
            string email = txt_EMAIL.Text;
            switch (HanhDong)
            {
                case "Them":
                    bus_kh.ThemKhachHang(MaKH_random, tenKH, SDT, email);
                    MessageBox.Show("Bạn đã thêm khách hàng thành công rồi nha!!!");
                    LoadData();
                    break;

                case "Sua":
                    bus_kh.SuaKhachHang(maKH, tenKH, SDT, email);
                    MessageBox.Show("Sửa khách hàng thành công.");
                    LoadData();

                    break;

                case "Xoa":
                    if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        khachhang kh = bus_kh.FindByID(maKH);
                        if (kh != null)
                        {
                            bool kq = bus_kh.XoaKhachHang(maKH);
                            if (kq == true)
                            {
                                MessageBox.Show("Xóa khách hàng thành công!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá khách hàng này!");
                            }
                        }

                    }
                    break;
                default:
                    MessageBox.Show("Không có hành động nào được chọn!");
                    break;
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Search_tenSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_KhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void LoadData()
        {
            dataGridView_KhachHang.DataSource = bus_kh.GetData();
        }
    }
}
