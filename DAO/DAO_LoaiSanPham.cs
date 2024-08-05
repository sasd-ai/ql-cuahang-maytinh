using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_LoaiSanPham
    {
        private QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_LoaiSanPham() { }

        //Lấy danh sách chức vụ
        public List<loaisp> GetData()
        {
            return qlch.loaisps.Select(lsp => lsp).ToList<loaisp>();
        }

        public void ThemLoaiSanPham(string malsp, string tenlsp)
        {

            loaisp lsp = new loaisp();
            lsp.MaLoai = malsp;
            lsp.TenLoai = tenlsp;

            qlch.loaisps.InsertOnSubmit(lsp);
            qlch.SubmitChanges();
        }

        public void SuaLoaiSanPham(string malsp, string tenlsp)
        {

            loaisp lsp = qlch.loaisps.SingleOrDefault(k => k.MaLoai == malsp);
            if (lsp != null)
            {
                lsp.TenLoai = tenlsp;

                qlch.SubmitChanges();
            }

        }
        //public void XoaLoaiSanPham(string malsp)
        //{
        //    var maloaisanpham = qlch.loaisps.FirstOrDefault(lsp => lsp.MaLoai == malsp);
        //    if (maloaisanpham != null)
        //    {
        //        qlch.loaisps.DeleteOnSubmit(maloaisanpham);
        //        qlch.SubmitChanges();
        //    }
        //}

        public bool XoaLoaiSanPham(string malsp)
        {
            try
            {
                loaisp lspDelete = FindByID(malsp);
                if (lspDelete != null)
                {
                    qlch.loaisps.DeleteOnSubmit(lspDelete);
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

        public List<loaisp> FindByName(string lsp)
        {
            return qlch.loaisps
                       .Where(kh => kh.TenLoai.Contains(lsp))
                       .ToList();
        }


        public loaisp FindByID(string malsp)
        {
            return qlch.loaisps.Where(lsp => lsp.MaLoai == malsp).FirstOrDefault();
        }
    }
}
