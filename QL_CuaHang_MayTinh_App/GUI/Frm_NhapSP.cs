using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BUS;
using DTO;
using Guna.UI2.WinForms;
namespace QL_CuaHang_MayTinh_App.GUI
{
    public partial class Frm_NhapSP : Form
    {

        private BUS_SanPham bUS_SanPham=new BUS_SanPham();
        public string maNCC;
        public List<sanpham> listSanPham;
        public BindingList<sanpham> sanPhamDaChon;
        List<loaisp> loaisps =new List<loaisp>();
        public Frm_NhapSP()
        {
            InitializeComponent();

            loaisps=bUS_SanPham.LoadLoaiSP();
            this.Load += Frm_NhapSP_Load;
            //Danh sách sản phảm
            this.dgv_List_SP.CellContentClick += Dgv_List_SP_CellContentClick;
            this.dgv_List_SP.CellValueChanged += Dgv_List_SP_CellValueChanged;
            //Danh sách sản phẩm đã chọn
            this.dgv_SP_DaChon.CellContentClick += Dgv_SP_DaChon_CellContentClick;
            this.cbo_LoaiSP.SelectedValueChanged += Cbo_LoaiSP_SelectedValueChanged;
        }

     

        private void Frm_NhapSP_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            //Format giao diện
            this.dgv_List_SP.RowTemplate.Height = 50;
            this.dgv_SP_DaChon.RowTemplate.Height = 50;
        
            sanPhamDaChon = new BindingList<sanpham>();
            //Load dữ liệu

            //Load sản phẩm
            loadDataSanPham(maNCC);
            //Danh sách sản phẩm
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "Chon";
            checkBoxColumn.HeaderText = "Chọn";
            checkBoxColumn.Width = 50;
            checkBoxColumn.ReadOnly = false;
            dgv_List_SP.Columns.Insert(0, checkBoxColumn);


            loadSanPhamDaChon();
            loadCBBLoaiSP();

            //Thay đổi
            formatDataGridView_SanPham();
            formatDataGridVieew_SPDaChon();
            this.cbo_LoaiSP.SelectedIndexChanged += Cbo_LoaiSP_SelectedIndexChanged;
        }

        private void Dgv_SP_DaChon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu cột được nhấn là cột button và hàng hợp lệ
                if (e.ColumnIndex == dgv_SP_DaChon.Columns["Action"].Index && e.RowIndex >= 0)
                {
                    // Lấy sản phẩm hiện tại
                    var selectedSanPham = dgv_SP_DaChon.Rows[e.RowIndex].DataBoundItem as sanpham;

                    if (selectedSanPham != null)
                    {
                        dgv_SP_DaChon.Rows.RemoveAt(e.RowIndex);

                        if (sanPhamDaChon.Contains(selectedSanPham))
                        {
                            sanPhamDaChon.Remove(selectedSanPham);
                        }
                        // Cập nhật lại danh sách sản phẩm
                        foreach (DataGridViewRow row in dgv_List_SP.Rows)
                        {
                            if (row.DataBoundItem == selectedSanPham)
                            {
                                row.Cells["Chon"].Value = false; 
                                break; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Cung cấp thông báo lỗi cụ thể hơn
                Console.WriteLine(ex.ToString());
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


      
        //Load combobox loại sản phẩm
        void loadCBBLoaiSP()
        {
           
            loaisps.Insert(0,new loaisp()
            {
                MaLoai="ALL",
                TenLoai="Tất cả"
            });
            cbo_LoaiSP.DataSource = loaisps;
            cbo_LoaiSP.DisplayMember = "TenLoai";
            cbo_LoaiSP.ValueMember = "MaLoai";
        }
        //Load sản phẩm theo loại
        private void Cbo_LoaiSP_SelectedValueChanged(object sender, EventArgs e)
        {
            string maLoai=cbo_LoaiSP.SelectedValue.ToString();
            if(maLoai =="'ALL")
            {
                dgv_List_SP.DataSource = listSanPham;
            }    
            else
            {
               dgv_List_SP.DataSource=listSanPham.Where(sp=>sp.MaLoai==maLoai && sp.MaNCC==maNCC).ToList();    
            }    
        }
        private void Cbo_LoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(cbo_LoaiSP.SelectedValue.ToString().Equals("ALL"))
           {
                loadDataSanPham(maNCC);
                return;
           }
          
            List<sanpham> sanphams = listSanPham.Where(sp => sp.MaLoai == cbo_LoaiSP.SelectedValue.ToString()).ToList();
            dgv_List_SP.DataSource = null;
            dgv_List_SP.DataSource = sanphams;
            formatDataGridView_SanPham();
        }

        //Add sản phẩm vào danh sách chọn
        private void Dgv_List_SP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_List_SP.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgv_List_SP.CommitEdit(DataGridViewDataErrorContexts.Commit);
                loadSanPhamDaChon();
            }

        }
        private void Dgv_List_SP_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu ô được thay đổi là ô checkbox
            if (e.ColumnIndex == dgv_List_SP.Columns["Chon"].Index && e.RowIndex >= 0)
            {
                // Lấy trạng thái của checkbox
                bool isChecked = Convert.ToBoolean(dgv_List_SP.Rows[e.RowIndex].Cells["Chon"].Value);

                // Kiểm tra nếu DataBoundItem không phải là null
                var selectedSanPham = dgv_List_SP.Rows[e.RowIndex].DataBoundItem as sanpham;
                if (selectedSanPham != null)
                {
                    if (isChecked)
                    {
                        // Thêm sản phẩm vào danh sách nếu không có sẵn
                        if (!sanPhamDaChon.Contains(selectedSanPham))
                        {
                            sanPhamDaChon.Add(selectedSanPham);
                           
                        }
                    }
                    else
                    {
                        // Loại bỏ sản phẩm khỏi danh sách nếu checkbox không được chọn
                        if (sanPhamDaChon.Contains(selectedSanPham))
                        {
                            dgv_SP_DaChon.DataSource = null;
                            sanPhamDaChon.Remove(selectedSanPham);
                        }
                    }
                    // Cập nhật dgv_SP_DaChon sau mỗi thay đổi
                    loadSanPhamDaChon();
                }
            }
        }

        void loadSanPhamDaChon()
        {
            dgv_SP_DaChon.DataSource = sanPhamDaChon ;
            
        }
       
        //Sự kiện

        //Load sản phẩm theo ncc
        void loadDataSanPham(string maNCC)
        {
           
            listSanPham =bUS_SanPham.FindByMaNCC(maNCC);
            dgv_List_SP.DataSource = listSanPham;
        }
        private void btn_Huy_Click(object sender, EventArgs e)
        {
            dgv_List_SP.DataSource = null;
            dgv_SP_DaChon.DataSource= null;
            listSanPham.Clear();
            this.Close();
        }

        private void btn_ThemVaoPN_Click(object sender, EventArgs e)
        {
            Program.frmMain.uc_SPNhap.UpdateDataSanPham(sanPhamDaChon);
            this.Close();
        }
        void formatDataGridVieew_SPDaChon()
        {
            // Tạo cột button mới
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Action";
            buttonColumn.HeaderText = "Hành động";
            buttonColumn.Text = "Xoá";
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.Width = 100;

            dgv_SP_DaChon.Columns.Add( buttonColumn);

            // Căn giữa tiêu đề các cột
            foreach (DataGridViewColumn column in dgv_List_SP.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Thiết lập tiêu đề cột
            dgv_SP_DaChon.Columns["MaSP"].HeaderText = "Mã sản phẩm";
            dgv_SP_DaChon.Columns["TenSP"].HeaderText = "Tên sản phẩm";

            dgv_SP_DaChon.Columns["MaLoai"].HeaderText = "Mã loại";
            dgv_SP_DaChon.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            // Ẩn cột 
            if (dgv_SP_DaChon.Columns.Contains("HinhAnh"))
            {
                dgv_SP_DaChon.Columns["HinhAnh"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("GiaDeXuat"))
            {
                dgv_SP_DaChon.Columns["GiaDeXuat"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("SoLuong"))
            {
                dgv_SP_DaChon.Columns["SoLuong"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("GiaBan"))
            {
                dgv_SP_DaChon.Columns["GiaBan"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("Tg_BaoHanh"))
            {
                dgv_SP_DaChon.Columns["Tg_BaoHanh"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("loaisp"))
            {
                dgv_SP_DaChon.Columns["loaisp"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("nhacungcap"))
            {
                dgv_SP_DaChon.Columns["nhacungcap"].Visible = false;
            }
            if (dgv_SP_DaChon.Columns.Contains("MoTa"))
            {
                dgv_SP_DaChon.Columns["MoTa"].Visible = false;
            }
            // Đặt thuộc tính Font của DataGridView
            dgv_SP_DaChon.Font = new Font("Arial", 12, FontStyle.Regular);
        }
        void formatDataGridView_SanPham()
        {
            // Căn giữa tiêu đề các cột
            foreach (DataGridViewColumn column in dgv_List_SP.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Thiết lập tiêu đề cột
            dgv_List_SP.Columns["MaSP"].HeaderText = "Mã sản phẩm";
            dgv_List_SP.Columns["TenSP"].HeaderText = "Tên sản phẩm";

            dgv_List_SP.Columns["MaLoai"].HeaderText = "Mã loại";
            dgv_List_SP.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            // Ẩn cột 
            if (dgv_List_SP.Columns.Contains("HinhAnh"))
            {
                dgv_List_SP.Columns["HinhAnh"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("GiaDeXuat"))
            {
                dgv_List_SP.Columns["GiaDeXuat"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("SoLuong"))
            {
                dgv_List_SP.Columns["SoLuong"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("GiaBan"))
            {
                dgv_List_SP.Columns["GiaBan"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("Tg_BaoHanh"))
            {
                dgv_List_SP.Columns["Tg_BaoHanh"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("loaisp"))
            {
                dgv_List_SP.Columns["loaisp"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("nhacungcap"))
            {
                dgv_List_SP.Columns["nhacungcap"].Visible = false;
            }
            if (dgv_List_SP.Columns.Contains("MoTa"))
            {
                dgv_List_SP.Columns["MoTa"].Visible = false;
            }
            // Đặt thuộc tính Font của DataGridView
            dgv_List_SP.Font = new Font("Arial", 12, FontStyle.Regular);
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            string tenSP= txt_TenSP.Text;
            if(String.IsNullOrEmpty(tenSP))
            {
                dgv_List_SP.DataSource = null;
                dgv_List_SP.DataSource = bUS_SanPham.FindByMaNCC(maNCC);
               
                formatDataGridView_SanPham();
            }
            else
            {
                List<sanpham> sanphams=listSanPham.Where(sp=>sp.TenSP.Contains(tenSP)).ToList();
                dgv_List_SP.DataSource = null;
                dgv_List_SP.DataSource = sanphams;
                formatDataGridView_SanPham();
            }
        }
    }
}
