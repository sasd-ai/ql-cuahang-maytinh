using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_PhieuNhapHang
    {
        private DAO_PhieuNhapHang dAO_PhieuNhapHang=new DAO_PhieuNhapHang();
        public BUS_PhieuNhapHang()
        { }

        //Lấy tất cả phiếu nhập hàng
        public List<DS_PhieuNhap>GetPhieuNhap()
        {
            return dAO_PhieuNhapHang.GetPhieuNhap();
        }
        public List<phieunhaphang>GetData()
        {
            return dAO_PhieuNhapHang.GetData();
        }

        //Tìm theo mã
        public phieunhaphang FindByID(string maPN)
        {
            return dAO_PhieuNhapHang.FindByID(maPN);
        }
        //Tìm theo ngày nhập
        public List<phieunhaphang> FindByDate(DateTime ngayNhap)
        {
            return dAO_PhieuNhapHang.FindByDate(ngayNhap);
        }
        //Tìm theo tên nhân viên
        public List<phieunhaphang> FindByNhanVien(string tenNV)
        {
            return dAO_PhieuNhapHang.FindByNhanVien(tenNV);
        }
        //Tìm theo tên nhà cung cấp
        public List<phieunhaphang> FindByNCC(string tenNCC)
        {
            return dAO_PhieuNhapHang.FindByNCC(tenNCC);
        }
        //Thêm
        public bool Insert(phieunhaphang pn)
        {
            return dAO_PhieuNhapHang.Insert(pn);
        }
        public bool Delete(phieunhaphang pn)
        {
            return dAO_PhieuNhapHang.Delete(pn);
        }
    }
}
