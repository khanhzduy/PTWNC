using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Helpers;
using BookOnline.Models;
using BookOnline.ViewModels;

namespace BookOnline.Controllers;

public class GioHangController : Controller
{
    private const string CART_SESSION_KEY = "Cart";
    private const string VOUCHER_SESSION_KEY = "Cart_Voucher";
    private readonly AppDbContext _context;

    public GioHangController(AppDbContext context)
    {
        _context = context;
    }

    // =========================================================================
    // CÁC HÀM TIỆN ÍCH QUẢN LÝ SESSION GIỎ HÀNG & MÃ GIẢM GIÁ (STATE MANAGEMENT)
    // =========================================================================

    /// <summary>
    /// Lấy danh sách sản phẩm trong giỏ hàng từ Session.
    /// Dữ liệu được giải tuần tự hóa từ chuỗi JSON lưu trong HttpContext.Session.
    /// </summary>
    private List<CartItem> GetCart()
    {
        return HttpContext.Session.GetObjectFromJson<List<CartItem>>(CART_SESSION_KEY) ?? new List<CartItem>();
    }

    /// <summary>
    /// Lưu danh sách sản phẩm trong giỏ hàng vào Session dưới dạng chuỗi JSON.
    /// </summary>
    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetObjectAsJson(CART_SESSION_KEY, cart);
    }

    /// <summary>
    /// Tính toán số tiền giảm giá dựa trên mã Voucher và Tổng tiền giỏ hàng (Tạm tính)
    /// </summary>
    private decimal CalculateDiscount(string? voucherCode, decimal subtotal)
    {
        if (string.IsNullOrWhiteSpace(voucherCode) || subtotal <= 0)
            return 0;

        voucherCode = voucherCode.Trim().ToUpper();
        switch (voucherCode)
        {
            case "VIETHUNG":
            case "VHU":
                // Giảm 20% tổng đơn hàng cho sinh viên ĐHCN Việt - Hung
                return Math.Round(subtotal * 0.20m, 0);

            case "PN10":
            case "GIAM10":
                // Giảm 10% tổng đơn hàng
                return Math.Round(subtotal * 0.10m, 0);

            case "FREESHIP":
                // Giảm 20.000 VNĐ tương đương phí giao hàng
                return Math.Min(subtotal, 20000m);

            case "BANMOI":
                // Giảm 15.000 VNĐ cho đơn hàng thành viên mới
                return Math.Min(subtotal, 15000m);

            case "TRIENTRIC":
                // Giảm 30.000 VNĐ cho đơn hàng từ 100.000 VNĐ trở lên
                return (subtotal >= 100000m) ? 30000m : 0;

            case "FLASH50":
                return (subtotal >= 200000m) ? 50000m : 0;

            case "CUOITUAN":
                return Math.Round(subtotal * 0.15m, 0);

            default:
                return 0;
        }
    }

    // =========================================================================
    // 1. CHỨC NĂNG THÊM SÁCH VÀO GIỎ HÀNG
    // =========================================================================

    /// <summary>
    /// POST: /GioHang/ThemGioHang
    /// Nhận mã sách và số lượng mua, đưa vào giỏ hàng Session.
    /// Hỗ trợ cả AJAX (ở lại trang hiện tại và cập nhật badge tức thì) và Form Submit thông thường.
    /// </summary>
    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> ThemGioHang(int ms, int soLuong = 1, string? returnUrl = null)
    {
        // [Bước 1]: Kiểm tra cuốn sách có tồn tại trong CSDL SQL Server hay không
        var sach = await _context.Saches.FindAsync(ms);
        if (sach == null)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = false, message = "Không tìm thấy cuốn sách này!" });
            }
            return NotFound();
        }

        // [Bước 2]: Lấy giỏ hàng hiện tại ra từ Session
        var cart = GetCart();

        // [Bước 3]: Kiểm tra xem cuốn sách này đã có sẵn trong giỏ hàng hay chưa
        var item = cart.FirstOrDefault(c => c.Ms == ms);
        var qtyToAdd = Math.Max(1, soLuong);
        if (item != null)
        {
            // Nếu đã có: Tăng thêm số lượng (tối đa 99)
            item.SoLuong = Math.Min(99, item.SoLuong + qtyToAdd);
        }
        else
        {
            // Nếu chưa có: Tạo mới một CartItem và thêm vào danh sách
            cart.Add(new CartItem
            {
                Ms = sach.Ms,
                TenSach = sach.TenSach ?? "Sách",
                HinhMinhHoa = sach.HinhMinhHoa,
                DonGia = sach.DonGia ?? 0,
                SoLuong = qtyToAdd
            });
        }

        // [Bước 4]: Lưu ngược giỏ hàng cập nhật vào Session
        SaveCart(cart);

        // Tính toán tổng số lượng và số tiền giỏ hàng cập nhật
        var voucher = HttpContext.Session.GetString(VOUCHER_SESSION_KEY);
        var subtotal = cart.Sum(c => c.ThanhTien);
        var discount = CalculateDiscount(voucher, subtotal);
        var totalAmount = Math.Max(0, subtotal - discount);
        var totalQty = cart.Sum(c => c.SoLuong);

        // [Bước 5 - AJAX]: Trả về kết quả JSON cho Client để không phải rời khỏi trang đang xem
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new
            {
                success = true,
                message = $"Đã thêm \"{sach.TenSach}\" vào giỏ hàng!",
                tenSach = sach.TenSach,
                ms = sach.Ms,
                soLuong = item?.SoLuong ?? qtyToAdd,
                totalQty = totalQty,
                subtotal = subtotal,
                discount = discount,
                totalAmount = totalAmount
            });
        }

        // [Bước 5 - Form thường]: Ở lại trang hiện tại (ReturnUrl / Referer) thay vì chuyển sang /GioHang
        TempData["SuccessMessage"] = $"Đã thêm \"{sach.TenSach}\" vào giỏ hàng thành công! Bạn có thể tiếp tục chọn sách hoặc bấm vào Giỏ hàng trên thanh menu khi muốn thanh toán.";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        var referer = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }

        return RedirectToAction("Index", "Home");
    }

    // =========================================================================
    // 2. XEM TRANG CHI TIẾT GIỎ HÀNG (GRIDVIEW TABLE)
    // =========================================================================

    /// <summary>
    /// GET: /GioHang/Index
    /// Hiển thị bảng danh sách các mặt hàng đã chọn mua, đơn giá, số lượng và tổng tiền.
    /// </summary>
    public IActionResult Index()
    {
        var cart = GetCart();
        if (!cart.Any() && HttpContext.Request.Query.ContainsKey("demo"))
        {
            var sampleBooks = _context.Saches.Take(2).ToList();
            if (sampleBooks.Any())
            {
                cart.Add(new CartItem
                {
                    Ms = sampleBooks[0].Ms,
                    TenSach = sampleBooks[0].TenSach ?? "Giáo trình Lập trình Web",
                    HinhMinhHoa = sampleBooks[0].HinhMinhHoa,
                    DonGia = sampleBooks[0].DonGia ?? 75000,
                    SoLuong = 2
                });
                if (sampleBooks.Count > 1)
                {
                    cart.Add(new CartItem
                    {
                        Ms = sampleBooks[1].Ms,
                        TenSach = sampleBooks[1].TenSach ?? "Cơ sở dữ liệu nâng cao",
                        HinhMinhHoa = sampleBooks[1].HinhMinhHoa,
                        DonGia = sampleBooks[1].DonGia ?? 90000,
                        SoLuong = 1
                    });
                }
                SaveCart(cart);
            }
        }
        var voucher = HttpContext.Session.GetString(VOUCHER_SESSION_KEY);
        var subtotal = cart.Sum(c => c.ThanhTien);
        var discount = CalculateDiscount(voucher, subtotal);
        var total = Math.Max(0, subtotal - discount);

        ViewBag.VoucherCode = voucher;
        ViewBag.GiamGia = discount;
        ViewBag.TamTinh = subtotal;
        ViewBag.TongThanhToan = total;

        return View(cart);
    }

    // 3. Cập nhật số lượng mặt hàng (Form submit thông thường)
    [HttpPost]
    public IActionResult CapNhatSoLuong(int ms, int soLuong)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.Ms == ms);

        if (item != null)
        {
            if (soLuong <= 0)
            {
                cart.Remove(item);
                TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng.";
            }
            else
            {
                item.SoLuong = Math.Min(99, soLuong);
                TempData["SuccessMessage"] = "Đã cập nhật số lượng thành công.";
            }
            SaveCart(cart);
        }

        return RedirectToAction(nameof(Index));
    }

    // 3.1 Cập nhật số lượng bằng AJAX (Real-time mượt mà, không giật trang)
    [HttpPost]
    public IActionResult CapNhatSoLuongAjax(int ms, int soLuong)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.Ms == ms);
        if (item != null)
        {
            if (soLuong <= 0)
            {
                cart.Remove(item);
            }
            else
            {
                item.SoLuong = Math.Min(99, soLuong);
            }
            SaveCart(cart);
        }

        var voucher = HttpContext.Session.GetString(VOUCHER_SESSION_KEY);
        var subtotal = cart.Sum(c => c.ThanhTien);
        var discount = CalculateDiscount(voucher, subtotal);
        var total = Math.Max(0, subtotal - discount);
        var totalQty = cart.Sum(c => c.SoLuong);

        return Json(new
        {
            success = true,
            ms = ms,
            soLuong = item?.SoLuong ?? 0,
            itemThanhTien = item?.ThanhTien ?? 0,
            tamTinh = subtotal,
            giamGia = discount,
            tongThanhToan = total,
            tongSoLuong = totalQty,
            itemCount = cart.Count
        });
    }

    // 3.2 Áp dụng mã giảm giá / Voucher
    [HttpPost]
    public IActionResult ApDungVoucher(string maVoucher)
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            TempData["ErrorMessage"] = "Giỏ hàng đang trống, không thể áp dụng mã ưu đãi!";
            return RedirectToAction(nameof(Index));
        }

        if (string.IsNullOrWhiteSpace(maVoucher))
        {
            TempData["ErrorMessage"] = "Vui lòng nhập mã giảm giá!";
            return RedirectToAction(nameof(Index));
        }

        var code = maVoucher.Trim().ToUpper();
        var subtotal = cart.Sum(c => c.ThanhTien);
        var discount = CalculateDiscount(code, subtotal);

        if (discount > 0)
        {
            HttpContext.Session.SetString(VOUCHER_SESSION_KEY, code);
            TempData["SuccessMessage"] = $"🎉 Áp dụng mã \"{code}\" thành công! Bạn được giảm {discount:N0} VNĐ.";
        }
        else if (code == "TRIENTRIC" && subtotal < 100000m)
        {
            TempData["ErrorMessage"] = $"Mã \"{code}\" chỉ áp dụng cho đơn hàng từ 100.000 VNĐ trở lên (hiện tại: {subtotal:N0} VNĐ).";
        }
        else if (code == "FLASH50" && subtotal < 200000m)
        {
            TempData["ErrorMessage"] = $"Mã \"{code}\" (Flash Sale) chỉ áp dụng cho đơn hàng từ 200.000 VNĐ trở lên (hiện tại: {subtotal:N0} VNĐ).";
        }
        else
        {
            TempData["ErrorMessage"] = $"Mã giảm giá \"{maVoucher}\" không hợp lệ hoặc đã hết hạn!";
        }

        return RedirectToAction(nameof(Index));
    }

    // 3.3 Hủy bỏ mã giảm giá
    [HttpPost]
    public IActionResult HuyVoucher()
    {
        HttpContext.Session.Remove(VOUCHER_SESSION_KEY);
        TempData["SuccessMessage"] = "Đã hủy áp dụng mã giảm giá.";
        return RedirectToAction(nameof(Index));
    }

    // 4. Hủy một mục hàng (Xóa 1 sản phẩm)
    [HttpPost]
    public IActionResult XoaItem(int ms)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.Ms == ms);
        if (item != null)
        {
            cart.Remove(item);
            SaveCart(cart);
            TempData["SuccessMessage"] = "Đã hủy sản phẩm khỏi giỏ hàng.";
        }

        return RedirectToAction(nameof(Index));
    }

    // 5. Hủy toàn bộ giỏ hàng
    [HttpPost]
    public IActionResult XoaTatCa()
    {
        HttpContext.Session.Remove(CART_SESSION_KEY);
        HttpContext.Session.Remove(VOUCHER_SESSION_KEY);
        TempData["SuccessMessage"] = "Đã làm trống giỏ hàng.";
        return RedirectToAction(nameof(Index));
    }

    // =========================================================================
    // 6. XỬ LÝ ĐẶT HÀNG VÀ THANH TOÁN (TRANSACTIONAL CHECKOUT)
    // =========================================================================

    /// <summary>
    /// POST: /GioHang/DatHang
    /// Thực hiện lưu Đơn đặt hàng (DON_DAT_HANG) và Chi tiết đặt hàng (CT_DAT_HANG)
    /// Sử dụng Database Transaction để đảm bảo tính toàn vẹn (ACID), cập nhật số lượng bán
    /// và dọn dẹp Session giỏ hàng sau khi hoàn tất.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> DatHang()
    {
        // [Bước 1]: Lấy giỏ hàng và kiểm tra tính rỗng
        var cart = GetCart();
        if (!cart.Any())
        {
            TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
            return RedirectToAction(nameof(Index));
        }

        // [Bước 2]: Xác định khách hàng đặt hàng (từ Claims nếu đã đăng nhập, hoặc tài khoản khách)
        int maKhachHang = 1; // ID khách hàng mặc định

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int uid))
            {
                maKhachHang = uid;
            }
        }
        else
        {
            // Nếu khách vãng lai chưa đăng nhập: Lấy khách hàng có sẵn hoặc tạo mới hồ sơ vãng lai
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync();
            if (khachHang != null)
            {
                maKhachHang = khachHang.Mkh;
            }
            else
            {
                var newKh = new KhachHang
                {
                    HoTen = "Khách Hàng Vãng Lai",
                    TenDangNhap = "guest_" + Guid.NewGuid().ToString().Substring(0, 8),
                    MatKhau = "123456",
                    Email = "guest@bookonline.vn",
                    DiaChi = "Hà Nội"
                };
                _context.KhachHangs.Add(newKh);
                await _context.SaveChangesAsync();
                maKhachHang = newKh.Mkh;
            }
        }

        // [Bước 3]: Khởi tạo Transaction đảm bảo tính trọn vẹn (Tất cả thành công hoặc cùng hủy bỏ)
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tính toán tổng tiền thực tế sau khi áp dụng mã giảm giá (Voucher)
            var subtotal = cart.Sum(c => c.ThanhTien);
            var voucher = HttpContext.Session.GetString(VOUCHER_SESSION_KEY);
            var discount = CalculateDiscount(voucher, subtotal);
            var finalTotal = Math.Max(0, subtotal - discount);

            // [Bước 4]: Tạo mới bản ghi Đơn hàng (DON_DAT_HANG)
            var donHang = new DonDatHang
            {
                Mkh = maKhachHang,                          // Mã khách hàng
                NgayDatHang = DateTime.Now,                  // Ngày lập đơn
                NgayGiaoHang = DateTime.Now.AddDays(3),      // Dự kiến giao hàng sau 3 ngày
                DaGiaoHang = false,                          // Trạng thái giao hàng (chưa giao)
                TriGia = finalTotal                          // Tổng trị giá tiền của đơn hàng (sau khi trừ giảm giá)
            };

            _context.DonDatHangs.Add(donHang);
            await _context.SaveChangesAsync(); // Lưu để sinh khóa chính tự tăng Sdh

            // [Bước 5]: Lặp qua từng mặt hàng trong giỏ để lưu vào Chi tiết đặt hàng (CT_DAT_HANG)
            foreach (var item in cart)
            {
                var ct = new CtDatHang
                {
                    Sdh = donHang.Sdh,                      // Khóa ngoại liên kết tới đơn hàng vừa tạo
                    Ms = item.Ms,                           // Mã sách
                    SoLuong = item.SoLuong,                 // Số lượng mua
                    DonGia = (double)item.DonGia,           // Đơn giá bán
                    ThanhTien = (double)item.ThanhTien      // Thành tiền mặt hàng
                };
                _context.CtDatHangs.Add(ct);

                // [Bước 6]: Tăng số lượng đã bán (SoLuongBan) của cuốn sách phục vụ thống kê Sách Bán Chạy
                var sach = await _context.Saches.FindAsync(item.Ms);
                if (sach != null)
                {
                    sach.SoLuongBan = (sach.SoLuongBan ?? 0) + item.SoLuong;
                }
            }

            // Lưu toàn bộ chi tiết và cập nhật số lượng sách vào CSDL
            await _context.SaveChangesAsync();

            // Xác nhận hoàn tất Transaction an toàn
            await transaction.CommitAsync();

            // [Bước 7]: Xóa session giỏ hàng và voucher sau khi đặt mua thành công
            HttpContext.Session.Remove(CART_SESSION_KEY);
            HttpContext.Session.Remove(VOUCHER_SESSION_KEY);

            // [Bước 8]: Trả về View thông báo Đặt hàng thành công cùng Mã số đơn hàng (Sdh)
            return View("DatHangThanhCong", donHang.Sdh);
        }
        catch (Exception ex)
        {
            // Nếu có bất kỳ lỗi nào xảy ra: Hoàn tác toàn bộ Transaction (Rollback)
            await transaction.RollbackAsync();
            TempData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình đặt hàng: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public IActionResult DatHangThanhCong(int id = 10024)
    {
        return View("DatHangThanhCong", id);
    }
}
