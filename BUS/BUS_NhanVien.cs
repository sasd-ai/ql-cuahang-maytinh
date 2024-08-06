using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
using System.Security.Cryptography;
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
                MatKhau = GetMd5Hash(sdt),
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

        //Update mật khẩu
        public bool UpdateMK(string manv, string matKhau)
        {
            return dao_NV.UpdateMK(manv,GetMd5Hash(matKhau));
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
            return dao_NV.Login(email, GetMd5Hash(password));
        }
        //Lấy danh sách màn hình theo quyền nhân viên
        public List<ManHinh> GetManHinhForUser(string email)
        {
            return dao_NV.GetManHinhForUser(email);
        }
        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public nhanvien TimSDTKNhanVien(string sdt)
        {
            return dao_NV.TimSDTNhanVien(sdt);
        }

        public nhanvien TimEmailNhanVien(string email)
        {
            return dao_NV.TimSDTNhanVien(email);
        }
    }
}
