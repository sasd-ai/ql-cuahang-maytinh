using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using QL_CuaHang_MayTinh_App.GUI;
namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_NhapHang : UserControl
    {
        BUS_PhieuNhapHang bUS_phieuNhapHang=new BUS_PhieuNhapHang();
        List<DS_PhieuNhap> listPN = new List<DS_PhieuNhap>();
        public UC_NhapHang()
        {
            InitializeComponent();
            this.Load += UC_NhapHang_Load;
            listPN = bUS_phieuNhapHang.GetPhieuNhap();
        }

        private void UC_NhapHang_Load(object sender, EventArgs e)
        {
            this.dgv_PhieuNhapHang.RowTemplate.Height = 50;
            LoadDataPhieuNhap();
        }

        private void LoadDataPhieuNhap()
        {
           
            dgv_PhieuNhapHang.DataSource=listPN;
            formatDataGridView_PhieuNhap();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dateTimePicker = sender as DateTimePicker;

            if (dateTimePicker != null)
            {
               //Lấy giá trị
                DateTime selectedDate = dateTimePicker.Value;
                MessageBox.Show(selectedDate.ToString());
                dgv_PhieuNhapHang.DataSource = listPN.Where(pn => pn.NgayNhap.Date==selectedDate.Date).ToList(); ;
            }
        }
        void formatDataGridView_PhieuNhap()
        {
            // Thiết lập tiêu đề cột
            dgv_PhieuNhapHang.Columns["MaPN"].HeaderText = "Mã phiếu";
            dgv_PhieuNhapHang.Columns["TenNV"].HeaderText = "Nhân viên";
            dgv_PhieuNhapHang.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
            dgv_PhieuNhapHang.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgv_PhieuNhapHang.Columns["TongTien"].HeaderText ="Tổng tiền";
            dgv_PhieuNhapHang.Columns["NgayNhap"].HeaderText = "Ngày nhập";

            

            // Ẩn cột "MatKhau" nếu tồn tại
            if (dgv_PhieuNhapHang.Columns.Contains("MaNV"))
            {
                dgv_PhieuNhapHang.Columns["MaNV"].Visible = false;
            }
            if (dgv_PhieuNhapHang.Columns.Contains("MaNCC"))
            {
                dgv_PhieuNhapHang.Columns["MaNCC"].Visible = false;
            }
        }
       
        private void btn_Sreach_Click(object sender, EventArgs e)
        {
            string keySreach=txt_KeySreach.Text;
            string trangThai=cbb_TrangThaiTimKiem.SelectedItem.ToString();
            if (String.IsNullOrEmpty(trangThai))
            {
                switch (trangThai)
                {
                    case "Tất cả":
                        //Lấy hết danh sách
                        dgv_PhieuNhapHang.DataSource = listPN;
                        break;
                    case "Tên nhân viên":
                        dgv_PhieuNhapHang.DataSource = bUS_phieuNhapHang.FindByNhanVien(keySreach);
                        break;
                    case "Tên nhà cung cấp":
                        dgv_PhieuNhapHang.DataSource = bUS_phieuNhapHang.FindByNCC(keySreach);
                        break;

                }
            }
        }
    }
}
