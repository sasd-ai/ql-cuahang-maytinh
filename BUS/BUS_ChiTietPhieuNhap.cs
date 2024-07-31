using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_ChiTietPhieuNhap
    {
        private DAO_ChiTietPhieuNhap dAO_ChiTietPhieuNhap=new DAO_ChiTietPhieuNhap();
        public BUS_ChiTietPhieuNhap()
        {
        }

        //Tìm theo khoá
        public chitietphieunhap FindByID(string maPN,string maSP)
        {
            return dAO_ChiTietPhieuNhap.GetByID(maPN, maSP);
        }
        //Thêm
        public bool Insert(chitietphieunhap ctpn)
        {
            return dAO_ChiTietPhieuNhap.Insert(ctpn);
        }
        //Xoá
        public bool Delete(chitietphieunhap ctpn)
        {
            return dAO_ChiTietPhieuNhap.Delete(ctpn);
        }
    }
}
