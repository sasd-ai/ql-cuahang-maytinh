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

        // Lấy doanh thu theo ngày từ bảng DatHang
        public List<Tuple<DateTime, int>> GetNgay(DateTime date)
        {
            return qlch.dathangs
                .Where(dh => dh.NgayDatHang.Value.Date == date.Date) // Lọc đơn đặt hàng theo ngày
                .GroupBy(dh => dh.NgayDatHang.Value.Date) // Nhóm các đơn đặt hàng theo ngày
                .Select(g => new Tuple<DateTime, int>(g.Key, g.Sum(dh => dh.ThanhTien))) // Tính tổng doanh thu cho mỗi ngày
                .ToList();
        }

        // Lấy doanh thu theo tháng từ bảng DatHang
        public List<Tuple<int, int>> GetMonth(DateTime startDate, DateTime endDate)
        {
            return qlch.dathangs
                .Where(dh => dh.NgayDatHang >= startDate && dh.NgayDatHang <= endDate) // Lọc đơn đặt hàng theo khoảng thời gian
                .GroupBy(dh => dh.NgayDatHang.Value.Month) // Nhóm các đơn đặt hàng theo tháng
                .Select(g => new Tuple<int, int>(g.Key, g.Sum(dh => dh.ThanhTien))) // Tính tổng doanh thu cho mỗi tháng
                .ToList();
        }

        // Lấy doanh thu theo ngày từ bảng BanHang
        public List<Tuple<DateTime, double>> GetNgay_off(DateTime date)
        {
            return qlch.banhangs
                .Where(bh => bh.NgayBan.Value.Date == date.Date) // Lọc đơn bán hàng theo ngày
                .GroupBy(bh => bh.NgayBan.Value.Date) // Nhóm các đơn bán hàng theo ngày
                .Select(g => new Tuple<DateTime, double>(g.Key, g.Sum(bh => bh.TongTien))) // Tính tổng doanh thu cho mỗi ngày
                .ToList();
        }

        // Lấy doanh thu theo tháng từ bảng BanHang
        public List<Tuple<double, double>> GetMonth_off(DateTime startDate, DateTime endDate)
        {
            return qlch.banhangs
                .Where(bh => bh.NgayBan >= startDate && bh.NgayBan <= endDate) // Lọc đơn bán hàng theo khoảng thời gian
                .GroupBy(bh => bh.NgayBan.Value.Month) // Nhóm các đơn bán hàng theo tháng
                .Select(g => new Tuple<double, double>(g.Key, g.Sum(bh => bh.TongTien))) // Tính tổng doanh thu cho mỗi tháng
                .ToList();
        }

        public List<DTO_ThongKe> GetDoanhThuNgay_Tong(DateTime date)
        {
            var onlineSales = qlch.dathangs
                .Where(dh => dh.NgayDatHang.Value.Date == date.Date)
                .Select(dh => new { Date = dh.NgayDatHang.Value.Date, TongTien = (double)dh.ThanhTien, MaHD = dh.MaDH });

            var offlineSales = qlch.banhangs
                .Where(bh => bh.NgayBan.Value.Date == date.Date)
                .Select(bh => new { Date = bh.NgayBan.Value.Date, TongTien = bh.TongTien, MaHD = bh.MaBanHang });

            var combinedSales = onlineSales.Union(offlineSales)
                .GroupBy(x => x.Date)
                .Select(g => new DTO_ThongKe
                {
                    Ngay = g.Key,
                    TongTien = g.Sum(x => x.TongTien),
                    SoLuongHoaDon = g.Count(x => x.MaHD != null && x.MaHD.Trim() != "") // Kiểm tra MaHD không rỗng
                })
                .ToList();

            return combinedSales;
        }

        public List<DTO_ThongKe> GetDoanhThuThang_Tong(DateTime startDate, DateTime endDate)
        {
            // Lọc doanh thu theo năm hiện tại
            int currentYear = startDate.Year;

            var onlineSales = qlch.dathangs
                .Where(dh => dh.NgayDatHang.Value.Year == currentYear &&
                            dh.NgayDatHang.Value.Month >= startDate.Month &&
                            dh.NgayDatHang.Value.Month <= endDate.Month &&
                            dh.NgayDatHang.Value.Day >= startDate.Day &&
                            dh.NgayDatHang.Value.Day <= endDate.Day)
                .Select(dh => new { Month = dh.NgayDatHang.Value.Month, TongTien = (double)dh.ThanhTien, MaHD = dh.MaDH });

            var offlineSales = qlch.banhangs
                .Where(bh => bh.NgayBan.Value.Year == currentYear &&
                            bh.NgayBan.Value.Month >= startDate.Month &&
                            bh.NgayBan.Value.Month <= endDate.Month &&
                            bh.NgayBan.Value.Day >= startDate.Day &&
                            bh.NgayBan.Value.Day <= endDate.Day)
                .Select(bh => new { Month = bh.NgayBan.Value.Month, TongTien = bh.TongTien, MaHD = bh.MaBanHang });

            var combinedSales = onlineSales.Union(offlineSales)
                .GroupBy(x => x.Month)
                .Select(g => new DTO_ThongKe
                {
                    Ngay = new DateTime(startDate.Year, g.Key, 1),
                    TongTien = g.Sum(x => x.TongTien),
                    SoLuongHoaDon = g.Count(x => x.MaHD != null && x.MaHD.Trim() != "")
                })
                .ToList();

            return combinedSales;
        }
    }
}