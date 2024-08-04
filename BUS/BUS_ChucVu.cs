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
        public chucvu FindByID(string maCV)
        {
            return dao_ChucVu.FindByID(maCV);
        }
        public bool Delete(chucvu cv)
        {
            return dao_ChucVu.Delete(cv);
        }
    }
}
