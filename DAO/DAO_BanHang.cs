using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_BanHang
    {
        QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_BanHang() { }


        public List<sanpham> LoadSanPham()
        {
            return qlch.sanphams.Select(mh=>mh).ToList<sanpham>();
        }

        public List<sanpham> LoadSanPhamTheoLoai(string loaiSanPham)
        {
            return qlch.sanphams.Where(sp => sp.MaLoai == loaiSanPham).ToList();
        }
        public List<loaisp> LoadLoaiSanPham()
        {
           return qlch.loaisps.Select(loaisp=>loaisp).ToList<loaisp>();
        }


    }
}
