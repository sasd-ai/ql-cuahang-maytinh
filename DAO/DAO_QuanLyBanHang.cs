using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_QuanLyBanHang
    {
        QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_QuanLyBanHang() { }

        public object LoadDonBanHang()
        {
            var query = from banhang in qlch.banhangs
                        join khachhang in qlch.khachhangs on banhang.MaKhachHang equals khachhang.MaKH
                        join nhanvien in qlch.nhanviens on banhang.MaNV equals nhanvien.MaNV
                        select new
                        {
                            MaBanHang = banhang.MaBanHang,
                            TenKH = khachhang.TenKH,
                            TenNV = nhanvien.TenNV,
                            TongTien = banhang.TongTien,
                            NgayBan=banhang.NgayBan,
                        };

            return query.ToList();
        }

        public object LoadChiTietBanHang(string MaBH)
        {
            var query = from chitietbanhang in qlch.chitietbanhangs
                        join sanpham in qlch.sanphams on chitietbanhang.MaSP equals sanpham.MaSP
                        where chitietbanhang.MaBanHang==MaBH
                        select new
                        {
                            MaBanHang = chitietbanhang.MaBanHang,
                            TenSP = sanpham.TenSP,
                            SoLuong = chitietbanhang.SoLuong,
                            DonGia = chitietbanhang.DonGia,
                            ThanhTien = chitietbanhang.ThanhTien
                        };

            return query.ToList();
        }

        public object TimKiemDonHang_SDT(string sdt)
        {
            var query = from banhang in qlch.banhangs
                        join khachhang in qlch.khachhangs on banhang.MaKhachHang equals khachhang.MaKH
                        join nhanvien in qlch.nhanviens on banhang.MaNV equals nhanvien.MaNV
                        where khachhang.SDT == sdt
                        select new
                        {
                            MaBanHang = banhang.MaBanHang,
                            TenKH = khachhang.TenKH,
                            TenNV = nhanvien.TenNV,
                            TongTien = banhang.TongTien,
                            NgayBan = banhang.NgayBan,
                        };

            return query.ToList();
        }

        public object TimKiemDonHang_MaBH(string ma)
        {
            var query = from banhang in qlch.banhangs
                        join khachhang in qlch.khachhangs on banhang.MaKhachHang equals khachhang.MaKH
                        join nhanvien in qlch.nhanviens on banhang.MaNV equals nhanvien.MaNV
                        where banhang.MaBanHang.Equals(ma)
                        select new
                        {
                            MaBanHang = banhang.MaBanHang,
                            TenKH = khachhang.TenKH,
                            TenNV = nhanvien.TenNV,
                            TongTien = banhang.TongTien,
                            NgayBan = banhang.NgayBan,
                        };

            return query.ToList();
        }


    }
}
