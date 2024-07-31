namespace QL_CuaHang_MayTinh_App.GUI
{
    partial class Frm_NhapSP
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxNhomSP;
        private System.Windows.Forms.TextBox textBoxTimKiem;
        private System.Windows.Forms.Button buttonTimKiem;
        private System.Windows.Forms.Button buttonHienThiTatCa;
        private System.Windows.Forms.DataGridView dataGridViewKetQua;
        private System.Windows.Forms.Button buttonHuy;
        private System.Windows.Forms.Button buttonThemVaoDon;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxNhomSP = new System.Windows.Forms.ComboBox();
            this.textBoxTimKiem = new System.Windows.Forms.TextBox();
            this.buttonTimKiem = new System.Windows.Forms.Button();
            this.buttonHienThiTatCa = new System.Windows.Forms.Button();
            this.dataGridViewKetQua = new System.Windows.Forms.DataGridView();
            this.buttonHuy = new System.Windows.Forms.Button();
            this.buttonThemVaoDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm từ kho hàng hóa";
            // 
            // comboBoxNhomSP
            // 
            this.comboBoxNhomSP.FormattingEnabled = true;
            this.comboBoxNhomSP.Location = new System.Drawing.Point(25, 70);
            this.comboBoxNhomSP.Name = "comboBoxNhomSP";
            this.comboBoxNhomSP.Size = new System.Drawing.Size(200, 24);
            this.comboBoxNhomSP.TabIndex = 1;
            this.comboBoxNhomSP.Text = "--Chọn nhóm--";
            // 
            // textBoxTimKiem
            // 
            this.textBoxTimKiem.Location = new System.Drawing.Point(250, 70);
            this.textBoxTimKiem.Name = "textBoxTimKiem";
            this.textBoxTimKiem.Size = new System.Drawing.Size(300, 22);
            this.textBoxTimKiem.TabIndex = 2;
            this.textBoxTimKiem.Text = "Nhập tên để tìm kiếm";
            // 
            // buttonTimKiem
            // 
            this.buttonTimKiem.Location = new System.Drawing.Point(580, 68);
            this.buttonTimKiem.Name = "buttonTimKiem";
            this.buttonTimKiem.Size = new System.Drawing.Size(150, 30);
            this.buttonTimKiem.TabIndex = 3;
            this.buttonTimKiem.Text = "Tìm kiếm theo bộ lọc";
            this.buttonTimKiem.UseVisualStyleBackColor = true;
            // 
            // buttonHienThiTatCa
            // 
            this.buttonHienThiTatCa.Location = new System.Drawing.Point(750, 68);
            this.buttonHienThiTatCa.Name = "buttonHienThiTatCa";
            this.buttonHienThiTatCa.Size = new System.Drawing.Size(150, 30);
            this.buttonHienThiTatCa.TabIndex = 4;
            this.buttonHienThiTatCa.Text = "Hiển thị tất cả";
            this.buttonHienThiTatCa.UseVisualStyleBackColor = true;
            // 
            // dataGridViewKetQua
            // 
            this.dataGridViewKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKetQua.Location = new System.Drawing.Point(25, 120);
            this.dataGridViewKetQua.Name = "dataGridViewKetQua";
            this.dataGridViewKetQua.RowHeadersWidth = 51;
            this.dataGridViewKetQua.RowTemplate.Height = 24;
            this.dataGridViewKetQua.Size = new System.Drawing.Size(1150, 400);
            this.dataGridViewKetQua.TabIndex = 5;
            // 
            // buttonHuy
            // 
            this.buttonHuy.Location = new System.Drawing.Point(950, 540);
            this.buttonHuy.Name = "buttonHuy";
            this.buttonHuy.Size = new System.Drawing.Size(100, 30);
            this.buttonHuy.TabIndex = 6;
            this.buttonHuy.Text = "Hủy";
            this.buttonHuy.UseVisualStyleBackColor = true;
            // 
            // buttonThemVaoDon
            // 
            this.buttonThemVaoDon.Location = new System.Drawing.Point(1075, 540);
            this.buttonThemVaoDon.Name = "buttonThemVaoDon";
            this.buttonThemVaoDon.Size = new System.Drawing.Size(100, 30);
            this.buttonThemVaoDon.TabIndex = 7;
            this.buttonThemVaoDon.Text = "Thêm vào đơn";
            this.buttonThemVaoDon.UseVisualStyleBackColor = true;
            // 
            // Frm_NhapSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.ControlBox = false;
            this.Controls.Add(this.buttonThemVaoDon);
            this.Controls.Add(this.buttonHuy);
            this.Controls.Add(this.dataGridViewKetQua);
            this.Controls.Add(this.buttonHienThiTatCa);
            this.Controls.Add(this.buttonTimKiem);
            this.Controls.Add(this.textBoxTimKiem);
            this.Controls.Add(this.comboBoxNhomSP);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_NhapSP";
            this.Text = "Frm_NhapSP";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKetQua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
