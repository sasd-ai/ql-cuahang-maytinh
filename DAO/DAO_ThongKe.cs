using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_ThongKe
    {
        QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_ThongKe() { }

       
        public List<Tuple<DateTime, int>> GetNgay(DateTime date) 
        {
            return qlch.dathangs
                .Where(dh => dh.NgayDatHang.Value.Date == date.Date)
                .GroupBy(dh => dh.NgayDatHang.Value.Date)
                .Select(g => new Tuple<DateTime, int>(g.Key, g.Sum(dh => dh.TongTien)))
                .ToList();
        }

       
        public List<Tuple<int, int>> GetMonth(DateTime startDate, DateTime endDate)
        {
            return qlch.dathangs
                .Where(dh => dh.NgayDatHang >= startDate && dh.NgayDatHang <= endDate)
                .GroupBy(dh => dh.NgayDatHang.Value.Month)
                .Select(g => new Tuple<int, int>(g.Key, g.Sum(dh => dh.TongTien)))
                .ToList();
        }



        public List<Tuple<DateTime, double>> GetNgay_off(DateTime date)
        {
            return qlch.banhangs
                .Where(dh => dh.NgayBan.Value.Date == date.Date)
                .GroupBy(dh => dh.NgayBan.Value.Date)
                .Select(g => new Tuple<DateTime, double>(g.Key, g.Sum(bh =>bh.TongTien)))
                .ToList();
        }


        public List<Tuple<double, double>> GetMonth_off(DateTime startDate, DateTime endDate)
        {
            return qlch.banhangs
                .Where(dh => dh.NgayBan >= startDate && dh.NgayBan <= endDate)
                .GroupBy(dh => dh.NgayBan.Value.Month)
                .Select(g => new Tuple<double, double>(g.Key, g.Sum(dh => dh.TongTien)))
                .ToList();
        }
    }
}