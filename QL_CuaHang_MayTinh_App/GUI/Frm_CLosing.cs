using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CuaHang_MayTinh_App.My_Form
{
    public partial class Frm_CLosing : Form
    {
        public Frm_CLosing()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Thiết lập tiêu đề cho form
            this.Text = "Xác nhận đóng form";

            // Thiết lập kích thước và bố cục cho form
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;

            // Thêm Label hiển thị thông báo
            Label lblMessage = new Label();
            lblMessage.Text = "Bạn có chắc chắn muốn đóng form không?";
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(50, 20);
            this.Controls.Add(lblMessage);

            // Thêm Button Yes
            Button btnYes = new Button();
            btnYes.Text = "Yes";
            btnYes.Location = new Point(50, 70);
            btnYes.Click += BtnYes_Click;
            this.Controls.Add(btnYes);

            // Thêm Button No
            Button btnNo = new Button();
            btnNo.Text = "No";
            btnNo.Location = new Point(150, 70);
            btnNo.Click += BtnNo_Click;
            this.Controls.Add(btnNo);
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }

}
