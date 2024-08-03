using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_ThongKe
    {
        DAO_ThongKe tk = new DAO_ThongKe();
        public BUS_ThongKe() { }

        public List<Tuple<DateTime, int>> GetDoanhThuNgay_Online(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<DateTime> allDays = Enumerable.Range(0, (lastDayOfMonth - firstDayOfMonth).Days + 1)
                                            .Select(x => firstDayOfMonth.AddDays(x))
                                            .ToList();

            List<Tuple<DateTime, int>> salesData = allDays
                .Select(day => tk.GetNgay(day))
                .SelectMany(x => x)
                .ToList();

            return allDays.GroupJoin(
                salesData,
                day => day,
                sale => sale.Item1,
                (day, sales) => new Tuple<DateTime, int>(day, sales.Sum(s => s.Item2))
            ).ToList();
        }
        public List<Tuple<DateTime, int>> GetDoanhThuThang_Online(DateTime date)
        {
            DateTime startDate = new DateTime(date.Year, 1, 1);
            DateTime endDate = new DateTime(date.Year, 12, 31);

            List<Tuple<int, int>> salesData = tk.GetMonth(startDate, endDate);

            return salesData
                .Select(x => new Tuple<DateTime, int>(new DateTime(date.Year, x.Item1, 1), x.Item2))
                .ToList();
        }
       

      
        public List<Tuple<DateTime, double>> GetDoanhThuNgay_off(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<DateTime> allDays = Enumerable.Range(0, (lastDayOfMonth - firstDayOfMonth).Days + 1)
                                            .Select(x => firstDayOfMonth.AddDays(x))
                                            .ToList();

            List<Tuple<DateTime, double>> salesData = allDays
                .Select(day => tk.GetNgay_off(day))
                .SelectMany(x => x)
                .ToList();

            return allDays.GroupJoin(
                salesData,
                day => day,
                sale => sale.Item1,
                (day, sales) => new Tuple<DateTime, double>(day, sales.Sum(s => s.Item2))
            ).ToList();
        }

        public List<Tuple<DateTime, double>> GetDoanhThuThang_off(DateTime date)
        {
            DateTime startDate = new DateTime(date.Year, 1, 1);
            DateTime endDate = new DateTime(date.Year, 12, 31);

            List<Tuple<double, double>> salesData = tk.GetMonth_off(startDate, endDate);

            return salesData
                .Select(x => new Tuple<DateTime, double>(new DateTime(date.Year, (int)x.Item1, 1), x.Item2))
                .ToList();
        }
        
        public List<DTO_ThongKe> GetDoanhThuNgay_Tong(DateTime date)
        {
            return tk.GetDoanhThuNgay_Tong(date);
        }

        public List<DTO_ThongKe> GetDoanhThuThang_Tong(DateTime startDate, DateTime endDate)
        {
            return tk.GetDoanhThuThang_Tong(startDate, endDate);
        }

        public List<dathang> LoadDatHang()
        {
            return tk.LoadDatHang();
        }
        public List<banhang> LoadBanHang()
        {
            return tk.LoadBanHang();
        }

        public (int TongHoaDon, double TongDoanhThu, int TongKhachHang, int TongNhanVien) ThongKe()
        {
            return tk.ThongKe();
        }
        public List<(string TenSP, int SoLuong)> GetTop5SanPhamBanChay()
        {
            return tk.GetTop5SanPhamBanChay();
        }
    }
}