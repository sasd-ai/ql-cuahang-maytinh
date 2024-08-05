using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_LoaiSanPham
    {
        private DAO_LoaiSanPham dao_lsp = new DAO_LoaiSanPham();
        public BUS_LoaiSanPham() { }

        public List<loaisp> GetData()
        {
            return dao_lsp.GetData();
        }

        public void ThemLoaiSanPham(string malsp, string tenlsp)
        {
            dao_lsp.ThemLoaiSanPham(malsp, tenlsp);
        }

        public void SuaLoaiSanPham(string malsp, string tenlsp)
        {

            dao_lsp.SuaLoaiSanPham(malsp, tenlsp);
        }
        public bool XoaLoaiSanPham(string malsp)
        {
            return dao_lsp.XoaLoaiSanPham(malsp);
        }

        public loaisp FindByID(string malsp)
        {
            return dao_lsp.FindByID(malsp);
        }

        public List<loaisp> FindByName(string tenlsp)
        {
            return dao_lsp.FindByName(tenlsp);
        }
    }
}
