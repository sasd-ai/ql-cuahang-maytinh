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
    public partial class UC_NhaCungCap : UserControl
    {
        BUS_NhaCungCap bus_ncc = new BUS_NhaCungCap();
        private string HanhDong = "";
        public UC_NhaCungCap()
        {
            InitializeComponent();
            LoadData();
            dataGridView_NCC.SelectionChanged += DataGridView_NCC_SelectionChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btn_delete.Click += Btn_delete_Click;
            this.btn_Huy.Click += Btn_Huy_Click;
            this.btn_save.Click += Btn_save_Click;
            this.btn_update.Click += Btn_update_Click;
            this.btn_Search_TenNCC.Click += Btn_Search_TenNCC_Click;
        }
        public void XoaText()
        {
            txt_maNCC.Text = "";
            txt_tenNCC.Text = "";
            txt_diachiNCC.Text = "";
            txt_sdtNCC.Text = "";
        }

        private void Btn_Search_TenNCC_Click(object sender, EventArgs e)
        {

            string input_tenncc = txt_Search_tenNCC.Text;
            dataGridView_NCC.DataSource = bus_ncc.FindByName(input_tenncc);
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {

            txt_maNCC.Enabled = false;

            HanhDong = "Sua";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btnThem.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
                string MaNCC_random = new Random().Next(100, 9999).ToString();
                string maNCC = txt_maNCC.Text;
                string tenncc = txt_tenNCC.Text;
                string diachi = txt_diachiNCC.Text;
                string sdt = txt_sdtNCC.Text;
            nhacungcap ncc = new nhacungcap()
            {
                MaNCC = MaNCC_random,
                TenNCC = tenncc,
                DiaChi = diachi,
                SDT = sdt,
            };

                switch (HanhDong)
                {
                    case "Them":
                        bus_ncc.ThemNhaCungCap(ncc);
                        MessageBox.Show("Bạn đã thêm nhà cung cấp thành công rồi nha!!!");
                        LoadData();
                        break;

                    case "Sua":
                        bus_ncc.SuaNhaCungCap(maNCC, tenncc, diachi, sdt);
                        MessageBox.Show("Sửa nhà cung cấp thành công.");
                        LoadData();

                        break;

                    case "Xoa":
                        if (MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                        nhacungcap nccs = bus_ncc.FindByID(maNCC);
                        if (nccs != null)
                        {
                            bool kq = bus_ncc.XoaNhaCungCap(maNCC);
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
            txt_maNCC.Enabled = false;
            if (txt_maNCC != null)
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
            txt_maNCC.Enabled = false;
        }

        private void DataGridView_NCC_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_NCC.SelectedRows.Count > 0)
            {
                string mancc = dataGridView_NCC.CurrentRow.Cells["MaNCC"].Value.ToString();
                string tenncc = dataGridView_NCC.CurrentRow.Cells["TenNCC"].Value.ToString();
                string diachincc = dataGridView_NCC.CurrentRow.Cells["DiaChi"].Value.ToString();
                string sdtncc = dataGridView_NCC.CurrentRow.Cells["SDT"].Value.ToString();

                txt_maNCC.Text = mancc;
                txt_tenNCC.Text = tenncc;
                txt_diachiNCC.Text = diachincc;
                txt_sdtNCC.Text = sdtncc;
            }
        }

        public void LoadData()
        {
            dataGridView_NCC.DataSource = bus_ncc.GetData();
        }
    }
}
