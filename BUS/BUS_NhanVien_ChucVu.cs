using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_NhanVien_ChucVu
    {
        private DAO_NhanVien_ChucVu dAO_NhanVien_ChucVu=new DAO_NhanVien_ChucVu();
        public BUS_NhanVien_ChucVu() { }

        public NhanVien_ChucVu FindByID(string maNV,string maCV)
        {
            return dAO_NhanVien_ChucVu.FindByID(maNV, maCV);
        }

        public bool Insert(string maNV, string maCV,string ghiChu) 
        { 
            return dAO_NhanVien_ChucVu.Insert(maNV, maCV, ghiChu);
        }
        public bool Delete(string maNV,string maCV)
        {
            return dAO_NhanVien_ChucVu.Delete(maNV, maCV);
        }
    }
}
