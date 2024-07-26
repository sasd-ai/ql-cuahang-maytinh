
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
            bus_NhanVien=new BUS_NhanVien();
            //----------
            this.panel3.Paint += Panel3_Paint;
            this.FormClosing += Frm_Main_FormClosing;
            this.FormClosed += Frm_Main_FormClosed;
           
            //Xử lý giao diện
           this.panel_SanPham.Visible = false;
            this.panelBanHang.Visible = false;
            this.panel_QuanTri.Visible = false;
            this.panel_DoiTac.Visible=false;
        }

       

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            //Thông tin cửa hàng
            //Tên của hàng
            lblNameStore.Text = "CỦA HÀNG MÁY \n TÍNH TQĐ";

            //Tên User
            //LoadThongTinUser();

            // Tạo một instance của UC_Home
            UC_TrangChu homeControl = new UC_TrangChu();
            homeControl.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(homeControl);
            homeControl.BringToFront();
        }
        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.frm_Login.Close();
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_CLosing frm_CLosing=new Frm_CLosing();
        
            frm_CLosing.MdiParent= this;
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
        //private void LoadThongTinUser()
        //{
        //    nhanvien nv = bus_NhanVien.FindByEmai(email);

        //    if (nhanVien == null)
        //    {
        //        lblNameUser.Text = "Nhân viên";
        //        return;
        //    }
        //    lblNameUser.Text = nhanVien.TenNV;
        //}
        #region  Pannal
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

        #region Giao diện Button
        private void btnHome_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_SaleProduct_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_Product_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_QLKho_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_QlThuChi_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_BaoCao_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_NhanVien_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_QuanTri_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }

        private void btn_Setting_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
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
                    g.DrawLine(pen, 0, button.Height - borderWidth / 2, button.Width, button.Height - borderWidth / 2);
                }
            }
        }
        #endregion

        #region  Chuyển form chức năng
        private void btn_TrangChu_Click(object sender, EventArgs e)
        {
            // Tạo một instance của UC_Home
            UC_TrangChu homeControl = new UC_TrangChu();
            homeControl.Dock = DockStyle.Fill;
            pannel_Main.Controls.Clear();
            pannel_Main.Controls.Add(homeControl);
            homeControl.BringToFront();
        }

        private void btn_BanHang_Click(object sender, EventArgs e)
        {
            if(this.panelBanHang.Visible==true)
            {
                this.panelBanHang.Visible =false;
                return;
            }
            this.panelBanHang.Visible = true;
            this.panel_SanPham.Visible = false;
            this.panel_QuanTri.Visible = false;
           
            
        }

        #endregion

        #region Menu con
        //Đóng mở menu con của menu Nhân viên
        
        #endregion

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {

        }

        private void btn_QLKho_Click(object sender, EventArgs e)
        {

        }

        private void btn_SP_Click(object sender, EventArgs e)
        {
            if (this.panel_SanPham.Visible == true)
            {
                this.panel_SanPham.Visible=false;
                return ;
            }
            this.panel_SanPham.Visible = true;
            this.panel_QuanTri.Visible = false;
            this.panel_DoiTac.Visible = false;
            this.panelBanHang.Visible = false;
        }

        private void btn_QuanTri_Click(object sender, EventArgs e)
        {
            if (this.panel_QuanTri.Visible == true)
            {
                this.panel_QuanTri.Visible=false;
                return;
            }
            this.panel_QuanTri.Visible = true;
            this.panel_SanPham.Visible = false;
            this.panel_DoiTac.Visible = false;
            this.panelBanHang.Visible=false;
        }

        private void btn_DoiTac_Click(object sender, EventArgs e)
        {
            if(this.panel_DoiTac.Visible== true)
            {
                this.panel_DoiTac.Visible=false;
                return;
            }
            this.panel_DoiTac.Visible = true;
            this.panel_QuanTri.Visible = false;
            this.panel_SanPham.Visible = false;
            this.panelBanHang.Visible = false;
        }
    }
}
