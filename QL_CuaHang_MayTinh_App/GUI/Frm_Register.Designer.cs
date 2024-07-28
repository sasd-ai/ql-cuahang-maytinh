namespace QL_CuaHang_MayTinh_App.GUI
{
    partial class Frm_Register
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
            this.uC_Register1 = new CustomControl.UC_Register();
            this.SuspendLayout();
            // 
            // uC_Register1
            // 
            this.uC_Register1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Register1.Location = new System.Drawing.Point(0, 0);
            this.uC_Register1.Name = "uC_Register1";
            this.uC_Register1.Size = new System.Drawing.Size(623, 341);
            this.uC_Register1.TabIndex = 0;
            // 
            // Frm_Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 341);
            this.Controls.Add(this.uC_Register1);
            this.Name = "Frm_Register";
            this.Text = "Frm_Register";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.UC_Register uC_Register1;
    }
}