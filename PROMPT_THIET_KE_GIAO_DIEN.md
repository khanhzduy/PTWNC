# MASTER PROMPT: THIẾT KẾ GIAO DIỆN WEBSITE BÁN SÁCH "BOOK ONLINE"
### (LẤY CẢM HƯNG TỪ THƯƠNG MẠI ĐIỆN TỬ FAHASA & TUÂN THỦ BAREM ĐIỂM 10/10 BÀI TẬP LỚN ASP.NET CORE MVC)

---

## BỐI CẢNH & VAI TRÒ CỦA AGENT (ROLE DEFINITION)
> **Bạn là một Chuyên gia UI/UX Designer kiêm Senior ASP.NET Core MVC Fullstack Developer.**
> Nhiệm vụ của bạn là thiết kế và triển khai toàn bộ hệ thống giao diện cho website bán sách trực tuyến **"Book Online"** (Bài tập lớn môn Phát triển ứng dụng Web nâng cao - ĐHCN Việt - Hưng).
> 
> **Thách thức cốt lõi:**
> Bản thiết kế gốc trong đề bài ([Layout2.jpg](BaiTapLon/Layout2.jpg)) có bố cục chuẩn năm 2012 với các phân vùng chức năng bắt buộc. Bạn cần **hiện đại hóa toàn diện giao diện này**, lấy cảm hứng từ trang thương mại điện tử nhà sách hàng đầu **Fahasa.com** (từ ảnh chụp thực tế), nhưng **phải giữ nguyên 100% đầy đủ tất cả các phân vùng và nghiệp vụ đã quy định trong file [CHI_DAN_AGENT_LAM_BTL.md](CHI_DAN_AGENT_LAM_BTL.md) để đạt điểm tuyệt đối 10.0/10.0**.

---

## 1. NGUYÊN TẮC THIẾT KẾ LẤY CẢM HƯNG TỪ FAHASA (DESIGN SYSTEM)

### 1.1. Bảng màu & Phong cách thị giác (Color Palette & Styling)
* **Màu sắc chủ đạo (Primary & Accent):**
  - **Màu đỏ cam thương mại (Fahasa Red/Orange - `#C92127` / `#F33828`):** Dùng cho Header nổi bật, các nhãn giảm giá (Discount Badge), giá tiền sản phẩm, nút Đặt hàng, biểu tượng Flash Sale / Sách hot.
  - **Màu xanh nhận diện nhà trường (School Theme - `#1B7A3E` hoặc `#0d6efd`):** Phối hợp hài hòa ở các điểm nhấn phụ hoặc thanh menu phụ để thể hiện thương hiệu Nhà sách ĐHCN Việt - Hưng.
  - **Màu nền trang (Background):** Sử dụng tông xám nhẹ `#F0F2F5` để làm nổi bật các khối nội dung dạng Card trắng `#FFFFFF`.
* **Thẻ sản phẩm (Product Card) chuẩn E-Commerce:**
  - Nền trắng, bo góc mềm mại `border-radius: 8px`, đổ bóng nhẹ `box-shadow: 0 1px 4px rgba(0,0,0,0.08)`.
  - Hiệu ứng tương tác: Hover nâng thẻ (`transform: translateY(-4px)`, bóng đổ sâu hơn).
  - Tên sách hiển thị tối đa 2 dòng (dùng CSS `-webkit-line-clamp: 2`), in đậm vừa phải, đổi màu khi hover.
  - Đơn giá hiển thị màu đỏ đậm nổi bật, font chữ lớn kèm hậu tố `đ` hoặc `VNĐ`.
  - Nút **"Chọn mua"** thiết kế phong cách button bo góc hiện đại, tích hợp icon giỏ hàng hoặc nút hình ảnh `nut_chonmua.gif`.
* **Typography & Icon:**
  - Font chữ hiện đại: `Roboto`, `Open Sans` hoặc `system-ui`.
  - Iconography: Sử dụng icon vector tinh tế (Bootstrap Icons / FontAwesome) kết hợp với các icon có sẵn trong `BaiTapLon/Images/` (`Giohang.gif`, `Sotien.gif`, `muitendo.gif`, `tittle_sachmoi.gif`).

---

## 2. ĐẶC TẢ CHI TIẾT BỐ CỤC MASTER LAYOUT (`_Layout.cshtml`)
*(Kết hợp giữa bố cục 3 cột theo đề bài và phong cách hiện đại của Fahasa)*

```
+-----------------------------------------------------------------------------------------------+
| TOP BAR & HEADER: Logo Book Online | Thanh tìm kiếm Fahasa Style | Hotline | Tài khoản | Giỏ  |
+-----------------------------------------------------------------------------------------------+
| MENU NGANG (Red Bar): [Trang chủ] [Sách mới] [Giới thiệu] [Chủ đề hot] [Đăng ký] [Đăng nhập]  |
+-------------------+-------------------------------------------------------+-------------------+
| CỘT TRÁI (20%)    | CỘT GIỮA - NỘI DUNG CHÍNH (58%)                       | CỘT PHẢI (22%)    |
| (Sidebar Left)    | (Main Content Area)                                   | (Sidebar Right)   |
|                   |                                                       |                   |
| 1. Card "CHỦ ĐỀ": | 1. Section "SÁCH MỚI CẬP NHẬT" (Phong cách Fahasa Hot)| 1. WIDGET GIỎ HÀNG|
|    - Tiếng Việt(0)|    - Header nghệ thuật (tittle_sachmoi.gif / badge)   |    (Box vàng VIP):|
|    - Tin học (32) |    - Danh sách 5 đầu sách mới nhất có icon mũi tên đỏ |    - Icon + Tổng: |
|    - Kinh tế (5)  |    - Link xem chi tiết sách                           |      257.000 vnđ  |
|    - ...          |                                                       |    - Giỏ hàng:    |
|    (Menu bo góc,  | 2. QUẢNG CÁO 1 (Flash Banners):                       |      8 cuốn       |
|     hover xanh/đỏ)|    - Hiển thị ngẫu nhiên 3 banner/ảnh QC mỗi lần F5   |    - Nút Xem giỏ  |
|                   |                                                       |                   |
| 2. CAM KẾT SHOP:  | 3. TIÊU ĐỀ: "DANH MỤC SÁCH: [TÊN CHỦ ĐỀ]"             | 2. QUẢNG CÁO 2:   |
|    - 100% Sách thật| 4. LƯỚI CARD VIEW SÁCH THEO CHỦ ĐỀ (DataList Style):  |    Danh mục banner|
|    - Đổi trả 7 ngày|    - Bìa sách (Upload/*.jpg)                          |    đối tác còn hạn|
|    - Giao nhanh   |    - Tên sách, Tác giả, Nhà xuất bản, Đơn giá         |    từ DB QLBanSach|
|                   |    - Nút [Chọn mua]                                   |    (Ben, CBV,...) |
+-------------------+-------------------------------------------------------+-------------------+
| FOOTER (Đa cột hiện đại):                                                                     |
| - Cột 1: Thông tin: @2012 - Nhà sách ĐHCN Việt - Hưng | 16 Xuân Khanh, Sơn Tây, Hà Nội        |
| - Cột 2: Hỗ trợ khách hàng, chính sách đổi trả, phương thức vận chuyển                       |
| - Cột 3: Phương thức thanh toán (Tiền mặt, VNPay, Momo) & Kết nối mạng xã hội                 |
+-----------------------------------------------------------------------------------------------+
```

---

## 3. THIẾT KẾ CHI TIẾT TỪNG PHÂN VÙNG BẮT BUỘC

### 3.1. Header & Thanh Menu Ngang
- **Header:**
  - Bên trái: Logo "Book Online" kế thừa từ `BaiTapLon/Images/` hoặc thiết kế logo chữ kết hợp icon sách mở.
  - Ở giữa: Thanh tìm kiếm sản phẩm lớn viền đỏ cam, có nút tìm kiếm icon kính lúp (chuẩn Fahasa).
  - Bên phải: Khối "Đăng nhập / Tài khoản" và nút tắt xem nhanh giỏ hàng.
- **Thanh Menu ngang:** Nền đỏ cam rực rỡ hoặc xanh đậm, chữ trắng in hoa thanh thoát: `TRANG CHỦ`, `SÁCH MỚI`, `GIỚI THIỆU`, `HƯỚNG DẪN MUA HÀNG`, `ĐĂNG KÝ`, `ĐĂNG NHẬP`.

### 3.2. Cột trái (Sidebar 1) - Danh mục Chủ đề Sách
- Hiển thị danh sách các thể loại từ bảng `CHU_DE` kèm số đếm sách trong ngoặc: `Công nghệ thông tin (32)`, `Kinh tế (5)`...
- Thiết kế dạng danh mục dọc hiện đại (Vertical Nav), hiệu ứng hover chuyển màu nền nhẹ, icon sách nhỏ phía trước.
- Khi click vào chủ đề nào thì kích hoạt trạng thái `active` và tải lại sách thuộc chủ đề đó ở cột giữa.

### 3.3. Cột phải (Sidebar 2) - Box Giỏ hàng & Quảng cáo 2
- **Box Thông tin Giỏ hàng (Bắt buộc theo BTL):**
  - Giữ nét nhận diện đặc trưng là tông vàng nhạt (`#FFF9E6` / `#FFF3CD`), viền nét đứt sang trọng màu cam đất.
  - Phân dòng rõ ràng:
    - Dòng 1: Icon cọc tiền (`Sotien.gif`) + **Tổng tiền** (`...,... vnđ`).
    - Dòng 2: Icon giỏ hàng (`Giohang.gif`) + **Số lượng** (`Giỏ hàng: X cuốn`).
  - Nút bấm nhỏ "Xem chi tiết giỏ hàng" dẫn ngay tới trang `/GioHang`.
- **Quảng cáo 2 (Bắt buộc theo BTL):**
  - Khối chứa danh sách các banner đối tác từ bảng `QUANG_CAO` (chỉ lấy quảng cáo có `Ngay_het_han >= DateTime.Now`).
  - Các logo như Ben, LinkTam, CBV... được bo góc `6px`, có border nhẹ, hiệu ứng hover phóng to nhẹ 1.03x.

### 3.4. Vùng Nội dung chính (Cột giữa)
- **Khu vực Sách Mới (Top 5):**
  - Khung viền tinh tế, header có ảnh nghệ thuật `tittle_sachmoi.gif` hoặc tiêu đề "Sách mới phát hành".
  - Danh sách 5 cuốn sách mới nhất hiển thị kèm icon mũi tên đỏ (`muitendo.gif`), tiêu đề sách là link dẫn trực tiếp sang trang chi tiết cuốn sách.
- **Khu vực Quảng cáo 1:**
  - Một banner hoặc dải 3 ảnh quảng cáo được chọn ngẫu nhiên (`Random`) mỗi lần tải trang, căn giữa trang, liên kết tới link của nhà quảng cáo.
- **Khu vực Lưới Sách theo Chủ đề (DataList tương đương Grid CSS):**
  - Header khối: "Danh mục sách: [Tên chủ đề được chọn]".
  - Lưới sản phẩm responsive (2 đến 3 cột):
    - Ảnh bìa sách (`/images/Upload/{HinhMinhHoa}.jpg`) có tỉ lệ chuẩn, bo góc, bóng đổ.
    - Tên sách (font đậm, màu xanh đen).
    - Tên Nhà xuất bản & Tác giả (chữ nhỏ màu xám).
    - Giá bán (màu đỏ đậm Fahasa).
    - Nút chọn mua: Nút ảnh `nut_chonmua.gif` hoặc Button CSS đỏ gradient có icon giỏ hàng.

---

## 4. THIẾT KẾ CÁC TRANG CHỨC NĂNG CON ĐỒNG BỘ UI/UX

### 4.1. Trang Chi tiết Sách (`Views/Sach/Details.cshtml`)
- Chia 2 cột lớn:
  - Cột trái: Ảnh bìa sách cỡ lớn, khung viền chất lượng cao.
  - Cột phải: Tên sách (font lớn `24px`), Nhà xuất bản, Danh sách các tác giả (kết nối bảng `ThamGia` & `TacGia`), Ngày cập nhật, Số lượt xem, Đơn giá định dạng nổi bật.
  - Bộ chọn số lượng mua (+ / -) và nút **"THÊM VÀO GIỎ HÀNG"** lớn màu đỏ.
- Khối bên dưới: Tab hoặc Card "Mô tả nội dung cuốn sách" hiển thị chi tiết trường `MoTa` từ CSDL.

### 4.2. Trang Quản lý Giỏ hàng (`Views/GioHang/Index.cshtml`)
- Bảng GridView hiện đại (Bootstrap Table Striped):
  - Các cột: STT | Bìa sách | Tên cuốn sách | Đơn giá | Số lượng (input +/-) | Thành tiền | Thao tác (Icon thùng rác/Xóa).
  - Hàng tổng kết: Nổi bật **Tổng số lượng cuốn** và **Tổng tiền thanh toán**.
- Nút bấm hành động:
  - Nút "Tiếp tục mua sách" (secondary button).
  - Nút **"TIẾN HÀNH ĐẶT HÀNG"** (nút lớn màu đỏ phong cách Fahasa).

### 4.3. Trang Đặt hàng & Thanh toán (`Views/GioHang/DatHang.cshtml`)
- Hiển thị tóm tắt đơn hàng và form thông tin nhận hàng của khách (tự động điền nếu đã đăng nhập từ `KHACH_HANG`).
- Nút "Xác nhận Đặt hàng" -> Gọi Action lưu dữ liệu vào `DON_DAT_HANG` & `CT_DAT_HANG` và hiển thị trang cảm ơn.

### 4.4. Trang Đăng nhập & Đăng ký (`Views/Account/Login.cshtml`, `Register.cshtml`)
- Thiết kế dạng Form Card đặt giữa màn hình hoặc dạng Modal đẹp mắt.
- Input bo góc, icon tài khoản/mật khẩu, validation lỗi màu đỏ (`asp-validation-for`).

### 4.5. Trang Thử nghiệm LINQ Query (`Views/Sach/QueryDemo.cshtml`)
- Giữ thanh dropdown chọn 7 câu truy vấn (theo slide TH03), kết quả hiển thị bảng dữ liệu sạch đẹp có phân trang hoặc kẻ viền thanh lịch.

---

## 5. YÊU CẦU KỸ THUẬT TRIỂN KHAI CHO AGENT
1. **Sử dụng ViewComponents** để cô lập mã nguồn:
   - `ChuDeMenuViewComponent`: Tự động nạp danh mục và đếm sách cho Sidebar trái.
   - `CartSummaryViewComponent`: Tự động đọc Session giỏ hàng cho Sidebar phải.
   - `QuangCaoViewComponent`: Tự động truy vấn quảng cáo còn hạn từ DB.
2. **File CSS riêng:** Tạo file `wwwroot/css/fahasa-theme.css` chứa toàn bộ rules về màu sắc, grid card, hover animations để không ghi đè mất bootstrap mặc định.
3. **Responsive Design:** Giao diện co giãn mượt mà từ màn hình máy tính (1200px) xuống tablet và mobile (3 cột sẽ tự động xếp chồng trên di động).

---

## 6. TIÊU CHÍ NGHIỆM THU (ACCEPTANCE CRITERIA)
- [ ] Giao diện có màu sắc rực rỡ, hiện đại, bắt mắt như Fahasa nhưng giữ trọn vẹn thông tin nhận diện ĐHCN Việt - Hưng.
- [ ] Có đầy đủ tất cả các vùng của đề bài: Banner, Menu ngang, Sidebar Trái (Chủ đề đếm sách), Sidebar Phải (Hộp vàng giỏ hàng + Quảng cáo DB), Cột Giữa (Sách mới top 5, Quảng cáo ngẫu nhiên, Lưới sách), Footer địa chỉ trường.
- [ ] Mọi link sản phẩm, nút chọn mua, cập nhật số lượng giỏ hàng, đặt hàng lưu DB đều hoạt động trơn tru.

