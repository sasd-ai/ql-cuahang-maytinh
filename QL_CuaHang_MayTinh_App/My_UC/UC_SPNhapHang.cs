using QL_CuaHang_MayTinh_App.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
using DTO;
using BUS;
using Newtonsoft.Json.Linq;
namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_SPNhapHang : UserControl
    {
        private BUS_SanPham bUSanPham=new BUS_SanPham();
        private BUS_NhanVien bUS_NhanVien=new BUS_NhanVien();
        private BUS_PhieuNhapHang bUS_PhieuNhapHang=new BUS_PhieuNhapHang();
        private BUS_ChiTietPhieuNhap bUS_ChiTietPhieuNhap=new BUS_ChiTietPhieuNhap();
        public BindingList<sanpham> listSanPham { get; set; }
        public string maNCC;
        public UC_SPNhapHang()
        {
            InitializeComponent();
            this.Load += UC_SPNhapHang_Load;
            this.cbb_NhaCungCap.SelectedIndexChanged += Cbb_NhaCungCap_SelectedIndexChanged;
            this.dgv_DSSanPham.CellContentClick += Dgv_DSSanPham_CellContentClick;
            this.dgv_DSSanPham.CellValueChanged += Dgv_DSSanPham_CellValueChanged;
            this.dgv_DSSanPham.CellEndEdit += Dgv_DSSanPham_CellEndEdit;


            //Nhập hàng 
         
        }

        private void Dgv_DSSanPham_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu hàng và cột hợp lệ
                if (e.RowIndex >= 0 &&
                    (e.ColumnIndex == dgv_DSSanPham.Columns["sl"].Index ||
                     e.ColumnIndex == dgv_DSSanPham.Columns["GiaNhap"].Index))
                {
                    // Lấy giá trị của cột Số lượng và Giá nhập
                    var quantityCell = dgv_DSSanPham.Rows[e.RowIndex].Cells["SoLuong"];
                    var priceCell = dgv_DSSanPham.Rows[e.RowIndex].Cells["GiaNhap"];
                    var totalCell = dgv_DSSanPham.Rows[e.RowIndex].Cells["TongCong"];

                    // Kiểm tra nếu giá trị hợp lệ
                    if (quantityCell.Value != null && priceCell.Value != null &&
                        int.TryParse(quantityCell.Value.ToString(), out int quantity) &&
                        decimal.TryParse(priceCell.Value.ToString(), out decimal price))
                    {
                        // Tính tổng cộng
                        decimal total = quantity * price;
                        totalCell.Value = total;
                    }
                    else
                    {
                        // Xóa tổng cộng nếu giá trị không hợp lệ
                        totalCell.Value = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Dgv_DSSanPham_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        //Xoá một sản phẩm
        private void Dgv_DSSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu cột được nhấn là cột button và hàng hợp lệ
                if (e.ColumnIndex == dgv_DSSanPham.Columns["Action"].Index && e.RowIndex >= 0)
                {
                    // Lấy sản phẩm hiện tại
                    var selectedSanPham = dgv_DSSanPham.Rows[e.RowIndex].DataBoundItem as sanpham;

                    if (selectedSanPham != null)
                    {
                        // Xóa sản phẩm khỏi DataGridView
                        dgv_DSSanPham.Rows.RemoveAt(e.RowIndex);

                        // Xóa sản phẩm khỏi danh sách nếu có
                        if (listSanPham.Contains(selectedSanPham))
                        {
                            listSanPham.Remove(selectedSanPham);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                // Xử lý lỗi
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }


        private void UC_SPNhapHang_Load(object sender, EventArgs e)
        {
            loadComboBox_NCC();
            cbb_NhaCungCap.SelectedIndex = 0;
            maNCC=cbb_NhaCungCap.SelectedValue.ToString();
        }

        public void UpdateDataSanPham(BindingList<sanpham> list)
        {
            dgv_DSSanPham.DataSource = null;
            dgv_DSSanPham.DataSource = list;
            listSanPham = list;
            formatDataGridVieew_SPDaChon();
            // Duyệt qua từng hàng để cập nhật giá trị cột
            foreach (DataGridViewRow row in dgv_DSSanPham.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua hàng mới nếu có

                // Cập nhật giá trị mặc định cho cột Số lượng và Giá nhập
                row.Cells["sl"].Value = 0; // Giá trị mặc định cho Số lượng
                row.Cells["GiaNhap"].Value = 0; // Giá trị mặc định cho Giá nhập

                // Cập nhật tổng cộng dựa trên các giá trị mặc định
                row.Cells["TongCong"].Value = 0; // Tổng cộng = Số lượng * Giá nhập (ở đây là 0)
            }
        }

        private void Cbb_NhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            maNCC = cbb_NhaCungCap.SelectedValue.ToString();
            dgv_DSSanPham.Rows.Clear();
        }
        void loadComboBox_NCC()
        {
            cbb_NhaCungCap.DataSource = bUSanPham.LoadNCC();
            cbb_NhaCungCap.DisplayMember = "TenNCC";
            cbb_NhaCungCap.ValueMember = "MaNCC";
        }
       

        private void btn_AddSP_Click(object sender, EventArgs e)
        {
            if (Program.frm_NhapSP == null || Program.frm_NhapSP.IsDisposed)
            {
                // Khởi tạo form chính và hiển thị
                Program.frm_NhapSP = new Frm_NhapSP();
            }
            Program.frm_NhapSP.maNCC = cbb_NhaCungCap.SelectedValue.ToString();
            Program.frm_NhapSP.Show();
            Program.frm_NhapSP.BringToFront();
        }

        void formatDataGridVieew_SPDaChon()
        {

            // Thêm cột Số lượng
            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.Name = "sl";
            quantityColumn.HeaderText = "Số lượng";
            quantityColumn.Width = 100;
            dgv_DSSanPham.Columns.Add(quantityColumn);

            // Thêm cột Giá nhập
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "GiaNhap";
            priceColumn.HeaderText = "Giá nhập";
            priceColumn.Width = 100;
            dgv_DSSanPham.Columns.Add(priceColumn);

            // Thêm cột Tổng cộng
            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            totalColumn.Name = "TongCong";
            totalColumn.HeaderText = "Tổng cộng";
            totalColumn.Width = 100;
            totalColumn.ReadOnly = true; // Cột Tổng cộng chỉ đọc
            dgv_DSSanPham.Columns.Add(totalColumn);

            // Tạo cột button mới
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Action";
            buttonColumn.HeaderText = "Hành động";
            buttonColumn.Text = "Xoá";
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.Width = 100;

            dgv_DSSanPham.Columns.Add( buttonColumn);


            // Căn giữa tiêu đề các cột
            foreach (DataGridViewColumn column in dgv_DSSanPham.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Thiết lập tiêu đề cột
            dgv_DSSanPham.Columns["MaSP"].HeaderText = "Mã sản phẩm";
            dgv_DSSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";

            dgv_DSSanPham.Columns["MaLoai"].HeaderText = "Mã loại";
            dgv_DSSanPham.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            // Ẩn cột 
            if (dgv_DSSanPham.Columns.Contains("HinhAnh"))
            {
                dgv_DSSanPham.Columns["HinhAnh"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("GiaDeXuat"))
            {
                dgv_DSSanPham.Columns["GiaDeXuat"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("SoLuong"))
            {
                dgv_DSSanPham.Columns["SoLuong"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("GiaBan"))
            {
                dgv_DSSanPham.Columns["GiaBan"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("Tg_BaoHanh"))
            {
                dgv_DSSanPham.Columns["Tg_BaoHanh"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("loaisp"))
            {
                dgv_DSSanPham.Columns["loaisp"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("nhacungcap"))
            {
                dgv_DSSanPham.Columns["nhacungcap"].Visible = false;
            }
            if (dgv_DSSanPham.Columns.Contains("MoTa"))
            {
                dgv_DSSanPham.Columns["MoTa"].Visible = false;
            }
            // Đặt thuộc tính Font của DataGridView
            dgv_DSSanPham.Font = new Font("Arial", 12, FontStyle.Regular);
        }

        private void btn_NhapHang_Click(object sender, EventArgs e)
        {
            string maNV = bUS_NhanVien.FindByEmail(Program.frmMain.email).MaNV;
            if (maNV != null)
            {
                MessageBox.Show("Hệ thống lỗi!");
                return;
            }
            //Tạo mã đơn hàng
            string ngayNhap = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            List<phieunhaphang> list = bUS_PhieuNhapHang.GetData();
            string ngayPN= DateTime.Now.ToString("yyyyMMdd");
            var phieuNhapTrongNgay = list.Where(pn => pn.NgayNhap.ToString("yyyyMMdd") == ngayPN).ToList();

            // Đếm số lượng phiếu nhập trong ngày hiện tại
            int soLuongPhieuNhap = phieuNhapTrongNgay.Count;
            string maPN= $"PN{ngayPN}{soLuongPhieuNhap + 1:D2}";

            foreach (DataGridViewRow item in dgv_DSSanPham.Rows)
            {
                //Duyệt và thêm xuống chi tiết phiếu nhập
            }
        }
    }
}
