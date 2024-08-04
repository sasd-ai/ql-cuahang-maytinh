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
        public chucvu FindByID(string maCV)
        {
            return qlch.chucvus.Where(cv=>cv.MaCV == maCV).FirstOrDefault();
        }

        public bool Delete(chucvu cv)
        {
            try
            {
                qlch.chucvus.DeleteOnSubmit(cv);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
