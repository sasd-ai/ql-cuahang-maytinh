using System.Drawing;
using System.Windows.Forms;

public class CustomTabControl : TabControl
{
    public CustomTabControl()
    {
        this.DrawMode = TabDrawMode.OwnerDrawFixed;
        this.ItemSize = new Size(200, 40); // Chiều rộng tự động, chiều cao cố định
        this.SizeMode = TabSizeMode.Fixed; // Đặt kích thước tab cố định
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        base.OnDrawItem(e);

        // Lấy thông tin TabPage hiện tại
        TabPage tabPage = this.TabPages[e.Index];
        Rectangle tabRect = this.GetTabRect(e.Index);

        // Kiểm tra nếu tab đang được chọn
        bool isSelected = this.SelectedIndex == e.Index;

        // Vẽ nền của Tab
        Color backgroundColor = isSelected ? Color.FromArgb(220, 220, 220) : Color.White;
        e.Graphics.FillRectangle(new SolidBrush(backgroundColor), tabRect);

        // Vẽ chữ với font tùy chỉnh
        TextRenderer.DrawText(e.Graphics, tabPage.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), tabRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }
}
