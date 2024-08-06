using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_BanHang
    {
        DAO_BanHang bh = new DAO_BanHang();
        public BUS_BanHang() { }

        public List<sanpham> LoadSanPham()
        {
            return bh.LoadSanPham();
        }

        public List<sanpham> LoadSanPhamTheoLoai(string loaiSanPham)
        {
            return bh.LoadSanPhamTheoLoai(loaiSanPham);
        }


        public List<loaisp> LoadLoaiSanPham()
        {
            return bh.LoadLoaiSanPham();
        }

        public List<khachhang> FindBySDT(string sdt)
        {
            return bh.FindBySDT(sdt);
        }

        public List<khuyenmai> LoadKhuyenMai()
        {
            return bh.LoadKhuyenMai();
        }

        public khuyenmai LoadKhuyenMaiTheoMa(string maKM)
        {
            return bh.LoadKhuyenMaiTheoMa(maKM); 
        }

        public bool InsertBanHang(banhang banHang)
        {
            return bh.InsertBanHang(banHang);
        }

        public bool InsertChiTietBanHang(chitietbanhang chiTietBanHang)
        {
            return bh.InsertChiTietBanHang(chiTietBanHang);
        }
        public bool KiemTraKhachHangTonTai(string maKhachHang)
        {
            return bh.KiemTraKhachHangTonTai(maKhachHang);
        }
        public List<HoaDon> LayHoaDon(string maBanHang)
        {
            return bh.LayHoaDonTheoMaBanHang(maBanHang);
        }

        
    }
}
