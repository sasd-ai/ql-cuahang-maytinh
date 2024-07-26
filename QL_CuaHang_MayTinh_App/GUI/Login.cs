﻿using System;
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
                Program.frmMain.email = email;
            }
            List<ManHinh> listManHinh = bus_NhanVien.GetManHinhForUser(email);
            foreach (ManHinh item in listManHinh)
            {
                Console.WriteLine(item.ToString());
            }
            if (listManHinh != null)
            {
                foreach (Control control in Program.frmMain.panelLeft.Controls)
                {
                    // Kiểm tra nếu control là button và không nằm trong một panel con
                    if (control is Button button && control.Parent == Program.frmMain.panelLeft)
                    {
                        bool found = listManHinh.Any(item => item.MaManHinh == button.Name);
                        button.Visible = found;
                    }
                }
            }

            Program.frmMain.Show();
            this.Hide(); // Thay 'this.Visible = false;' bằng 'this.Hide();'
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
