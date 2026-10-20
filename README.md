# 📚 BookOnline - Hệ Thống Website Bán Sách Trực Tuyến

> **Đồ án môn học:** Phát Triển Web Nâng Cao (PTWNC)  
> **Sinh viên thực hiện:** Phan Danh Khánh  
> **Mã số sinh viên:** 2300371  
> **Nền tảng:** ASP.NET Core MVC (.NET 10.0) & Entity Framework Core

---

## 📌 Giới thiệu dự án

**BookOnline** là một ứng dụng web thương mại điện tử chuyên cung cấp và kinh doanh sách trực tuyến, được xây dựng trên nền tảng **ASP.NET Core (.NET 10)** theo mô hình kiến trúc **MVC (Model-View-Controller)** chuẩn mực và mạnh mẽ.

Dự án mô phỏng đầy đủ quy trình mua sắm sách trực tuyến từ duyệt danh mục, tìm kiếm, xem chi tiết, quản lý giỏ hàng, xác thực khách hàng đến đặt hàng và lưu trữ thông tin đơn hàng vào hệ quản trị cơ sở dữ liệu **Microsoft SQL Server**.

---

## 🚀 Công nghệ sử dụng (Tech Stack)

| Thành phần | Công nghệ / Thư viện | Mô tả |
| :--- | :--- | :--- |
| **Framework** | **.NET 10.0** (ASP.NET Core Web App MVC) | Framework backend hiện đại, hiệu năng cao |
| **Ngôn ngữ** | **C#** | Ngôn ngữ lập trình hướng đối tượng chủ đạo |
| **ORM / Data Access** | **Entity Framework Core 10.0** | Tương tác dữ liệu với LINQ, Database-First/Code-First |
| **Database** | **Microsoft SQL Server** | Cơ sở dữ liệu quan hệ `QLBanSach` |
| **Authentication** | **Cookie Authentication** | Xác thực đăng nhập an toàn, lưu phiên người dùng |
| **Session State** | **In-Memory Distributed Session** | Quản lý trạng thái giỏ hàng xuyên suốt phiên làm việc |
| **Giao diện (Frontend)** | **Razor Views (`.cshtml`), Bootstrap 5, CSS3, JS** | Thiết kế phong cách trang nhã, tương thích đa thiết bị |
| **Tái sử dụng giao diện** | **ViewComponents & Partial Views** | Modular hóa menu chủ đề, giỏ hàng, sách bán chạy, quảng cáo |

---

## ✨ Các chức năng chính

### 1. Phân hệ Khách hàng & Mua sắm
* **Trang chủ (`Home/Index`):**
  * Hiển thị danh mục sách mới nhất, phân trang trực quan.
  * Sidebar danh mục chủ đề (`ChuDeMenuViewComponent`).
  * Vùng sách bán chạy (`SachBanChayViewComponent`).
  * Vùng quảng cáo động (`QuangCaoSidebarViewComponent`): Tự động lọc các banner còn thời hạn (`NgayBatDau <= Now <= NgayKetThuc`), hỗ trợ các banner GIF sinh động.
* **Chi tiết sách (`Sach/Details`):**
  * Hiển thị đầy đủ thông tin: Ảnh bìa, Tên sách, Giá bán, Tác giả, Nhà xuất bản, Mô tả tóm tắt.
  * Thêm sách trực tiếp vào giỏ hàng.
* **Tìm kiếm & Lọc sách (`Sach/Search`, `Sach/TheoChuDe`):**
  * Tìm kiếm sách theo từ khóa tên sách.
  * Lọc danh sách theo từng chủ đề / thể loại.

### 2. Phân hệ Giỏ hàng (`GioHangController`)
* Thêm sách vào giỏ hàng từ trang chủ hoặc trang chi tiết.
* Xem chi tiết giỏ hàng: Cập nhật số lượng, tính thành tiền theo từng đầu sách, tính tổng tiền đơn hàng.
* Xóa từng sản phẩm hoặc làm trống toàn bộ giỏ hàng.
* Widget tóm tắt giỏ hàng (`CartSummaryViewComponent`) hiển thị số lượng tức thời trên thanh menu.

### 3. Phân hệ Tài khoản (`AccountController`)
* **Đăng ký (`Account/Register`):** Kiểm tra trùng tên đăng nhập, lưu thông tin khách hàng vào bảng `KhachHang`.
* **Đăng nhập (`Account/Login`):** Xác thực tài khoản với Cookie Authentication (`ClaimsPrincipal`).
* **Đăng xuất (`Account/Logout`):** Hủy cookie và làm sạch phiên làm việc an toàn.

### 4. Phân hệ Đặt hàng (`GioHang/DatHang`)
* Yêu cầu khách hàng đăng nhập trước khi tiến hành thanh toán.
* Tự động lấy thông tin người nhận từ hồ sơ khách hàng hoặc cho phép cập nhật thông tin giao hàng.
* Lưu thông tin đơn đặt hàng vào bảng `DonDatHang`.
* Lưu chi tiết từng sản phẩm đặt mua vào bảng `CtDatHang`.
* Cập nhật số lượng tồn kho và làm trống giỏ hàng sau khi đặt thành công.

---

## 📂 Cấu trúc thư mục dự án

```text
PTWNC/
│
├── BookOnline/                      # Mã nguồn chính của dự án ASP.NET Core
│   ├── Controllers/                 # Điều hướng logic (Home, Sach, GioHang, Account...)
│   │   ├── AccountController.cs
│   │   ├── GioHangController.cs
│   │   ├── HomeController.cs
│   │   ├── SachController.cs
│   │   └── SachQueryController.cs
│   ├── Models/                      # Thực thể dữ liệu & EF Core DbContext
│   │   ├── AppDbContext.cs
│   │   ├── Sach.cs
│   │   ├── ChuDe.cs
│   │   ├── NhaXuatBan.cs
│   │   ├── KhachHang.cs
│   │   ├── DonDatHang.cs
│   │   ├── CtDatHang.cs
│   │   └── QuangCao.cs
│   ├── ViewComponents/              # Các component tái sử dụng độc lập
│   │   ├── CartSummaryViewComponent.cs
│   │   ├── ChuDeMenuViewComponent.cs
│   │   ├── QuangCaoSidebarViewComponent.cs
│   │   └── SachBanChayViewComponent.cs
│   ├── Views/                       # Giao diện Razor Pages (.cshtml)
│   │   ├── Account/
│   │   ├── GioHang/
│   │   ├── Home/
│   │   ├── Sach/
│   │   └── Shared/
│   ├── wwwroot/                     # Tài nguyên tĩnh (CSS, JS, Hình ảnh sách & quảng cáo)
│   ├── appsettings.json             # File cấu hình chuỗi kết nối CSDL
│   ├── Program.cs                   # Điểm khởi chạy & đăng ký Middleware / Service
│   └── BookOnline.csproj            # File cấu hình dependencies .NET
│
├── BaiTapLon/                       # Tài nguyên cơ sở dữ liệu đính kèm
│   ├── CSDL/
│   │   └── QLBanSach.bak            # File sao lưu cơ sở dữ liệu SQL Server
│   └── Images/                      # Thư viện ảnh gốc
│
├── BookOnline.slnx                  # Solution file Visual Studio
├── .gitignore                       # Cấu hình bỏ qua file biên dịch, cache và tài liệu
└── README.md                        # Hướng dẫn chi tiết dự án
```

---

## 🛠️ Hướng dẫn cài đặt & khởi chạy

### 1. Yêu cầu môi trường
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) trở lên.
* [Microsoft SQL Server](https://www.microsoft.com/sql-server/) (phiên bản 2019/2022 hoặc SQL Server Express).
* [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) hoặc Azure Data Studio.
* IDE: Visual Studio 2022+ / Visual Studio Code / JetBrains Rider.

---

### 2. Phục hồi cơ sở dữ liệu (Restore Database)
1. Mở **SQL Server Management Studio (SSMS)** và kết nối tới máy chủ SQL của bạn.
2. Nhấp chuột phải vào mục **Databases** ➔ Chọn **Restore Database...**
3. Chọn tùy chọn **Device** ➔ Nhấn nút `...` và bấm **Add**.
4. Duyệt tới file sao lưu trong project:
   ```text
   PTWNC/BaiTapLon/CSDL/QLBanSach.bak
   ```
5. Đặt tên cơ sở dữ liệu là `QLBanSach` và nhấn **OK** để tiến hành phục hồi.

---

### 3. Cấu hình chuỗi kết nối
Mở file `BookOnline/appsettings.json` và điều chỉnh chuỗi kết nối cho phù hợp với máy của bạn:

```json
{
  "ConnectionStrings": {
    "QLBanSachConnection": "Server=.\\SQLEXPRESS;Database=QLBanSach;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
> *Lưu ý: Nếu sử dụng SQL Server LocalDB hoặc tài khoản `sa`, hãy thay đổi giá trị `Server` và thông tin chứng thực tương ứng.*

---

### 4. Khởi chạy ứng dụng

#### Sử dụng Command Line (Terminal / PowerShell):
```powershell
# Chuyển vào thư mục dự án
cd BookOnline

# Khôi phục dependencies và build
dotnet build

# Chạy ứng dụng
dotnet run
```

Sau khi chạy, truy cập đường dẫn xuất hiện trên console (thông thường là `http://localhost:5000` hoặc `https://localhost:7001`).

#### Sử dụng Visual Studio:
1. Mở file `BookOnline.slnx` bằng Visual Studio.
2. Chọn project `BookOnline` làm dự án khởi động (Set as Startup Project).
3. Nhấn phím `F5` hoặc nút **Run / IIS Express** để trải nghiệm ứng dụng.

---

## 👤 Thông tin liên hệ & Bản quyền
* **Sinh viên:** Phan Danh Khánh
* **MSSV:** 2300371
* **Email:** khanhzduy@gmail.com
* **GitHub Repository:** [khanhzduy/PTWNC](https://github.com/khanhzduy/PTWNC)
