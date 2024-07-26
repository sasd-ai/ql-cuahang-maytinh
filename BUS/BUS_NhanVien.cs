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

        //Thêm
        public bool Insert(string tenNV, string diaChi, string sdt, string email)
        {
            nhanvien nv = new nhanvien
            {
                MaNV = Guid.NewGuid().ToString(), // Tạo UUID và gán cho MaNV
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
        public List<nhanvien> FindVyName(string name)
        {
            return dao_NV.FindByName(name);
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
