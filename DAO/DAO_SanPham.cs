using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using CloudinaryDotNet.Actions;


namespace DAO
{
    public class DAO_SanPham
    {
        QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
      
        public DAO_SanPham() { }

       
        public List<SanPhamFull> LoadsanPham()
        {
            return qlch.SanPhamFulls.Select(sp=>sp).ToList<SanPhamFull>();
        }

        public List<loaisp> LoadLoaiSP()
        {
            return qlch.loaisps.Select(lsp =>lsp).ToList<loaisp>();
        }

        public List<nhacungcap> Loadnhacungcap()
        {
            return qlch.nhacungcaps.Select(ncc=>ncc).ToList<nhacungcap>();
        }

        public void ThemSanPham(string masp, string tensp, string hinhanh, string giadx,
                                string sl, string giaban, string mota, string tgbh, string maloai, string mancc)
        {
           
            sanpham sp = new sanpham();         
            sp.MaSP = masp;
            sp.TenSP = tensp;
            sp.HinhAnh = hinhanh;         
            sp.GiaDeXuat = int.Parse(giadx);
            sp.SoLuong = int.Parse(sl);
            sp.GiaBan = int.Parse(giaban);
            sp.MoTa = mota;
            sp.Tg_BaoHanh = int.Parse(tgbh);
            sp.MaLoai = maloai;
            sp.MaNCC = mancc;
       
            qlch.sanphams.InsertOnSubmit(sp);
            qlch.SubmitChanges();
        }

        public void SuaSanPham(string masp, string tensp, string hinhanh, string giadx,
                           string sl, string giaban, string mota, string tgbh, string maloai, string mancc)
        {
        
            sanpham sp = qlch.sanphams.SingleOrDefault(s => s.MaSP == masp);
            if (sp != null)
            {            
                sp.TenSP = tensp;
                sp.HinhAnh = hinhanh;
                sp.GiaDeXuat = int.Parse(giadx);
                sp.SoLuong = int.Parse(sl);
                sp.GiaBan = int.Parse(giaban);
                sp.MoTa = mota;
                sp.Tg_BaoHanh = int.Parse(tgbh);
                sp.MaLoai = maloai;
                sp.MaNCC = mancc;
           
                qlch.SubmitChanges();
            }
            
        }
        public void XoaSanPham(string masp)
        {
            var masanpham = qlch.sanphams.FirstOrDefault(sp => sp.MaSP == masp);
            if(masanpham != null)
            {
                qlch.sanphams.DeleteOnSubmit(masanpham);
                qlch.SubmitChanges();
            }    
        }
    }
}
