using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DoanhThu
    {
        public string STT1 { get; set; }
        public string STT2 { get; set; }
        public string MaDH { get; set; }
        public DateTime NgayBan { get; set; }
        public DateTime NgayDatHang { get; set; }
        public double TongTien { get; set; }
        public double ThanhTien { get; set; }
        public string Loai { get; set; }
        public string MaBanHang { get; set; }
    }
}
