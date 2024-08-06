using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_KhachHang
    {
        private QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_KhachHang() { }

        //Lấy danh sách chức vụ
        public List<khachhang> GetData()
        {
            return qlch.khachhangs.Select(kh => kh).ToList<khachhang>();
        }

        public void ThemKhachHang(string maKH, string tenKH, string SDT, string email)
        {

            khachhang kh = new khachhang();
            kh.MaKH = maKH;
            kh.TenKH = tenKH;
            kh.SDT = SDT;
            kh.Email = email;

            qlch.khachhangs.InsertOnSubmit(kh);
            qlch.SubmitChanges();
        }

        public void SuaKhachHang(string maKH, string tenKH, string SDT, string email)
        {

            khachhang kh = qlch.khachhangs.SingleOrDefault(k => k.MaKH == maKH);
            if (kh != null)
            {
                kh.TenKH = tenKH;
                kh.SDT = SDT;
                kh.Email = email;

                qlch.SubmitChanges();
            }

        }
        //public void XoaKhachHang(string maKH)
        //{
        //    var makhachhang = qlch.khachhangs.FirstOrDefault(kh => kh.MaKH == maKH);
        //    if (makhachhang != null)
        //    {
        //        qlch.khachhangs.DeleteOnSubmit(makhachhang);
        //        qlch.SubmitChanges();
        //    }
        //}


        public bool XoaKhachHang(string makh)
        {
            try
            {
                khachhang khDelete = FindByID(makh);
                if (khDelete != null)
                {
                    qlch.khachhangs.DeleteOnSubmit(khDelete);
                    qlch.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<khachhang> FindByName(string tenkh)
        {
            return qlch.khachhangs
                       .Where(kh => kh.TenKH.Contains(tenkh))
                       .ToList();
        }

        public khachhang FindByID(string makh)
        {
            return qlch.khachhangs.Where(kh => kh.MaKH == makh).FirstOrDefault();
        }

        public khachhang TimSDTKhachHang(string sdt)
        {
            return qlch.khachhangs.FirstOrDefault(kh => kh.SDT == sdt);
        }

        public khachhang TimEmailKhachHang(string email)
        {
            return qlch.khachhangs.FirstOrDefault(kh => kh.Email == email);
        }
    }
}
