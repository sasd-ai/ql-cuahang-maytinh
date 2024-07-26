# Thành viên
<h4>Tên nhóm:  Hệ thống quản lý của hàng máy tính </h4>

  
| STT | Họ tên | Chức vụ  |
|----------------|--------------------|--------------------|
|  1  |  Nguyễn Phương Điền  |   Nhóm trưởng  |
|  2  |  Trần Ngọc Thanh  |   Thành viên  |
|  3  |  Đặng Thị Diễm Quỳnh  |   Thành viên  |
-----------------------------------------------
### Sử dụng 
 - .Net Framework version 4.8
 - Visual studio 2022
-----------------------------------------------

### Chi tiết
<img src="https://i.imgur.com/FehXExF.jpg">

-----------------------------------------------
#  Hệ thống Quản lý Cửa hàng Máy tính

## Mô tả dự án
Dự án là một hệ thống ứng dụng đa nền tảng cho phép người dùng mua hàng trực tuyến. Hệ thống này sẽ mở ra chức năng mua hàng online qua giao diện web, đồng thời cung cấp một ứng dụng tiện lợi để quản lý sản phẩm yêu thích và đơn hàng.

## Các thành phần chính của hệ thống

### Website cho Khách Hàng
- **Xem Sản Phẩm:** Khách hàng có thể duyệt qua các sản phẩm, xem chi tiết và đánh giá sản phẩm.
- **Giỏ Hàng:** Khách hàng có thể thêm sản phẩm vào giỏ hàng và quản lý giỏ hàng của mình.
- **Mua Hàng:** Hỗ trợ các phương thức thanh toán khác nhau và cung cấp xác nhận đơn hàng qua email hoặc thông báo trên website.

### Ứng Dụng cho Chủ Cửa Hàng
- **Quản Lý Nhập Hàng:** Chủ cửa hàng có thể nhập thông tin về sản phẩm mới, cập nhật số lượng hàng tồn kho.
- **Quản Lý Bán Hàng:** Theo dõi các giao dịch bán hàng, cập nhật trạng thái đơn hàng và quản lý doanh thu.
- **Quản Lý Danh Mục:** Tạo và quản lý các danh mục sản phẩm, cập nhật thông tin sản phẩm.

## Lợi ích của hệ thống
- **Tiện Ích và Linh Hoạt:** Người dùng có thể mua sắm và quản lý sản phẩm một cách dễ dàng và tiện lợi.
- **Tiết Kiệm Không Gian:** Sử dụng không gian sách điện tử giúp tiết kiệm không gian lưu trữ.
- **Quản Lý Hiệu Quả:** Ứng dụng quản lý giúp chủ cửa hàng dễ dàng theo dõi và quản lý hoạt động kinh doanh của mình.

## Công nghệ sử dụng
- **Front-end:** HTML, CSS, JavaScript, React (hoặc Angular/Vue).
- **Back-end:** ASP.NET/C#.
- **Database:** SQL Server (hoặc MySQL, MongoDB).


## Kiến trúc hệ thống
1. **Web Server:** Xử lý các yêu cầu từ client (người dùng) và gửi phản hồi.
2. **Application Server:** Xử lý logic nghiệp vụ và giao tiếp với cơ sở dữ liệu.
3. **Database Server:** Lưu trữ dữ liệu sản phẩm, người dùng, đơn hàng, và các thông tin liên quan khác.

## Kế hoạch phát triển
1. **Phân tích yêu cầu:** Thu thập và phân tích yêu cầu từ khách hàng và các bên liên quan.
2. **Thiết kế hệ thống:** Lên kế hoạch thiết kế kiến trúc hệ thống và giao diện người dùng.
3. **Phát triển:** Tiến hành lập trình và phát triển các tính năng của hệ thống.
4. **Kiểm thử:** Kiểm thử hệ thống để đảm bảo không có lỗi và hoạt động mượt mà.
5. **Triển khai:** Triển khai hệ thống lên môi trường thực tế.
6. **Bảo trì và nâng cấp:** Theo dõi và bảo trì hệ thống, cập nhật và nâng cấp khi cần thiết.

Hệ thống quản lý cửa hàng máy tính này không chỉ cung cấp giải pháp mua hàng trực tuyến tiện lợi mà còn hỗ trợ chủ cửa hàng trong việc quản lý và tối ưu hóa hoạt động kinh doanh của mình.

### Yêu cầu 
<p>AI: Gợi ý sản phẩm cùng loại</p>
<p>Web: MVC ASP, LinQ</p>
<p>Ngôn ngữ: Javascript, C# </p>
<p>Thư viện: Bootstrap</p>

<p>Nghiệp vụ</p>

| STT | Nghiệp vụ | Phân công  |
|----------------|--------------------|--------------------|
|  1  |  Tìm kiếm sản phẩm |   ...  |



<p>App: Winform </p>
<p>Ngôn ngữ: C# </p>

| STT | Nghiệp vụ | Phân công  |
|----------------|--------------------|--------------------|
|  1  |  Đăng nhập/đăng xuất |   ...  |
|  2  | Phân quyền  |  ... |



## Chức năng
#### WEDSITE
<p>Tìm kiếm và hiện thi thông tin sản phẩm:</p>
<ul>
  <li>Tìm kiếm sản phẩm theo tên,từ khóa, v.v.</li>
  <li>Hiển thị thông tin chi tiết của mỗi sản phẩm, bao gồm tên, mô tả, đánh giá, giá bán, v.v.</li>
</ul>
<p>
  Giỏ hàng, mua sản phẩm và thanh toán:
</p>
<ul>
  <li>Thêm sản phẩm vào giỏ hàng </li>
  <li>Xem lại giỏ hàng trước khi tiến hành thanh toán</li>
   <li>Hỗ trợ nhiều phương thức thanh toán an toàn như ví điện tử, v.v.</li>
	<li>Hiển thị thông tin chi tiết của giỏ hàng và tổng số tiền.</li>
</ul>
<p>
 Quản lý thông tin tài khoản 
</p>
<ul>
<li>
    Đăng ký và đăng nhập tài khoản cho người dùng.
  </li>
  <li>Quản lý thông tin cá nhân, mật khẩu</li>
  <li>Hiển thị danh sách các sản phẩm mà người dùng đã thêm vào giỏ hàng hoặc đã mua.</li>
<li>Đơn hàng của người dùng</li>
<li>Hiện thị sản phảm yêu thích</li>
<li>Thông tin tài khoản</li>
 <li>Đổi mật khẩuh</li>
<li>Thông tin khách hàng</li>
<li>Người dùng có thể đánh giá và viết nhận xét về các sản phẩm đã mua.</li>
</ul>

#### APP

<p>
Quản lý sản phẩm
</p>
<ul>
<li>Thêm và cập nhật sản phẩm</li>
<li>Tra cứu thông tin sản phẩm</li>
</ul>
<p>
Quản lý loại sản phẩm và nhà sản xuất
</p>
<ul>
<li>Thêm và cập nhật </li>
</ul>
<p>
Quản lý khách hàng
</p>
<ul>
<li>Thêm và cập nhật </li>
<li>Tra cứu thông tin khách hàng</li>
</ul>
<p>
Quản lý tin tức
</p>
<ul>
<li>Thêm và cập nhật </li>
<li>Duyệt tin tức </li>
</ul>
<p>
Quản lý nhân viên và chức vụ
</p>
<ul>
<li>Thêm và cập nhật </li>
<li> Phân quyền nhân viên tùy theo chức vụ</li>
<li> Tra cứu thông tin của nhân viên </li></li>
</ul>
<p>
Báo cáo
</p>
<ul>
<li>Sản phẩm được mau nhiều nhất trong tháng </li>
<li> Sản phẩm đuợc Thêm vào yêu thích nhiều nhất</li></li>
</ul>
<p>
Thống kê</p>
<ul>
<li>Doanh thu </li>
<li> Đơn hàng theo ngày theo tháng, theo năm</li></li>
</ul>

