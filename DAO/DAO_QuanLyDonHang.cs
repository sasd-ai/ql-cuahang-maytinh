using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_QuanLyDonHang
    {
        QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_QuanLyDonHang() { }

        public List<View_DonHang> LoadDonHang(string NewTrangThai)
        {
            return qlch.View_DonHangs.Where(dhxn=>dhxn.TinhTrang_DH== NewTrangThai).ToList<View_DonHang>();
        }

        public bool CapNhatTrangThaiDonHang(string maDH, string trangThaiMoi)
        {
            try
            {
                var donHang = qlch.dathangs.FirstOrDefault(dh => dh.MaDH == maDH);
                if (donHang != null)
                {
                    donHang.TinhTrang_DH = trangThaiMoi;
                    qlch.SubmitChanges(); 
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public List<View_ChiTietDatHang> LoadChiTietDatHang(string NewMaDH)
        {
            return qlch.View_ChiTietDatHangs.Where(ct=>ct.MaDH==NewMaDH).ToList<View_ChiTietDatHang>();
        }

    }
}
