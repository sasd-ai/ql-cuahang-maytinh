using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_NhanVien_ChucVu
    {
        QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_NhanVien_ChucVu() { }


        public NhanVien_ChucVu FindByID(string maNV, string maCV)
        {
            return qlch.NhanVien_ChucVus
                       .Where(nv_cv => nv_cv.MaNV == maNV && nv_cv.MaCV == maCV)
                       .FirstOrDefault();

        }
       
        public bool Delete(string maNV,string maCV)
        {
            try
            {
                NhanVien_ChucVu pdelete=FindByID(maNV, maCV);
                if(pdelete!=null)
                {
                    qlch.NhanVien_ChucVus.DeleteOnSubmit(pdelete);
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
        public bool Insert(string maNV, string maCV, string ghiChu)
        {
            try
            {
                NhanVien_ChucVu nv_cv = new NhanVien_ChucVu()
                {
                    MaNV = maNV,
                    MaCV = maCV,
                    GhiChu = ghiChu
                };
                qlch.NhanVien_ChucVus.InsertOnSubmit(nv_cv);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

    }
}
