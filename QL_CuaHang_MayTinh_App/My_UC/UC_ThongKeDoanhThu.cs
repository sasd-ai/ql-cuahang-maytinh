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
            LoadCharts();
            LoadCharts_off();
            LoadChartsTong();
            chart_ThangTong.Click += Chart_ThangTong_Click;
        }

        private void Chart_ThangTong_Click(object sender, EventArgs e)
        {
            // Kiểm tra sự kiện nhấp chuột trên biểu đồ
            var result = chart_ThangTong.HitTest(MousePosition.X, MousePosition.Y, ChartElementType.DataPoint);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // Lấy điểm dữ liệu được nhấp
                var dataPoint = chart_ThangTong.Series["Series1"].Points[result.PointIndex];

                // Lấy tháng và doanh thu từ điểm dữ liệu
                int month = (int)dataPoint.XValue;
                double totalRevenue = dataPoint.YValues[0];

                // Cập nhật Label với tổng doanh thu của tháng được chọn
                lblDoanhThuThang.Text = $"Tổng doanh thu tháng {month}: {totalRevenue:#,###,###,##0} VND";
            }
        }


        private void LoadCharts()
        {
            DateTime today = DateTime.Now;

            
            List<Tuple<DateTime, int>> dailySales = tk.GetDoanhThuNgay(today);
            chart_Ngay.Series["Ngay"].Points.Clear();
            foreach (var sale in dailySales)
            {
                chart_Ngay.Series["Ngay"].Points.AddXY(sale.Item1.ToString("dd/MM/yyyy"), sale.Item2);
            }
            chart_Ngay.ChartAreas[0].AxisX.Interval = 1;

          
            chart_Ngay.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";  

           
            List<Tuple<DateTime, int>> monthlySales = tk.GetDoanhThuThang(today);
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

        private void LoadChartsTong()
        {
            DateTime today = DateTime.Now;

            // Get data for online and offline sales
            List<Tuple<DateTime, int>> onlineSalesDateTime = tk.GetDoanhThuThang(today);
            List<Tuple<int, double>> offlineSales = tk.GetDoanhThu12Thang_Tong(today.Year);

            // Convert onlineSalesDateTime to List<Tuple<int, int>>
            List<Tuple<int, int>> onlineSales = onlineSalesDateTime
                .Select(x => new Tuple<int, int>(x.Item1.Month, x.Item2))
                .ToList();

            // Combine online and offline sales
            List<Tuple<int, double>> combinedSales = new List<Tuple<int, double>>();
            for (int month = 1; month <= 12; month++)
            {
                double onlineTotal = onlineSales.FirstOrDefault(x => x.Item1 == month)?.Item2 ?? 0;
                double offlineTotal = offlineSales.FirstOrDefault(x => x.Item1 == month)?.Item2 ?? 0;
                combinedSales.Add(new Tuple<int, double>(month, onlineTotal + offlineTotal));
            }

            // Populate the combined chart
            chart_ThangTong.Series["Series1"].Points.Clear();
            foreach (var sale in combinedSales)
            {
                // Convert month number to month name in Vietnamese
                string monthName = new DateTime(today.Year, sale.Item1, 1)
                                   .ToString("MMMM", new System.Globalization.CultureInfo("vi-VN"));
                chart_ThangTong.Series["Series1"].Points.AddXY(monthName, sale.Item2);
            }

            // Format Y-axis for the combined chart
            chart_ThangTong.ChartAreas[0].AxisY.LabelStyle.Format = "#,###,###,##0 VND";
        }

        private void chart_month_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
