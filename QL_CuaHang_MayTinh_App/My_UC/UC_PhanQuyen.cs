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

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_PhanQuyen : UserControl
    {
        private BUS_NhanVien bus_NhanVien = new BUS_NhanVien();
        private BUS_NguoiDung_ChucVu bus_NguoiDung_ChucVu = new BUS_NguoiDung_ChucVu();
        private BUS_ChucVu bus_ChucVu = new BUS_ChucVu();
        public UC_PhanQuyen()
        {
            InitializeComponent();
            this.Load += UC_PhanQuyen_Load;
            this.cbb_ChucVu.SelectedValueChanged += Cbb_ChucVu_SelectedValueChanged;
            this.dgv_User.SelectionChanged += Dgv_User_SelectionChanged;
        }





        // Load thông tin vào DataGridView
        private void UC_PhanQuyen_Load(object sender, EventArgs e)
        {
            //Load người dùng
            loadNguoiDung();
            formatDataGridViewNguoiDung();

            //Load combobox 
            loadComBoBox_ChucVu();
        }


        //Xử lý cho bảng người dùng
        void loadNguoiDung()
        {
            dgv_User.DataSource = bus_NhanVien.GetData();
            dgv_User.Font = new Font("Arial", 8, FontStyle.Regular);
        }
        void formatDataGridViewNguoiDung()
        {
            dgv_User.Columns["MaNV"].HeaderText = "Mã người dùng";
            dgv_User.Columns["TenNV"].HeaderText = "Tên người dùng";
            dgv_User.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgv_User.Columns["SDT"].HeaderText = "Số điện thoại";
            dgv_User.Columns["Email"].HeaderText = "Email";
            dgv_User.Columns["HoatDong"].HeaderText = "Hoạt động";

            // Ẩn cột MatKhau
            if (dgv_User.Columns.Contains("MatKhau"))
            {
                dgv_User.Columns["MatKhau"].Visible = false;
            }
        }
        private void Dgv_User_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_User.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv_User.SelectedRows[0];

                txt_MaNguoiDung.Text = row.Cells["MaNV"].Value.ToString();
                txt_TenNguoiDung.Text = row.Cells["TenNV"].Value.ToString();
                txt_DiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txt_SDT.Text = row.Cells["SDT"].Value.ToString();
                txt_Email.Text = row.Cells["Email"].Value.ToString();

                // Xử lý giá trị cột HoatDong
                string hoatdong = "";
                var hoatDongValue = row.Cells["HoatDong"].Value;

                if (hoatDongValue != null && (bool)hoatDongValue)
                {
                    hoatdong = "Đang hoạt động";
                }
                else
                {
                    hoatdong = "Không hoạt động";
                }
                txt_HoatDong.Text = hoatdong;
            }
        }

        void loadNguoiDung_ChucVu(string maCV)
        {
            dgv_Nhom.DataSource = bus_NguoiDung_ChucVu.GetDataByMaCV(maCV);
            dgv_Nhom.Font = new Font("Arial", 8, FontStyle.Regular);
        }
        void formatDataGridViewNguoiDung_ChucVu()
        {
            dgv_Nhom.Columns["TenNV"].HeaderText = "Tên người dùng";
            dgv_Nhom.Columns["TenCV"].HeaderText = "Tên chức vụ";
            dgv_Nhom.Columns["GhiChu"].HeaderText = "Ghi chú";


            // Ẩn cột
            if (dgv_Nhom.Columns.Contains("MaNV"))
            {
                dgv_Nhom.Columns["MaNV"].Visible = false;
            }
            if (dgv_Nhom.Columns.Contains("MaCV"))
            {
                dgv_Nhom.Columns["MaCV"].Visible = false;
            }
        }
        private void loadComBoBox_ChucVu()
        {
            cbb_ChucVu.DataSource = bus_ChucVu.GetData();
            cbb_ChucVu.DisplayMember = "TenCV";
            cbb_ChucVu.ValueMember = "MaCV";
        }
        private void Cbb_ChucVu_SelectedValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có giá trị được chọn
            if (cbb_ChucVu.SelectedValue != null)
            {
                // Lấy giá trị đã chọn
                string macv = cbb_ChucVu.SelectedValue.ToString();
                loadNguoiDung_ChucVu(macv);
                //Load chức vụ với người dùng
                formatDataGridViewNguoiDung_ChucVu();
            }
        }
    }
}
