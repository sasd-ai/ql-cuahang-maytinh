﻿using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_KhachHang
    {
        private DAO_KhachHang dao_kh = new DAO_KhachHang();
        public BUS_KhachHang() { }

        //Select all from chucvu
        public List<khachhang> GetData()
        {
            return dao_kh.GetData();
        }

        public void ThemKhachHang(string makh, string tenkh, string sdt, string email)
        {
            dao_kh.ThemKhachHang(makh, tenkh, sdt, email);
        }

        public void SuaKhachHang(string makh, string tenkh, string sdt, string email)
        {

            dao_kh.SuaKhachHang(makh, tenkh, sdt, email);
        }
        public bool XoaKhachHang(string makh)
        {
            return dao_kh.XoaKhachHang(makh);
        }

        public khachhang FindByID(string makh)
        {
            return dao_kh.FindByID(makh);
        }

        public List<khachhang> FindByName(string tenkh)
        {
            return dao_kh.FindByName(tenkh);
        }

        public khachhang TimSDTKHachHang(string sdt)
        {
            return dao_kh.TimSDTKhachHang(sdt);
        }

        public khachhang TimEmailKHachHang(string email)
        {
            return dao_kh.TimEmailKhachHang(email);
        }
        public bool KT_DinhDangEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
