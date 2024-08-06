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

namespace QL_CuaHang_MayTinh_App.GUI
{
    public partial class Frm_KhachHang : Form
    {
        public Frm_KhachHang()
        {
            InitializeComponent();
            UC_KhachHang homeControl = new UC_KhachHang();
            homeControl.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(homeControl);
            homeControl.BringToFront();
        }
    }
}
