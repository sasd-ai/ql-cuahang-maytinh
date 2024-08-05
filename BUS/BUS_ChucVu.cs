using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_ChucVu
    {

        private DAO_ChucVu dao_ChucVu = new DAO_ChucVu();
        public BUS_ChucVu() { }

        //Select all from chucvu
        public List<chucvu> GetData()
        {
            return dao_ChucVu.GetData();
        }

        public void ThemChucVu(string macv, string tencv, string ghichu)
        {
            dao_ChucVu.ThemChucVu(macv, tencv, ghichu);
        }

        public void SuaChucVu(string macv, string tencv, string ghichu)
        {

            dao_ChucVu.SuaChucVu(macv, tencv, ghichu);
        }

        public chucvu FindByID(string macv)
        {
            return dao_ChucVu.FindByID(macv);
        }


        public bool XoaChucVu(string macv)
        {
            return dao_ChucVu.XoaChucVu(macv);
        }

        public List<chucvu> FindByName(string tencv)
        {
            return dao_ChucVu.FindByName(tencv);
        }

        public bool KTraKhoaNgoai(string macv)
        {
            if(dao_ChucVu.KtraKhoaNgoai(macv))
            {
                return false;
            }    
            return true;
        }
    }
}
