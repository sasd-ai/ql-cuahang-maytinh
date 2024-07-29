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

        public List<khachhang> FindBySDT (string sdt)
        {
            return qlch.khachhangs.Where(kh=>kh.SDT==sdt).ToList();
        }

        public List<khuyenmai> LoadKhuyenMai()
        {
            return qlch.khuyenmais.Select(km => km).ToList<khuyenmai>();
        }

        public khuyenmai LoadKhuyenMaiTheoMa(string maKM)
        {
            return qlch.khuyenmais.SingleOrDefault(km => km.MaKM == maKM);
        }

        public bool InsertBanHang(banhang bh)
        {
            try
            {
                qlch.banhangs.InsertOnSubmit(bh);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
              
                Console.WriteLine("Error inserting banhang: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
               
                return false;
            }
        }


        public bool InsertChiTietBanHang(chitietbanhang ctb)
        {
            try
            {
                qlch.chitietbanhangs.InsertOnSubmit(ctb);
                qlch.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
