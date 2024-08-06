using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_NhaCungCap
    {
        private DAO_NhaCungCap dao_ncc = new DAO_NhaCungCap();
        public BUS_NhaCungCap() { }

        public List<nhacungcap> GetData()
        {
            return dao_ncc.GetData();
        }

        public bool ThemNhaCungCap(nhacungcap ncc)
        {
            return dao_ncc.Insert(ncc);
        }

        public void SuaNhaCungCap(string mancc, string tenncc, string diachi, string sdt)
        {

            dao_ncc.Update(mancc, tenncc, diachi, sdt);
        }
        public bool XoaNhaCungCap(string mancc)
        {
            return dao_ncc.XoaNhaCungCap(mancc);
        }

        public nhacungcap FindByID(string mancc)
        {
            return dao_ncc.FindByID(mancc);
        }

        public List<nhacungcap> FindByName(string tenlsp)
        {
            return dao_ncc.FindByName(tenlsp);
        }

      public nhacungcap TimSDTNCC(string sdt)
        {
            return dao_ncc.TimSDTNCC(sdt);
        }
    }
}
