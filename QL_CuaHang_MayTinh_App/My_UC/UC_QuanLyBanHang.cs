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
namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_QuanLyBanHang : UserControl
    {
        BUS_QuanLyBanHang qlbh=new BUS_QuanLyBanHang();
        public UC_QuanLyBanHang()
        {
            InitializeComponent();
            LoadDonBanHang();
            this.dgv_banhang.SelectionChanged += Dgv_banhang_SelectionChanged;
            this.dgv_banhang.RowTemplate.Height = 50;
            this.dgv_ctbh.RowTemplate.Height = 50;

        }

        private void Dgv_banhang_SelectionChanged(object sender, EventArgs e)
        {
            if(dgv_banhang.SelectedRows.Count >0)
            {
                dgv_ctbh.DataSource = qlbh.LoadChiTietBanHang(dgv_banhang.SelectedRows[0].Cells["MaBanHang"].Value.ToString());
            }    
          
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
        public void LoadDonBanHang()
        {
            dgv_banhang.DataSource=null;
            dgv_banhang.DataSource = qlbh.LoadDonBanHang();

        }

        private void btn_Search_SDT_Click(object sender, EventArgs e)
        {

            string sdt=txt_sdt.Text;
            if (sdt.Length > 0)
            {
               // dgv_banhang.DataSource = null;
                dgv_banhang.DataSource = qlbh.TimKiemDonHang_SDT(sdt);
                return;
            }
            LoadDonBanHang();

        }

        private void btn_Search_MaBH_Click(object sender, EventArgs e)
        {
            string mabh = txt_Mabh.Text;
            if (mabh.Length > 0)
            {
              //  dgv_banhang.DataSource = null;
                dgv_banhang.DataSource = qlbh.TimKiemDonHang_MaBH(mabh);
                return;
            }
            LoadDonBanHang();
        }
    }
}
