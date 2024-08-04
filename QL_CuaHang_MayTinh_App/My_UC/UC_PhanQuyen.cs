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
using DTO;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public partial class UC_PhanQuyen : UserControl
    {
        private BUS_NhanVien bus_NhanVien = new BUS_NhanVien();
        private BUS_NguoiDung_ChucVu bus_NguoiDung_ChucVu = new BUS_NguoiDung_ChucVu();
        private BUS_ChucVu bus_ChucVu = new BUS_ChucVu();
        private BUS_NhanVien_ChucVu bus_NhanVien_ChucVu = new BUS_NhanVien_ChucVu();
        public UC_PhanQuyen()
        {
            InitializeComponent();
            this.Load += UC_PhanQuyen_Load;
            this.cbb_ChucVu.SelectedValueChanged += Cbb_ChucVu_SelectedValueChanged;
            this.dgv_User.SelectionChanged += Dgv_User_SelectionChanged;
            this.btn_Add.Click += Btn_Add_Click;
            this.btn_Delete.Click += Btn_Delete_Click;

            //Sự kiện click nhóm người dùng
            this.dgv_NhomNguoiDung.SelectionChanged += Dgv_NhomNguoiDung_SelectionChanged;
            this.btn_Save.Click += Btn_Save_Click;
        }

       





        // Load thông tin vào DataGridView
        private void UC_PhanQuyen_Load(object sender, EventArgs e)
        {
            //Load người dùng
            loadNguoiDung();
            //Load dgv nhóm người dùng 
            LoadData_NhomNguoiDung();

            //Load combobox 
            loadComBoBox_ChucVu();
        }


        //Xử lý cho bảng người dùng
        void loadNguoiDung()
        {
            // Tải dữ liệu vào DataGridView
            dgv_User.DataSource = bus_NhanVien.GetData();
            dgv_User.Font = new Font("Arial", 8, FontStyle.Regular);

            // Định dạng các cột trong DataGridView
            formatDataGridViewNguoiDung();
        }

        void formatDataGridViewNguoiDung()
        {
            // Thiết lập tiêu đề cột
            dgv_User.Columns["MaNV"].HeaderText = "Mã người dùng";
            dgv_User.Columns["TenNV"].HeaderText = "Tên người dùng";
            dgv_User.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgv_User.Columns["SDT"].HeaderText = "Số điện thoại";
            dgv_User.Columns["Email"].HeaderText = "Email";

            // Chuyển cột "HoatDong" thành checkbox
            if (dgv_User.Columns.Contains("HoatDong"))
            {
                // Tạo một cột checkbox mới
                DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                chkColumn.Name = "HoatDong";
                chkColumn.HeaderText = "Hoạt động";
                chkColumn.DataPropertyName = "HoatDong"; // Nếu bạn có BindingSource hoặc DataTable

                // Tìm chỉ số của cột cần thay thế
                int columnIndex = dgv_User.Columns["HoatDong"].Index;

                // Thay thế cột hiện tại bằng cột checkbox
                dgv_User.Columns.RemoveAt(columnIndex);
                dgv_User.Columns.Insert(columnIndex, chkColumn);
            }

            // Ẩn cột "MatKhau" nếu tồn tại
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
            dgv_Nhom.Columns["MaNV"].HeaderText = "Mã người dùng";
            dgv_Nhom.Columns["TenNV"].HeaderText = "Tên người dùng";
            dgv_Nhom.Columns["TenCV"].HeaderText = "Tên chức vụ";
            dgv_Nhom.Columns["GhiChu"].HeaderText = "Ghi chú";


            // Ẩn cột
          
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

        //Nghiệp vụ
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNguoiDung.Text.Trim();
            string maCV = cbb_ChucVu.SelectedValue.ToString().Trim();
              
            // Tìm kiếm
            var nv = bus_NhanVien_ChucVu.FindByID(maNV, maCV);
          
            if (nv == null)
            {
                // Thêm vào bảng
                bool kq = bus_NhanVien_ChucVu.Insert(maNV, maCV, "");
                if (kq)
                {
                    MessageBox.Show("Thêm thành công");
                    loadNguoiDung_ChucVu(cbb_ChucVu.SelectedValue.ToString());
                    return;
                }
                MessageBox.Show("Thêm thất bại");
                return;
            }
            MessageBox.Show("Người dùng đã thuộc nhóm này rồi");
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
        
            if (dgv_Nhom.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv_Nhom.SelectedRows[0];

                string maNV = row.Cells["MaNV"].Value.ToString();
                string maCV = row.Cells["MaCV"].Value.ToString();
                string tenNV= row.Cells["TenNV"].Value.ToString();
                string tenCV= row.Cells["TenCV"].Value.ToString();

                DialogResult dialog = MessageBox.Show("Bạn có muốn xoá người dùng: " + tenCV +" thuộc nhóm: "+tenCV,"Thông báo",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {  
                    try
                    {
                        //Thực hiện xoá
                        bool kq = bus_NhanVien_ChucVu.Delete(maNV, maCV);
                        if (kq)
                        {
                            MessageBox.Show("Xoá thành công");
                            loadNguoiDung_ChucVu(cbb_ChucVu.SelectedValue.ToString());
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xoá thất bại");
                        Console.WriteLine(ex.ToString());
                        return;
                    }
                   
                }
            }
           
        }
        //-------------------------------Phân quyền nhóm người dùng
        //Load danh sách nhóm người dùng
        void LoadData_NhomNguoiDung()
        {
            dgv_NhomNguoiDung.DataSource = null;
            dgv_NhomNguoiDung.DataSource = bus_ChucVu.GetData();
            formatDataGridView_NhomNguoiDung();
        }
        //Format datagridview nhóm người dùng
       void formatDataGridView_NhomNguoiDung()
        {
            dgv_NhomNguoiDung.Columns["MaCV"].HeaderText = "Mã chức vụ";
            dgv_NhomNguoiDung.Columns["TenCV"].HeaderText = "Tên chức vụ";
            dgv_NhomNguoiDung.Columns["GhiChu"].HeaderText = "Ghi chú";

        }
        //Load quyền khi click vào nhóm người dùng
        private void Dgv_NhomNguoiDung_SelectionChanged(object sender, EventArgs e)
        {
            if(dgv_NhomNguoiDung.SelectedRows.Count>0)
            {
                DataGridViewRow row = dgv_NhomNguoiDung.SelectedRows[0];
                string maNhom = row.Cells["MaCV"].Value.ToString();
                if (!String.IsNullOrEmpty(maNhom))
                {
                    loadDataBanQuyen(maNhom);
                }
                //Load data bảng quyền
            }    
        }

        //Load data bảng quyền theo mã nhóm
        void loadDataBanQuyen(string maCV)
        {
            dgv_ManHinh.DataSource=bus_ChucVu.GetPhanQuyenByMaCV(maCV);
        }
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (dgv_NhomNguoiDung.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv_NhomNguoiDung.SelectedRows[0];
                string maNhom = row.Cells["MaCV"].Value.ToString();
                if (!String.IsNullOrEmpty(maNhom))
                {
                  //Duyệt bảng màn hình để update hoặc thêm quyền
                    foreach(DataGridViewRow item in dgv_ManHinh.Rows)
                    {
                        string maManHinh = item.Cells["MaMH"].Value.ToString();
                        bool coQuyen = false;
                        if (item.Cells["CoQuyen"].Value != null)
                        {
                            coQuyen = Convert.ToBoolean(item.Cells["CoQuyen"].Value);
                        }
                        //Kiểm tra đã có quyền này trong bảng hay chưa
                        PhanQuyen pq = bus_ChucVu.FindByID(maNhom, maManHinh);
                        
                        if (pq != null)
                        {
                            //Tồn tại thì update
                            bus_ChucVu.UpdatePhanQuyen(maNhom,maNhom,coQuyen);
                        }
                        else
                        {
                            //Chưa tồn tại thì thêm mới nếu có quyền bằng true
                            if(coQuyen==true)
                            {
                                bus_ChucVu.InsertQuyen(maNhom, maManHinh);
                            }    
                        }
                    }
                    MessageBox.Show("Lưu thành công");
                }
                //Load data bảng quyền
            }
        }
    }
}
