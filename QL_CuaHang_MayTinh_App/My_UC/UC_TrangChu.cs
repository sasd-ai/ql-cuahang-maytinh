using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_TrangChu : UserControl
    {
        BUS_ThongKe tk=new BUS_ThongKe();
        public UC_TrangChu()
        {
            InitializeComponent();
            LoadDuLieu();
            LoadChartTop5SanPham();
        }

        public void LoadDuLieu()
        {
            var (TongHoaDon, TongDoanhThu,TongKhachHang,TongNhanVien) = tk.ThongKe();

            label_TongDonHang.Text = TongHoaDon.ToString();
            label_Tong_DoanhThu.Text = TongDoanhThu.ToString("N0") + "VND";
            label_Tong_NhanVien.Text = TongNhanVien.ToString();
            label_Tong_KH.Text = TongKhachHang.ToString();
        }

        private void LoadChartTop5SanPham()
        {
           
            var top5SanPham = tk.GetTop5SanPhamBanChay();

            // Xóa dữ liệu hiện tại trong chart
            chart_Top5SP.Series["Series1"].Points.Clear();

            // Thêm dữ liệu vào chart
            foreach (var item in top5SanPham)
            {
                chart_Top5SP.Series["Series1"].Points.AddXY(item.TenSP, item.SoLuong);
            }
        }

    }
}
