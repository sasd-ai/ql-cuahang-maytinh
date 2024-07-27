using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public static class ThietKeButton
    {
        public static void ThietKeButtonSanPham(this GraphicsPath path, Rectangle rect, int radius)
        {
            // Mã để tạo hình chữ nhật góc tròn
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // Thêm đoạn code này
            path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);
            path.CloseFigure();
        }
    }
}
