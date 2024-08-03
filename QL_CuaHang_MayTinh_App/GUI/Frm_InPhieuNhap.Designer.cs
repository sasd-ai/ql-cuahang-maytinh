namespace QL_CuaHang_MayTinh_App.GUI
{
    partial class Frm_InPhieuNhap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rpt_PhieuNhap = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // rpt_PhieuNhap
            // 
            this.rpt_PhieuNhap.ActiveViewIndex = -1;
            this.rpt_PhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpt_PhieuNhap.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpt_PhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpt_PhieuNhap.Location = new System.Drawing.Point(0, 0);
            this.rpt_PhieuNhap.Name = "rpt_PhieuNhap";
            this.rpt_PhieuNhap.Size = new System.Drawing.Size(1682, 803);
            this.rpt_PhieuNhap.TabIndex = 0;
            // 
            // Frm_InPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1682, 803);
            this.Controls.Add(this.rpt_PhieuNhap);
            this.Name = "Frm_InPhieuNhap";
            this.Text = "Frm_InPhieuNhap";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer rpt_PhieuNhap;
    }
}