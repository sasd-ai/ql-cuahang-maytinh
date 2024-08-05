using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_NhanVien
    {
        DAO_NhanVien dao_NV=new DAO_NhanVien();
        public BUS_NhanVien() { }


        public List<nhanvien> GetData()
        {
            return dao_NV.GetData();
        }
        //Thêm
        public bool Insert(string manv, string tenNV, string diaChi, string sdt, string email)
        {
            nhanvien nv = new nhanvien
            {
                MaNV = manv,
                TenNV = tenNV,
                DiaChi = diaChi,
                SDT = sdt,
                Email = email,
                MatKhau = sdt,
                HoatDong= true,
            };

            return dao_NV.Insert(nv);
        }
        //Xoá
        public bool Delete(string maNV)
        {
            return  dao_NV.Delete(maNV);
        }
        //Sửa
        public bool Update(string Id, string tennv, string diachi,
            string sdt, bool hoatdong)
        {
            return dao_NV.Update(Id,tennv, diachi, sdt, hoatdong);
        }
        //Tìm kiếm theo tên
        public List<nhanvien> FindByName(string name)
        {
            return dao_NV.FindByName(name);
        }

        public nhanvien FindByEmail(string email)
        {
            return dao_NV.FindByEmail(email);
        }

        public nhanvien FindByID(string manv)
        {
            return dao_NV.FindByID(manv);
        }
        //Login 
        public bool Login(string email,string password)
        {
            return dao_NV.Login(email, password);
        }
        //Lấy danh sách màn hình theo quyền nhân viên
        public List<ManHinh> GetManHinhForUser(string email)
        {
            return dao_NV.GetManHinhForUser(email);
        }
    }
}
