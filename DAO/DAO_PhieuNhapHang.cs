using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_PhieuNhapHang
    {
        private QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_PhieuNhapHang() { }

        public List<DS_PhieuNhap> GetPhieuNhap()
        {
            return qlch.DS_PhieuNhaps.Select(dspn=>dspn).ToList();
        }
        //Lấy danh sách phiếu nhập hàng
        public List<phieunhaphang>GetData()
        {
            return qlch.phieunhaphangs.Select(p=>p).ToList<phieunhaphang>();
        }
        #region TÌM KIẾM
        //Tìm theo ngày
        public List<phieunhaphang>FindByDate(DateTime ngayNhap)
        {
            return qlch.phieunhaphangs.Where(p=>p.NgayNhap==ngayNhap).ToList<phieunhaphang>();  
        }
        //Tìm theo tên nhà cung cấp
        public List<phieunhaphang> FindByNCC(string tenNCC)
        {
            var results = from phieu in qlch.phieunhaphangs
                          join ncc in qlch.nhacungcaps on phieu.MaNCC equals ncc.MaNCC
                          where ncc.TenNCC.Contains(tenNCC)
                          select phieu;

            return results.ToList<phieunhaphang>();
        }
        //Tìm theo tên nhân viên
        public List<phieunhaphang> FindByNhanVien(string tenNhanVien)
        {
            var results = from phieu in qlch.phieunhaphangs
                          join nv in qlch.nhanviens on phieu.MaNV equals nv.MaNV
                          where nv.TenNV.Contains(tenNhanVien)
                          select phieu;

            return results.ToList<phieunhaphang>();
        }
        //Tìm theo mã phiếu nhập
        public phieunhaphang FindByID(string maPN)
        {
            return qlch.phieunhaphangs.Where(p=>p.MaPN== maPN).FirstOrDefault();    
        }
        #endregion

        #region INSERT 
        //Thêm mới
        public bool Insert(phieunhaphang pn)
        {
            try
            {
                qlch.phieunhaphangs.InsertOnSubmit(pn);
                qlch.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        #endregion

        #region DELETE
        public bool Delete(phieunhaphang pn)
        {
            try
            {
                qlch.phieunhaphangs.DeleteOnSubmit(pn);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        //public bool Update(phieunhaphang pn)
        //{
        //    try
        //    {

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}
         #endregion
        }
    }
