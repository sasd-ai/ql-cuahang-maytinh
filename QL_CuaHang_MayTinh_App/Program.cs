using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL_CuaHang_MayTinh_App.GUI;
namespace QL_CuaHang_MayTinh_App
{
    internal static class Program
    {
        public static Login frm_Login;
        public static Frm_Main frmMain;
        public static Frm_NhapSP frm_NhapSP;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
     
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm_Login = new Login();
            Application.Run(frm_Login);
        }
    }
}
