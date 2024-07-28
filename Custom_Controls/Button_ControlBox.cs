using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Custom_Controls
{
    public class Button_ControlBox:Button
    {
        public Button_ControlBox()
        {
            
            this.FlatStyle=FlatStyle.Flat;
            this.BackColor = Color.FromArgb(33, 124, 163);
            this.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = Color.White;
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Size = new System.Drawing.Size(234, 60);
            this.Dock = DockStyle.Top;
            this.TextAlign = ContentAlignment.MiddleLeft;
        }
        private void Button_ControlBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Thay đổi màu nền khi button được nhấn
            this.BackColor = Color.Red;
            this.Invalidate(); // Yêu cầu vẽ lại button
        }

        private void Button_ControlBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Khôi phục màu nền khi button không còn nhấn
            this.BackColor = Color.FromArgb(33, 124, 163);
            this.Invalidate(); // Yêu cầu vẽ lại button
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Định nghĩa màu và độ dày của đường viền
            Color borderColor = Color.White; // Bạn có thể thay đổi màu ở đây
            int borderWidth = 2; // Độ dày của đường viền

            // Lấy Graphics từ sự kiện Paint
            Graphics g = e.Graphics;

            // Tạo Pen để vẽ đường viền
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                // Vẽ đường viền ở phía dưới của panel
                g.DrawLine(pen, 0, this.Height - borderWidth / 2, this.Width, this.Height - borderWidth / 2);
            }
        }
    }
}
