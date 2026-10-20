using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Models;
using BookOnline.ViewModels;

namespace BookOnline.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // =========================================================================
    // 1. CHỨC NĂNG ĐĂNG NHẬP (LOGIN)
    // =========================================================================

    /// <summary>
    /// GET: /Account/Login
    /// Hiển thị giao diện form đăng nhập cho người dùng.
    /// </summary>
    /// <param name="returnUrl">Đường dẫn trang người dùng muốn truy cập trước khi bị chuyển hướng tới đây.</param>
    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        // Lưu lại ReturnUrl vào ViewData để truyền vào form ẩn (hidden input)
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    /// <summary>
    /// POST: /Account/Login
    /// Xử lý logic xác thực danh tính người dùng bằng Claims và Cookie Authentication.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken] // Chống tấn công giả mạo yêu cầu chéo trang (CSRF Attack)
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // [Bước 1]: Kiểm tra các ràng buộc dữ liệu đầu vào (Data Annotations trong ViewModel)
        if (!ModelState.IsValid)
        {
            return View(model); // Trả về form cùng các thông báo lỗi nếu không hợp lệ
        }

        // [Bước 2]: Cắt bỏ khoảng trắng thừa ở đầu và cuối chuỗi người dùng nhập vào
        var taiKhoanInput = model.TaiKhoan.Trim();
        var matKhauInput = model.MatKhau.Trim();

        // [Bước 3]: Truy vấn bảng KHACH_HANG trong CSDL SQL Server (không phân biệt hoa thường)
        // Cho phép người dùng đăng nhập bằng cả Tên tài khoản HOẶC Địa chỉ Email
        var user = await _context.KhachHangs
            .FirstOrDefaultAsync(u => 
                (u.TenDangNhap.ToLower() == taiKhoanInput.ToLower() || (u.Email != null && u.Email.ToLower() == taiKhoanInput.ToLower())) 
                && u.MatKhau == matKhauInput);

        // [Bước 4]: Nếu không tìm thấy bản ghi trùng khớp -> Báo lỗi cho người dùng
        if (user == null)
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác. Vui lòng thử lại.");
            return View(model);
        }

        // [Bước 5]: Thiết lập danh sách các thông tin nhận dạng (Claims) cho phiên làm việc
        var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Mkh.ToString()), // ID khách hàng (Mkh)
            new System.Security.Claims.Claim(ClaimTypes.Name, user.HoTen ?? user.TenDangNhap ?? "Khách hàng"), // Họ tên hiển thị
            new System.Security.Claims.Claim(ClaimTypes.Email, user.Email ?? ""), // Email khách hàng
            new System.Security.Claims.Claim(ClaimTypes.Role, (user.TenDangNhap?.ToLower() == "admin") ? "Admin" : "Customer") // Vai trò
        };

        // [Bước 6]: Khởi tạo định danh ClaimsIdentity dựa trên CookieAuthenticationDefaults
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // [Bước 7]: Cấu hình phiên đăng nhập (ghi nhớ đăng nhập trong 7 ngày)
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Duy trì cookie đăng nhập ngay cả khi đóng trình duyệt
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // Hạn sử dụng cookie
        };

        // [Bước 8]: Thực hiện cấp phát vé đăng nhập (SignInAsync) lưu vào Cookie trình duyệt
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        // [Bước 9]: Hiển thị thông báo chào mừng thông qua TempData Flash Message
        TempData["SuccessMessage"] = $"Đăng nhập thành công! Chào mừng {user.HoTen} đã quay trở lại.";

        // [Bước 10]: Điều hướng người dùng về trang họ yêu cầu trước đó (nếu có và hợp lệ), hoặc về Trang chủ
        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }
    // 2. CHỨC NĂNG ĐĂNG KÝ TÀI KHOẢN (REGISTER)
    /// <summary>
    /// GET: /Account/Register
    /// Hiển thị giao diện form đăng ký tài khoản thành viên.
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// POST: /Account/Register
    /// Xử lý logic tạo tài khoản mới: chuẩn hóa dữ liệu, kiểm tra trùng lặp và lưu vào CSDL.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken] // Chống CSRF Attack
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // [Bước 1]: Kiểm tra tính hợp lệ của ViewModel (Regex, StringLength, Required, Compare)
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // [Bước 2]: Chuẩn hóa và làm sạch dữ liệu đầu vào (cắt bỏ khoảng trắng)
        var hoTen = model.HoTen.Trim();
        var taiKhoan = model.TaiKhoan.Trim();
        var email = model.Email.Trim();
        var matKhau = model.MatKhau.Trim();
        var dienThoai = string.IsNullOrWhiteSpace(model.DienThoai) ? null : model.DienThoai.Trim();
        var diaChi = string.IsNullOrWhiteSpace(model.DiaChi) ? null : model.DiaChi.Trim();

        // [Bước 3]: Kiểm tra nghiệp vụ Ngày sinh (không được vượt quá ngày hiện tại)
        if (model.NgaySinh.HasValue && model.NgaySinh.Value.Date > DateTime.Today)
        {
            ModelState.AddModelError("NgaySinh", "Ngày sinh không được vượt quá ngày hiện tại.");
            return View(model);
        }

        // [Bước 4]: Kiểm tra trùng lặp Tên tài khoản trong bảng KHACH_HANG
        var usernameExists = await _context.KhachHangs
            .AnyAsync(u => u.TenDangNhap.ToLower() == taiKhoan.ToLower());
        if (usernameExists)
        {
            ModelState.AddModelError("TaiKhoan", "Tên tài khoản này đã được sử dụng. Vui lòng chọn tên khác.");
            return View(model);
        }

        // [Bước 5]: Kiểm tra trùng lặp Email trong bảng KHACH_HANG
        var emailExists = await _context.KhachHangs
            .AnyAsync(u => u.Email != null && u.Email.ToLower() == email.ToLower());
        if (emailExists)
        {
            ModelState.AddModelError("Email", "Địa chỉ Email này đã được đăng ký cho một tài khoản khác.");
            return View(model);
        }

        // [Bước 6]: Kiểm tra trùng lặp Số điện thoại (nếu khách hàng có cung cấp)
        if (!string.IsNullOrEmpty(dienThoai))
        {
            var phoneExists = await _context.KhachHangs
                .AnyAsync(u => u.DienThoai == dienThoai);
            if (phoneExists)
            {
                ModelState.AddModelError("DienThoai", "Số điện thoại này đã được đăng ký trên hệ thống.");
                return View(model);
            }
        }

        // [Bước 7]: Thực hiện thêm khách hàng vào CSDL với khối bảo vệ try-catch
        try
        {
            // Khởi tạo đối tượng Entity Model KhachHang với các kiểu dữ liệu khớp bảng KHACH_HANG
            var khachHang = new KhachHang
            {
                HoTen = hoTen,                                 // nvarchar(50)
                TenDangNhap = taiKhoan,                       // nvarchar(15)
                MatKhau = matKhau,                           // nvarchar(15)
                Email = email,                               // nvarchar(50)
                DiaChi = diaChi,                             // nvarchar(50)
                DienThoai = dienThoai,                       // nvarchar(10)
                NgaySinh = model.NgaySinh,                   // datetime
                // =====================================================================================
                // XỬ LÝ LƯU GIỚI TÍNH VÀO CSDL (Bảng KHACH_HANG, cột Gioi_tinh kiểu bit NOT NULL default ((0))):
                // - Khách chọn "Nam" -> Lưu true  (giá trị bit 1 trong SQL Server)
                // - Khách chọn "Nữ"  -> Lưu false (giá trị bit 0 trong SQL Server)
                // =====================================================================================
                GioiTinh = (model.GioiTinh == "Nam")
            };

            // Thêm vào DbContext và ghi dữ liệu xuống SQL Server
            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            // [Bước 8]: Trải nghiệm người dùng cao cấp - Tự động đăng nhập ngay sau khi đăng ký
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, khachHang.Mkh.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Name, khachHang.HoTen),
                new System.Security.Claims.Claim(ClaimTypes.Email, khachHang.Email ?? ""),
                new System.Security.Claims.Claim(ClaimTypes.Role, "Customer")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) });

            TempData["SuccessMessage"] = $"Chúc mừng {khachHang.HoTen} đã đăng ký tài khoản thành công!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            // Bắt lỗi ngoại lệ và báo lại cho người dùng, ngăn ngừa trang trắng lỗi 500
            ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu thông tin tài khoản. Vui lòng thử lại. Lỗi: " + ex.Message);
            return View(model);
        }
    }

    // =========================================================================
    // 3. CHỨC NĂNG ĐĂNG XUẤT (LOGOUT)
    // =========================================================================

    /// <summary>
    /// POST: /Account/Logout
    /// Hủy bỏ phiên làm việc và xóa cookie xác thực trên trình duyệt.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        TempData["SuccessMessage"] = "Đã đăng xuất khỏi hệ thống thành công.";
        return RedirectToAction("Index", "Home");
    }

    // 4. Access Denied truy cập bị từ chối 
    public IActionResult AccessDenied()
    {
        return View();
    }
}
