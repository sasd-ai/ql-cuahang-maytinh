using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_ChucVu
    {
        private QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_ChucVu() { }

        //Lấy danh sách chức vụ
        public List<chucvu> GetData()
        {
            return qlch.chucvus.Select(cv => cv).ToList<chucvu>();
        }

        public void ThemChucVu(string macv, string tencv, string ghichu)
        {

            chucvu cv = new chucvu();
            cv.MaCV = macv;
            cv.TenCV = tencv;
            cv.GhiChu = ghichu;

            qlch.chucvus.InsertOnSubmit(cv);
            qlch.SubmitChanges();
        }

        public void SuaChucVu(string macv, string tencv, string ghichu)
        {

            chucvu cv = qlch.chucvus.SingleOrDefault(s => s.MaCV == macv);
            if (cv != null)
            {
                cv.TenCV = tencv;
                cv.GhiChu = ghichu;

                qlch.SubmitChanges();
            }

        }
        public bool XoaChucVu(string macv)
        {
            try
            {
                chucvu cvDelete = FindByID(macv);
                if (cvDelete != null)
                {
                    qlch.chucvus.DeleteOnSubmit(cvDelete);
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

        public List<chucvu> FindByName(string TenCV)
        {
            return qlch.chucvus
                       .Where(cv => cv.TenCV.Contains(TenCV))
                       .ToList();
        }

        public chucvu FindByID(string maCV)
        {
            return qlch.chucvus.Where(cv => cv.MaCV == maCV).FirstOrDefault();
        }

        public bool KtraKhoaNgoai(string macv)
        {

            return qlch.NhanVien_ChucVus.Any(nv => nv.MaCV == macv);

        }
    }


}
