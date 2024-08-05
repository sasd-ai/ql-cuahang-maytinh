using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_KhuyenMai
    {

        private QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_KhuyenMai() { }

        //Lấy danh sách chức vụ
        public List<khuyenmai> GetData()
        {
            return qlch.khuyenmais.Select(km => km).ToList<khuyenmai>();
        }

        public void ThemKhuyenMai(string maKM, string tenKM , int giatri)
        {

            khuyenmai km = new khuyenmai();
            km.MaKM = maKM;
            km.TenKM = tenKM;
            km.GiaTri = giatri;

            qlch.khuyenmais.InsertOnSubmit(km);
            qlch.SubmitChanges();
        }

        public void SuaKhuyenMai(string maKM, string tenKM, int giatri)
        {

            khuyenmai km = qlch.khuyenmais.SingleOrDefault(k => k.MaKM == maKM);
            if (km != null)
            {
                km.TenKM = tenKM;
                km.GiaTri = giatri;

                qlch.SubmitChanges();
            }

        }
        //public void XoaKhuyenMai(string maKM)
        //{
        //    var makhuyenmai = qlch.khuyenmais.FirstOrDefault(km => km.MaKM == maKM);
        //    if (makhuyenmai != null)
        //    {
        //        qlch.khuyenmais.DeleteOnSubmit(makhuyenmai);
        //        qlch.SubmitChanges();
        //    }
        //}

        public bool XoaKhuyenMai(string makm)
        {
            try
            {
                khuyenmai kmDelete = FindByID(makm);
                if (kmDelete != null)
                {
                    qlch.khuyenmais.DeleteOnSubmit(kmDelete);
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

        public List<khuyenmai> FindByName(string tenkm)
        {
            return qlch.khuyenmais
                       .Where(kh => kh.TenKM.Contains(tenkm))
                       .ToList();
        }

        public khuyenmai FindByID(string makm)
        {
            return qlch.khuyenmais.Where(km => km.MaKM == makm).FirstOrDefault();
        }
    }
}
