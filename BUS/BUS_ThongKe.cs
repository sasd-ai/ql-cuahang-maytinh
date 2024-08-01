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

        public List<Tuple<DateTime, int>> GetDoanhThuNgay(DateTime date)
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

        public List<Tuple<DateTime, int>> GetDoanhThuThang(DateTime date)
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

        public List<Tuple<int, double>> GetDoanhThu12Thang_Tong(int nam)
        {
            DateTime startOfYear = new DateTime(nam, 1, 1);
            DateTime endOfYear = new DateTime(nam, 12, 31);

            // Get data for online and offline sales
            List<Tuple<int, int>> onlineSales = tk.GetMonth(startOfYear, endOfYear)
                                                .Select(x => new Tuple<int, int>(x.Item1, x.Item2))
                                                .ToList();
            List<Tuple<double, double>> offlineSales = tk.GetMonth_off(startOfYear, endOfYear)
                                                   .Select(x => new Tuple<double, double>(x.Item1, x.Item2))
                                                   .ToList();

            // Combine online and offline sales
            List<Tuple<int, double>> combinedSales = new List<Tuple<int, double>>();
            for (int month = 1; month <= 12; month++)
            {
                int onlineTotal = onlineSales.FirstOrDefault(x => x.Item1 == month)?.Item2 ?? 0;
                double offlineTotal = offlineSales.FirstOrDefault(x => x.Item1 == month)?.Item2 ?? 0.0;
                combinedSales.Add(new Tuple<int, double>(month, onlineTotal + offlineTotal));
            }

            return combinedSales;
        }

    }
}