using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
namespace BUS
{
    public class BUS_QuanLyBanHang
    {
        DAO_QuanLyBanHang qlbh=new DAO_QuanLyBanHang();

        public BUS_QuanLyBanHang()
        {

        }

        public object LoadDonBanHang()
        {
            return qlbh.LoadDonBanHang();
        }

        public object LoadChiTietBanHang(string mabh)
        {
            return qlbh.LoadChiTietBanHang(mabh);
        }

        public object TimKiemDonHang_SDT(string sdt)
        {
            return qlbh.TimKiemDonHang_SDT(sdt);
        }

        public object TimKiemDonHang_MaBH(string ma)
        {
            return qlbh.TimKiemDonHang_MaBH(ma);
        }
    }
}
