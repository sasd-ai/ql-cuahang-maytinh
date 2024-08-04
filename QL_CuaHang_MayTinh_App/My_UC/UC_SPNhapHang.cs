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
using System.Data.SqlClient;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_SPNhapHang : UserControl
    {
        private BUS_SanPham bUSanPham = new BUS_SanPham();
        private BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
        private BUS_PhieuNhapHang bUS_PhieuNhapHang = new BUS_PhieuNhapHang();
        private BUS_ChiTietPhieuNhap bUS_ChiTietPhieuNhap = new BUS_ChiTietPhieuNhap();
        private BUS_Report bUS_Report = new BUS_Report();
        private BUS_ChucVu bUS_ChucVu=new BUS_ChucVu();
        double tongTien = 0;
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

        private void Dgv_DSSanPham_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tongTien = 0;
            foreach (DataGridViewRow item in dgv_DSSanPham.Rows)
            {
                if (item.Cells["TongTien"].Value != null)
                {
                    tongTien += double.Parse(item.Cells["TongTien"].Value.ToString());
                }
            }
            txt_TongTienNhap.Text = tongTien.ToString() + " VND";
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
                    var quantityCell = dgv_DSSanPham.Rows[e.RowIndex].Cells["sl"];
                    var priceCell = dgv_DSSanPham.Rows[e.RowIndex].Cells["GiaNhap"];
                    var totalCell = dgv_DSSanPham.Rows[e.RowIndex].Cells["TongTien"];

                    // Kiểm tra nếu giá trị hợp lệ
                    if (quantityCell.Value != null && priceCell.Value != null &&
                        int.TryParse(quantityCell.Value.ToString(), out int quantity) &&
                        double.TryParse(priceCell.Value.ToString(), out double price))
                    {
                        // Tính tổng cộng
                        double total = quantity * price;
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
                MessageBox.Show("Lỗi: " + ex.Message);
            }
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
                        tongTien = 0;
                        foreach (DataGridViewRow item in dgv_DSSanPham.Rows)
                        {
                            if (item.Cells["TongTien"].Value != null)
                            {
                                tongTien += double.Parse(item.Cells["TongTien"].Value.ToString());
                            }
                        }
                        txt_TongTienNhap.Text = tongTien.ToString() + " VND";
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
            maNCC = cbb_NhaCungCap.SelectedValue.ToString();
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
                row.Cells["TongTien"].Value = 0; // Tổng cộng = Số lượng * Giá nhập (ở đây là 0)
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
            totalColumn.Name = "TongTien";
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

            dgv_DSSanPham.Columns.Add(buttonColumn);
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

        //private void btn_NhapHang_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if(dgv_DSSanPham.Rows.Count>=0)
        //        {
        //            string maNV = bUS_NhanVien.FindByEmail(Program.frmMain.email).MaNV;
        //            if (maNV == null)
        //            {
        //                MessageBox.Show("Hệ thống lỗi!");
        //                return;
        //            }

        //            // Tạo mã đơn hàng
        //            DateTime selectedDate = txt_NgayNhap.Value;
        //            string ngayNhap = selectedDate.ToString("yyyyMMdd");

        //            List<phieunhaphang> list = bUS_PhieuNhapHang.GetData();
        //            var phieuNhapTrongNgay = list.Where(pn => pn.NgayNhap.ToString("yyyyMMdd") == ngayNhap).ToList();

        //            // Đếm số lượng phiếu nhập trong ngày hiện tại
        //            int soLuongPhieuNhap = phieuNhapTrongNgay.Count;
        //            string maPN = $"PN{ngayNhap}{soLuongPhieuNhap + 1:D2}";
        //            string ghiChu = txt_GhiChu.Text;

        //            // Insert phiếu nhập
        //            phieunhaphang phieunhap = new phieunhaphang()
        //            {
        //                MaPN = maPN,
        //                MaNV = maNV,
        //                MaNCC = maNCC,
        //                TongTien = tongTien,
        //                GhiChu = ghiChu,
        //                NgayNhap = selectedDate
        //            };

        //            bool insertPN = bUS_PhieuNhapHang.Insert(phieunhap);
        //            if (insertPN)
        //            {
        //                foreach (DataGridViewRow item in dgv_DSSanPham.Rows)
        //                {
        //                    if (item.Cells["MaSP"].Value != null && item.Cells["sl"].Value != null && item.Cells["TongTien"].Value != null)
        //                    {
        //                        // Chi tiết phiếu nhập có mã phiếu nhập, mã sản phẩm, số lượng, thành tiền, ghi chú
        //                        chitietphieunhap chiTiet = new chitietphieunhap()
        //                        {
        //                            MaPN = maPN,
        //                            MaSP = item.Cells["MaSP"].Value.ToString(),
        //                            SoLuong = int.Parse(item.Cells["sl"].Value.ToString()),
        //                            ThanhTien = float.Parse(item.Cells["TongTien"].Value.ToString()),
        //                            GhiChu = ghiChu,
        //                            //GiaNhap
        //                            DonGiaNhap = float.Parse(item.Cells["GiaNhap"].Value.ToString())
        //                        };

        //                        bUS_ChiTietPhieuNhap.Insert(chiTiet);
        //                    }
        //                }

        //                dgv_DSSanPham.DataSource = null;
        //                listSanPham.Clear();
        //                //In report
        //                //In Hoá đơn
        //                InPhieuNhapResult inPhieuNhap = bUS_Report.GetDataPhieuNhap(maPN);

        //                rpt_InPhieuNhap rpt = new rpt_InPhieuNhap();
        //                rpt.SetDataSource(inPhieuNhap);
        //                Frm_InPhieuNhap frm_PhieuNhap = new Frm_InPhieuNhap();
        //                frm_PhieuNhap.rpt_PhieuNhap.ReportSource = rpt;
        //                frm_PhieuNhap.Show();

        //            }

        //        } 
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi: " + ex.Message);
        //    }
        //}
        private void btn_NhapHang_Click(object sender, EventArgs e)
        {

            if (dgv_DSSanPham.Rows.Count > 0)
            {
                string maNV = bUS_NhanVien.FindByEmail(Program.frmMain.email).MaNV;
                if (maNV == null)
                {
                    MessageBox.Show("Hệ thống lỗi!");
                    return;
                }

                // Tạo mã đơn hàng
                DateTime selectedDate = txt_NgayNhap.Value;
                string ngayNhap = selectedDate.ToString("yyyyMMdd");

                List<phieunhaphang> list = bUS_PhieuNhapHang.GetData();
                var phieuNhapTrongNgay = list.Where(pn => pn.NgayNhap.ToString("yyyyMMdd") == ngayNhap).ToList();

                // Đếm số lượng phiếu nhập trong ngày hiện tại
                int soLuongPhieuNhap = phieuNhapTrongNgay.Count;
                string maPN = $"PN{ngayNhap}{soLuongPhieuNhap + 1:D2}";
                string ghiChu = txt_GhiChu.Text;

                // Insert phiếu nhập
                phieunhaphang phieunhap = new phieunhaphang()
                {
                    MaPN = maPN,
                    MaNV = maNV,
                    MaNCC = maNCC,
                    TongTien = tongTien,
                    GhiChu = ghiChu,
                    NgayNhap = selectedDate
                };

                bool insertPN = bUS_PhieuNhapHang.Insert(phieunhap);
                if (insertPN)
                {
                    foreach (DataGridViewRow item in dgv_DSSanPham.Rows)
                    {
                        if (item.Cells["MaSP"].Value != null && item.Cells["sl"].Value != null && item.Cells["TongTien"].Value != null)
                        {
                            // Chi tiết phiếu nhập có mã phiếu nhập, mã sản phẩm, số lượng, thành tiền, ghi chú
                            chitietphieunhap chiTiet = new chitietphieunhap()
                            {
                                MaPN = maPN,
                                MaSP = item.Cells["MaSP"].Value.ToString(),
                                SoLuong = int.Parse(item.Cells["sl"].Value.ToString()),
                                ThanhTien = float.Parse(item.Cells["TongTien"].Value.ToString()),
                                GhiChu = ghiChu,
                                DonGiaNhap = float.Parse(item.Cells["GiaNhap"].Value.ToString())
                            };

                            bUS_ChiTietPhieuNhap.Insert(chiTiet);
                        }
                    }

                    dgv_DSSanPham.DataSource = null;
                    listSanPham.Clear();

                    // In report
                    List<InPhieuNhapResult> inPhieuNhap = bUS_Report.GetDataPhieuNhap(maPN);
                    if (inPhieuNhap != null && inPhieuNhap.Count > 0)
                    {
                        // Đảm bảo rằng có ít nhất một bản ghi trong inPhieuNhap
                        rpt_InPhieuNhap rpt = new rpt_InPhieuNhap();
                        rpt.SetDataSource(inPhieuNhap);

                        Frm_InPhieuNhap frm_PhieuNhap = new Frm_InPhieuNhap();
                        frm_PhieuNhap.rpt_PhieuNhap.ReportSource = rpt;
                        frm_PhieuNhap.Show();
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để in.");
                    }
                }
            }

            string macv = "cv01";
            chucvu cv = bUS_ChucVu.FindByID(macv);
            if(cv!=null)
            {
                bool kq = bUS_ChucVu.Delete(cv);
                if (kq == true)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else
                {
                    MessageBox.Show("Xoá thất bại");
                }    
            }
          
        }

    }

}
