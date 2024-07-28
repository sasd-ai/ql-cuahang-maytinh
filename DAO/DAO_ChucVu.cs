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
    }
}
