
using QL_CuaHang_MayTinh_App.My_UC;
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
using QL_CuaHang_MayTinh_App.My_Form;
namespace QL_CuaHang_MayTinh_App.GUI
{
    public partial class Frm_Main : Form
    {
        public string email;
        private BUS_NhanVien bus_NhanVien;
        public Frm_Main()
        {
            InitializeComponent();

            //Khai báo 
            bus_NhanVien = new BUS_NhanVien();
            //----------
            this.panel3.Paint += Panel3_Paint;
            this.FormClosing += Frm_Main_FormClosing;
            this.FormClosed += Frm_Main_FormClosed;

            //Xử lý giao diện
            this.panel_SanPham.Visible = false;
            this.panel_BanHang.Visible = false;
            this.panel_QuanTri.Visible = false;
            this.panel_Setting.Visible = false;

            //Mở dropdown menu
            this.btn_BanHangMenu.Click += Btn_BanHangMenu_Click;
            this.btn_SanPhamMenu.Click += Btn_SanPhamMenu_Click;
            this.btn_QuanTriMenu.Click += Btn_QuanTriMenu_Click;
            this.btn_Setting.Click += Btn_Setting_Click;
            this.btn_DangXuat.Click += Btn_DangXuat_Click;


            //Chuyển chức năng
            this.btn_BanHang.Click += Btn_BanHang_Click;
            this.btn_SanPham.Click += Btn_SanPham_Click;
            this.btn_QLKho.Click += Btn_QLKho_Click;
            this.btn_QtriPhanQuyen.Click += Btn_QtriPhanQuyen_Click;
        }

       


        //Chuyển chức năng
        #region Chuyển chức năng

        //Mở chắc năng bán hàng
        private void Btn_BanHang_Click(object sender, EventArgs e)
        {
            
            UC_BanHang uC_BanHang = new UC_BanHang();
            uC_BanHang.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uC_BanHang);
            uC_BanHang.BringToFront();
            nhanvien nv = bus_NhanVien.FindByEmail(email);
            if (nv != null)
            {
                uC_BanHang.MaNV = nv.MaNV;
            }
          
        }
        //Mở chắc năng quản lý sản phẩm
        private void Btn_SanPham_Click(object sender, EventArgs e)
        {
            // Tạo một instance của UC_Home
            UC_SanPham uC_SanPham = new UC_SanPham();
            uC_SanPham.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uC_SanPham);
            uC_SanPham.BringToFront();
        }

        //Quản lý kho, nhập hàng,...
        private void Btn_QLKho_Click(object sender, EventArgs e)
        {
            // Tạo một instance của UC_Home
            UC_QLKho uc_QLKho = new UC_QLKho();
            uc_QLKho.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uc_QLKho);
            uc_QLKho.BringToFront();
            uc_QLKho.btn_NhapHang.Click += Btn_NhapHang_Click;
        }
        private void Btn_NhapHang_Click(object sender, EventArgs e)
        {
            UC_NhapHang uc_Nhap = new UC_NhapHang();
            uc_Nhap.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uc_Nhap);
            pannel_Main.Controls.Add(uc_Nhap);
            uc_Nhap.BringToFront();
        }
        //QUản lý chức vụ của nhân viên và phân quyền
        private void Btn_QtriPhanQuyen_Click(object sender, EventArgs e)
        {
            UC_PhanQuyen uC_PhanQuyen = new UC_PhanQuyen();
            uC_PhanQuyen.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uC_PhanQuyen);
            pannel_Main.Controls.Add(uC_PhanQuyen);
            uC_PhanQuyen.BringToFront();
        }
        #endregion


        //Đăng xuất
        private void Btn_DangXuat_Click(object sender, EventArgs e)
        {
            if (Program.frm_Login == null || Program.frm_Login.IsDisposed)
            {
                Program.frm_Login = new Login();
            }
            this.Close();
            Program.frm_Login.Visible = true;
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            //Thông tin cửa hàng
            //Tên của hàng
            lblNameStore.Text = "CỦA HÀNG MÁY \n TÍNH TQĐ";

            //Tên User
            LoadThongTinUser();

            // Tạo một instance của UC_Home
            UC_TrangChu homeControl = new UC_TrangChu();
            homeControl.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(homeControl);
            homeControl.BringToFront();
        }



        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.frm_Login.Visible = true;
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_CLosing frm_CLosing = new Frm_CLosing();

            frm_CLosing.MdiParent = this;
            frm_CLosing.Show();
            using (Frm_CLosing frm_Closing = new Frm_CLosing())
            {
                DialogResult result = frm_Closing.ShowDialog();
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Hủy sự kiện đóng form nếu người dùng chọn No
                }
            }
        }

        #region Menu con
        //Mở menu quản lý bán hàng
        private void Btn_BanHangMenu_Click(object sender, EventArgs e)
        {

            if (this.panel_BanHang.Visible == true)
            {
                this.panel_BanHang.Visible = false;
                return;
            }
            this.panel_BanHang.Visible = true;
        }
        //Mở menu quản lý sản phẩm
        private void Btn_SanPhamMenu_Click(object sender, EventArgs e)
        {

            if (this.panel_SanPham.Visible == true)
            {
                this.panel_SanPham.Visible = false;
                return;
            }
            this.panel_SanPham.Visible = true;
        }
        //Mở menu quản lý nhân viên, phân quyền
        private void Btn_QuanTriMenu_Click(object sender, EventArgs e)
        {
            if (this.panel_QuanTri.Visible == true)
            {
                this.panel_QuanTri.Visible = false;
                return;
            }
            this.panel_QuanTri.Visible = true;
        }
        //Mở menu cài đặc 
        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            if (this.panel_Setting.Visible == true)
            {
                this.panel_Setting.Visible = false;
                return;
            }
            this.panel_Setting.Visible = true;
        }
        #endregion
        private void LoadThongTinUser()
        {
            nhanvien nv = bus_NhanVien.FindByEmail(email);
            if (nv == null)
            {
                lblNameUser.Text = "Nhân viên";
                return;
            }
            lblNameUser.Text = nv.TenNV;
        }
        #region  Panel
        private void Panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                // Định nghĩa màu và độ dày của đường viền
                Color borderColor = Color.White; // Bạn có thể thay đổi màu ở đây
                int borderWidth = 2; // Độ dày của đường viền

                // Lấy Graphics từ sự kiện Paint
                Graphics g = e.Graphics;

                // Tạo Pen để vẽ đường viền
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    // Vẽ đường viền ở phía dưới của panel
                    g.DrawLine(pen, 0, panel.Height - borderWidth / 2, panel.Width, panel.Height - borderWidth / 2);

                    // Vẽ đường viền ở phía bên phải của panel
                    g.DrawLine(pen, panel.Width - borderWidth / 2, 0, panel.Width - borderWidth / 2, panel.Height);
                }
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                // Định nghĩa màu và độ dày của đường viền
                Color borderColor = Color.White; // Bạn có thể thay đổi màu ở đây
                int borderWidth = 2; // Độ dày của đường viền

                // Lấy Graphics từ sự kiện Paint
                Graphics g = e.Graphics;

                // Tạo Pen để vẽ đường viền
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    // Vẽ đường viền ở phía dưới của panel
                    g.DrawLine(pen, 0, panel.Height - borderWidth / 2, panel.Width, panel.Height - borderWidth / 2);
                }
            }
        }

        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
             labelTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btn_DSBanOff_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_DonHangOnline_Click(object sender, EventArgs e)
        {
            UC_QuanLyDonHang uC_QuanLyDonHang = new UC_QuanLyDonHang();
            uC_QuanLyDonHang.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uC_QuanLyDonHang);
            uC_QuanLyDonHang.BringToFront();
        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            UC_ThongKeDoanhThu uC_ThongKeDoanhThu = new UC_ThongKeDoanhThu();
            uC_ThongKeDoanhThu.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uC_ThongKeDoanhThu);
            uC_ThongKeDoanhThu.BringToFront();
        }

        private void btn_TrangChu_Click(object sender, EventArgs e)
        {
            UC_TrangChu uC_TrangChu = new UC_TrangChu();
            uC_TrangChu.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(uC_TrangChu);
            uC_TrangChu.BringToFront();
        }
    }
}
