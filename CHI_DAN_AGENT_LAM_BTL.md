# HƯỚNG DẪN & CHỈ DẪN AGENT (SYSTEM PROMPT & EXECUTION SPECIFICATION)
## DỰ ÁN BÀI TẬP LỚN: XÂY DỰNG WEBSITE BÁN SÁCH TRỰC TUYẾN (BOOK ONLINE)
**Môn học:** Phát triển ứng dụng Web nâng cao (PTWNC)  
**Đơn vị:** Khoa Công nghệ Thông tin – Trường Đại học Công nghiệp Việt - Hung (VIU)  
**Giảng viên phụ trách:** TS. Nguyễn Hồng Quân  
**Công nghệ:** ASP.NET Core MVC (.NET 8/9), Entity Framework Core, SQL Server, Razor View Engine  

---

## 1. MỤC TIÊU & TỔNG QUAN DỰ ÁN

### 1.1. Mục tiêu
Xây dựng hoàn chỉnh website bán sách trực tuyến **"Book Online"** đáp ứng đầy đủ yêu cầu nghiệp vụ, kỹ thuật lập trình hiện đại và bố cục giao diện chuẩn theo tài liệu bài tập lớn (`BaiTapLon.docx`), ảnh bố cục (`Layout2.jpg`) cùng hệ thống slide bài giảng môn học (`02_ASPX_Core_MVC_Overview`, `03_ASPX_Core_MVC_LINQ`, `04_ASPX_Core_MVC_HttpContext`, `ThucHanh01`, `ThucHanh02`, `ThucHanh03`).

### 1.2. Bảng phân bổ thang điểm đánh giá (Tổng điểm: 10.0)
| STT | Hạng mục công việc / Tiêu chí | Điểm tối đa |
|:---:|:---|:---:|
| 1 | Giao diện đẹp, chuẩn bố cục `Layout2.jpg`, phối màu hài hòa, trải nghiệm tốt | **1.0 đ** |
| 2 | Tạo MasterPage (`_Layout.cshtml`) đầy đủ các phân vùng theo đặc tả | **1.0 đ** |
| 3 | Hiển thị danh mục sách theo chủ đề (Lưới Card View sản phẩm, chi tiết tác giả, NXB, giá, nút chọn mua) | **2.0 đ** |
| 4 | Hiển thị danh mục sách bán chạy / sách mới (Top 5 đầu sách, link chi tiết) | **1.0 đ** |
| 5 | Hiển thị danh mục Quảng cáo 1 (ngẫu nhiên 3 ảnh/load trang) & Quảng cáo 2 (danh mục banner còn hạn từ DB) | **1.0 đ** |
| 6 | Thêm sách vào giỏ hàng, hiển thị thông tin giỏ hàng ở sidebar (số cuốn, tổng tiền) | **2.0 đ** |
| 7 | Trang quản lý giỏ hàng: xem danh sách, cập nhật thêm/bớt số lượng, hủy mặt hàng, tính tổng tiền | **1.0 đ** |
| 8 | Xử lý Đặt hàng: lưu đơn hàng vào CSDL (`DON_DAT_HANG`, `CT_DAT_HANG`), xóa giỏ hàng khi thành công | **1.0 đ** |
| **Cộng** | **TỔNG ĐIỂM TOÀN BỘ BÀI TẬP LỚN** | **10.0 đ** |

---

## 2. TÀI NGUYÊN & CẤU TRÚC HIỆN CÓ TRONG WORKSPACE

Agent cần khai thác triệt để các tài nguyên có sẵn trong thư mục làm việc:
1. **Tài liệu đặc tả**:
   - `BaiTapLon/BaiTapLon.docx`: Đề bài gốc và tiêu chuẩn đánh giá.
   - `BaiTapLon/Layout2.jpg`: Thiết kế mẫu giao diện chuẩn (Master Layout).
2. **Cơ sở dữ liệu**:
   - `BaiTapLon/CSDL/QLBanSach.bak` (File backup SQL Server).
   - `BaiTapLon/CSDL/QLBanSach.mdf`, `QLBanSach_log.ldf` (File data SQL Server để Attach).
3. **Thư mục hình ảnh & Media**:
   - `BaiTapLon/Images/`: Các icon, logo banner (`Nen_banner.jpg`), nút bấm (`nut_chonmua.gif`, `Cancel.jpg`, `Giohang.gif`, `Sotien.gif`, `tittle_sachmoi.gif`,...).
   - `BaiTapLon/Images/Upload/`: Toàn bộ ảnh bìa các cuốn sách (`THCB.jpg`, `TH004.jpg`, `LTWeb2005.jpg`,...) và banner quảng cáo (`QC_BEN.png`, `QC_CBV.png`, `QC_LT.png`,...).
4. **Slide tài liệu lý thuyết & thực hành**:
   - `Slides/02_ASPX_Core_MVC_Overview.pdf`: Kiến trúc MVC, kết nối DB bằng EF Core Scaffold, xây dựng `SachViewModel`, CardView CSS.
   - `Slides/03_ASPX_Core_MVC_LINQ.pdf`: Kỹ thuật LINQ to Entities nâng cao, tối ưu `AsNoTracking()`, `Include()`, Async queries.
   - `Slides/04_ASPX_Core_MVC_HttpContext.pdf`: `HttpContext.Session` (lưu giỏ hàng), `CookieAuthentication` (đăng nhập/phân quyền Claims-based).
   - `Slides/ThucHanh01`, `ThucHanh02`, `ThucHanh03`: Thực hành mẫu, demo truy vấn LINQ (`QueryDemo`).

---

## 3. QUY CHUẨN CÔNG NGHỆ & NGUYÊN TẮC LẬP TRÌNH (TECH STACK STANDARDS)

Agent **bắt buộc** tuân thủ các chuẩn mực kỹ thuật theo bài giảng:
1. **Kiến trúc phần mềm**: ASP.NET Core MVC (.NET 8.0 hoặc .NET 9.0).
2. **ORM & Truy cập dữ liệu**:
   - Sử dụng **Entity Framework Core Database-First** (Scaffolding từ database `QLBanSach`).
   - Sử dụng **Async/Await** xuyên suốt các Controller Action: `await _context.Saches.ToListAsync()`, `FirstOrDefaultAsync()`.
   - Với các truy vấn chỉ đọc (danh mục, trang chủ, quảng cáo): Bắt buộc dùng `.AsNoTracking()` để tối ưu RAM và hiệu năng.
   - Với các quan hệ khóa ngoại (Sách -> NXB, Chủ đề, Tác giả): Sử dụng `.Include()` hoặc projection qua `Select(s => new SachViewModel { ... })` để tránh lỗi **N+1 Query**.
3. **Quản lý phiên làm việc & Trạng thái (State Management)**:
   - Giỏ hàng: Lưu trữ trong `HttpContext.Session` dưới dạng chuỗi JSON (`System.Text.Json`).
   - Xác thực: Sử dụng `CookieAuthentication` chuẩn ASP.NET Core (`ClaimsPrincipal`, `ClaimTypes.Name`, `ClaimTypes.Role`).
4. **UI & View**:
   - Tuân thủ cấu trúc phân vùng theo đúng ảnh `Layout2.jpg`.
   - Lưu trữ toàn bộ ảnh trong thư mục `wwwroot/images/` và `wwwroot/images/Upload/`.
   - Tạo file CSS tùy chỉnh (`wwwroot/css/product.css` hoặc `site.css`) để trình bày lưới sản phẩm, sidebar, box giỏ hàng màu vàng đặc trưng, footer.

---

## 4. CẤU TRÚC CƠ SỞ DỮ LIỆU `QLBanSach`

Agent cần nắm vững lược đồ quan hệ giữa 9 bảng trong CSDL:
```
[CHU_DE] 1 ---- N [SACH] N ---- 1 [NHA_XUAT_BAN]
                     |
                     +---- N [THAM_GIA] N ---- 1 [TAC_GIA]
                     |
                     +---- N [CT_DAT_HANG] N ---- 1 [DON_DAT_HANG] N ---- 1 [KHACH_HANG]

[QUANG_CAO] (Độc lập: Mqc, Ten_cong_ty, Hinh_anh, Link_lien_ket, Ngay_bat_dau, Ngay_het_han)
```

### Chi tiết các trường dữ liệu quan trọng:
- `CHU_DE`: `Mcd` (PK, int identity), `Ten_chu_de` (nvarchar), `PID` (int).
- `SACH`: `Ms` (PK, int identity), `Ten_sach` (nvarchar), `Don_gia` (decimal/money), `Don_vi_tinh` (nvarchar), `Mo_ta` (ntext/nvarchar), `Hinh_minh_hoa` (varchar - tên file ảnh không đuôi hoặc có đuôi), `Mcd` (FK), `Mnxb` (FK), `Ngay_cap_nhat` (datetime), `So_luong_ban` (int), `So_lan_xem` (int).
- `NHA_XUAT_BAN`: `Mnxb` (PK), `Ten_nha_xuat_ban`, `Dia_chi`, `Dien_thoai`.
- `TAC_GIA`: `Mtg` (PK), `Ten_tac_gia`, `Dia_chi`, `Tieu_su`, `Dien_thoai`.
- `THAM_GIA`: `Ms` (PK, FK), `Mtg` (PK, FK), `Vai_tro`, `Vi_tri`.
- `KHACH_HANG`: `Mkh` (PK), `Ho_ten`, `Tai_khoan`, `Mat_khau`, `Email`, `Dia_chi`, `Dien_thoai`, `Gioi_tinh`, `Ngay_sinh`.
- `DON_DAT_HANG`: `Mdh` (PK, identity), `Da_thanh_toan` (bit), `Tinh_trang_giao_hang` (int), `Ngay_dat` (datetime), `Ngay_giao` (datetime), `Mkh` (FK).
- `CT_DAT_HANG`: `Mdh` (PK, FK), `Ms` (PK, FK), `So_luong` (int), `Don_gia` (decimal).
- `QUANG_CAO`: `Mqc` (PK), `Ten_cong_ty`, `Hinh_anh`, `Link_lien_ket`, `Ngay_bat_dau`, `Ngay_het_han`.

---

## 5. ĐẶC TẢ CHI TIẾT GIAO DIỆN & PHÂN VÙNG (THEO `Layout2.jpg`)

Giao diện Master Layout (`Views/Shared/_Layout.cshtml`) được chia làm các khối sau:

```
+-----------------------------------------------------------------------------------+
| [Logo Book Online]                   BANNER TRANG WEB                             |
+-----------------------------------------------------------------------------------+
| [Trang chủ]  [Sách mới]  [Giới thiệu]  [Đăng ký]  [Đăng nhập / Xin chào User]    |
+-------------------+-------------------------------------------+-------------------+
| CỘT TRÁI (Sidebar)| KHỐI NỘI DUNG CHÍNH (Main Body)           | CỘT PHẢI (Sidebar)|
|                   |                                           |                   |
| 1. CHỦ ĐỀ SÁCH    | 1. Banner/List "Sách mới" (Top 5 đầu sách)| 1. THÔNG TIN      |
|    - Tiếng Việt (0)| 2. QUẢNG CÁO 1: 3 ảnh banner ngẫu nhiên   |    GIỎ HÀNG:      |
|    - Tin học (32) | 3. TIÊU ĐỀ: "Danh mục sách: [Tên chủ đề]" |    - 257.000 vnđ  |
|    - Kinh tế (5)  | 4. LƯỚI CARD SÁCH:                        |    - 8 cuốn       |
|    - ...          |    + Bìa sách, Tiêu đề (link), NXB, Giá   |    (Box nền vàng) |
|                   |    + Nút [Chọn mua]                       |                   |
|                   |                                           | 2. QUẢNG CÁO 2:   |
|                   |                                           |    Danh mục banner|
|                   |                                           |    còn hạn từ DB  |
|                   |                                           |    (Ben, CBV,...) |
+-------------------+-------------------------------------------+-------------------+
| FOOTER: @2012 - Nhà sách trường Đại học Công nghiệp Việt - Hung                   |
|         Địa chỉ: 16 Xuân Khanh, Sơn Tây, Hà Nội - ĐT: 0983308081                  |
+-----------------------------------------------------------------------------------+
```

### Mô tả hành vi chi tiết từng vùng:
1. **Header & Banner**: Hiển thị logo sách và banner trang trí màu xanh lá chủ đạo theo tông màu của ĐHCN Việt - Hung.
2. **Menu ngang**:
   - Trang chủ (`asp-controller="Home" asp-action="Index"`)
   - Sách mới (`asp-controller="Sach" asp-action="SachMoi"`)
   - Giới thiệu (`asp-controller="Home" asp-action="GioiThieu"`)
   - Nếu chưa đăng nhập: Hiện "Đăng nhập" và "Đăng ký"
   - Nếu đã đăng nhập: Hiện "Xin chào, @User.Identity.Name", liên kết Giỏ hàng, nút "Đăng xuất" (và menu "Quản trị" nếu là Admin).
3. **Cột trái - Danh mục Chủ đề sách**:
   - Lấy toàn bộ danh mục chủ đề kèm số lượng sách tương ứng (`TenChuDe (TongSoSach)`).
   - Khi nhấn vào một chủ đề: Điều hướng tới action hiển thị danh mục sách thuộc chủ đề đó (`/Sach/TheoChuDe?mcd=X`).
4. **Cột phải - Thông tin Giỏ hàng (Sidebar Widget)**:
   - Hộp màu vàng có viền nét đứt:
     - Dòng 1: Icon tiền + Tổng số tiền (định dạng `#,##0 vnđ`).
     - Dòng 2: Icon giỏ hàng (`Giohang.gif`) + Số lượng cuốn sách (`Giỏ hàng: X cuốn`).
   - Click vào hộp điều hướng sang trang chi tiết Giỏ hàng (`/GioHang/Index`).
5. **Cột phải - Quảng cáo 2**:
   - Hiển thị danh sách các logo/banner quảng cáo còn hạn (`NgayHetHan >= DateTime.Now`).
   - Click vào ảnh mở ra trang liên kết quảng cáo (`Link_lien_ket`) trong tab mới.
6. **Vùng nội dung chính**:
   - **Vùng Sách mới**: Hiển thị tiêu đề nghệ thuật `tittle_sachmoi.gif` và danh sách 5 đầu sách mới nhất có icon mũi tên đỏ (`muitendo.gif`). Click vào tên dẫn đến chi tiết sách.
   - **Vùng Quảng cáo 1**: Hiển thị ngẫu nhiên 3 hình ảnh quảng cáo cho mỗi lượt nạp trang kèm liên kết.
   - **Lưới sản phẩm (Card View)**: Trình bày dạng lưới nhiều cột (2-3 cột). Mỗi sản phẩm gồm: Hình ảnh bìa, Tên sách (in đậm, liên kết chi tiết), Tên Nhà xuất bản, Đơn giá (định dạng VNĐ), Nút hình ảnh `nut_chonmua.gif`.

---

## 6. LỘ TRÌNH TRIỂN KHAI TỪNG BƯỚC CHO AGENT (10 BƯỚC THỰC HIỆN)

### BƯỚC 1: KHỞI TẠO PROJECT & CÀI ĐẶT THƯ VIỆN
1. Khởi tạo ASP.NET Core MVC project:
   ```powershell
   dotnet new mvc -n BookOnline -f net8.0
   cd BookOnline
   ```
2. Cài đặt các gói NuGet bắt buộc:
   ```powershell
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package Microsoft.EntityFrameworkCore.Design
   ```
3. Copy toàn bộ assets:
   - Copy toàn bộ ảnh từ `BaiTapLon/Images/` vào `BookOnline/wwwroot/images/`.
   - Đảm bảo thư mục `BookOnline/wwwroot/images/Upload/` có đầy đủ ảnh bìa sách.

### BƯỚC 2: ATTACH CSDL & SCAFFOLD ENTITY FRAMEWORK CORE
1. Đảm bảo CSDL `QLBanSach` đã được đính kèm vào SQL Server (LocalDB hoặc SQLEXPRESS).
2. Chạy lệnh sinh Model và DbContext từ CSDL:
   ```powershell
   dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=QLBanSach;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext --data-annotations --force
   ```
3. Đăng ký ConnectionString trong `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "QLBanSachConnection": "Server=.\\SQLEXPRESS;Database=QLBanSach;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

### BƯỚC 3: CẤU HÌNH DỊCH VỤ TRONG `Program.cs`
Bổ sung các middleware và dịch vụ thiết yếu theo slide `04_ASPX_Core_MVC_HttpContext.pdf`:
1. Đăng ký `DbContext`:
   ```csharp
   builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("QLBanSachConnection")));
   ```
2. Cấu hình Session (để lưu Giỏ hàng):
   ```csharp
   builder.Services.AddDistributedMemoryCache();
   builder.Services.AddSession(options => {
       options.IdleTimeout = TimeSpan.FromMinutes(30);
       options.Cookie.HttpOnly = true;
       options.Cookie.IsEssential = true;
   });
   ```
3. Cấu hình Cookie Authentication (để Đăng nhập / Xác thực):
   ```csharp
   builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options => {
           options.LoginPath = "/Account/Login";
           options.AccessDeniedPath = "/Account/AccessDenied";
       });
   ```
4. Kích hoạt trong HTTP Request Pipeline:
   ```csharp
   app.UseStaticFiles();
   app.UseRouting();
   app.UseSession();
   app.UseAuthentication();
   app.UseAuthorization();
   ```

### BƯỚC 4: THIẾT KẾ MASTER LAYOUT & VIEWCOMPONENTS
Để sidebar luôn hiển thị ở mọi trang mà không phải viết lặp lại code trong từng action, Agent cần sử dụng **ViewComponent**:
1. `ChuDeMenuComponent`: Truy vấn danh mục chủ đề kèm số lượng sách:
   ```csharp
   var chuDes = await _context.ChuDes.Select(c => new ChuDeViewModel {
       Mcd = c.Mcd,
       TenChuDe = c.TenChuDe,
       TongSoSach = c.Saches.Count()
   }).ToListAsync();
   ```
2. `CartSummaryComponent`: Đọc Session giỏ hàng và hiển thị tổng số tiền + số lượng cuốn sách.
3. `QuangCaoSidebarComponent`: Lấy banner quảng cáo còn hạn (`NgayHetHan >= DateTime.Today`).
4. Nhúng vào `Views/Shared/_Layout.cshtml`:
   ```cshtml
   @await Component.InvokeAsync("ChuDeMenu")
   @await Component.InvokeAsync("CartSummary")
   @await Component.InvokeAsync("QuangCaoSidebar")
   ```

### BƯỚC 5: XÂY DỰNG MODULE TRANG CHỦ & DANH MỤC SÁCH
1. **Model DTO / ViewModel (`SachViewModel.cs`)**:
   - Thuộc tính: `Ms`, `TenSach`, `DonGia`, `HinhMinhHoa`, `MoTa`, `TenNXB`, `TenChuDe`, `TenTacGia`.
2. **Controller `SachController.cs`**:
   - `Index(int? mcd, int? page)`: Lấy danh sách sách theo chủ đề (nếu có `mcd`) hoặc toàn bộ sách.
   - Sử dụng `.AsNoTracking()` và `.Include(s => s.MnxbNavigation)`.
   - Action `SachMoi()`: Lấy top 5 cuốn sách có ngày cập nhật mới nhất.
   - Action `SachBanChay()`: Lấy top 5 cuốn sách có `SoLuongBan` cao nhất.
3. **View `Views/Sach/Index.cshtml` & `Views/Shared/_SachCardPartial.cshtml`**:
   - Hiển thị theo lưới CSS (thẻ sản phẩm như slide 47-50).
   - Nút "Chọn mua" gọi POST hoặc GET tới action thêm giỏ hàng.

### BƯỚC 6: XÂY DỰNG TRANG CHI TIẾT SÁCH
1. Action `Details(int id)`:
   - Lấy thông tin sách theo mã `id`.
   - Lấy danh sách tác giả tham gia qua bảng `ThamGia` kết nối `TacGia`.
   - Tăng số lần xem `SoLanXem = SoLanXem + 1` và `await _context.SaveChangesAsync()`.
2. View `Views/Sach/Details.cshtml`:
   - Bố cục 2 cột: Cột trái ảnh bìa lớn; Cột phải: Tên sách, Giá, Nhà xuất bản, Danh sách tác giả, Ngày phát hành.
   - Phần dưới: Mô tả chi tiết cuốn sách (`MoTa`).
   - Nút "Chọn mua" để thêm sách vào giỏ.

### BƯỚC 7: XÂY DỰNG MODULE GIỎ HÀNG (SHOPPING CART)
1. **Model `CartItem.cs`**:
   ```csharp
   public class CartItem {
       public int Ms { get; set; }
       public string TenSach { get; set; }
       public string HinhMinhHoa { get; set; }
       public decimal DonGia { get; set; }
       public int SoLuong { get; set; }
       public decimal ThanhTien => SoLuong * DonGia;
   }
   ```
2. **Helper `SessionHelper.cs`**:
   - Phương thức mở rộng `SetObjectAsJson` và `GetObjectFromJson<T>` cho `ISession`.
3. **Controller `GioHangController.cs`**:
   - `ThemGioHang(int ms, int soLuong = 1)`: Thêm sách vào giỏ (nếu đã có thì cộng dồn số lượng).
   - `Index()`: Hiển thị danh sách giỏ hàng.
   - `CapNhatSoLuong(int ms, int soLuong)`: Thay đổi số lượng mặt hàng. Nếu `soLuong <= 0` thì xóa.
   - `XoaGioHang(int ms)`: Xóa một mặt hàng khỏi giỏ.
   - `XoaTatCa()`: Làm rỗng giỏ hàng.
4. **View `Views/GioHang/Index.cshtml`**:
   - Sử dụng bảng hiển thị (`<table>` phong cách GridView).
   - Cột: STT, Ảnh bìa, Tên sách, Đơn giá, Số lượng (input có nút cập nhật), Thành tiền, Thao tác (nút Xóa).
   - Hàng tổng kết ở chân bảng: **Tổng số lượng cuốn** và **Tổng tiền thanh toán**.
   - Nút "Tiếp tục mua hàng" và nút **"Đặt hàng" / "Thanh toán"**.

### BƯỚC 8: XÂY DỰNG CHỨC NĂNG ĐẶT HÀNG (CHECKOUT)
1. **Kiểm tra đăng nhập**:
   - Khách hàng bắt buộc phải đăng nhập trước khi hoàn tất đặt hàng (`[Authorize]`).
   - Nếu chưa đăng nhập: Điều hướng tới trang Đăng nhập kèm tham số `returnUrl=/GioHang/DatHang`.
2. **Xử lý Đặt hàng (`HttpPost DatHang`)**:
   - Tạo bản ghi mới trong `DON_DAT_HANG`:
     - `Mkh`: Lấy từ thông tin đăng nhập của khách hàng hiện tại.
     - `NgayDat`: `DateTime.Now`.
     - `NgayGiao`: Ngày giao hẹn trước (ví dụ `DateTime.Now.AddDays(3)`).
     - `DaThanhToan`: `false`.
     - `TinhTrangGiaoHang`: 1 (Mới đặt).
   - Tạo các bản ghi trong `CT_DAT_HANG` tương ứng với từng `CartItem` trong giỏ hàng.
   - Cập nhật tăng `SoLuongBan` trong bảng `SACH`.
   - Sử dụng Transaction (`using var transaction = await _context.Database.BeginTransactionAsync()`) để đảm bảo toàn vẹn dữ liệu.
   - Xóa Session giỏ hàng sau khi lưu DB thành công.
   - Chuyển hướng sang trang thông báo đặt hàng thành công (`DatHangThanhCong.cshtml`).

### BƯỚC 9: XÂY DỰNG MODULE TÀI KHOẢN (ĐĂNG KÝ / ĐĂNG NHẬP / ĐĂNG XUẤT)
1. **ViewModels**: `LoginViewModel.cs`, `RegisterViewModel.cs`.
2. **Controller `AccountController.cs`**:
   - `Register`: Kiểm tra trùng tài khoản, validate dữ liệu đầu vào (Data Annotations), thêm khách hàng mới vào `KHACH_HANG`.
   - `Login`: Xác thực tài khoản & mật khẩu; tạo `ClaimsIdentity` gồm `Name`, `NameIdentifier`, `Email`; gọi `await HttpContext.SignInAsync(...)`.
   - `Logout`: Gọi `await HttpContext.SignOutAsync(...)`.

### BƯỚC 10: XÂY DỰNG TRANG THỬ NGHIỆM TRUY VẤN LINQ (QUERY DEMO)
*(Phục vụ hoàn thành 100% bài thực hành số 3 và đạt điểm tối đa)*
1. Tạo action `SachQuery/QueryDemo` với dropdown chọn các câu truy vấn:
   - Query 1: Lấy tất cả danh mục chủ đề.
   - Query 2: Lấy danh mục chủ đề có sách kèm số lượng và tổng lượt bán.
   - Query 3: Lấy danh mục sách kèm thông tin NXB.
   - Query 4: Lấy danh mục sách có thông tin NXB và Tác giả.
   - Query 5: Lấy danh mục sách mới cập nhật.
   - Query 7: Lấy danh mục sách bán chạy nhất.
   - Query 9: Lấy danh mục quảng cáo còn hạn.
2. Hiển thị kết quả động dưới dạng bảng Dictionary hoặc DTO Table như trong slide bài giảng.

---

## 7. BẢNG CHECKLIST NGHIỆM THU THEO BAREM ĐIỂM (10/10)

Trước khi bàn giao kết quả cho người dùng, Agent phải tự động kiểm tra và đánh dấu tích cho toàn bộ các mục:

- [ ] **Giao diện & Bố cục (1.0 đ)**:
  - Phối màu xanh lá trang nhã theo mẫu `Layout2.jpg`.
  - Responsive, căn chỉnh ngay ngắn, font chữ rõ ràng.
- [ ] **MasterPage / Layout (1.0 đ)**:
  - Banner hiển thị chuẩn hình ảnh `Nen_banner.jpg` và logo.
  - Menu ngang có đầy đủ: Trang chủ, Sách mới, Giới thiệu, Đăng ký, Đăng nhập.
  - Footer chứa đúng thông tin: `@2012 - Nhà sách trường Đại học công nghiệp Việt - Hung. Địa chỉ: 16 Xuân Khanh, Sơn Tây, Hà Nội. ĐT: 0983308081`.
- [ ] **Danh mục sách theo chủ đề (2.0 đ)**:
  - Sidebar hiển thị danh mục chủ đề có kèm đếm số lượng sách: `Tên chủ đề (X)`.
  - Click vào chủ đề tải đúng sách thuộc chủ đề đó.
  - Lưới sách hiển thị bìa, tên, tác giả, NXB, giá tiền định dạng VNĐ và nút "Chọn mua".
- [ ] **Sách mới / Sách bán chạy (1.0 đ)**:
  - Vùng sách mới hiển thị top 5 cuốn có tiêu đề nghệ thuật `tittle_sachmoi.gif` và icon mũi tên đỏ.
  - Tên sách có link chuyển hướng đến trang xem chi tiết sách.
- [ ] **Vùng Quảng cáo 1 & 2 (1.0 đ)**:
  - Quảng cáo 1: Random 3 banner mỗi lần F5 trang web.
  - Quảng cáo 2: Lấy đúng banner còn hạn từ bảng `QUANG_CAO` trên CSDL.
- [ ] **Thêm hàng & Widget giỏ hàng ở sidebar (2.0 đ)**:
  - Bấm "Chọn mua" thêm sản phẩm vào giỏ hàng mà không làm hỏng dữ liệu.
  - Box màu vàng ở góc phải sidebar hiển thị chính xác tổng tiền và tổng số cuốn sách trong giỏ.
- [ ] **Quản lý giỏ hàng chi tiết (1.0 đ)**:
  - Bảng danh sách giỏ hàng hiển thị đẹp, rõ ràng từng sản phẩm.
  - Có thể tăng/giảm số lượng, xóa từng sản phẩm hoặc xóa toàn bộ giỏ.
  - Dòng tổng tiền tự động tính lại chuẩn xác.
- [ ] **Cập nhật Đơn hàng vào CSDL (1.0 đ)**:
  - Bấm nút "Đặt hàng" lưu thành công vào bảng `DON_DAT_HANG` và `CT_DAT_HANG`.
  - Giỏ hàng được làm rỗng sau khi đặt thành công.

---

## 8. CÁC LỖI THƯỜNG GẶP CẦN TRÁNH TUYỆT ĐỐI (PITFALLS & MITIGATIONS)

1. **Lỗi đường dẫn ảnh tĩnh**:
   - Tên ảnh trong cột `Hinh_minh_hoa` của bảng `SACH` có thể chỉ chứa tên không có đuôi `.jpg` (ví dụ `THCB`, `TH004`). Cần kiểm tra chuỗi: nếu chưa có `.jpg` thì tự cộng thêm đuôi `.jpg` để tránh vỡ ảnh trên giao diện.
2. **Lỗi tuần hoàn quan hệ (Circular Reference JSON)**:
   - Khi scaffold EF Core, các entity sinh thuộc tính điều hướng hai chiều (`Sach -> ChuDe` và `ChuDe -> Sachs`). Tránh tuần tự hóa trực tiếp Entity sang JSON; luôn sử dụng ViewModel / DTO riêng hoặc dùng `CartItem` thuần túy.
3. **Lỗi đồng bộ/bất đồng bộ**:
   - Tuyệt đối không dùng `.Result` hoặc `.Wait()` gây nghẽn luồng (thread deadlock). Luôn dùng `await` và các hàm `ToListAsync()`, `FirstOrDefaultAsync()`.
4. **Lỗi mất giỏ hàng khi khởi động lại ứng dụng**:
   - Cần đảm bảo `app.UseSession()` được đặt **sau** `app.UseRouting()` và **trước** `app.UseAuthorization()`.

