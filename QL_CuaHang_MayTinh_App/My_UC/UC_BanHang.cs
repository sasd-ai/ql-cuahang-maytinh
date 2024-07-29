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
        // Class_SanPham class_sp=new Class_SanPham();
        //private Cloudinary cloudinary;
        private Dictionary<string, (int SoLuong, double GiaSP)> selectedProducts = new Dictionary<string, (int, double)>();
        private double totalPrice = 0;
        private double thanhTien = 0;

        public string MaNV;
        BUS_BanHang busBanHang = new BUS_BanHang();
        BUS_SanPham bus_SanPham = new BUS_SanPham();
        private int currentPage = 1;
        private int itemsPerPage = 12; // Mỗi trang 12 sản phẩm
        private int totalPages = 1;

        public UC_BanHang()
        {
            InitializeComponent();
            bus_SanPham.InitializeCloudinary();
            LoadButtons();
            LoadComboBoxLoaiSanPham();
            SetupDataGridView();
          

            // Khởi tạo FlowLayoutPanel và thêm vào panel2
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            panel4.Controls.Add(flowLayoutPanel1);
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

        
        private void LoadButtons()
        {
            tableLayoutPanel_SanPham.Controls.Clear();

            string searchText = textBox_SearchTenSP.Text.Trim();

            List<sanpham> sanPhams;

            // Kiểm tra nếu có giá trị tìm kiếm
            if (!string.IsNullOrEmpty(searchText))
            {
                // Gọi hàm tìm kiếm trong BUS_BanHang
                sanPhams = bus_SanPham.FindByName(searchText);
            }
            else
            {
                // Lấy mã loại sản phẩm được chọn
                string maLoaiSanPham = guna2ComboBox_LoaiSP.SelectedItem is KeyValuePair<string, string> ?
                    ((KeyValuePair<string, string>)guna2ComboBox_LoaiSP.SelectedItem).Key : "0";

                // Tải sản phẩm theo loại
                sanPhams = maLoaiSanPham == "0" ?
                busBanHang.LoadSanPham() : // Nếu chọn "Tất cả" thì tải tất cả sản phẩm
                busBanHang.LoadSanPhamTheoLoai(maLoaiSanPham);
            }

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

        // Đảm bảo rằng panel2 là FlowLayoutPanel
        private void UcSanPham_ProductClicked(object sender, CustomControl.UC_SanPham.ProductEventArgs e)
        {
            if (selectedProducts.ContainsKey(e.MaSP))
            {
                var current = selectedProducts[e.MaSP];
                selectedProducts[e.MaSP] = (current.SoLuong + 1, e.GiaSP);
            }
            else
            {
                selectedProducts[e.MaSP] = (1, e.GiaSP);
            }

            totalPrice += e.GiaSP;
            label_TongTien.Text = $"{totalPrice:N0} đ";

            // Truyền giá trị riêng lẻ thay vì tuple
            UpdateDataGridView(e.MaSP, e.TenSP, e.GiaSP, selectedProducts[e.MaSP].SoLuong);
           
        }


        private void UpdateDataGridView(string maSP, string tenSP, double giaSP, int soLuong)
        {
            bool productFound = false;

            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (row.Cells["masp"].Value != null && row.Cells["masp"].Value.ToString() == maSP)
                {
                    row.Cells["sl"].Value = soLuong;
                    row.Cells["giaban"].Value = $"{giaSP:N0} đ";
                    row.Cells["tt"].Value = $"{giaSP * soLuong:N0} đ";
                    productFound = true;
                    break;
                }
            }

            if (!productFound)
            {
                guna2DataGridView2.Rows.Add(maSP, tenSP, $"{giaSP:N0} đ", soLuong, $"{giaSP * soLuong:N0} đ");
            }
        }




        // Thiết lập cấu trúc cột cho DataGridView
        private void SetupDataGridView()
        {

            // Cột Xóa
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn
            {
                Name = "Xoa",
                HeaderText = "Hành Động",
                Width = 80,
                Image = Properties.Resources.delete11 // Sử dụng hình ảnh từ thư mục Resources
            };

            guna2DataGridView2.Columns.Add(imgColumn);
            guna2DataGridView2.RowTemplate.Height = 50;
            guna2DataGridView2.CellContentClick += Guna2DataGridView2_CellContentClick;
        }

        private void Guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView2.Columns["Xoa"].Index && e.RowIndex >= 0)
            {
                // Xóa hàng hiện tại
                var rowToRemove = guna2DataGridView2.Rows[e.RowIndex];
                double rowTotal = double.Parse(rowToRemove.Cells["tt"].Value.ToString().Replace(" đ", ""));
                guna2DataGridView2.Rows.RemoveAt(e.RowIndex);

                // Cập nhật tổng tiền
                totalPrice -= rowTotal;
                label_TongTien.Text = $"{totalPrice:N0} đ";
               
            }
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

        

        private void guna2ComboBox_LoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadButtons();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }

        private void button_SearchTenSP_Click(object sender, EventArgs e)
        {
            LoadButtons();
        }

        private void btn_Searchsdt_Click(object sender, EventArgs e)
        {
            string sdt = txt_Searchsdt.Text.Trim(); // Lấy text tìm kiếm
            label_makh.Visible = false;
            // Kiểm tra text tìm kiếm có rỗng hay không
            if (string.IsNullOrEmpty(sdt))
            {
                // Nếu text tìm kiếm rỗng, làm label trống
                label_KhachHang.Text = "";
            }
            else
            {
                // Nếu text tìm kiếm không rỗng, thực hiện tìm kiếm
                List<khachhang> khachhangs = busBanHang.FindBySDT(sdt);

                if (khachhangs.Count > 0)
                {
                    // Hiển thị chỉ tên khách hàng
                    label_KhachHang.Text = $"Tên khách hàng: {khachhangs[0].TenKH}";
                    label_makh.Text = khachhangs[0].MaKH;
                    
                }
                else
                {
                    // Hiển thị thông báo không tìm thấy
                    label_KhachHang.Text = "Không tìm thấy khách hàng";
                }
            }
        }
        
        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                
                string maBanHang = "MBH" + new Random().Next(100, 9999).ToString();

              
                string maKhachHang = label_makh.Text;  

              
                double tongTien = totalPrice;
                double thanhtien = thanhTien;

               
               
             
                DateTime ngayBan = DateTime.Now;

              
                banhang bh = new banhang
                {
                    MaBanHang = maBanHang,
                    MaNV = this.MaNV,  
                    MaKhachHang = maKhachHang,
                    TongTien = (float)tongTien,                 
                    GhiChu = "",
                    NgayBan = ngayBan,
                   
                };

               
                if (busBanHang.InsertBanHang(bh))
                {
                   
                    foreach (var item in selectedProducts)
                    {
                       
                        double thanhTien = item.Value.SoLuong * item.Value.GiaSP;

                        chitietbanhang ctb = new chitietbanhang
                        {
                            MaBanHang = maBanHang,
                            MaSP = item.Key,
                            ThanhTien = thanhTien
                        };

                        if (!busBanHang.InsertChiTietBanHang(ctb))
                        {
                            MessageBox.Show("Lỗi khi lưu chi tiết bán hàng. Vui lòng thử lại.");
                            return;
                        }
                    }

                    MessageBox.Show("Thanh toán thành công!");
                 
                    selectedProducts.Clear();
                    totalPrice = 0;
                    label_TongTien.Text = "0 đ";
                   
                    guna2DataGridView2.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thanh toán. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
             
                Console.WriteLine("Exception caught: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
        }

    }
}