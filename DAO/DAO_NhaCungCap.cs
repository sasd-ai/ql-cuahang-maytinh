using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_NhaCungCap
    {
        QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_NhaCungCap() { }

        //Lấy danh sách nhà cung cấp
        public List<nhacungcap>GetData()
        {
            return qlch.nhacungcaps.Select(ncc=>ncc).ToList();
        }
        #region TÌM KIẾM
        //Tìm theo mã
        public nhacungcap FindByID(string maNCC)
        {
            return qlch.nhacungcaps.Where(ncc=>ncc.MaNCC==maNCC).FirstOrDefault();
        }
        //Tìm theo tên
        public List<nhacungcap> FindByName(string tenNCC)
        {
            return qlch.nhacungcaps.Where(ncc => ncc.TenNCC.Contains(tenNCC)).ToList();
        }

        #endregion
        public bool Insert(nhacungcap ncc)
        {
            try
            {
                qlch.nhacungcaps.InsertOnSubmit(ncc);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        //public void Delete(string mancc)
        //{
        //    var ma_ncc = qlch.nhacungcaps.FirstOrDefault(ncc => ncc.MaNCC == mancc);
        //    if (ma_ncc != null)
        //    {
        //        qlch.nhacungcaps.DeleteOnSubmit(ma_ncc);
        //        qlch.SubmitChanges();
        //    }
        //}

        public bool XoaNhaCungCap(string mancc)
        {
            try
            {
                nhacungcap nccDelete = FindByID(mancc);
                if (nccDelete != null)
                {
                    qlch.nhacungcaps.DeleteOnSubmit(nccDelete);
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

        public bool Update(string maNCC, string tenNCC, string diaChi, string SDT)
        {
            try
            {
                nhacungcap ncc=FindByID(maNCC);
                if(ncc!=null)
                {
                    ncc.TenNCC = tenNCC;
                    ncc.DiaChi=diaChi;
                    ncc.SDT = SDT;
                    qlch.nhacungcaps.InsertOnSubmit(ncc);
                    qlch.SubmitChanges();
                    return true ;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
