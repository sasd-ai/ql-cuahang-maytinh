using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_ChucVu
    {

        private DAO_ChucVu dao_ChucVu = new DAO_ChucVu();
        public BUS_ChucVu() { }

        //Select all from chucvu
        public List<chucvu> GetData()
        {
            return dao_ChucVu.GetData();
        }
        public chucvu FindByID(string maCV)
        {
            return dao_ChucVu.FindByID(maCV);
        }
        public bool Delete(chucvu cv)
        {
            return dao_ChucVu.Delete(cv);
        }

        //Load bảng phân quyền theo mã chức vụ
        public List<BangQuyenManHinh> GetPhanQuyenByMaCV(string maCV)
        {
            return dao_ChucVu.GetPhanQuyenByMaCV(maCV);
        }
        //Tìm quyền theo chức vụ
        public PhanQuyen FindByID(string maCV,string maManHinh)
        {
            return dao_ChucVu.FindByID(maCV, maManHinh);
        }
       
        
        //Update quyền cho nhóm người dùng
        public bool UpdatePhanQuyen(string maCV, string maManHinh, bool coQuyen)
        {
            return dao_ChucVu.UpdatePhanQuyen(maCV,maManHinh , coQuyen);
        }
        
        //Insert quyền cho nhóm người dùng
        public bool InsertQuyen(string maCV, string maManHinh)
        {
            return dao_ChucVu.InsertQuyen(maCV,maManHinh);
        }
    }
}
