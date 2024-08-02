using BUS;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_ThongKeDoanhThu : UserControl
    {
        BUS_ThongKe tk = new BUS_ThongKe();
        public UC_ThongKeDoanhThu()
        {
            InitializeComponent();
            LoadCharts_Online();
            LoadCharts_off();

            LoadMonths();
        }
        private void LoadMonths()
        {
            // Sử dụng mảng để lưu trữ tên các tháng tiếng Việt
            string[] monthNames = { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                    "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };

            for (int i = 0; i < 12; i++)
            {
                cbb_Month.Items.Add(monthNames[i]);
            }
            cbb_Month.SelectedIndex = 0; // Chọn tháng đầu tiên (tháng 1)
        }



        private void LoadCharts_Online()
        {
            DateTime today = DateTime.Now;


            List<Tuple<DateTime, int>> dailySales = tk.GetDoanhThuNgay_Online(today);
            chart_Ngay.Series["Ngay"].Points.Clear();
            foreach (var sale in dailySales)
            {
                chart_Ngay.Series["Ngay"].Points.AddXY(sale.Item1.ToString("dd/MM/yyyy"), sale.Item2);
            }
            chart_Ngay.ChartAreas[0].AxisX.Interval = 1;


            chart_Ngay.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";


            List<Tuple<DateTime, int>> monthlySales = tk.GetDoanhThuThang_Online(today);
            chart_month.Series["Thang"].Points.Clear();
            foreach (var sale in monthlySales)
            {
                chart_month.Series["Thang"].Points.AddXY(sale.Item1.ToString("MM/yyyy"), sale.Item2);
            }
            chart_month.ChartAreas[0].AxisX.Interval = 1;


            chart_month.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";
        }


        private void LoadCharts_off()
        {
            DateTime today = DateTime.Now;


            List<Tuple<DateTime, double>> dailySales = tk.GetDoanhThuNgay_off(today);
            chart_Ngay_off.Series["Ngay"].Points.Clear();
            foreach (var sale in dailySales)
            {
                chart_Ngay_off.Series["Ngay"].Points.AddXY(sale.Item1.ToString("dd/MM/yyyy"), sale.Item2);
            }
            chart_Ngay_off.ChartAreas[0].AxisX.Interval = 1;


            chart_Ngay_off.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";


            List<Tuple<DateTime, double>> monthlySales = tk.GetDoanhThuThang_off(today);
            chart_thang_off.Series["Thang"].Points.Clear();
            foreach (var sale in monthlySales)
            {
                chart_thang_off.Series["Thang"].Points.AddXY(sale.Item1.ToString("MM/yyyy"), sale.Item2);
            }
            chart_thang_off.ChartAreas[0].AxisX.Interval = 1;


            chart_thang_off.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";
        }


        private void LoadCharts_Tong(DateTime date, bool showMonth = false)
        {
            chart_TheoThang.Series["Series1"].Points.Clear();

            // Biến để lưu tổng tiền và số lượng hóa đơn
            double tongTien = 0;
            int soLuongHoaDon = 0;

            if (showMonth)
            {
                // Load biểu đồ theo tháng
                DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                List<DTO_ThongKe> monthlySales = tk.GetDoanhThuThang_Tong(firstDayOfMonth, lastDayOfMonth);

                foreach (var sale in monthlySales)
                {
                    chart_TheoThang.Series["Series1"].Points.AddXY(sale.Ngay.ToString("MM/yyyy"), sale.TongTien);
                    tongTien += sale.TongTien;
                    soLuongHoaDon += sale.SoLuongHoaDon; // Cập nhật số lượng hóa đơn từ DTO
                }
            }
            else
            {
                // Load biểu đồ theo ngày
                List<DTO_ThongKe> dailySales = tk.GetDoanhThuNgay_Tong(date);

                foreach (var sale in dailySales)
                {
                    chart_TheoThang.Series["Series1"].Points.AddXY(sale.Ngay.ToString("dd/MM/yyyy"), sale.TongTien);
                    tongTien += sale.TongTien;
                    soLuongHoaDon += sale.SoLuongHoaDon; // Cập nhật số lượng hóa đơn từ DTO
                }
            }

            chart_TheoThang.ChartAreas[0].AxisX.Interval = 1;
            chart_TheoThang.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";

            // Cập nhật label
            label_SoLuongDonHang.Text = soLuongHoaDon.ToString();
            label_TongTienDonHang.Text = tongTien.ToString("#,###,###,##0 VND");
        }

        private void btn_Today_Click_1(object sender, EventArgs e)
        {
            LoadCharts_Tong(DateTime.Now, false); // Hiển thị theo ngày
        }

        private void btn_month_Click_1(object sender, EventArgs e)
        {
            // Lấy tháng được chọn từ combobox (chỉ số bắt đầu từ 0)
            int selectedMonth = cbb_Month.SelectedIndex + 1;
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, selectedMonth, 1);
            LoadCharts_Tong(firstDayOfMonth, true); // Hiển thị theo tháng
        }

    }
}