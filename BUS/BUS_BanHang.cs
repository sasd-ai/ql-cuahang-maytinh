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
        DAO_BanHang bh=new DAO_BanHang();
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
    }
}
