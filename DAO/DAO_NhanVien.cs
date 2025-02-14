﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_NhanVien
    {
        QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_NhanVien()
        {

        }

        public List<nhanvien> GetData()
        {
            return qlch.nhanviens.Select(nv => nv).ToList<nhanvien>();
        }
        //Tìm theo mã
        public nhanvien GetByID(string id)
        {
            return qlch.nhanviens.Where(nv => nv.MaNV == id).FirstOrDefault();
        }

        //Thêm mới
        public bool Insert(nhanvien nv)
        {
            try
            {
                qlch.nhanviens.InsertOnSubmit(nv);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        //Xoá theo ID
        public bool Delete(string Id)
        {
            try
            {
                nhanvien nvDelete = GetByID(Id);
                if (nvDelete != null)
                {
                    qlch.nhanviens.DeleteOnSubmit(nvDelete);
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
        //Sửa thông tin
        public bool Update(string Id, string tennv, string diachi,
            string sdt, bool hoatdong)
        {
            try
            {
                nhanvien nv_Update = GetByID(Id);
                if (nv_Update != null)
                {
                    //Sửa thuộc tính nhân viên
                    nv_Update.TenNV = tennv;
                    nv_Update.DiaChi = diachi;
                    nv_Update.SDT = sdt;
                    nv_Update.HoatDong = hoatdong;

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
        public bool UpdateMK(string manv,string matKhau)
        {
            try
            {
                nhanvien nv = FindByID(manv);
                if(nv!= null)
                {
                    nv.MatKhau=matKhau;
                    qlch.SubmitChanges();
                    return true;
                }  
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString());
                return false;
            }
        }
        // Tìm kiếm theo tên gần đúng
        public List<nhanvien> FindByName(string Name)
        {
            return qlch.nhanviens
                       .Where(nv => nv.TenNV.Contains(Name))
                       .ToList();
        }
        public nhanvien FindByEmail(string email)
        {
            return qlch.nhanviens.Where(nv => nv.Email == email).FirstOrDefault();
        }

        public nhanvien FindByID(string manv)
        {
            return qlch.nhanviens.Where(nv => nv.MaNV == manv).FirstOrDefault();
        }

        //Phương thức login
        public bool Login(string email, string matKhau)
        {
            try
            {
                // Tìm nhân viên theo email
                var nhanVien = qlch.nhanviens.SingleOrDefault(nv => nv.Email == email);

                // Kiểm tra xem nhân viên có tồn tại và mật khẩu có khớp không
                if (nhanVien != null && nhanVien.MatKhau == matKhau)
                {
                    return true; // Đăng nhập thành công
                }
                else
                {
                    return false; // Đăng nhập thất bại
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (như ghi log hoặc thông báo cho người dùng)
                Console.WriteLine(ex.Message);
                return false; // Trả về false nếu xảy ra lỗi
            }
        }
        //Lấy danh sánh màn hình theo quyền
        public List<ManHinh> GetManHinhForUser(string email)
        {
            try
            {
                // Tìm MaNV dựa trên email
                var maNV = (from nv in qlch.nhanviens
                            where nv.Email == email
                            select nv.MaNV).SingleOrDefault();

                if (string.IsNullOrEmpty(maNV))
                {
                    // Nếu không tìm thấy MaNV, trả về danh sách rỗng
                    return new List<ManHinh>();
                }

                // Truy vấn để lấy thông tin quyền và màn hình
                var query = from pq in qlch.PhanQuyens
                            join cv in qlch.NhanVien_ChucVus on pq.MaCV equals cv.MaCV
                            join mh in qlch.ManHinhs on pq.MaManHinh equals mh.MaManHinh
                            where cv.MaNV == maNV && pq.CoQuyen==true
                            select new
                            {
                                mh.MaManHinh,
                                mh.TenManHinh
                            };

                // Chuyển đổi kết quả truy vấn thành danh sách đối tượng ManHinh
                var manHinhList = query.ToList().Select(mh => new ManHinh
                {
                    MaManHinh = mh.MaManHinh,
                    TenManHinh = mh.TenManHinh
                }).ToList();
                return manHinhList;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (như ghi log hoặc thông báo cho người dùng)
                Console.WriteLine(ex.Message);
                return new List<ManHinh>(); // Trả về danh sách rỗng nếu xảy ra lỗi
            }
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

        public nhanvien TimSDTNhanVien(string sdt)
        {
            return qlch.nhanviens.FirstOrDefault(nv => nv.SDT == sdt);
        }

        public nhanvien TimEmailNhanVien(string email)
        {
            return qlch.nhanviens.FirstOrDefault(nv => nv.Email == email);
        }
    }
}
