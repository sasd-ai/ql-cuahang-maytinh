using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CustomControl;
using Custom_Controls;
using BUS;
using DTO;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_BanHang : UserControl
    {
        private Cloudinary cloudinary;
        BUS_BanHang busBanHang = new BUS_BanHang();
        private int currentPage = 1;
        private int itemsPerPage = 12; // Mỗi trang 12 sản phẩm
        private int totalPages = 1;

        public UC_BanHang()
        {
            InitializeComponent();
            InitializeCloudinary();
            LoadButtons();
            LoadComboBoxLoaiSanPham();
        }

        private void InitializeCloudinary()
        {
            Account account = new Account(
                "dfiyhshnk",         // Thay thế bằng Cloud Name của bạn
                "782519644662424",   // Thay thế bằng API Key của bạn
                "xoXUWZ8ZiCLuF7UDfnTTOem1jg8"   // Thay thế bằng API Secret của bạn
            );
            cloudinary = new Cloudinary(account);
        }


        private void LoadComboBoxLoaiSanPham()
        {
            // Lấy danh sách các loại sản phẩm từ cơ sở dữ liệu
            List<loaisp> loaiSanPhams = busBanHang.LoadLoaiSanPham();

            // Thêm vào combobox
            guna2ComboBox_LoaiSP.Items.Clear();
            guna2ComboBox_LoaiSP.Items.Add(new KeyValuePair<string, string>("0", "Tất cả")); // Thêm mục "Tất cả"

            foreach (loaisp loai in loaiSanPhams)
            {
                // Thêm vào combobox, sử dụng mã loại làm giá trị và tên loại làm hiển thị
                guna2ComboBox_LoaiSP.Items.Add(new KeyValuePair<string, string>(loai.MaLoai, loai.TenLoai));
            }

            // Thiết lập DisplayMember và ValueMember
            guna2ComboBox_LoaiSP.DisplayMember = "Value";
            guna2ComboBox_LoaiSP.ValueMember = "Key";

            // Thiết lập mục mặc định là "Tất cả"
            guna2ComboBox_LoaiSP.SelectedIndex = 0;
        }

        //private void LoadButtons()
        //{
        //    tableLayoutPanel_SanPham.Controls.Clear();

        //    // Lấy mã loại sản phẩm được chọn
        //    string maLoaiSanPham = guna2ComboBox_LoaiSP.SelectedItem is KeyValuePair<string, string> ?
        //        ((KeyValuePair<string, string>)guna2ComboBox_LoaiSP.SelectedItem).Key : "0";

        //    // Tải sản phẩm theo loại
        //    List<sanpham> sanPhams = maLoaiSanPham == "0" ?
        //        busBanHang.LoadSanPham() : // Nếu chọn "Tất cả" thì tải tất cả sản phẩm
        //        busBanHang.LoadSanPhamTheoLoai(maLoaiSanPham);

        //    // Tính toán số trang
        //    totalPages = (int)Math.Ceiling((double)sanPhams.Count / itemsPerPage);

        //    // Lấy danh sách sản phẩm cho trang hiện tại
        //    var sanPhamsPage = sanPhams.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();
        //    int currentColumn = 0; // Biến theo dõi cột hiện tại
        //    int currentRow = 0; // Biến theo dõi dòng hiện tại

        //    foreach (var sp in sanPhamsPage)
        //    {
        //        // Kiểm tra nếu đã đầy 3 cột trong dòng hiện tại (vì mỗi trang 12 sản phẩm, 4 cột/dòng)
        //        if (currentColumn >= 4)
        //        {
        //            currentColumn = 0; // Quay về cột đầu tiên
        //            currentRow++; // Tăng dòng lên 1

        //            // Tạo dòng mới cho tableLayoutPanel
        //            tableLayoutPanel_SanPham.RowCount = currentRow + 1;
        //        }

        //        string maSP = sp.MaSP;
        //        double giaSP = sp.GiaBan;
        //        string tenSP = sp.TenSP;
        //        string imageUrl = sp.HinhAnh;
        //        System.Drawing.Image image = LoadImageFromUrl(imageUrl);

        //        // Tạo button mới
        //        System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
        //        btn.Width = 150;
        //        btn.Height = 180;
        //        btn.TextAlign = ContentAlignment.BottomCenter;
        //        btn.FlatStyle = FlatStyle.Flat;
        //        btn.FlatAppearance.BorderSize = 1; // Loại bỏ viền mặc định
        //        btn.BackColor = Color.White; // Đặt màu nền cho button
        //        btn.Tag = maSP; // Lưu mã sản phẩm trong Tag để dễ dàng truy xuất
        //        btn.Click += Btn_Click;
        //        // Tạo GraphicsPath để tạo border góc tròn
        //        GraphicsPath path = new GraphicsPath();
        //        path.ThietKeButtonSanPham(new Rectangle(0, 0, btn.Width, btn.Height), 15);
        //        btn.Region = new Region(path);

        //        // Tạo panel chứa ảnh và text
        //        System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
        //        panel.Width = btn.Width;
        //        panel.Height = btn.Height;
        //        panel.Location = new System.Drawing.Point(0, 0); // Đặt panel ở vị trí góc trái trên
        //        panel.Click += Panel_Click;
        //        // Tạo PictureBox chứa ảnh
        //        PictureBox pictureBox = new PictureBox();
        //        pictureBox.Width = 70; // Chiều rộng ảnh = chiều rộng button - 40
        //        pictureBox.Height = 70; // Chiều cao ảnh = nửa chiều cao button - 20
        //        pictureBox.Image = image;
        //        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        //        pictureBox.Location = new System.Drawing.Point(40, 20); // Đặt ảnh cách trái và trên 20px

        //        // Tạo Label cho tên sản phẩm
        //        System.Windows.Forms.Label labelTenSP = new System.Windows.Forms.Label();
        //        labelTenSP.Text = tenSP;
        //        labelTenSP.TextAlign = ContentAlignment.MiddleCenter;
        //        labelTenSP.Font = new Font(labelTenSP.Font.FontFamily, 12, FontStyle.Bold); // Thiết kế font chữ
        //        labelTenSP.Location = new System.Drawing.Point(10, 100);
        //        labelTenSP.AutoSize = true; // Cho phép tự động điều chỉnh kích thước
        //        labelTenSP.MaximumSize = new System.Drawing.Size(130, 0);

        //        // Tạo Label cho giá sản phẩm
        //        System.Windows.Forms.Label labelGiaSP = new System.Windows.Forms.Label();
        //        labelGiaSP.Text = $"{giaSP:N0} đ"; // Định dạng giá trị và thêm "VND"
        //        labelGiaSP.TextAlign = ContentAlignment.MiddleCenter;
        //        labelGiaSP.Font = new Font(labelGiaSP.Font.FontFamily, 12, FontStyle.Bold); // Thiết kế font chữ
        //        labelGiaSP.ForeColor = Color.Red; // Đặt màu chữ là màu đỏ
        //        labelGiaSP.Location = new System.Drawing.Point(10, 150); // Đặt text cách trái 20px và cách trên 150px
        //        labelGiaSP.AutoSize = true; // Cho phép tự động điều chỉnh kích thước
        //        labelGiaSP.MaximumSize = new System.Drawing.Size(130, 0); // Giới hạn chiều rộng tối đa của label là 130px

        //        // Thêm PictureBox và hai Label vào Panel
        //        panel.Controls.Add(pictureBox);
        //        panel.Controls.Add(labelTenSP);
        //        panel.Controls.Add(labelGiaSP);

        //        // Thêm Panel vào Button
        //        btn.Controls.Add(panel);

        //        // Thêm button vào TableLayoutPanel
        //        tableLayoutPanel_SanPham.Controls.Add(btn, currentColumn, currentRow); // Thêm button vào cột hiện tại, dòng hiện tại

        //        currentColumn++;
        //    }

        //    // Thêm các nút phân trang
        //    AddPaginationButtons();
        //}
        private void LoadButtons()
        {
            tableLayoutPanel_SanPham.Controls.Clear();

            // Lấy mã loại sản phẩm được chọn
            string maLoaiSanPham = guna2ComboBox_LoaiSP.SelectedItem is KeyValuePair<string, string> ?
                ((KeyValuePair<string, string>)guna2ComboBox_LoaiSP.SelectedItem).Key : "0";

            // Tải sản phẩm theo loại
            List<sanpham> sanPhams = maLoaiSanPham == "0" ?
                busBanHang.LoadSanPham() : // Nếu chọn "Tất cả" thì tải tất cả sản phẩm
                busBanHang.LoadSanPhamTheoLoai(maLoaiSanPham);

            // Tính toán số trang
            totalPages = (int)Math.Ceiling((double)sanPhams.Count / itemsPerPage);

            // Lấy danh sách sản phẩm cho trang hiện tại
            var sanPhamsPage = sanPhams.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            int currentColumn = 0; // Biến theo dõi cột hiện tại
            int currentRow = 0; // Biến theo dõi dòng hiện tại

            foreach (var sp in sanPhamsPage)
            {
                // Kiểm tra nếu đã đầy 3 cột trong dòng hiện tại (vì mỗi trang 12 sản phẩm, 4 cột/dòng)
                if (currentColumn >= 4)
                {
                    currentColumn = 0; // Quay về cột đầu tiên
                    currentRow++; // Tăng dòng lên 1

                    // Tạo dòng mới cho tableLayoutPanel
                    tableLayoutPanel_SanPham.RowCount = currentRow + 1;
                }

                // Tạo UC_SanPham mới và thiết lập các giá trị sản phẩm
                var ucSanPham = new CustomControl.UC_SanPham(sp.MaSP, sp.TenSP, sp.GiaBan, sp.HinhAnh);

                // Thêm sự kiện ProductClicked
                ucSanPham.ProductClicked += UcSanPham_ProductClicked;

                // Thêm UC_SanPham vào TableLayoutPanel
                tableLayoutPanel_SanPham.Controls.Add(ucSanPham, currentColumn, currentRow);
                currentColumn++;
            }

            // Thêm các nút phân trang
            AddPaginationButtons();
        }

        private void UcSanPham_ProductClicked(object sender, CustomControl.UC_SanPham.ProductEventArgs e)
        {
            // In thông tin sản phẩm ra console

            Console.WriteLine($"Mã sản phẩm: {e.MaSP}");
            Console.WriteLine($"Tên sản phẩm: {e.TenSP}");
            Console.WriteLine($"Giá sản phẩm: {e.GiaSP:N0} đ");
            Console.WriteLine("-------------------");
            MessageBox.Show($"MaSP: {e.MaSP}, TenSP: {e.TenSP}, GiaSP: {e.GiaSP:N0}");
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void Btn_Click(object sender, EventArgs e)
        {
           
        }

        private void AddPaginationButtons()
        {
            // Xóa các điều khiển phân trang hiện tại
            paginationPanel.Controls.Clear();

            // Nút "Trước"
            Button btnPrev = new Button();
            btnPrev.Text = "Trước";
            btnPrev.Enabled = currentPage > 1;
            btnPrev.Click += (s, e) =>
            {
                currentPage--;
                LoadButtons();
            };
            paginationPanel.Controls.Add(btnPrev);

            // Thêm các nút số trang
            for (int i = 1; i <= totalPages; i++)
            {
                Button btnPage = new Button();
                btnPage.Text = i.ToString();
                btnPage.Enabled = true;
                btnPage.Click += (s, e) => {
                    currentPage = int.Parse(((Button)s).Text);
                    LoadButtons();
                };
                paginationPanel.Controls.Add(btnPage);
            }

           // Nút "Sau"
            Button btnNext = new Button();
            btnNext.Text = "Sau";
            btnNext.Enabled = currentPage < totalPages;
            btnNext.Click += (s, e) =>
            {
                currentPage++;
                LoadButtons();
            };
            paginationPanel.Controls.Add(btnNext);

            // Hiển thị số trang hiện tại
            Label lblPageInfo = new Label();
            lblPageInfo.Text = $"Trang {currentPage} / {totalPages}";
            lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblPageInfo.AutoSize = true;
            paginationPanel.Controls.Add(lblPageInfo);

            // Sắp xếp các nút nằm ngang
            foreach (Control control in paginationPanel.Controls)
            {
                control.Anchor = AnchorStyles.Left | AnchorStyles.Top; // Cho phép điều chỉnh kích thước
                control.Margin = new Padding(5, 0, 5, 0); // Thêm khoảng cách giữa các nút
            }
        }

        private System.Drawing.Image LoadImageFromUrl(string url)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(url);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        var image = System.Drawing.Image.FromStream(ms);
                        return ResizeImage(image, 50, 50);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return newImage;
        }

        private void guna2ComboBox_LoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadButtons();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }
    }
}