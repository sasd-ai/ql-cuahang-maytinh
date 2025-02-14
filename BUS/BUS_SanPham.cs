﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using CloudinaryDotNet;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_SanPham
    {
        private Cloudinary cloudinary;
        DAO_SanPham sp = new DAO_SanPham();
        public BUS_SanPham() { }


        public void InitializeCloudinary()
        {
            Account account = new Account(
                "dfiyhshnk",         // Thay thế bằng Cloud Name của bạn
                "782519644662424",   // Thay thế bằng API Key của bạn
                "xoXUWZ8ZiCLuF7UDfnTTOem1jg8"   // Thay thế bằng API Secret của bạn
            );
            cloudinary = new Cloudinary(account);
        }

        public System.Drawing.Image LoadImageFromUrl(string url)
        {
            using (var webClient = new System.Net.WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(url);
                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    return ResizeImage(image, 50, 50);
                }
            }
        }

        public System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return newImage;
        }
        public List<SanPhamFull> LoadSanPham()
        {
            return sp.LoadsanPham();
        }

        public List<loaisp> LoadLoaiSP()
        {
            return sp.LoadLoaiSP();
        }

        public List<nhacungcap> LoadNCC()
        {
            return sp.Loadnhacungcap();
        }

        public void ThemSanPham(string masp, string tensp, string hinhanh, string giadx,
                                string sl, string giaban, string mota, string tgbh, string maloai, string mancc)
        {
            sp.ThemSanPham(masp, tensp, hinhanh, giadx, sl, giaban, mota, tgbh, maloai, mancc);
        }

        public void SuaSanPham(string masp, string tensp, string hinhanh, string giadx,
                          string sl, string giaban, string mota, string tgbh, string maloai, string mancc)
        {

            sp.SuaSanPham(masp, tensp, hinhanh, giadx, sl, giaban, mota, tgbh, maloai, mancc);
        }
        public void XoaSanPham(string masp)
        {
            sp.XoaSanPham(masp);
        }

        //Tìm kiếm
        public sanpham FindByID(string maSP)
        {
            return sp.FindByID(maSP);
        }
        public List<sanpham> FindByName(string TenSP)
        {
            return sp.FindByName(TenSP);
        }
        public List<SanPhamFull> TimKiemTenSP(string TenSP)
        {
            return sp.TimKiemTenSP(TenSP);
        }
        //Lấy danh sách sản phẩm
        public List<sanpham> GetData()
        {
            return sp.GetData(); 
        }
        //Tìm kiếm theo mã loại,mã nhà sản xuất
        public List<sanpham> FindByMaLoai(string maLoai, List<sanpham> sanphams)
        {
            return sanphams.Where(sp => sp.MaLoai == maLoai).ToList<sanpham>();
        }
        public List<sanpham> FindByMaNCC(string maNCC)
        {
            return sp.FindByMaNCC(maNCC);
        }
      

        public bool UpdateSLBan(string masp,int soluong)
        {
            return sp.UpdateSLBan(masp, soluong);
        }



        //Update số lượng khi nhập
        public bool UpdateSLNhap(string maSP,int sl)
        {
            return sp.UpdateSLNhap(maSP, sl);
        }
    }
}
