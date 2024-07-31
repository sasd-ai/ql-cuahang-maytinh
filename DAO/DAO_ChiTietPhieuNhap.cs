using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_ChiTietPhieuNhap
    {
        private QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_ChiTietPhieuNhap() { }

        //Lấy danh sách chi tiết nhập hàng theo mã phiếu nhập
        public List<chitietphieunhap> GetByMaPN(string maPN)
        {
            return qlch.chitietphieunhaps.Where(pn=>pn.MaPN == maPN).ToList();
        }
        //Tìm theo mã phiếu nhập và mã sản phẩm
        public chitietphieunhap GetByID(string maPN,string maSP)
        {
            return qlch.chitietphieunhaps.Where(ph=>ph.MaPN== maPN && ph.MaSP==maSP).FirstOrDefault();
        }
        //Thêm
        public bool Insert(chitietphieunhap ctpn)
        {
            try
            {
                qlch.chitietphieunhaps.InsertOnSubmit(ctpn);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //Xoá
        public bool Delete(chitietphieunhap ctpn)
        {
            try
            {
                qlch.chitietphieunhaps.DeleteOnSubmit(ctpn);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
