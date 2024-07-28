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
        private QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_ManHinh() { }

        public List<ManHinh> GetData()
        {
            return qlch.ManHinhs.Select(mh=>mh).ToList<ManHinh>();    
        }
    }
}
