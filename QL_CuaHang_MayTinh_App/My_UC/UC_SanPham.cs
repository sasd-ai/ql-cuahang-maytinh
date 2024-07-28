using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DTO;
using BUS;
using System.Data.SqlClient;
using CloudinaryDotNet.Core;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_SanPham : UserControl
    {
      
        BUS_SanPham bus_SanPham = new BUS_SanPham();
        private System.Drawing.Image loadedImageFromDataGridView;
        private Cloudinary cloudinary;
        private string HanhDong = "";
        public UC_SanPham()
        {
            InitializeComponent();
            this.Load += UC_SanPham_Load;
        }

        private void UC_SanPham_Load(object sender, EventArgs e)
        {
            LoadDLSanPham();
            LoadCBB_LoaiSP();
            LoadCBB_NhaCC();
            InitializeCloudinary();           
        }
        public void InitializeCloudinary()
        {
            Account account = new Account(
                "dfiyhshnk",         // Thay thế bằng Cloud Name của bạn
                "782519644662424",   // Thay thế bằng API Key của bạn
                "xoXUWZ8ZiCLuF7UDfnTTOem1jg8"   // Thay thế bằng API Secret của bạn
            );
            cloudinary = new Cloudinary(account);
        }
        public void LoadCBB_NhaCC()
        {
            // Lấy danh sách các loại sản phẩm từ cơ sở dữ liệu
            List<nhacungcap> nhacc = bus_SanPham.LoadNCC();

            // Thêm vào combobox
            cbb_TenNCC.Items.Clear();
            // cbb_TenLoai.Items.Add(new KeyValuePair<string, string>("0", "Tất cả")); // Thêm mục "Tất cả"

            foreach (nhacungcap ncc in nhacc)
            {
                // Thêm vào combobox, sử dụng mã loại làm giá trị và tên loại làm hiển thị
                cbb_TenNCC.Items.Add(new KeyValuePair<string, string>(ncc.MaNCC, ncc.TenNCC));
            }

            // Thiết lập DisplayMember và ValueMember
            cbb_TenNCC.DisplayMember = "Value";
            cbb_TenNCC.ValueMember = "Key";

            // Thiết lập mục mặc định là "Tất cả"
            cbb_TenNCC.SelectedIndex = 0;
        }

        public void LoadCBB_LoaiSP()
        {
            // Lấy danh sách các loại sản phẩm từ cơ sở dữ liệu
            List<loaisp> loaiSanPhams = bus_SanPham.LoadLoaiSP();
            
            // Thêm vào combobox
            cbb_TenLoai.Items.Clear();
           // cbb_TenLoai.Items.Add(new KeyValuePair<string, string>("0", "Tất cả")); // Thêm mục "Tất cả"

            foreach (loaisp loai in loaiSanPhams)
            {
                // Thêm vào combobox, sử dụng mã loại làm giá trị và tên loại làm hiển thị
                cbb_TenLoai.Items.Add(new KeyValuePair<string, string>(loai.MaLoai, loai.TenLoai));
            }

            // Thiết lập DisplayMember và ValueMember
            cbb_TenLoai.DisplayMember = "Value";
            cbb_TenLoai.ValueMember = "Key";

            // Thiết lập mục mặc định là "Tất cả"
            cbb_TenLoai.SelectedIndex = 0;
        }
        public void LoadDLSanPham()
        {
            // Xóa tất cả các cột và hàng hiện có trước khi thêm mới
            dataGridView_SanPham.Columns.Clear();
            dataGridView_SanPham.Rows.Clear();

        
            dataGridView_SanPham.Columns.Add("MaSP", "Mã Sản Phẩm");
            dataGridView_SanPham.Columns.Add("TenSP", "Tên Sản Phẩm");          
            

            // Tạo cột hình ảnh với kiểu DataGridViewImageColumn
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "HinhAnh",
                HeaderText = "Hình Ảnh",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 80 
            };
            dataGridView_SanPham.Columns.Add(imageColumn);
            dataGridView_SanPham.Columns.Add("giadexuat", "Giá Đề Xuất");
            dataGridView_SanPham.Columns.Add("soluong", "Số Lượng");
            dataGridView_SanPham.Columns.Add("giaban", "Giá Bán");
            dataGridView_SanPham.Columns.Add("mota", "Mô Tả");
            dataGridView_SanPham.Columns.Add("tgbh", "Thời Gian Bảo Hành");
            dataGridView_SanPham.Columns.Add("tenloai", "Tên Loại");
            dataGridView_SanPham.Columns.Add("tenncc", "Tên Nhà Cung Cấp");
            // Lấy dữ liệu từ sp.LoadSanPham()
            var sanPhamList = bus_SanPham.LoadSanPham();

            // Duyệt qua từng hàng trong danh sách sản phẩm và tải hình ảnh từ URL
            foreach (var sp in sanPhamList)
            {
                string imageUrl = sp.hinhanh;
                System.Drawing.Image image = bus_SanPham.LoadImageFromUrl(imageUrl);

                // Thêm hàng vào DataGridView
                dataGridView_SanPham.Rows.Add(sp.Masp, sp.tensp, image,sp.giadexuat,sp.soluong,sp.giaban,sp.mota,sp.tg_baohanh,sp.TenLoai,sp.Tenncc);
            }

            // Đặt chiều cao hàng
            dataGridView_SanPham.RowTemplate.Height = 80; 
          
            // Cài đặt cài đặt lại kích thước tự động
           
            dataGridView_SanPham.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private async void dataGridView_SanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_SanPham.SelectedRows.Count > 0)
            {               
                string maSP = dataGridView_SanPham.CurrentRow.Cells[0].Value.ToString();             
                string tenSP = dataGridView_SanPham.CurrentRow.Cells[1].Value.ToString();
                string giaDX= dataGridView_SanPham.CurrentRow.Cells[3].Value.ToString();
                string soLuong = dataGridView_SanPham.CurrentRow.Cells[4].Value.ToString();
                string giaBan = dataGridView_SanPham.CurrentRow.Cells[5].Value.ToString();
                string moTa = dataGridView_SanPham.CurrentRow.Cells[6].Value.ToString();
                string tgbaoHnah = dataGridView_SanPham.CurrentRow.Cells[7].Value.ToString();
                string tenLoai = dataGridView_SanPham.CurrentRow.Cells[8].Value.ToString();
                string tenNCC = dataGridView_SanPham.CurrentRow.Cells[9].Value.ToString();

                // Lưu trữ hình ảnh đã tải từ DataGridView
                loadedImageFromDataGridView = dataGridView_SanPham.CurrentRow.Cells[2].Value as System.Drawing.Image;

                // Hiển thị hình ảnh lên PictureBox
                if (loadedImageFromDataGridView != null)
                {
                    pictureBox_Image.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox_Image.Image = loadedImageFromDataGridView;
                }
                else
                {
                    // Lấy URL hình ảnh từ cột và tải hình ảnh từ URL
                    string imageUrl = dataGridView_SanPham.CurrentRow.Cells[2].Value.ToString();

                    try
                    {
                        if (IsValidUrl(imageUrl))
                        {
                            using (var httpClient = new System.Net.Http.HttpClient())
                            {
                                var response = await httpClient.GetAsync(imageUrl);
                                response.EnsureSuccessStatusCode();

                                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                                using (var ms = new System.IO.MemoryStream(imageBytes))
                                {
                                    pictureBox_Image.SizeMode = PictureBoxSizeMode.Zoom;
                                    pictureBox_Image.Image = System.Drawing.Image.FromStream(ms);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("URL hình ảnh không hợp lệ.");
                            pictureBox_Image.Image = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể tải hình ảnh: {ex.Message}");
                        pictureBox_Image.Image = null;
                    }
                }           
                txt_maSP.Text = maSP;
                text_tenSP.Text = tenSP;
                txt_GDX.Text = giaDX;
                txt_GB.Text = giaBan;
                txt_MoTa.Text = moTa;
                txt_SL.Text = soLuong;
                txt_TGBH.Text = tgbaoHnah;
                cbb_TenLoai.Text = tenLoai;
                cbb_TenNCC.Text=tenNCC;
              
            }
        }
        private bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        private void pictureBox_Image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Load the new image directly
                    pictureBox_Image.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        public void XoaText()
        {
            txt_maSP.Text = "";
            text_tenSP.Text = "";
            txt_GDX.Text= "";
            txt_SL.Text = "";
            txt_GB.Text = "";
            txt_MoTa.Text = "";
           txt_TGBH.Text = "";

        }

        public bool KT_NhapText()
        {
          
            // Duyệt qua tất cả các điều khiển trong panel2
            foreach (Control control in panel3.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox
                if (control is TextBox textBox)
                {
                    // Kiểm tra nếu TextBox trống
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Thêm vào danh sách nếu trống
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin nha!!");
                    }
                }
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {          
            XoaText();
            HanhDong = "Them";
            btn_save.Enabled=true;
            btn_Huy.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            txt_maSP.Enabled = true;
            //    string MaSP = txt_maSP.Text;
            //    string TenSP = text_tenSP.Text;
            //    string GiaDeXuat = int.Parse(txt_GDX.Text).ToString();
            //    string SoLuong = int.Parse(txt_SL.Text).ToString();
            //    string GiaBan =int.Parse(txt_GB.Text).ToString();                       
            //    string MoTa = txt_MoTa.Text;
            //    string Tg_BaoHanh = int.Parse(txt_TGBH.Text).ToString();
            //    var selectedLoai = (KeyValuePair<string, string>)cbb_TenLoai.SelectedItem;
            //    string MaLoai = selectedLoai.Key;
            //    var selectedNCC = (KeyValuePair<string, string>)cbb_TenNCC.SelectedItem;
            //    string MaNCC = selectedNCC.Key;
            //if (pictureBox_Image.Image != null)
            //    {
            //        // Tải hình ảnh từ PictureBox lên Cloudinary
            //        using (var memoryStream = new System.IO.MemoryStream())
            //        {
            //            pictureBox_Image.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            //            memoryStream.Position = 0;

            //            var uploadParams = new ImageUploadParams()
            //            {
            //                File = new FileDescription("image.png", memoryStream)
            //            };
            //            var uploadResult = cloudinary.Upload(uploadParams);
            //            string imageUrl = uploadResult.SecureUrl.ToString();

            //            // Lưu URL hình ảnh vào cơ sở dữ liệu
            //            bus_SanPham.ThemSanPham(MaSP,TenSP,imageUrl,GiaDeXuat,SoLuong,GiaBan, MoTa,Tg_BaoHanh, MaLoai,MaNCC);
            //        MessageBox.Show("Bạn đã thêm sản phẩm thành công rồi nha!!!");
            //        LoadDLSanPham();
            //    }
            //    }
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn hình ảnh từ PictureBox trước.");
            //}
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            txt_maSP.Enabled = false;
            if (txt_maSP!=null)
            {
                HanhDong = "Xoa";
                btn_save.Enabled = true;
                btn_Huy.Enabled = true;
                btnThem.Enabled = false;
                btn_update.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui Lòng nhập dữ liệu đầy đủ !!");
                return;
            }
            //if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    string MaSP = txt_maSP.Text;
            //    bus_SanPham.XoaSanPham(MaSP);
            //    MessageBox.Show("Xóa Sản Phẩm thành công!");
            //    LoadDLSanPham() ;
            //}
        }

        private  void btn_update_Click(object sender, EventArgs e)
        {

            txt_maSP.Enabled = false;
           
                HanhDong = "Sua";
                btn_save.Enabled = true;
                btn_Huy.Enabled = true;
                btnThem.Enabled = false;
                btn_delete.Enabled = false;
            
            //string MaSP = txt_maSP.Text;
            //string TenSP = text_tenSP.Text;
            //string GiaDeXuat = int.Parse(txt_GDX.Text).ToString();
            //string SoLuong = int.Parse(txt_SL.Text).ToString();
            //string GiaBan = int.Parse(txt_GB.Text).ToString();
            //string MoTa = txt_MoTa.Text;
            //string Tg_BaoHanh = int.Parse(txt_TGBH.Text).ToString();
            //var selectedLoai = (KeyValuePair<string, string>)cbb_TenLoai.SelectedItem;
            //string MaLoai = selectedLoai.Key;
            //var selectedNCC = (KeyValuePair<string, string>)cbb_TenNCC.SelectedItem;
            //string MaNCC = selectedNCC.Key;

            //if (pictureBox_Image.Image != null)
            //{
                
            //        using (var memoryStream = new System.IO.MemoryStream())
            //        {
            //        pictureBox_Image.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            //            memoryStream.Position = 0;

            //            var uploadParams = new ImageUploadParams()
            //            {
            //                File = new FileDescription("image.png", memoryStream)
            //            };
            //            var uploadResult = await cloudinary.UploadAsync(uploadParams); // Sử dụng UploadAsync để đảm bảo async
            //            string imageUrl = uploadResult.SecureUrl.ToString();
            //        bus_SanPham.SuaSanPham(MaSP, TenSP, imageUrl, GiaDeXuat, SoLuong, GiaBan, MoTa, Tg_BaoHanh, MaLoai, MaNCC);
            //        MessageBox.Show("Sửa sản phẩm thành công.");
            //        LoadDLSanPham();
            //    }            
            // }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            if (KT_NhapText()==false)
            {
                MessageBox.Show("Vui Lòng nhập đầy đủ thông tin nha!!");
                return;
            }
            string MaSP = txt_maSP.Text;
            string TenSP = text_tenSP.Text;
            string GiaDeXuat = int.Parse(txt_GDX.Text).ToString();
            string SoLuong = int.Parse(txt_SL.Text).ToString();
            string GiaBan = int.Parse(txt_GB.Text).ToString();
            string MoTa = txt_MoTa.Text;
            string Tg_BaoHanh = int.Parse(txt_TGBH.Text).ToString();
            var selectedLoai = (KeyValuePair<string, string>)cbb_TenLoai.SelectedItem;
            string MaLoai = selectedLoai.Key;
            var selectedNCC = (KeyValuePair<string, string>)cbb_TenNCC.SelectedItem;
            string MaNCC = selectedNCC.Key;
            string imageUrl=null;
            if (pictureBox_Image.Image != null)
            {
                // Tải hình ảnh từ PictureBox lên Cloudinary
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    pictureBox_Image.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Position = 0;

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription("image.png", memoryStream)
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    imageUrl = uploadResult.SecureUrl.ToString();

                    // Lưu URL hình ảnh vào cơ sở dữ liệu
                   
                }
            }
            
            switch (HanhDong)
            {
                case "Them":
                    bus_SanPham.ThemSanPham(MaSP, TenSP, imageUrl, GiaDeXuat, SoLuong, GiaBan, MoTa, Tg_BaoHanh, MaLoai, MaNCC);
                    MessageBox.Show("Bạn đã thêm sản phẩm thành công rồi nha!!!");
                    LoadDLSanPham();
                    break;

                case "Sua":
                    bus_SanPham.SuaSanPham(MaSP, TenSP, imageUrl, GiaDeXuat, SoLuong, GiaBan, MoTa, Tg_BaoHanh, MaLoai, MaNCC);
                    MessageBox.Show("Sửa sản phẩm thành công.");
                    LoadDLSanPham();

                    break;

                case "Xoa":
                    if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        
                        bus_SanPham.XoaSanPham(MaSP);
                        MessageBox.Show("Xóa Sản Phẩm thành công!");
                        LoadDLSanPham();
                    }
                    break;
                default:
                    MessageBox.Show("Không có hành động nào được chọn!");
                    break;
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            XoaText();
            btn_save.Enabled = false;   
            btn_Huy.Enabled = false;
            btnThem.Enabled = true;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            HanhDong = "";
        }

       
    }
}