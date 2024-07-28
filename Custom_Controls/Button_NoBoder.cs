using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Custom_Controls
{
    public class Button_NoBoder : Button
    {
        public Button_NoBoder()
        {
            this.FlatStyle = FlatStyle.Flat; // Thiết lập FlatStyle
            this.FlatAppearance.BorderSize = 0; // Loại bỏ đường viền
            this.BackColor = Color.FromArgb(33, 124, 163);
            this.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = Color.White;
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Size = new System.Drawing.Size(234, 60);
            this.Dock = DockStyle.Top;
            this.TextAlign = ContentAlignment.MiddleLeft;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
        }
    }
}
