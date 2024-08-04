using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BangQuyenManHinh
    {
        public string MaCV { get; set; }
        public string MaMH { get; set; }
        public string TenManHinh { get; set; }
        public bool CoQuyen { get; set; }
        public BangQuyenManHinh() { }
        public BangQuyenManHinh(string maCV, string maMH, string tenManHinh, bool coQuyen)
        {
            MaCV = maCV;
            MaMH = maMH;
            TenManHinh = tenManHinh;
            CoQuyen = coQuyen;
        }
    }
}
