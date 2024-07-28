using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_NguoiDung_ChucVu
    {
        public QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_NguoiDung_ChucVu()
        {
        }
        //Lấy danh sách người dùng theo chức vụ  lấy tên người dùng và tên chức vụ liên quan tới 2 bảng nhanvien và chucvu
        //Điều kiện get là nhanviens.manv =... chucvu.macv =... select nhanviens.TenNV,  chucvus,TenCV
        public List<NguoiDung_ChucVu> GetDataByMaCV(string maCV)
        {
            return qlch.NguoiDung_ChucVus.Where(user => user.MaCV == maCV).ToList<NguoiDung_ChucVu>();
        }

    }
}
