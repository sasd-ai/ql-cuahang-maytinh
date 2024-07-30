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
        public UC_QuanLyDonHang()
        {
            InitializeComponent();
            
            this.Load += UC_QuanLyDonHang_Load;

        }

        private void UC_QuanLyDonHang_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDonHangChoXacNhan();
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
                HeaderText = "Hành Động",
                Width = 80,
                Image = Properties.Resources.xacnhan // Sử dụng hình ảnh từ thư mục Resources
            };

            DataGridViewImageColumn imgColumn1 = new DataGridViewImageColumn
            {
                Name = "XemChiTiet",
                HeaderText = "Hành Động",
                Width = 80,
                Image = Properties.Resources.xemchitiet // Sử dụng hình ảnh từ thư mục Resources
            };
            guna2DataGridView_DatHang.Columns.Add(imgColumn);
            guna2DataGridView_DatHang.Columns.Add(imgColumn1);
            guna2DataGridView_DatHang.RowTemplate.Height = 50;
            guna2DataGridView_CTDH.RowTemplate.Height = 50;
            guna2DataGridView_DatHang.CellContentClick += Guna2DataGridView1_CellContentClick;
           
        }

        private void Guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView_DatHang.Columns["XacNhan"].Index)
            {
               
                string maDH = guna2DataGridView_DatHang.Rows[e.RowIndex].Cells["MaDH"].Value.ToString();
            
                dh.CapNhatTTXacNhanDonHang(maDH, "Đã xác nhận"); 
                LoadDonHangChoXacNhan(); 
            }
            if (e.ColumnIndex == guna2DataGridView_DatHang.Columns["XemChiTiet"].Index)
            {

                string maDH = guna2DataGridView_DatHang.Rows[e.RowIndex].Cells["MaDH"].Value.ToString();

               LoadChiTietDatHang(maDH);
            }
        }

        public void LoadDonHangChoXacNhan()
        {
            guna2DataGridView_DatHang.DataSource=dh.LoadDonHangChoXacNhan();
            guna2DataGridView_DatHang.AutoGenerateColumns = false;
        }
        public void LoadChiTietDatHang(string NewMadh)
        {
            guna2DataGridView_CTDH.DataSource = dh.LoadChiTietDatHang(NewMadh);
            guna2DataGridView_CTDH.AutoGenerateColumns = false;
        }

    }
}
