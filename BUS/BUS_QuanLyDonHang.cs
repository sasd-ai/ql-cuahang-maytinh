using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_QuanLyDonHang
    {
        DAO_QuanLyDonHang dh=new DAO_QuanLyDonHang();
        public BUS_QuanLyDonHang() { }  

        public List<View_DonHang> LoadDonHangChoXacNhan()
        {
            return dh.LoadXacNhanDonHang();
        }

        public bool CapNhatTTXacNhanDonHang(string madh, string trangThaiMoi)
        {
            return dh.CapNhatTrangThaiDonHang(madh, trangThaiMoi);
        }

        public List<chitietdathang> LoadChiTietDatHang(string NewMADH)
        {
            return dh.LoadChiTietDatHang(NewMADH);
        }
    }
}
