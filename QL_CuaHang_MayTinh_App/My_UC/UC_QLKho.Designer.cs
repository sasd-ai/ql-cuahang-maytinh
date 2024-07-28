namespace QL_CuaHang_MayTinh_App.My_UC
{
    partial class UC_QLKho
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btn_NhapHang = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(159, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "QL Kho";
            // 
            // btn_NhapHang
            // 
            this.btn_NhapHang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_NhapHang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_NhapHang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_NhapHang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_NhapHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_NhapHang.ForeColor = System.Drawing.Color.White;
            this.btn_NhapHang.Location = new System.Drawing.Point(510, 278);
            this.btn_NhapHang.Name = "btn_NhapHang";
            this.btn_NhapHang.Size = new System.Drawing.Size(180, 45);
            this.btn_NhapHang.TabIndex = 2;
            this.btn_NhapHang.Text = "Nhập hàng";
            // 
            // UC_QLKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_NhapHang);
            this.Controls.Add(this.label1);
            this.Name = "UC_QLKho";
            this.Size = new System.Drawing.Size(1200, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public Guna.UI2.WinForms.Guna2Button btn_NhapHang;
    }
}
