﻿using CloudinaryDotNet;
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
        //tìm kiếm
        public sanpham FindByID(string maSP)
        {
            return qlch.sanphams.Where(sp => sp.MaSP == maSP).FirstOrDefault();
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
        //Update
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
        public bool UpdateSLNhap(string maSP, int sl)
        {
            try
            {
                sanpham sp=FindByID(maSP);
                if(sp != null)
                {
                    if(sp.SoLuong==0)
                    {
                        sp.SoLuong = sl;
                    }    
                    else
                    {
                        sp.SoLuong += sl;
                    } 
                    qlch.SubmitChanges();
                    return true;
                }    
                return false;
            }
            catch (Exception ex)
            {
                return false;
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

        //Tìm kiếm theo tên
        public List<sanpham> FindByName(string TenSP)
        {
            return qlch.sanphams
                       .Where(sp => sp.TenSP.Contains(TenSP)) 
                       .ToList();
        }
        public List<SanPhamFull> TimKiemTenSP(string TenSP)
        {
            return qlch.SanPhamFulls
                       .Where(sp => sp.tensp.Contains(TenSP))
                       .ToList();
        }
        //Lấy dánh sách sản phẩm
        public List<sanpham> GetData()
        {
            return qlch.sanphams.Select(sp => sp).ToList<sanpham>();
        }
        public List<sanpham> FindByMaNCC(string maNCC)
        {
            return qlch.sanphams.Where(sp => sp.MaNCC==maNCC).ToList<sanpham>();
        }

        //public sanpham FindByID(string maSP)
        //{
        //    return qlch.sanphams.Where(sp => sp.MaSP == maSP).FirstOrDefault();
        //}

        public bool UpdateSLBan(string maSP, int sl)
        {
            try
            {
                sanpham sp = FindByID(maSP);
                if (sp != null)
                {                   
                    sp.SoLuong = sp.SoLuong - sl;
                    qlch.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


     
    }
}
