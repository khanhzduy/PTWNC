using System.ComponentModel.DataAnnotations;

namespace BookOnline.ViewModels;

/// <summary>
/// ViewModel phục vụ chức năng Đăng nhập tài khoản.
/// Đảm bảo dữ liệu đầu vào được kiểm tra hợp lệ trước khi truy vấn Cơ sở dữ liệu.
/// </summary>
public class LoginViewModel
{
    // Bắt buộc nhập tên đăng nhập hoặc email; giới hạn tối đa 50 ký tự theo độ dài cột Email/TenDangNhap trong CSDL
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập hoặc email.")]
    [StringLength(50, ErrorMessage = "Tên đăng nhập hoặc email tối đa 50 ký tự.")]
    [Display(Name = "Tên đăng nhập / Email")]
    public string TaiKhoan { get; set; } = string.Empty;

    // Bắt buộc nhập mật khẩu; giới hạn tối đa 15 ký tự khớp với cột Mat_khau (nvarchar(15)) trong bảng KHACH_HANG
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [StringLength(15, MinimumLength = 1, ErrorMessage = "Mật khẩu tối đa 15 ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string MatKhau { get; set; } = string.Empty;

    // Lưu lại URL trang người dùng đang xem dở trước khi bị yêu cầu đăng nhập (ví dụ: giỏ hàng, thanh toán)
    public string? ReturnUrl { get; set; }
}

/// <summary>
/// ViewModel phục vụ chức năng Đăng ký tài khoản thành viên mới.
/// Mọi ràng buộc Validation (StringLength, RegularExpression) đều được thiết kế khớp 100%
/// với cấu trúc các cột trong bảng KHACH_HANG của CSDL SQL Server (tránh lỗi String or binary data would be truncated).
/// </summary>
public class RegisterViewModel
{
    // 1. Họ và tên: Cột Ho_ten trong CSDL là nvarchar(50), không được để trống
    // Cho phép nhập họ tên tiếng Việt có dấu hoặc không dấu mượt mà (độ dài từ 2 đến 50 ký tự)
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ và tên phải từ 2 đến 50 ký tự.")]
    [Display(Name = "Họ và tên")]
    public string HoTen { get; set; } = string.Empty;

    // 2. Tên đăng nhập: Cột Ten_dang_nhap trong CSDL là nvarchar(15), không được để trống
    // Biểu thức Regex ^[a-zA-Z0-9_]+$ chỉ cho phép chữ cái không dấu, chữ số và dấu gạch dưới (chuẩn username an toàn)
    [Required(ErrorMessage = "Vui lòng nhập tên tài khoản.")]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Tên tài khoản từ 3 đến 15 ký tự.")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Tên tài khoản chỉ được chứa chữ cái không dấu (a-z), số (0-9) và dấu gạch dưới (_).")]
    [Display(Name = "Tên đăng nhập")]
    public string TaiKhoan { get; set; } = string.Empty;

    // 3. Mật khẩu: Cột Mat_khau trong CSDL là nvarchar(15), không được để trống
    // Giới hạn an toàn từ 6 đến 15 ký tự để người dùng không nhập vượt quá kích thước cột của database
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [StringLength(15, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 15 ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string MatKhau { get; set; } = string.Empty;

    // 4. Xác nhận mật khẩu: Thuộc tính Compare đảm bảo người dùng gõ lại chính xác mật khẩu ở trên
    [Required(ErrorMessage = "Vui lòng xác nhận lại mật khẩu.")]
    [DataType(DataType.Password)]
    [Display(Name = "Xác nhận mật khẩu")]
    [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    public string XacNhanMatKhau { get; set; } = string.Empty;

    // 5. Email: Cột Email trong CSDL là nvarchar(50)
    // Thuộc tính EmailAddress kiểm tra cú pháp email hợp lệ (có @ và tên miền)
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ email.")]
    [StringLength(50, ErrorMessage = "Email tối đa 50 ký tự.")]
    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ (Ví dụ: sinhvien@gmail.com).")]
    [Display(Name = "Địa chỉ Email")]
    public string Email { get; set; } = string.Empty;

    // 6. Số điện thoại: Cột Dien_thoai trong CSDL là nvarchar(10)
    // Biểu thức Regex ^0\d{9}$ kiểm tra đúng quy chuẩn số điện thoại di động Việt Nam: 10 chữ số và bắt đầu bằng số 0
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải gồm đúng 10 chữ số.")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải gồm đúng 10 chữ số và bắt đầu bằng số 0 (Ví dụ: 0983308081).")]
    [Display(Name = "Số điện thoại")]
    public string? DienThoai { get; set; }

    // 7. Địa chỉ: Cột Dia_chi trong CSDL là nvarchar(50)
    [StringLength(50, ErrorMessage = "Địa chỉ tối đa 50 ký tự.")]
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    // 8. Ngày sinh: Cột Ngay_sinh trong CSDL là datetime (nullable)
    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    public DateTime? NgaySinh { get; set; }

    // =========================================================================================
    // 9. GIỚI TÍNH: Cột Gioi_tinh trong CSDL SQL Server là kiểu bit NOT NULL (1 = Nam, 0 = Nữ).
    // GHI CHÚ DÀNH CHO BÁO CÁO / GIẢNG VIÊN HỎI:
    // - Trạng thái hiện tại: Đã đặt mặc định là "Nam" (= "Nam";).
    // - Khi thầy hỏi: "Muốn đổi mặc định từ Nam sang Nữ thì vào đâu sửa?":
    //   -> Sửa ngay tại dòng 101 bên dưới: public string? GioiTinh { get; set; } = "Nữ";
    // - Khi thầy hỏi: "Muốn không đặt mặc định ô nào (để người dùng tự chọn)?":
    //   -> Sửa thành: public string? GioiTinh { get; set; }
    // =========================================================================================
    [Display(Name = "Giới tính")]
    public string? GioiTinh { get; set; } = "Nam";
}
