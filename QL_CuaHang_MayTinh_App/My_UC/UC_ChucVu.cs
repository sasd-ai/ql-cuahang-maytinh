using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CustomControl;
using DTO;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_ChucVu : UserControl
    {
        BUS_ChucVu bus_cv = new BUS_ChucVu();
        chucvu cv = new chucvu();
        private string HanhDong = "";
        public UC_ChucVu()
        {
            InitializeComponent();
            LoadData();
            this.datagridview_cv.SelectionChanged += Datagridview_cv_SelectionChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btn_delete.Click += Btn_delete_Click;
            this.btn_update.Click += Btn_update_Click;
            this.btn_save.Click += Btn_save_Click;
            this.btn_Huy.Click += Btn_Huy_Click;
            this.btn_Search_TenCV.Click += Btn_Search_TenCV_Click;
        }

        private void Btn_Search_TenCV_Click(object sender, EventArgs e)
        {
            string text_chucvu = txt_Search_tenCV.Text;
            //bus_cv.FindByName(text_chucvu);
            datagridview_cv.DataSource = bus_cv.FindByName(text_chucvu);
        }
        public bool KT_ThongTin_CV()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_CV.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox và không phải là txt_maKH
                if (control is TextBox textBox && textBox != txt_maCV)
                {
                    // Kiểm tra nếu TextBox trống
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Hiển thị thông báo và trả về false nếu có TextBox trống
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin nha!!");
                        return false;
                    }
                }
            }
            // Trả về true nếu tất cả các TextBox (trừ txt_maKH) đều không trống
            return true;
        }

        public void XoaText_ThongTin_CV()
        {
            // Duyệt qua tất cả các điều khiển trong panel_TTKhachHang
            foreach (Control control in panel_TT_CV.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox
                if (control is TextBox textBox)
                {
                    // Kiểm tra nếu TextBox không trống
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Xóa nội dung của TextBox
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            XoaText_ThongTin_CV();
            btn_save.Enabled = false;
            btn_Huy.Enabled = false;
            btnThem.Enabled = true;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            HanhDong = "";
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            string MaCV_random = new Random().Next(100, 9999).ToString();
            string maCV = txt_maCV.Text;
            string tenCV = txt_tenCV.Text;
            string ghichu = txt_GhiChuCV.Text;

            switch (HanhDong)
            {
                case "Them":
                    if(!KT_ThongTin_CV())
                    {
                        return;
                    }    
                    bus_cv.ThemChucVu(MaCV_random, tenCV, ghichu);
                    MessageBox.Show("Bạn đã thêm chức vụ thành công rồi nha!!!");
                    LoadData();
                    break;

                case "Sua":
                    if (string.IsNullOrEmpty(maCV))
                    {
                        MessageBox.Show("Vui lòng chọn mã chức vụ để sửa.");
                        break;
                    }
                        bus_cv.SuaChucVu(maCV, tenCV, ghichu);
                    MessageBox.Show("Sửa chức vụ thành công.");
                    LoadData();

                    break;

                case "Xoa":
                    if (string.IsNullOrEmpty(maCV))
                    {
                        MessageBox.Show("Vui lòng chọn mã chức vụ để xóa.");
                        break;
                    }

                    if (MessageBox.Show("Bạn có chắc muốn xóa chức vụ này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        chucvu cv = bus_cv.FindByID(maCV);
                        if (cv != null)
                        {
                            bool kq = bus_cv.XoaChucVu(maCV);
                            if (kq == true)
                            {
                                MessageBox.Show("Xóa chức vụ thành công!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá chức vụ này!");
                            }
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Không có hành động nào được chọn!");
                    break;
            }
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            txt_maCV.Enabled = false;

            HanhDong = "Sua";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btnThem.Enabled = false;
            btn_delete.Enabled = false;

        }



        private void Btn_delete_Click(object sender, EventArgs e)
        {
            txt_maCV.Enabled = false;
            if (txt_maCV != null)
            {
                HanhDong = "Xoa";
                btn_save.Enabled = true;
                btn_Huy.Enabled = true;
                btnThem.Enabled = false;
                btn_update.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui Lòng nhập dữ liệu đầy đủ !!");
                return;
            }
        }

        //public void XoaText()
        //{
        //    txt_maCV.Text = "";
        //    txt_tenCV.Text = "";
        //    txt_GhiChuCV.Text = "";
        //}

        private void BtnThem_Click(object sender, EventArgs e)
        {
            XoaText_ThongTin_CV();
            HanhDong = "Them";
            btn_save.Enabled = true;
            btn_Huy.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            txt_maCV.Enabled = false;
        }

        private void Datagridview_cv_SelectionChanged(object sender, EventArgs e)
        {
            if (datagridview_cv.SelectedRows.Count > 0)
            {
                string macv = datagridview_cv.CurrentRow.Cells[0].Value.ToString();
                string tencv = datagridview_cv.CurrentRow.Cells[1].Value.ToString();
                string ghichu = datagridview_cv.CurrentRow.Cells[2].Value.ToString();
                txt_maCV.Text = macv;
                txt_tenCV.Text = tencv;
                txt_GhiChuCV.Text = ghichu;
            }

        }

        public void LoadData()
        {
            string text_chucvu = txt_Search_tenCV.Text;
            bus_cv.FindByName(text_chucvu);
            datagridview_cv.DataSource = bus_cv.GetData();

        }


    }
}
