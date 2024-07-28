using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_NguoiDung_ChucVu
    {
        DAO_NguoiDung_ChucVu dao_NguoiDung_ChucVu = new DAO_NguoiDung_ChucVu();
        public BUS_NguoiDung_ChucVu()
        { }

        //Lấy danh sách người dùng và chức vụ theo mã chức vụ
        public List<NguoiDung_ChucVu> GetDataByMaCV(string maCV)
        {
            return dao_NguoiDung_ChucVu.GetDataByMaCV(maCV);
        }
    }
}
