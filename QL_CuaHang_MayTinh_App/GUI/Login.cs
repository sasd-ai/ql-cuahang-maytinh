using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace QL_CuaHang_MayTinh_App.GUI
{
    public partial class Login : Form
    {
        private BUS_NhanVien bus_NhanVien;
        public Login()
        {
            InitializeComponent();
            bus_NhanVien = new BUS_NhanVien();
            this.btn_Login.Click += Btn_Login_Click;
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            string email = txt_Username.Text;
            string password = txt_Password.Text;
            bool checkLogin = bus_NhanVien.Login(email, password);
            if (!checkLogin)
            {
                MessageBox.Show("Thông tin không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Program.frmMain == null || Program.frmMain.IsDisposed)
            {
                // Khởi tạo form chính và hiển thị
                Program.frmMain = new Frm_Main(); // Thay 'FrmMain' bằng tên thực tế của form chính
                                                  // Hiển thị form chính và ẩn form đăng nhập
                Program.frmMain.email = txt_Username.Text;
            }
            List<ManHinh> listManHinh = bus_NhanVien.GetManHinhForUser(email);

            foreach (ManHinh item in listManHinh)
            {

                Console.WriteLine("Ma man hinh: " + item.MaManHinh);
            }
            if (listManHinh == null)
            {
                foreach (Control control in Program.frmMain.panel_Menu.Controls)
                {
                    // Kiểm tra nếu control là button và không nằm trong một panel con
                    if (control is Button button
                        && control.Parent == Program.frmMain.panel_Menu
                        && button.Name != "btn_Setting"
                        && button.Name != "btn_DangXuat")
                    {
                        button.Visible = false;
                    }
                }
            }
            else
            {
                foreach (Control control in Program.frmMain.panel_Menu.Controls)
                {
                    // Kiểm tra nếu control là button và không nằm trong một panel con
                    if (control is Button button
                        && control.Parent == Program.frmMain.panel_Menu
                        && button.Name != "btn_Setting"
                        && button.Name != "btn_DangXuat")
                    {
                        bool found = listManHinh.Any(item => item.MaManHinh == button.Name);
                        button.Visible = found;
                    }
                }
            }

           

            this.txt_Username.Text = "";
            this.txt_Password.Text = "";
            Program.frmMain.Show();
            this.Visible=false;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
