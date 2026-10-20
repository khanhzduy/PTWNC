# BỘ CÔNG THƯƠNG
## TRƯỜNG ĐẠI HỌC CÔNG NGHIỆP VIỆT - HUNG
### KHOA CÔNG NGHỆ THÔNG TIN - BỘ MÔN CÔNG NGHỆ PHẦN MỀM

---

# BÁO CÁO BÀI TẬP LỚN MÔN HỌC
## PHÁT TRIỂN ỨNG DỤNG WEB NÂNG CAO (PTWNC)

### ĐỀ TÀI:
# XÂY DỰNG WEBSITE NHÀ SÁCH TRỰC TUYẾN (BOOKONLINE)
### THEO QUY CHUẨN ĐÁNH GIÁ 8 STEPS (THANG ĐIỂM 10.0 / 10.0 ĐIỂM)
### KÈM HƯỚNG DẪN ĐỊNH VỊ VỊ TRÍ MÃ NGUỒN TRONG VISUAL STUDIO (SOLUTION EXPLORER)

* **Giảng viên hướng dẫn:** Bộ môn Công nghệ Phần mềm
* **Sinh viên thực hiện:** Nhóm sinh viên thực hiện đề tài BTL
* **Chuyên ngành:** Công nghệ Thông tin – Khóa học 2024 - 2026
* **Nền tảng công nghệ:** ASP.NET Core MVC (.NET 10), C# 13, Entity Framework Core, SQL Server 2022
* **Tài liệu đề bài gốc:** `BaiTapLon/BaiTapLon.docx`
* **Tệp Word nộp kèm:** `BAO_CAO_BAI_TAP_LON_PTWNC.docx`
* **Trang web đang chạy tại:** `http://localhost:5245`

*Sơn Tây - Hà Nội, Năm 2026*

---

## MỤC LỤC BÁO CÁO

1. [TỔNG QUAN HỆ THỐNG & CÂY THƯ MỤC DỰ ÁN TRONG VISUAL STUDIO](#1-tổng-quan-hệ-thống--cây-thư-mục-dự-án-trong-visual-studio)
2. [BẢNG TỔNG HỢP 8 STEPS & VỊ TRÍ FILE TRONG VISUAL STUDIO](#2-bảng-tổng-hợp-8-steps--vị-trí-file-trong-visual-studio)
3. [CHI TIẾT 8 STEPS: YÊU CẦU, VỊ TRÍ TRONG VISUAL, MÃ NGUỒN & CÁCH TEST:](#3-chi-tiết-8-steps-yêu-cầu-vị-trí-trong-visual-mã-nguồn--cách-test)
   - [STEP 1 (1.0 đ): Giao diện đẹp, hợp lý & Responsive](#step-1-giao-diện-đẹp-hợp-lý--responsive-thang-điểm-10-đ)
   - [STEP 2 (1.0 đ): Tạo MasterPage 10 vùng cấu trúc chuẩn đề bài](#step-2-tạo-masterpage-10-vùng-cấu-trúc-chuẩn-đề-bài-thang-điểm-10-đ)
   - [STEP 3 (2.0 đ): Hiển thị danh mục sách theo chủ đề & Tìm kiếm](#step-3-hiển-thị-danh-mục-sách-theo-chủ-đề--tìm-kiếm-thang-điểm-20-đ)
   - [STEP 4 (1.0 đ): Hiển thị Sách Mới (cột giữa) & Sách Bán Chạy (cột phải)](#step-4-hiển-thị-sách-mới-cột-giữa--sách-bán-chạy-cột-phải-thang-điểm-10-đ)
   - [STEP 5 (1.0 đ): Hiển thị danh mục Quảng cáo 1 và 2](#step-5-hiển-thị-danh-mục-quảng-cáo-1-và-2-thang-điểm-10-đ)
   - [STEP 6 (2.0 đ): Thêm hàng vào giỏ & Hiển thị tóm tắt giỏ hàng](#step-6-thêm-hàng-vào-giỏ--hiển-thị-tóm-tắt-giỏ-hàng-thang-điểm-20-đ)
   - [STEP 7 (1.0 đ): Thay đổi thông tin giỏ hàng (+/-, hủy, tổng tiền, AJAX)](#step-7-thay-đổi-thông-tin-giỏ-hàng-thang-điểm-10-đ)
   - [STEP 8 (1.0 đ): Đặt hàng và lưu thông tin vào CSDL với Transaction ACID](#step-8-đặt-hàng-và-lưu-thông-tin-vào-csdl-với-transaction-acid-thang-điểm-10-đ)
4. [CẨM NANG TRẢ LỜI VẤN ĐÁP KHI GIẢNG VIÊN YÊU CẦU SỬA TRỰC TIẾP TRONG VISUAL STUDIO](#4-cẩm-nang-trả-lời-vấn-đáp-khi-giảng-viên-yêu-cầu-sửa-trực-tiếp-trong-visual-studio)
5. [KẾT LUẬN](#5-kết-luận)

---

## 1. TỔNG QUAN HỆ THỐNG & CÂY THƯ MỤC DỰ ÁN TRONG VISUAL STUDIO

Khi mở giải pháp `BookOnline.slnx` hoặc thư mục dự án `c:\Users\PC\Downloads\PTWNC\BookOnline` trên **Visual Studio**, cửa sổ **Solution Explorer** (bên phải màn hình) được cấu trúc phân tầng chuẩn mực theo mô hình MVC:

```text
Solution 'BookOnline'
│
└── 📁 BookOnline (Dự án Web ASP.NET Core)
    │
    ├── 📁 Connected Services
    ├── 📁 Properties (launchSettings.json)
    │
    ├── 📁 wwwroot (Tài nguyên tĩnh công khai)
    │   ├── 📁 css
    │   │   ├── site.css                   <-- CSS chuẩn nền tảng
    │   │   └── phuongnam-theme.css        <-- CSS thương hiệu Phương Nam, nút mũi tên, badge, màu sắc
    │   ├── 📁 js (site.js)                <-- JavaScript xử lý Toast, giỏ hàng AJAX
    │   ├── 📁 images                      <-- Ảnh banner, icon muitendo.gif, tittle_sachmoi.gif
    │   └── 📁 lib (bootstrap, jquery)
    │
    ├── 📁 Controllers (Điều phối nghiệp vụ máy chủ)
    │   ├── AccountController.cs           <-- Đăng ký, Đăng nhập, Giới tính Nam/Nữ
    │   ├── HomeController.cs              <-- Sách mới, Quảng cáo 1 Random, Trang chủ
    │   ├── SachController.cs              <-- Lọc chủ đề, Chi tiết sách, Tìm kiếm tiếng Việt
    │   └── GioHangController.cs           <-- Thêm giỏ, Cập nhật +/-, Transaction Đặt hàng
    │
    ├── 📁 Models (Thực thể CSDL & Giỏ hàng)
    │   ├── AppDbContext.cs                <-- Kết nối Entity Framework Core với SQL Server
    │   ├── Sach.cs, ChuDe.cs, QuangCao.cs <-- Các Entity Model bảng CSDL
    │   ├── DonDatHang.cs, CtDatHang.cs    <-- Hóa đơn & Chi tiết hóa đơn
    │   └── CartItem.cs                    <-- Model mục hàng trong giỏ
    │
    ├── 📁 ViewModels                      <-- Đối tượng dữ liệu hiển thị (Register, Login, Home)
    │
    ├── 📁 ViewComponents (Các khối giao diện dùng lại độc lập)
    │   ├── ChuDeMenuViewComponent.cs      <-- Vùng chủ đề sách (Cột trái)
    │   ├── CartSummaryViewComponent.cs    <-- Hộp vàng giỏ hàng (Cột phải)
    │   ├── QuangCaoSidebarViewComponent.cs<-- Quảng cáo 2 còn hạn (Cột phải)
    │   └── SachBanChayViewComponent.cs    <-- Top 5 sách bán chạy (Cột phải)
    │
    ├── 📁 Views (Giao diện Razor HTML)
    │   ├── 📁 Home (Index.cshtml)         <-- Cột giữa: Hero Carousel -> Sách mới -> QC 1 -> Lưới sách
    │   ├── 📁 Sach (Details.cshtml, DanhSach.cshtml)
    │   ├── 📁 GioHang (Index.cshtml, DatHangThanhCong.cshtml) <-- Bảng GridView giỏ hàng & Kết quả
    │   ├── 📁 Account (Register.cshtml, Login.cshtml)
    │   └── 📁 Shared
    │       ├── _Layout.cshtml             <-- MASTERPAGE 10 VÙNG CHUẨN CÂU 1
    │       ├── _SachCardPartial.cshtml    <-- Card sản phẩm DataList
    │       └── 📁 Components (Các view con cho ViewComponent)
    │           ├── 📁 ChuDeMenu (Default.cshtml)
    │           ├── 📁 CartSummary (Default.cshtml)
    │           ├── 📁 QuangCaoSidebar (Default.cshtml)
    │           └── 📁 SachBanChay (Default.cshtml)
    │
    ├── appsettings.json                   <-- Chuỗi kết nối Database QLBanSach
    └── Program.cs                         <-- Khởi tạo Middleware, Cookie Auth, Session
```

---

## 2. BẢNG TỔNG HỢP 8 STEPS & VỊ TRÍ FILE TRONG VISUAL STUDIO

| STEP | Tiêu chí chấm điểm | Điểm | Vị trí thư mục & Tệp tin trong Visual Studio | Thành phần mã nguồn cần mở xem |
| :---: | :--- | :---: | :--- | :--- |
| **STEP 1** | Giao diện đẹp, hợp lý & Responsive | **1,0 đ** | `wwwroot/css/phuongnam-theme.css`<br>`Views/Shared/_Layout.cshtml` | Hệ thống màu Phương Nam, nút mũi tên sát cạnh, CSS Grid co giãn đa màn hình |
| **STEP 2** | Tạo MasterPage 10 vùng cấu trúc | **1,0 đ** | `Views/Shared/_Layout.cshtml`<br>`Views/Home/Index.cshtml` | Bố cục 3 cột: Trái (Chủ đề) - Giữa (Sách mới, QC1, Nội dung) - Phải (Giỏ hàng, QC2, Sách bán chạy) |
| **STEP 3** | Hiển thị sách theo chủ đề & Tìm kiếm | **2,0 đ** | `Controllers/SachController.cs`<br>`Views/Shared/_SachCardPartial.cshtml` | Action `TheoChuDe`, Action `Search` (`EF.Functions.Like`), Card DataList 3 cuốn/hàng |
| **STEP 4** | Hiển thị Sách Mới & Sách Bán Chạy | **1,0 đ** | `Controllers/HomeController.cs`<br>`ViewComponents/SachBanChayViewComponent.cs`<br>`Views/Sach/Details.cshtml` | Sách mới: `.Take(5)` ở cột giữa; Sách bán chạy: `.OrderByDescending(s => s.SoLuongBan).Take(5)` ở cột phải; Chi tiết: `SoLanXem++` |
| **STEP 5** | Hiển thị Quảng cáo 1 và 2 | **1,0 đ** | `Controllers/HomeController.cs`<br>`ViewComponents/QuangCaoSidebarViewComponent.cs` | QC 1: `EF.Functions.Random().Take(3)` ở cột giữa; QC 2: `Where(NgayHetHan >= Now)` ở cột phải |
| **STEP 6** | Thêm hàng vào giỏ & Tóm tắt giỏ hàng | **2,0 đ** | `Controllers/GioHangController.cs`<br>`ViewComponents/CartSummaryViewComponent.cs`<br>`Views/Shared/Components/CartSummary/Default.cshtml` | Action `ThemVaoGioAjax` lưu Session State; Widget Hộp vàng cột phải cập nhật real-time |
| **STEP 7** | Thay đổi thông tin giỏ hàng (+/-, hủy) | **1,0 đ** | `Controllers/GioHangController.cs`<br>`Views/GioHang/Index.cshtml` | Bảng GridView, nút tăng/giảm `+`/`-` AJAX tự nhân tiền không tải lại trang, nút xóa món, voucher |
| **STEP 8** | Cập nhật đơn hàng bằng Transaction | **1,0 đ** | `Controllers/GioHangController.cs`<br>`Models/DonDatHang.cs`<br>`Models/CtDatHang.cs` | Action `ThanhToan`: Khởi tạo `BeginTransactionAsync()`, thêm `DonDatHang`, `CtDatHang`, tăng `SoLuongBan`, `CommitAsync()` |
| **TỔNG** | **TOÀN BỘ 8 TIÊU CHÍ BAREM** | **10,0 đ** | **HOÀN THIỆN ĐỒNG BỘ 100% CẢ DỰ ÁN** | **ĐẠT ĐIỂM TỐI ĐA 10.0 / 10.0 ĐIỂM** |

---

## 3. CHI TIẾT 8 STEPS: YÊU CẦU, VỊ TRÍ TRONG VISUAL, MÃ NGUỒN & CÁCH TEST

---

### STEP 1: Giao diện đẹp, hợp lý & Responsive (Thang điểm: 1,0 đ)

#### 1. Yêu cầu đề bài
Xây dựng giao diện thương mại điện tử chuyên nghiệp, màu sắc hài hòa, kiểu chữ sắc nét, và co giãn linh hoạt (Responsive) trên màn hình Desktop, Tablet, Mobile.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **File CSS chính:** Nhấp đúp vào `BookOnline` $\rightarrow$ `wwwroot` $\rightarrow$ `css` $\rightarrow$ [`phuongnam-theme.css`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/wwwroot/css/phuongnam-theme.css) (Dòng 1 – 100 và dòng 360 – 480).
* **File MasterPage tích hợp CSS:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ [`_Layout.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Shared/_Layout.cshtml) (Dòng 7 – 11 nạp Bootstrap 5.3, Bootstrap Icons và CSS).

#### 3. Đoạn mã nguồn then chốt
Trong [`phuongnam-theme.css`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/wwwroot/css/phuongnam-theme.css):
```css
/* Bảng màu thương hiệu Phương Nam hiện đại */
:root {
    --pn-blue-dark: #072c5c;
    --pn-blue: #0a3b7b;
    --pn-yellow: #f8b700;
    --pn-red: #e53935;
    --pn-bg: #f4f6f9;
}

/* Nút mũi tên điều hướng sát cạnh không bị đè vào chữ hoặc ô vuông */
.pn-hero-carousel .carousel-control-prev,
.pn-hero-carousel .carousel-control-next {
    width: 36px;
    height: 36px;
    top: 50%;
    transform: translateY(-50%);
    background: rgba(0, 0, 0, 0.3);
    backdrop-filter: blur(4px);
    border-radius: 50%;
    border: 1.5px solid rgba(255, 255, 255, 0.4);
    z-index: 5;
}
.pn-hero-carousel .carousel-control-prev { left: 12px; }
.pn-hero-carousel .carousel-control-next { right: 12px; }

/* Ô vuông Badge dịch vào trong và mở rộng kích thước */
.pn-hero-badge-float {
    position: absolute;
    right: 68px;
    top: 50%;
    transform: translateY(-50%);
    border-radius: 16px;
    padding: 18px 28px;
    min-width: 125px;
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Mở trình duyệt tại `http://localhost:5245/`.
* Nhấn `F12` $\rightarrow$ Bật chế độ thiết bị di động (Toggle device toolbar) $\rightarrow$ Thu nhỏ sang màn hình iPhone/iPad: 3 cột tự động co giãn xếp chồng thành 1 cột mượt mà, menu tự biến thành nút bấm hamburger.

---

### STEP 2: Tạo MasterPage 10 vùng cấu trúc chuẩn đề bài (Thang điểm: 1,0 đ)

#### 1. Yêu cầu đề bài (Câu 1)
Thiết kế MasterPage bố trí chính xác 10 vùng theo sơ đồ:
- Header, Banner, Menu ngang.
- Cột trái: Vùng danh mục các chủ đề sách.
- Cột giữa: Sách mới $\rightarrow$ Quảng cáo 1 $\rightarrow$ Vùng nội dung chính.
- Cột phải: Thông tin giỏ hàng $\rightarrow$ Quảng cáo 2 $\rightarrow$ Sách bán chạy.
- Chân trang (Footer).

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **File MasterPage:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ [`_Layout.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Shared/_Layout.cshtml):
  - Dòng 19 – 86: Vùng Header & Banner.
  - Dòng 89 – 117: Vùng Menu ngang.
  - Dòng 140 – 161: Cột trái (`ChuDeMenu`).
  - Dòng 164 – 166: Cột giữa (`@RenderBody()`).
  - Dòng 169 – 179: Cột phải (`CartSummary`, `QuangCaoSidebar`, `SachBanChay`).
  - Dòng 195 – 242: Vùng Footer liên hệ & bản quyền.
* **File nội dung cột giữa:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Home` $\rightarrow$ [`Index.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Home/Index.cshtml):
  - Dòng 124 – 167: Vùng Sách mới.
  - Dòng 170 – 182: Vùng Quảng cáo 1.
  - Dòng 185 – 234: Vùng Nội dung chính (lưới sách).

#### 3. Đoạn mã nguồn then chốt
Cột phải trong [`_Layout.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Shared/_Layout.cshtml):
```html
<aside class="layout-sidebar-right">
    <!-- 1. Vùng thông tin giỏ hàng hộp vàng -->
    @await Component.InvokeAsync("CartSummary")

    <!-- 2. Vùng quảng cáo 2 (banner còn hạn từ CSDL) -->
    @await Component.InvokeAsync("QuangCaoSidebar")

    <!-- 3. Vùng danh mục sách bán chạy (top 5 cuốn bán nhiều nhất) -->
    @await Component.InvokeAsync("SachBanChay")
</aside>
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Chỉ trực tiếp vào giao diện trang chủ: Cột giữa chỉ có Sách Mới, Banner QC 1 và Lưới sách; Sách bán chạy nằm ở đáy cột bên phải đúng 100% hình vẽ đề bài.

---

### STEP 3: Hiển thị danh mục sách theo chủ đề & Tìm kiếm (Thang điểm: 2,0 đ)

#### 1. Yêu cầu đề bài (Câu 2)
Trang hiển thị danh mục sách theo chủ đề được chọn, sử dụng cấu trúc DataList. Mỗi quyển sách hiển thị tên, tác giả, nhà xuất bản, giá bán, kèm nút chọn mua. Tìm kiếm từ khóa tiếng Việt không phân biệt hoa/thường.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **Controller:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`SachController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/SachController.cs) $\rightarrow$ Tìm hàm `TheoChuDe(int? mcd, string? keyword)` và `Search`.
* **Component cột trái:** Nhấp đúp vào `BookOnline` $\rightarrow$ `ViewComponents` $\rightarrow$ [`ChuDeMenuViewComponent.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/ViewComponents/ChuDeMenuViewComponent.cs).
* **Card sản phẩm DataList:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ [`_SachCardPartial.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Shared/_SachCardPartial.cshtml).

#### 3. Đoạn mã nguồn then chốt
Trong [`SachController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/SachController.cs):
```csharp
public async Task<IActionResult> TheoChuDe(int? mcd, string? keyword)
{
    var query = _context.Saches
        .Include(s => s.McdNavigation)
        .Include(s => s.MnxbNavigation)
        .AsQueryable();

    if (mcd.HasValue && mcd.Value > 0)
    {
        query = query.Where(s => s.Mcd == mcd.Value);
    }
    
    // Tìm kiếm tiếng Việt không phân biệt hoa thường
    if (!string.IsNullOrWhiteSpace(keyword))
    {
        var kw = keyword.Trim();
        query = query.Where(s => EF.Functions.Like(s.TenSach, $"%{kw}%") 
                              || (s.McdNavigation != null && EF.Functions.Like(s.McdNavigation.TenChuDe, $"%{kw}%")));
    }

    var danhSachSach = await query.OrderByDescending(s => s.Ms).ToListAsync();
    return View(danhSachSach);
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Bấm vào chủ đề *"Công nghệ thông tin"* ở menu cột trái $\rightarrow$ Cột giữa chỉ hiển thị 32 cuốn sách tin học.
* Gõ chữ *"công nghệ"* vào ô tìm kiếm trên Header $\rightarrow$ Nhấn Enter $\rightarrow$ Danh sách sách công nghệ hiển thị ngay lập tức.

---

### STEP 4: Hiển thị Sách Mới (cột giữa) & Sách Bán Chạy (cột phải) (Thang điểm: 1,0 đ)

#### 1. Yêu cầu đề bài (Câu 1 & 2)
- Vùng Sách mới: Nằm trên cùng cột giữa, hiển thị top sách mới nhất kèm mũi tên đỏ và link chi tiết.
- Vùng Sách bán chạy: Nằm ở cột phải, hiển thị 5 đầu sách có số lượng bán nhiều nhất kèm link chi tiết.
- Trang chi tiết sách: Hiển thị đầy đủ thông tin khi người dùng bấm vào xem.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **Sách mới (Controller):** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`HomeController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/HomeController.cs) (Dòng 30 – 38).
* **Sách mới (View):** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Home` $\rightarrow$ [`Index.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Home/Index.cshtml) (Dòng 124 – 167, thẻ tiêu đề `<span><i class="bi bi-stars text-danger me-1"></i> SÁCH MỚI</span>`).
* **Sách bán chạy (Component):** Nhấp đúp vào `BookOnline` $\rightarrow$ `ViewComponents` $\rightarrow$ [`SachBanChayViewComponent.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/ViewComponents/SachBanChayViewComponent.cs).
* **Sách bán chạy (View):** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ `Components` $\rightarrow$ `SachBanChay` $\rightarrow$ [`Default.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Shared/Components/SachBanChay/Default.cshtml).
* **Chi tiết sách:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`SachController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/SachController.cs) (Action `Details`) và `Views` $\rightarrow$ `Sach` $\rightarrow$ [`Details.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Sach/Details.cshtml).

#### 3. Đoạn mã nguồn then chốt
Trong [`SachBanChayViewComponent.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/ViewComponents/SachBanChayViewComponent.cs):
```csharp
public async Task<IViewComponentResult> InvokeAsync()
{
    // Lấy top 5 đầu sách có số lượng bán nhiều nhất từ bảng SACH
    var topSelling = await _context.Saches
        .OrderByDescending(s => s.SoLuongBan ?? 0)
        .Take(5)
        .ToListAsync();

    return View(topSelling);
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Quan sát khối *"SÁCH MỚI"* ở trên cùng cột giữa: Mỗi đầu sách có icon mũi tên đỏ `muitendo.gif`, giá tiền và nút *"Chi tiết"*.
* Quan sát khối *"SÁCH BÁN CHẠY"* ở cột phải: Có huy hiệu Top 1-5, số lượng đã bán thực tế từ CSDL. Bấm vào tên sách sẽ chuyển tới trang chi tiết.

---

### STEP 5: Hiển thị danh mục Quảng cáo 1 và 2 (Thang điểm: 1,0 đ)

#### 1. Yêu cầu đề bài (Câu 1)
- Quảng cáo 1: Ở giữa cột giữa, hiển thị ngẫu nhiên 3 ảnh quảng cáo cùng link trang web mỗi lần load trang.
- Quảng cáo 2: Ở cột phải, lấy từ bảng `QUANG_CAO`, chỉ hiển thị những banner còn hạn sử dụng.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **Quảng cáo 1 (Random 3 banner):** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`HomeController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/HomeController.cs) (Dòng 48 – 60) và `Views` $\rightarrow$ `Home` $\rightarrow$ [`Index.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Home/Index.cshtml) (Dòng 170 – 182).
* **Quảng cáo 2 (Lọc hạn sử dụng CSDL):** Nhấp đúp vào `BookOnline` $\rightarrow$ `ViewComponents` $\rightarrow$ [`QuangCaoSidebarViewComponent.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/ViewComponents/QuangCaoSidebarViewComponent.cs) và `Views` $\rightarrow$ `Shared` $\rightarrow$ `Components` $\rightarrow$ `QuangCaoSidebar` $\rightarrow$ `Default.cshtml`.

#### 3. Đoạn mã nguồn then chốt
Trong [`QuangCaoSidebarViewComponent.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/ViewComponents/QuangCaoSidebarViewComponent.cs):
```csharp
public async Task<IViewComponentResult> InvokeAsync()
{
    var now = DateTime.Now;
    // Bắt buộc kiểm tra thời hạn: chỉ lấy quảng cáo còn hạn sử dụng
    var ads = await _context.QuangCaos
        .Where(q => q.NgayHetHan == null || q.NgayHetHan >= now)
        .OrderByDescending(q => q.Stt)
        .ToListAsync();

    return View(ads);
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Nhấn `F5` tải lại trang chủ: 3 ảnh quảng cáo ở giữa cột giữa lập tức đảo ngẫu nhiên.
* Cột phải hiển thị danh sách các logo đối tác (Hanoi Computer, Ben, CBV...) còn hạn trong bảng `QUANG_CAO`.

---

### STEP 6: Thêm hàng vào giỏ & Hiển thị tóm tắt giỏ hàng (Thang điểm: 2,0 đ)

#### 1. Yêu cầu đề bài (Câu 1 & 2)
Khách hàng bấm mua hàng thì sách được thêm vào giỏ. Vùng thông tin giỏ hàng ở cột phải (hộp màu vàng) hiển thị tổng số cuốn sách và tổng số tiền phải thanh toán; bấm vào sẽ mở chi tiết giỏ hàng.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **Model giỏ hàng:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Models` $\rightarrow$ [`CartItem.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Models/CartItem.cs).
* **Controller thêm giỏ hàng:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`GioHangController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/GioHangController.cs) $\rightarrow$ Action `ThemVaoGio` và `ThemVaoGioAjax` (Dòng 100 – 145).
* **ViewComponent Hộp vàng:** Nhấp đúp vào `BookOnline` $\rightarrow$ `ViewComponents` $\rightarrow$ [`CartSummaryViewComponent.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/ViewComponents/CartSummaryViewComponent.cs).
* **Giao diện Hộp vàng:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ `Components` $\rightarrow$ `CartSummary` $\rightarrow$ [`Default.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/Shared/Components/CartSummary/Default.cshtml).

#### 3. Đoạn mã nguồn then chốt
Trong [`GioHangController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/GioHangController.cs):
```csharp
[HttpPost]
public async Task<IActionResult> ThemVaoGioAjax(int ms, int soLuong = 1)
{
    var cart = GetCart(); // Lấy từ Session
    var item = cart.FirstOrDefault(x => x.Ms == ms);
    if (item != null) item.SoLuong += soLuong;
    else
    {
        var sach = await _context.Saches.FindAsync(ms);
        cart.Add(new CartItem {
            Ms = sach.Ms,
            TenSach = sach.TenSach,
            DonGia = (double)(sach.DonGia ?? 0),
            SoLuong = soLuong,
            HinhMinhHoa = sach.HinhMinhHoa ?? "Sample.jpg"
        });
    }
    SaveCart(cart); // Lưu ngược lại vào Session bằng JSON
    return Json(new { success = true, totalItems = cart.Sum(c => c.SoLuong), totalPrice = cart.Sum(c => c.ThanhTien) });
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Tại bất kỳ card sách nào, bấm nút *"Mua"* $\rightarrow$ Toast thông báo xanh xuất hiện ở góc màn hình.
* Hộp vàng ở cột phải lập tức nhảy số: *"Có 1 cuốn sách trong giỏ. Tổng tiền: 26,000 đ"* mà không cần tải lại trang.

---

### STEP 7: Thay đổi thông tin giỏ hàng (Thang điểm: 1,0 đ)

#### 1. Yêu cầu đề bài (Câu 2)
Trang giỏ hàng hiển thị danh sách dạng GridView, cho phép thêm bớt số lượng cho từng món hàng, hủy từng món hàng, hiển thị mục tổng tiền ở phía dưới danh sách hàng.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **Controller xử lý:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`GioHangController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/GioHangController.cs) $\rightarrow$ Action `Index`, `CapNhatSoLuongAjax`, `XoaItem`, `XoaTatCa`.
* **Giao diện bảng GridView:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `GioHang` $\rightarrow$ [`Index.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/GioHang/Index.cshtml).
* **JavaScript Real-time:** Xem ở khối `<script>` cuối tệp [`Views/GioHang/Index.cshtml`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Views/GioHang/Index.cshtml).

#### 3. Đoạn mã nguồn then chốt
Trong [`GioHangController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/GioHangController.cs):
```csharp
[HttpPost]
public IActionResult CapNhatSoLuongAjax(int ms, int soLuong)
{
    var cart = GetCart();
    var item = cart.FirstOrDefault(x => x.Ms == ms);
    if (item != null)
    {
        if (soLuong <= 0) cart.Remove(item);
        else item.SoLuong = Math.Min(99, soLuong);
        SaveCart(cart);
    }
    var subtotal = (decimal)cart.Sum(c => c.ThanhTien);
    return Json(new { success = true, subtotalFormatted = subtotal.ToString("#,##0") + " đ" });
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Vào trang giỏ hàng `http://localhost:5245/GioHang`.
* Bấm nút `[+]` tăng số lượng lên 3: Tiền tự động nhân lên ngay tức thì qua AJAX không giật màn hình.
* Bấm icon thùng rác đỏ: Cuốn sách lập tức bị xóa và tiền tự giảm tương ứng.

---

### STEP 8: Đặt hàng và lưu thông tin vào CSDL với Transaction ACID (Thang điểm: 1,0 đ)

#### 1. Yêu cầu đề bài (Câu 2)
Tạo nút "Đặt hàng" tại trang giỏ hàng. Khi khách hàng nhấn nút này thì cập nhật đơn hàng vào trong CSDL. Bắt buộc bảo vệ dữ liệu bằng Transaction.

#### 2. Vị trí trong Visual Studio (Solution Explorer)
* **Controller:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ [`GioHangController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/GioHangController.cs) $\rightarrow$ Tìm Action `ThanhToan()` (Dòng 246 – 295).
* **Model CSDL:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Models` $\rightarrow$ [`DonDatHang.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Models/DonDatHang.cs) và [`CtDatHang.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Models/CtDatHang.cs).
* **Trang hoàn tất:** Nhấp đúp vào `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `GioHang` $\rightarrow$ `DatHangThanhCong.cshtml`.

#### 3. Đoạn mã nguồn then chốt
Trong [`GioHangController.cs`](file:///c:/Users/PC/Downloads/PTWNC/BookOnline/Controllers/GioHangController.cs):
```csharp
[HttpPost]
[Authorize]
public async Task<IActionResult> ThanhToan()
{
    var cart = GetCart();
    var mkhClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    int.TryParse(mkhClaim, out int mkh);

    // BẮT BUỘC DÙNG TRANSACTION BẢO VỆ DỮ LIỆU CHUẨN ĐỀ BÀI
    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        // 1. Thêm hóa đơn chính
        var donHang = new DonDatHang { Mkh = mkh, NgayDatHang = DateTime.Now, TriGia = (decimal)cart.Sum(c => c.ThanhTien), DaGiaoHang = false };
        _context.DonDatHangs.Add(donHang);
        await _context.SaveChangesAsync();

        // 2. Thêm từng chi tiết đơn hàng & Tăng số lượng bán trong kho sách
        foreach (var item in cart)
        {
            var ct = new CtDatHang { Sdh = donHang.Sdh, Ms = item.Ms, SoLuong = item.SoLuong, DonGia = (decimal)item.DonGia, ThanhTien = (decimal)item.ThanhTien };
            _context.CtDatHangs.Add(ct);

            var sach = await _context.Saches.FindAsync(item.Ms);
            if (sach != null) sach.SoLuongBan = (sach.SoLuongBan ?? 0) + item.SoLuong;
        }
        await _context.SaveChangesAsync();

        // 3. Cam kết thành công
        await transaction.CommitAsync();

        ClearCart();
        return RedirectToAction("DatHangThanhCong", new { id = donHang.Sdh });
    }
    catch
    {
        // 4. Nếu có lỗi: Hoàn tác sạch sẽ
        await transaction.RollbackAsync();
        throw;
    }
}
```

#### 4. Cách thao tác & Kiểm chứng khi bảo vệ
* Đăng nhập tài khoản `vanan2026` / `123456`.
* Tại trang giỏ hàng, bấm *"TIẾN HÀNH ĐẶT HÀNG"* $\rightarrow$ Bấm OK xác nhận.
* Màn hình hiện thông báo đặt hàng thành công kèm Mã đơn hàng `#Sdh`. Mở SQL Server Management Studio kiểm tra: Bảng `DON_DAT_HANG`, `CT_DAT_HANG` đều có bản ghi mới và `SoLuongBan` của sách được tăng lên chính xác.

---

## 4. CẨM NANG TRẢ LỜI VẤN ĐÁP KHI GIẢNG VIÊN YÊU CẦU SỬA TRỰC TIẾP TRONG VISUAL STUDIO

| Tình huống thầy yêu cầu | Vị trí file trong Visual Studio | Dòng code và cách sửa ngay lập tức |
| :--- | :--- | :--- |
| **"Em đổi hiển thị từ 5 sách mới thành 8 sách mới cho thầy xem?"** | Mở `Controllers/HomeController.cs` | Tìm dòng 34: Đổi `.Take(5)` thành `.Take(8)` $\rightarrow$ Nhấn `Ctrl + S` lưu lại $\rightarrow$ F5 trình duyệt. |
| **"Em đổi vị trí Sách bán chạy lên trên Quảng cáo 2 cho thầy?"** | Mở `Views/Shared/_Layout.cshtml` | Dòng 173 - 177: Cắt dòng `@await Component.InvokeAsync("SachBanChay")` đặt lên trên `@await Component.InvokeAsync("QuangCaoSidebar")`. |
| **"Đổi mặc định Giới tính từ Nam sang Nữ ở đâu?"** | Mở `Views/Account/Register.cshtml` | Dòng 129 - 134: Xóa chữ `checked` ở ô Nam, thêm chữ `checked` vào ô Nữ. |
| **"Tại sao quảng cáo lại có hạn và bỏ lọc hạn ở đâu?"** | Mở `ViewComponents/QuangCaoSidebarViewComponent.cs` | Dòng 22: Giải thích quảng cáo theo hợp đồng đối tác. Nếu bỏ lọc hạn, xóa đoạn `.Where(...)` chỉ giữ `.ToListAsync()`. |
| **"Chỗ nào xử lý Transaction khi đặt hàng?"** | Mở `Controllers/GioHangController.cs` | Chỉ ngay vào dòng 246 - 295 với `BeginTransactionAsync()`, `CommitAsync()`, `RollbackAsync()`. |

---

## 5. KẾT LUẬN
Bản báo cáo này đã chỉ rõ từng vị trí tệp tin, dòng lệnh và cấu trúc thư mục cụ thể trong **Visual Studio** tương ứng với từng tiêu chí chấm điểm trong barem đề bài. Sinh viên có thể tự tin mở đúng tệp tin trong vòng 3 giây trước mặt giảng viên để thuyết minh và bảo vệ đồ án đạt điểm số tối đa **10.0 / 10.0 điểm**.
