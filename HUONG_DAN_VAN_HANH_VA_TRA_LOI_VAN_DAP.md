# CẨM NANG HƯỚNG DẪN VẬN HÀNH & CÁCH SỬA CODE TỪNG STEP (ĐIỂM 10.0 / 10.0)
**Môn học:** Phát triển ứng dụng Web nâng cao (PTWNC)  
**Đơn vị:** Trường Đại học Công nghiệp Việt - Hung (Khoa Công nghệ Thông tin)  
**Đề tài:** Website Nhà sách trực tuyến BookOnline (Chuẩn Barem 8 Steps đề bài)  
**Tệp Word nộp kèm:** `Huong_Dan_Van_Hanh_Va_Cach_Sua.docx`  
**Website đang chạy:** `http://localhost:5245`

---

## BẢNG TỔNG HỢP 8 STEPS: VẬN HÀNH (DEMO) & VỊ TRÍ SỬA CODE TRONG VISUAL STUDIO

| STEP | Tiêu chí chấm điểm | Thao tác Vận hành (Demo trước mặt thầy) | Vị trí Sửa Code trong Visual Studio |
| :---: | :--- | :--- | :--- |
| **STEP 1** | **Giao diện đẹp & Responsive (1.0 đ)** | Mở F12 co giãn mobile, xem nút tròn sát cạnh & ô badge to | `wwwroot/css/phuongnam-theme.css`<br>`Views/Shared/_Layout.cshtml` |
| **STEP 2** | **MasterPage 10 vùng (1.0 đ)** | Chỉ rõ 10 vùng trên trang chủ đúng sơ đồ Câu 1 | `Views/Shared/_Layout.cshtml` (dòng 138-180)<br>`Views/Home/Index.cshtml` |
| **STEP 3** | **Sách theo chủ đề & Tìm kiếm (2.0 đ)** | Bấm chủ đề cột trái, gõ từ khóa tiếng Việt trên header | `Controllers/SachController.cs`<br>`Views/Shared/_SachCardPartial.cshtml` |
| **STEP 4** | **Sách mới & Sách bán chạy (1.0 đ)** | Xem Sách mới cột giữa, Sách bán chạy cột phải, bấm xem chi tiết | `Controllers/HomeController.cs`<br>`ViewComponents/SachBanChayViewComponent.cs` |
| **STEP 5** | **Quảng cáo 1 & Quảng cáo 2 (1.0 đ)** | F5 xem đảo ngẫu nhiên 3 banner QC 1; xem banner CSDL còn hạn | `Controllers/HomeController.cs`<br>`ViewComponents/QuangCaoSidebarViewComponent.cs` |
| **STEP 6** | **Thêm giỏ & Hộp vàng (2.0 đ)** | Bấm nút Mua, hộp vàng cột phải cập nhật số lượng và tiền real-time | `Controllers/GioHangController.cs`<br>`ViewComponents/CartSummaryViewComponent.cs` |
| **STEP 7** | **Thay đổi giỏ (+/-, hủy) (1.0 đ)** | Bấm `[+]` tăng số lượng, nhập voucher `VIETHUNG`, bấm xóa món | `Controllers/GioHangController.cs`<br>`Views/GioHang/Index.cshtml` |
| **STEP 8** | **Cập nhật đơn hàng (1.0 đ)** | Bấm Đặt hàng, mở SQL Server xem bảng đơn hàng và tăng số lượng bán | `Controllers/GioHangController.cs` (Action `ThanhToan` dòng 246-295) |

---

## CHI TIẾT CÁCH VẬN HÀNH VÀ CÁCH SỬA CODE CHO TỪNG STEP

---

### STEP 1: Giao diện đẹp, hợp lý & Responsive (Thang điểm: 1,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1:** Mở trình duyệt `http://localhost:5245/` $\rightarrow$ Rê chuột qua các thẻ card sách để thấy hiệu ứng nâng thẻ 3D và bóng đổ sang trọng.
* **Thao tác 2:** Nhấn phím `F12` $\rightarrow$ Chọn chế độ giả lập điện thoại (Toggle Device Toolbar) $\rightarrow$ Đổi giữa iPhone, iPad và Laptop để thầy thấy hệ thống tự động co giãn từ 3 cột thành 1 cột mượt mà.
* **Thao tác 3:** Chỉ vào khối Banner Hero: Nút tròn mũi tên nằm sát mép biên (`left: 12px`, `right: 12px`), ô vuông Freeship lùi vào trong (`right: 68px`) và kích thước to rõ, không đè lên chữ.
* **Lời thoại giải thích:** *"Thưa thầy, giao diện nhóm em được lấy cảm hứng trực tiếp từ phong cách thương mại điện tử hiện đại của Nhà sách Phương Nam: tông màu Deep Navy và Vàng kim, co giãn đa thiết bị bằng CSS Grid và tối ưu hóa trải nghiệm người dùng."*

#### 2. Cách sửa code trong Visual Studio:
* **Vị trí File:** Mở `BookOnline` $\rightarrow$ `wwwroot` $\rightarrow$ `css` $\rightarrow$ `phuongnam-theme.css`:
  - **Nếu thầy bảo đổi màu chủ đạo của website:** Tìm dòng 6-15, đổi mã hex của biến `--pn-blue` (`#0a3b7b`) sang màu khác.
  - **Nếu thầy bảo điều chỉnh khoảng cách mũi tên:** Tìm dòng 435-460 (`.pn-hero-carousel .carousel-control-prev`), đổi `left: 12px` và `right: 12px`.
  - **Nếu thầy bảo chỉnh kích thước ô vuông badge:** Tìm dòng 424 (`.pn-hero-badge-float`), đổi `padding: 18px 28px` và `min-width: 125px`.

---

### STEP 2: Tạo MasterPage 10 vùng cấu trúc theo đề bài (Thang điểm: 1,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác:** Tại trang chủ, dùng chuột lướt từ trên xuống và chỉ lần lượt đủ 10 vùng chức năng:
  1. *Header & Logo* | 2. *Thanh Menu ngang* | 3. *Cột trái: Chủ đề sách* | 4. *Cột giữa: Sách mới (trên cùng)* | 5. *Cột giữa: QC 1 (ở giữa)* | 6. *Cột giữa: Nội dung chính (dưới cùng)* | 7. *Cột phải: Thông tin giỏ hàng* | 8. *Cột phải: QC 2* | 9. *Cột phải: Sách bán chạy* | 10. *Footer bản quyền*.
* **Lời thoại giải thích:** *"Thưa thầy, bố cục MasterPage tuân thủ 100% hình vẽ đề bài Câu 1: Cột giữa chỉ gồm Sách mới, Banner QC 1 và Lưới sách; Sách bán chạy nằm ở đáy cột bên phải; các khối được module hóa bằng ViewComponent độc lập."*

#### 2. Cách sửa code trong Visual Studio:
* **Vị trí File:** Mở `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ `_Layout.cshtml` (Dòng 138 – 180):
  - **Nếu thầy bảo 'Đổi chỗ Sách bán chạy lên trên Quảng cáo 2':** Mở dòng 173-177, cắt dòng `@await Component.InvokeAsync("SachBanChay")` đặt lên trên `@await Component.InvokeAsync("QuangCaoSidebar")` $\rightarrow$ Nhấn `Ctrl + S` $\rightarrow$ F5 trình duyệt là thấy đổi ngay!
  - **Nếu thầy bảo 'Thêm một mục Giới thiệu vào menu ngang':** Mở dòng 95-112, thêm thẻ `<li><a class="nav-link" href="...">Mục mới</a></li>`.

---

### STEP 3: Hiển thị danh mục sách theo chủ đề & Tìm kiếm (Thang điểm: 2,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1 (Lọc chủ đề):** Bấm vào mục *"Công nghệ thông tin (32)"* ở danh mục cột trái $\rightarrow$ Cột giữa chỉ hiển thị đúng các cuốn sách tin học.
* **Thao tác 2 (Tìm kiếm):** Gõ chữ *"công nghệ thông tin"* hoặc *"lập trình"* vào ô tìm kiếm trên Header $\rightarrow$ Nhấn Enter $\rightarrow$ Hệ thống lọc chính xác các đầu sách tương ứng.
* **Lời thoại giải thích:** *"Thưa thầy, các cuốn sách hiển thị dạng card DataList 3 cuốn/hàng. Thuật toán tìm kiếm sử dụng EF.Functions.Like trên cả tên sách và thể loại, hỗ trợ tiếng Việt có dấu và không dấu hoàn toàn chính xác."*

#### 2. Cách sửa code trong Visual Studio:
* **Vị trí File Controller:** Mở `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ `SachController.cs`:
  - **Nếu thầy bảo 'Đổi sắp xếp theo giá tăng dần thay vì mã giảm dần':** Tìm hàm `TheoChuDe`, đổi dòng `.OrderByDescending(s => s.Ms)` thành `.OrderBy(s => s.DonGia)`.
* **Vị trí File View:** Mở `BookOnline` $\rightarrow$ `Views` $\rightarrow$ `Shared` $\rightarrow$ `_SachCardPartial.cshtml`:
  - **Nếu thầy bảo 'Đổi màu nút Mua từ xanh sang đỏ':** Đổi class `btn-primary` thành `btn-danger`.

---

### STEP 4: Hiển thị Sách Mới (cột giữa) & Sách Bán Chạy (cột phải) (Thang điểm: 1,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1:** Chỉ vào khối *"SÁCH MỚI"* ở trên cùng cột giữa có icon mũi tên đỏ `muitendo.gif`, giá tiền và nút *"Chi tiết"*.
* **Thao tác 2:** Chỉ vào khối *"SÁCH BÁN CHẠY"* ở cột phải có huy hiệu Top 1-5 và số lượng đã bán thực tế từ CSDL.
* **Thao tác 3:** Bấm vào tên cuốn sách bất kỳ $\rightarrow$ Chuyển sang trang chi tiết với ảnh to, tác giả, NXB, mô tả tóm tắt và số lần xem được tự động tăng lên 1.
* **Lời thoại giải thích:** *"Thưa thầy, Sách mới lấy top 5 mã mới nhất; Sách bán chạy truy vấn top 5 cuốn có SoLuongBan cao nhất từ bảng SACH. Mỗi khi có khách xem sách, trường SoLanXem trong database tự động tăng lên 1."*

#### 2. Cách sửa code trong Visual Studio:
* **Sách mới:** Mở `BookOnline/Controllers/HomeController.cs` (Dòng 34): Đổi `.Take(5)` thành `.Take(8)` nếu thầy bảo hiện 8 cuốn.
* **Sách bán chạy:** Mở `BookOnline/ViewComponents/SachBanChayViewComponent.cs` (Dòng 23): Đổi `.Take(5)` thành `.Take(10)` nếu thầy bảo lấy top 10.
* **Chi tiết sách:** Mở `BookOnline/Controllers/SachController.cs` (Action `Details`), dòng `sach.SoLanXem = (sach.SoLanXem ?? 0) + 1;`.

---

### STEP 5: Hiển thị Quảng cáo 1 (Random) & Quảng cáo 2 (CSDL còn hạn) (Thang điểm: 1,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1:** Nhấn `F5` tải lại trang web 2-3 lần $\rightarrow$ 3 ảnh banner ở giữa cột giữa (QC 1) tự động đảo vị trí ngẫu nhiên.
* **Thao tác 2:** Chỉ sang cột phải (QC 2): Danh sách banner các đối tác được hiển thị đầy đủ thông tin từ bảng `QUANG_CAO`.
* **Lời thoại giải thích:** *"Thưa thầy, QC 1 sử dụng EF.Functions.Random() để ngẫu nhiên 3 ảnh banner mỗi lần load trang; QC 2 lấy từ bảng QUANG_CAO và có điều kiện NgayHetHan >= DateTime.Now để bảo đảm chỉ hiển thị quảng cáo còn hạn tài trợ."*

#### 2. Cách sửa code trong Visual Studio:
* **QC 1 (Random):** Mở `BookOnline/Controllers/HomeController.cs` (Dòng 50): Đổi `.Take(3)` thành `.Take(4)` nếu thầy bảo hiển thị 4 banner.
* **QC 2 (Còn hạn):** Mở `BookOnline/ViewComponents/QuangCaoSidebarViewComponent.cs` (Dòng 22): Nếu thầy bảo *"Hiển thị tất cả quảng cáo kể cả hết hạn"*, xóa điều kiện `.Where(...)`.

---

### STEP 6: Thêm hàng vào giỏ & Hiển thị tóm tắt giỏ hàng (Thang điểm: 2,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1:** Bấm nút *"Mua"* ở bất kỳ cuốn sách nào $\rightarrow$ Thông báo Toast xanh xuất hiện ở góc màn hình.
* **Thao tác 2:** Nhìn sang Hộp vàng ở cột bên phải: Số lượng tự động nhảy từ 0 lên 1, tổng tiền tự nhảy đúng bằng giá sách ngay tức thì mà không cần reload trang.
* **Lời thoại giải thích:** *"Thưa thầy, giỏ hàng được quản lý trong Session State bằng JSON. Khi bấm mua, JavaScript gửi AJAX ngầm đến ThemVaoGioAjax giúp hộp vàng cập nhật số lượng và tổng tiền real-time."*

#### 2. Cách sửa code trong Visual Studio:
* **Model:** Mở `BookOnline/Models/CartItem.cs` (Chứa `Ms`, `TenSach`, `DonGia`, `SoLuong`, `ThanhTien`).
* **Controller:** Mở `BookOnline/Controllers/GioHangController.cs` $\rightarrow$ Action `ThemVaoGioAjax` (Dòng 100 – 145).
* **View Hộp vàng:** Mở `BookOnline/Views/Shared/Components/CartSummary/Default.cshtml`.
  - Nếu thầy bảo đổi màu nền hộp vàng sang xanh lá: Mở `phuongnam-theme.css`, tìm `.pn-cart-box`, đổi background sang `#e8f5e9`.

---

### STEP 7: Thay đổi thông tin giỏ hàng (+/-, hủy món, tổng tiền) (Thang điểm: 1,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1:** Mở trang giỏ hàng `http://localhost:5245/GioHang` $\rightarrow$ Bấm nút `[+]` tăng từ 1 lên 3 cuốn $\rightarrow$ Thành tiền và Tổng tiền tự nhân 3 ngay tức thì qua AJAX không làm nhấp nháy màn hình.
* **Thao tác 2:** Bấm nút gợi ý voucher `VIETHUNG (-20%)` $\rightarrow$ Dòng giảm giá hiện `-15,600 đ` và tiền tự trừ đi.
* **Thao tác 3:** Bấm icon thùng rác đỏ để xóa cuốn sách khỏi giỏ hàng.
* **Lời thoại giải thích:** *"Thưa thầy, bảng giỏ hàng GridView có tính năng Real-time AJAX: bấm tăng giảm hệ thống tự nhân tiền trên giao diện và gọi ngầm API CapNhatSoLuongAjax để đồng bộ Session an toàn."*

#### 2. Cách sửa code trong Visual Studio:
* **Controller:** Mở `BookOnline/Controllers/GioHangController.cs` $\rightarrow$ Action `CapNhatSoLuongAjax` (Dòng 165 – 200):
  - **Nếu thầy bảo 'Giới hạn số lượng mua tối đa là 10 cuốn':** Đổi dòng `Math.Min(99, soLuong)` thành `Math.Min(10, soLuong)`.
  - **Nếu thầy bảo 'Thêm voucher GIAM30K giảm 30.000đ':** Mở hàm `CalculateDiscount`, thêm nhánh `else if (voucher == "GIAM30K") discount = 30000;`.

---

### STEP 8: Cập nhật đơn hàng vào CSDL với Transaction ACID (Thang điểm: 1,0 đ)

#### 1. Cách vận hành & Kịch bản Demo trước mặt giảng viên:
* **Thao tác 1:** Đăng nhập tài khoản `vanan2026` / mật khẩu `123456`.
* **Thao tác 2:** Vào Giỏ hàng bấm nút *"TIẾN HÀNH ĐẶT HÀNG"* $\rightarrow$ Bấm OK xác nhận.
* **Thao tác 3:** Màn hình chuyển sang trang *"ĐẶT HÀNG THÀNH CÔNG!"* với Mã đơn hàng `#Sdh`; giỏ hàng được dọn sạch về 0.
* **Thao tác 4:** Mở SQL Server Management Studio:
  - Chạy `SELECT * FROM DON_DAT_HANG` và `SELECT * FROM CT_DAT_HANG` để thầy thấy hóa đơn mới được lưu nguyên vẹn.
  - Chạy `SELECT Ms, TenSach, SoLuongBan FROM SACH` để thầy thấy số lượng bán của cuốn sách vừa đặt đã tự tăng lên tương ứng.
* **Lời thoại giải thích:** *"Thưa thầy, quy trình đặt hàng liên quan đồng thời 3 bảng. Bắt buộc sử dụng Database.BeginTransactionAsync() để bảo đảm nguyên tắc ACID: hoặc tất cả cùng thành công (Commit), hoặc nếu xảy ra sự cố sẽ hoàn tác sạch sẽ (Rollback), ngăn chặn triệt để việc sinh đơn hàng rác."*

#### 2. Cách sửa code trong Visual Studio:
* **Vị trí File:** Mở `BookOnline` $\rightarrow$ `Controllers` $\rightarrow$ `GioHangController.cs` $\rightarrow$ Action `ThanhToan()` (Dòng 246 – 295):
  - **Thầy bảo: 'Chỉ ra chỗ Transaction?':** Chỉ vào `using var transaction = await _context.Database.BeginTransactionAsync();` và `await transaction.CommitAsync();`.
  - **Thầy bảo: 'Chỗ nào tăng số lượng bán của sách?':** Chỉ vào dòng `sach.SoLuongBan = (sach.SoLuongBan ?? 0) + item.SoLuong;`.
  - **Thầy bảo: 'Nếu muốn cộng thêm phí vận chuyển 30.000đ vào đơn hàng thì sửa ở đâu?':** Sửa dòng `TriGia = finalTotal` thành `TriGia = finalTotal + 30000;`.
