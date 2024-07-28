using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_ManHinh
    {
        private QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_ManHinh() { }

        public List<ManHinh> GetData()
        {
            return qlch.ManHinhs.Select(mh => mh).ToList<ManHinh>();
        }
        public bool Insert(string maMH, string tenMH)
        {
            try
            {
                ManHinh mh = new ManHinh()
                {
                    MaManHinh = maMH,
                    TenManHinh = tenMH
                };
                qlch.ManHinhs.InsertOnSubmit(mh);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
