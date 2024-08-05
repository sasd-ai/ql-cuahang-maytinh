using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Security.Cryptography;
namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_DoiMatKhau : UserControl
    {
        public string email;
        private BUS_NhanVien bUS_NhanVien=new BUS_NhanVien();
        public UC_DoiMatKhau()
        {
            InitializeComponent();
        }

        private void btn_DoiMK_Click(object sender, EventArgs e)
        {
            nhanvien nv=bUS_NhanVien.FindByEmail(email);
            string matKhau=txt_MatKhau.Text;
            string matKhauMoi = txt_MatKhauMoi.Text;
            string nhapLai=txt_NhapLai.Text;

            if (nv != null)
            {
                if(nv.MatKhau!=GetMd5Hash(matKhau))
                {
                    MessageBox.Show("Mật khẩu không đúng");
                    return;
                }
                if (matKhauMoi != nhapLai)
                {
                    MessageBox.Show("Xác nhận mật khẩu không đúng");
                    return;
                }    
                //Thực hiện đổi mật khẩu
                nv.MatKhau=matKhauMoi;
                bool kq = bUS_NhanVien.UpdateMK(nv.MaNV, matKhauMoi);

                if(kq==true)
                {
                    MessageBox.Show("Đổi thành công");
                }    
            }

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
