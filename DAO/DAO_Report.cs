using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_Report
    {
        private QL_CuaHangDataContext qlch=new QL_CuaHangDataContext();
        public DAO_Report() { }

        public List<InPhieuNhapResult> GetDataPhieuNhap(string maPN)
        {
            string query = $"EXEC InPhieuNhap @MaPN = '{maPN}'";
            return qlch.ExecuteQuery<InPhieuNhapResult>(query).ToList();
        }
    }
}
