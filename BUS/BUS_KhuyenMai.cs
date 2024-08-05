using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_KhuyenMai
    {
        private DAO_KhuyenMai dao_km = new DAO_KhuyenMai();
        public BUS_KhuyenMai() { }

        public List<khuyenmai> GetData()
        {
            return dao_km.GetData();
        }

        public void ThemKhuyenMai(string makm, string tenkm, int giatri)
        {
            dao_km.ThemKhuyenMai(makm, tenkm, giatri);
        }

        public void SuaKhuyenMai(string makm, string tenkm, int giatri)
        {

            dao_km.SuaKhuyenMai(makm, tenkm, giatri);
        }
        public bool XoaKhuyenMai(string makm)
        {
            return dao_km.XoaKhuyenMai(makm);
        }

        public khuyenmai FindByID(string makm)
        {
            return dao_km.FindByID(makm);
        }

        public List<khuyenmai> FindByName(string tenkm)
        {
            return dao_km.FindByName(tenkm);
        }
    }
}
