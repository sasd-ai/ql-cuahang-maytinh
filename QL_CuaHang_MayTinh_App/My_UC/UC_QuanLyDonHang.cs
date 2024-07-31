using Guna.UI2.WinForms;
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
namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_QuanLyDonHang : UserControl
    {
        BUS_QuanLyDonHang dh=new BUS_QuanLyDonHang();
        int flag = 0;
        public UC_QuanLyDonHang()
        {
            InitializeComponent();
            
            this.Load += UC_QuanLyDonHang_Load;
            this.panel_YeuCauHuy.Click += Panel_YeuCauHuy_Click;
            this.panel_ChoXacNhan.Click += Panel_ChoXacNhan_Click;
            this.panel_DaHuy.Click += Panel_DaHuy_Click;
            this.panel_DaXacNhan.Click += Panel_DaXacNhan_Click;
            LoadSLDonHang();
        }

        private void Panel_DaXacNhan_Click(object sender, EventArgs e)
        {
            flag = 2;
            string TrangThai = "Đã xác nhận đơn";
            guna2DataGridView_DH.DataSource = dh.LoadDonHang(TrangThai);
            guna2DataGridView_DH.Columns["XacNhan"].Visible = false;
            //int sl = dh.LoadDonHang(TrangThai).Count;
            //label_SL_CXN.Text = sl.ToString();
        }

        private void Panel_DaHuy_Click(object sender, EventArgs e)
        {
            flag = 4;
            string TrangThai = "Đã xác nhận hủy";
            guna2DataGridView_DH.DataSource = dh.LoadDonHang(TrangThai);
            guna2DataGridView_DH.Columns["XacNhan"].Visible = false;
        
        }

        private void Panel_ChoXacNhan_Click(object sender, EventArgs e)
        {
            flag = 1;
            string TrangThai = "Chờ Xác Nhận Đơn";
            guna2DataGridView_DH.DataSource = dh.LoadDonHang(TrangThai);
        }

        private void Panel_YeuCauHuy_Click(object sender, EventArgs e)
        {
            flag = 3;
            string TrangThai = "Chờ Xác Nhận Hủy Đơn Hàng";
            guna2DataGridView_DH.DataSource = dh.LoadDonHang(TrangThai);
        }

        private void LoadSLDonHang()
        {
            label_SL_CXN.Text = dh.LoadDonHang("Chờ xác nhận").Count.ToString();
            label_SL_DXN.Text = dh.LoadDonHang("Đã xác nhận đơn").Count.ToString();
            label_SL_YCH.Text = dh.LoadDonHang("Chờ Xác Nhận Hủy Đơn Hàng").Count.ToString();
            label_SL_H.Text = dh.LoadDonHang("Đã xác nhận hủy").Count.ToString();
        }
        private void UC_QuanLyDonHang_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
           // LoadDonHangChoXacNhan();
;        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetupDataGridView()
        {

            // Cột Xóa
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn
            {
                Name = "XacNhan",
                HeaderText = "Xác Nhận",
               // Width = 130,
                Image = Properties.Resources.xacnhan // Sử dụng hình ảnh từ thư mục Resources
            };

            DataGridViewImageColumn imgColumn1 = new DataGridViewImageColumn
            {
                Name = "XemChiTiet",
                HeaderText = "Xem Chi Tiết",
               // Width = 170,
                Image = Properties.Resources.xemchitiet // Sử dụng hình ảnh từ thư mục Resources
            };
            guna2DataGridView_DH.Columns.Add(imgColumn);
            guna2DataGridView_DH.Columns.Add(imgColumn1);
            guna2DataGridView_DH.RowTemplate.Height = 50;
            
            guna2DataGridView_DH.CellContentClick += Guna2DataGridView1_CellContentClick;
           
        }

        private void Guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView_DH.Columns["XacNhan"].Index)
            {
                if (flag == 1)
                {
                    string maDH = guna2DataGridView_DH.Rows[e.RowIndex].Cells["MaDH"].Value.ToString();

                    dh.CapNhatTTXacNhanDonHang(maDH, "Đã xác nhận đơn");
                    LoadDonHang("Chờ Xác Nhận Đơn");
                    LoadSLDonHang();
                    return;
                }
               
                if (flag == 3)
                {
                    string maDH = guna2DataGridView_DH.Rows[e.RowIndex].Cells["MaDH"].Value.ToString();

                    dh.CapNhatTTXacNhanDonHang(maDH, "Đã xác nhận hủy");
                    LoadDonHang("Chờ Xác Nhận Hủy Đơn Hàng");
                    LoadSLDonHang();
                    return;
                }

            }
            if (e.ColumnIndex == guna2DataGridView_DH.Columns["XemChiTiet"].Index)
            {

                string maDH = guna2DataGridView_DH.Rows[e.RowIndex].Cells["MaDH"].Value.ToString();

               LoadChiTietDatHang(maDH);
            }
        }

        public void LoadDonHang(string trangthai)
        {
            //string TrangThai = "Chờ Xác Nhận Đơn";
            guna2DataGridView_DH.DataSource = dh.LoadDonHang(trangthai);
            // dataGridView_DH.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // dataGridView_DH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
        public void LoadChiTietDatHang(string NewMadh)
        {
            guna2DataGridView_CTDH.DataSource = dh.LoadChiTietDatHang(NewMadh);
            guna2DataGridView_CTDH.RowTemplate.Height = 50;
        }

       
       
    }
}
