using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
namespace QL_CuaHang_MayTinh_App.GUI
{
    public partial class Frm_NhapSP : Form
    {
        private BUS_SanPham bUS_SanPham=new BUS_SanPham();
        public string maNCC;
        public List<sanpham> listSanPham;
        public Frm_NhapSP()
        {
            InitializeComponent();
            this.Load += Frm_NhapSP_Load;
        }

        private void Frm_NhapSP_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            //Load dữ liệu
            loadCBBLoaiSP();
            loadCBB_NCC();
            string maNCC = cbb_NhaCungCap.SelectedValue.ToString();
            if (String.IsNullOrEmpty(maNCC))
            {
                listSanPham = bUS_SanPham.FindByMaNCC(maNCC);
                loadSanPham(maNCC);
            }
            //Thay đổi
            this.cbo_LoaiSP.SelectedValueChanged += Cbo_LoaiSP_SelectedValueChanged;
            this.cbb_NhaCungCap.SelectedValueChanged += Cbb_NhaCungCap_SelectedValueChanged;
        }

        


        //Load combobox loại sản phẩm
        void loadCBBLoaiSP()
        {
            List<loaisp> loaisps= bUS_SanPham.LoadLoaiSP();
            loaisps.Insert(0,new loaisp()
            {
                MaLoai="ALL",
                TenLoai="Tất cả"
            });
            cbo_LoaiSP.DataSource = loaisps;
            cbo_LoaiSP.DisplayMember = "TenLoai";
            cbo_LoaiSP.ValueMember = "MaLoai";
        }
        //Load combobox nhà cung cấp
        void loadCBB_NCC()
        {
          
            cbb_NhaCungCap.DataSource = bUS_SanPham.LoadNCC();
            cbb_NhaCungCap.DisplayMember = "TenNCC";
            cbb_NhaCungCap.ValueMember = "MaNCC";
        }

        void loadSanPham(string maNCC)
        {
            dgv_DSSanPham.DataSource=bUS_SanPham.FindByMaNCC(maNCC);
            formatDataGridView_SanPham();
        }
        void formatDataGridView_SanPham()
        {
            // Thiết lập tiêu đề cột
            dgv_DSSanPham.Columns["MaSP"].HeaderText = "Mã sản phẩm";
            dgv_DSSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgv_DSSanPham.Columns["MoTa"].HeaderText = "Mô tả";
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
        }
        //Sự kiện


        private void btn_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Load theo loại
        private void Cbo_LoaiSP_SelectedValueChanged(object sender, EventArgs e)
        {
            if(listSanPham==null)
            {
                return;
            }  
            List<sanpham> list = new List<sanpham>();
            string maLoai = cbo_LoaiSP.SelectedValue.ToString();
            if (maLoai.Equals("ALL"))
            {
                list = bUS_SanPham.FindByMaNCC(maNCC);
            }
            else
            {
                list = bUS_SanPham.FindByMaLoai(maLoai, listSanPham);
            }
            dgv_DSSanPham.DataSource = list;
            formatDataGridView_SanPham();
        }
        //Load theo nhà cung cấp
        private void Cbb_NhaCungCap_SelectedValueChanged(object sender, EventArgs e)
        {
            maNCC = cbb_NhaCungCap.SelectedValue.ToString();
            if (String.IsNullOrEmpty(maNCC))
                return;
            //Thực hiện lọc dữ liệu
            listSanPham = bUS_SanPham.FindByMaNCC(maNCC);
            dgv_DSSanPham.DataSource = listSanPham;
            formatDataGridView_SanPham();
        }
    }
}
