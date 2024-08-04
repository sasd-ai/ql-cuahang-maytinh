using Custom_Controls;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Windows.Forms;
namespace CustomControl
{
    public partial class UC_SanPham : UserControl
    {

        public void DisableButton()
        {
            this.Enabled = false;
        }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public double GiaSP { get; set; }
        public string ImageUrl { get; set; }
        public event EventHandler<ProductEventArgs> ProductClicked;

        public UC_SanPham(string maSP, string tenSP, double giaSP, string imageUrl)
        {
            InitializeComponent();

            MaSP = maSP;
            TenSP = tenSP;
            GiaSP = giaSP;
            ImageUrl = imageUrl;

            InitializeButton();
        }

        public void InitializeButton()
        {
            // Tạo button mới
            Button btn = new Button
            {
                Width = 150,
                Height = 170,
                TextAlign = ContentAlignment.BottomCenter,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Tag = MaSP
            };
            btn.FlatAppearance.BorderSize = 1; // Loại bỏ viền mặc định
            btn.Click += UC_SanPham_Click; // Thêm sự kiện click cho Button

            // Tạo GraphicsPath để tạo border góc tròn
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, 30, 30, 180, 90);
            path.AddArc(btn.Width - 30, 0, 30, 30, 270, 90);
            path.AddArc(btn.Width - 30, btn.Height - 30, 30, 30, 0, 90);
            path.AddArc(0, btn.Height - 30, 30, 30, 90, 90);
            path.CloseAllFigures();
            btn.Region = new Region(path);

            // Tạo panel chứa ảnh và text
            Panel panel = new Panel
            {
                Width = btn.Width,
                Height = btn.Height,
                Location = new Point(0, 0)
            };

            panel.Click += UC_SanPham_Click;

            // Tạo PictureBox chứa ảnh
            PictureBox pictureBox = new PictureBox
            {
                Width = 70,
                Height = 70,
                Image = LoadImageFromUrl(ImageUrl),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(40, 20)
            };
            pictureBox.Click += UC_SanPham_Click; // Thêm sự kiện click cho PictureBox

            // Tạo Label cho tên sản phẩm
            //Label labelTenSP = new Label
            //{
            //    Text = GetEllipsizedText(TenSP, 130, 40, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)),
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
            //    Location = new Point(10, 100),
            //    AutoSize = false,
            //    MaximumSize = new Size(130, 40),
            //    Size = new Size(130, 40)
            //};
            Label labelTenSP = new Label
            {
                Text = GetEllipsizedText(TenSP, 130, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                Location = new Point(10, 100),
                AutoSize = false,
                Size = new Size(130, 40)
            };

            labelTenSP.Click += UC_SanPham_Click;

            // Tạo Label cho giá sản phẩm
            Label labelGiaSP = new Label
            {
                Text = $"{GiaSP:N0} đ",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                ForeColor = Color.Red,
                Location = new Point(10, 140),
                AutoSize = false,
                MaximumSize = new Size(130, 0),
                Size = new Size(160, 30)
            };
            labelGiaSP.Click += UC_SanPham_Click;

            // Thêm PictureBox và hai Label vào Panel
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(labelTenSP);
            panel.Controls.Add(labelGiaSP);

            // Thêm Panel vào Button
            btn.Controls.Add(panel);

            // Thêm button vào UserControl
            Controls.Add(btn);
        }
        public System.Drawing.Image LoadImageFromUrl(string url)
        {
            using (var webClient = new System.Net.WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(url);
                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    return ResizeImage(image, 50, 50);
                }
            }
        }

        public System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return newImage;
        }
        private void UC_SanPham_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện click cho tất cả các thành phần
            ProductClicked?.Invoke(this, new ProductEventArgs(MaSP, TenSP, GiaSP));
        }

        

        //private string GetEllipsizedText(string text, int width, Font font)
        //{
        //    using (var tempBitmap = new Bitmap(1, 1))
        //    {
        //        using (var graphics = Graphics.FromImage(tempBitmap))
        //        {
        //            var format = new StringFormat
        //            {
        //                FormatFlags = StringFormatFlags.LineLimit,
        //                Trimming = StringTrimming.EllipsisWord,
        //                Alignment = StringAlignment.Center,
        //                LineAlignment = StringAlignment.Center
        //            };

        //            var size = new SizeF(width, float.MaxValue);
        //            var measuredSize = graphics.MeasureString(text, font, size, format);

        //            if (measuredSize.Width > width)
        //            {
        //                // Create a StringFormat to truncate text with ellipses
        //                var trimmedText = text;
        //                while (measuredSize.Width > width && trimmedText.Length > 0)
        //                {
        //                    trimmedText = trimmedText.Substring(0, trimmedText.Length - 1);
        //                    measuredSize = graphics.MeasureString(trimmedText + "...", font, size, format);
        //                }
        //                return trimmedText + "...";
        //            }

        //            return text;
        //        }
        //    }
        //}

        private string GetEllipsizedText(string text, int width, Font font)
        {
            using (var tempBitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(tempBitmap))
                {
                    var format = new StringFormat
                    {
                        FormatFlags = StringFormatFlags.LineLimit,
                        Trimming = StringTrimming.EllipsisWord,
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    var size = new SizeF(width, float.MaxValue);
                    var measuredSize = graphics.MeasureString(text, font, size, format);

                    if (measuredSize.Width > width)
                    {
                        // Cắt bớt văn bản cho đến khi kích thước vừa
                        var trimmedText = text;
                        while (measuredSize.Width > width && trimmedText.Length > 0)
                        {
                            trimmedText = trimmedText.Substring(0, trimmedText.Length - 1);
                            measuredSize = graphics.MeasureString(trimmedText + "...", font, size, format);
                        }
                        return trimmedText + "...";
                    }

                    return text;
                }
            }
        }



        public class ProductEventArgs : EventArgs
        {
            public string MaSP { get; }
            public string TenSP { get; }
            public double GiaSP { get; }

            public ProductEventArgs(string maSP, string tenSP, double giaSP)
            {
                MaSP = maSP;
                TenSP = tenSP;
                GiaSP = giaSP;
            }
        }
    }
}
