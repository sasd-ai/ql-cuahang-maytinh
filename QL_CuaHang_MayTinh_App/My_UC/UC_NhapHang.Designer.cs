namespace QL_CuaHang_MayTinh_App.My_UC
{
    partial class UC_NhapHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.CheckBox checkBoxTimTheoNgayHoaDon;
        private System.Windows.Forms.ComboBox comboBoxTrangThai;
        private System.Windows.Forms.TextBox textBoxTuKhoa;
        private System.Windows.Forms.Button buttonThemMoi;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.checkBoxTimTheoNgayHoaDon = new System.Windows.Forms.CheckBox();
            this.comboBoxTrangThai = new System.Windows.Forms.ComboBox();
            this.textBoxTuKhoa = new System.Windows.Forms.TextBox();
            this.buttonThemMoi = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1800, 80);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F);
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập hàng";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.AllowDrop = true;
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Location = new System.Drawing.Point(6, 26);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(340, 30);
            this.dateTimePicker.TabIndex = 1;
            // 
            // checkBoxTimTheoNgayHoaDon
            // 
            this.checkBoxTimTheoNgayHoaDon.AutoSize = true;
            this.checkBoxTimTheoNgayHoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTimTheoNgayHoaDon.Location = new System.Drawing.Point(427, 170);
            this.checkBoxTimTheoNgayHoaDon.Name = "checkBoxTimTheoNgayHoaDon";
            this.checkBoxTimTheoNgayHoaDon.Size = new System.Drawing.Size(234, 29);
            this.checkBoxTimTheoNgayHoaDon.TabIndex = 2;
            this.checkBoxTimTheoNgayHoaDon.Text = "Tìm theo ngày hóa đơn";
            this.checkBoxTimTheoNgayHoaDon.UseVisualStyleBackColor = true;
            // 
            // comboBoxTrangThai
            // 
            this.comboBoxTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTrangThai.FormattingEnabled = true;
            this.comboBoxTrangThai.Location = new System.Drawing.Point(730, 168);
            this.comboBoxTrangThai.Name = "comboBoxTrangThai";
            this.comboBoxTrangThai.Size = new System.Drawing.Size(150, 33);
            this.comboBoxTrangThai.TabIndex = 3;
            this.comboBoxTrangThai.Text = "Tất cả";
            // 
            // textBoxTuKhoa
            // 
            this.textBoxTuKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTuKhoa.Location = new System.Drawing.Point(949, 169);
            this.textBoxTuKhoa.Name = "textBoxTuKhoa";
            this.textBoxTuKhoa.Size = new System.Drawing.Size(399, 30);
            this.textBoxTuKhoa.TabIndex = 4;
            this.textBoxTuKhoa.Text = "Tìm kiếm theo mã phiếu / đối tác / nhân viên";
            // 
            // buttonThemMoi
            // 
            this.buttonThemMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(173)))), ((int)(((byte)(78)))));
            this.buttonThemMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonThemMoi.ForeColor = System.Drawing.Color.White;
            this.buttonThemMoi.Image = global::QL_CuaHang_MayTinh_App.Properties.Resources.add1;
            this.buttonThemMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonThemMoi.Location = new System.Drawing.Point(1198, 101);
            this.buttonThemMoi.Name = "buttonThemMoi";
            this.buttonThemMoi.Size = new System.Drawing.Size(150, 43);
            this.buttonThemMoi.TabIndex = 6;
            this.buttonThemMoi.Text = "Thêm mới";
            this.buttonThemMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonThemMoi.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(31, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 63);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thời gian";
            // 
            // UC_NhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonThemMoi);
            this.Controls.Add(this.textBoxTuKhoa);
            this.Controls.Add(this.comboBoxTrangThai);
            this.Controls.Add(this.checkBoxTimTheoNgayHoaDon);
            this.Controls.Add(this.panel1);
            this.Name = "UC_NhapHang";
            this.Size = new System.Drawing.Size(1800, 850);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
