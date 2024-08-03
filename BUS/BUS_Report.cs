using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_Report
    {
        private DAO_Report dAO_Report = new DAO_Report();
        public BUS_Report() { }
        //Lấy thông tin phiếu nhập theo mã phiếu nhập
        public List<InPhieuNhapResult>GetDataPhieuNhap(string maPN)
        {
            return dAO_Report.GetDataPhieuNhap(maPN);
        }

    }
}
