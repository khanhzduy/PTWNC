using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Models;
using BookOnline.ViewModels;

namespace BookOnline.Controllers;

public class SachController : Controller
{
    private readonly AppDbContext _context;

    public SachController(AppDbContext context)
    {
        _context = context;
    }

    // =========================================================================
    // 1. HIỂN THỊ DANH MỤC SÁCH THEO CHỦ ĐỀ HOẶC TẤT CẢ SÁCH (CÂU 2 ĐỀ BÀI)
    // =========================================================================

    /// <summary>
    /// Hiển thị danh sách sách theo chủ đề được chọn (hoặc tất cả sách nếu mcd = 0).
    /// </summary>
    /// <param name="mcd">Mã chủ đề chính thức (từ route hoặc query string).</param>
    /// <param name="maCd">Tham số dự phòng tương thích ngược.</param>
    public async Task<IActionResult> TheoChuDe(int? mcd, int? maCd)
    {
        // [Bước 1]: Xác định mã chủ đề (nếu không truyền mặc định lấy 0 = Tất cả sách)
        int selectedId = mcd ?? maCd ?? 0;
        string tenChuDe = "Tất cả sách trực tuyến";

        // [Bước 2]: Khởi tạo truy vấn AsNoTracking để tối ưu hóa hiệu năng đọc dữ liệu
        IQueryable<Sach> query = _context.Saches.AsNoTracking();

        // [Bước 3]: Nếu người dùng chọn một chủ đề cụ thể (> 0) thì lọc theo mã chủ đề đó
        if (selectedId > 0)
        {
            var chuDe = await _context.ChuDes.FindAsync(selectedId);
            if (chuDe != null)
            {
                tenChuDe = chuDe.TenChuDe ?? "Chủ đề";
                query = query.Where(s => s.Mcd == selectedId);
            }
        }

        // [Bước 4]: Lấy danh sách sách và ánh xạ (projection) sang SachViewModel có đầy đủ Tên, Tác giả, NXB, Giá, Nút mua
        var dsSach = await query
            .OrderBy(s => s.Ms)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "Chưa có tên",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : null,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault(),
                TenChuDe = tenChuDe
            })
            .ToListAsync();

        // [Bước 5]: Lấy 5 đầu sách mới nhất hiển thị tại khối Sách Mới (Cột giữa bên trên)
        var sachMoi = await _context.Saches
            .AsNoTracking()
            .OrderByDescending(s => s.NgayCapNhat ?? DateTime.MinValue)
            .Take(5)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault()
            })
            .ToListAsync();

        // [Bước 5.1]: Lấy 5 đầu sách bán chạy nhất (Vùng Sách Bán Chạy)
        var sachBanChay = await _context.Saches
            .AsNoTracking()
            .OrderByDescending(s => s.SoLuongBan ?? 0)
            .ThenByDescending(s => s.Ms)
            .Take(5)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault(),
                SoLuongBan = s.SoLuongBan
            })
            .ToListAsync();

        // [Bước 6]: Lấy ngẫu nhiên 3 banner quảng cáo từ danh sách tài trợ có URL thực tế (Quảng cáo 1)
        var allBannerAds = new List<QuangCaoItem>
        {
            new QuangCaoItem { HinhAnh = "/images/QC01.gif", LinkUrl = "https://www.fahasa.com/", TenQuangCao = "Hội Sách Fahasa Online" },
            new QuangCaoItem { HinhAnh = "/images/QC02.gif", LinkUrl = "https://tiki.vn/nha-sach-tiki", TenQuangCao = "Nhà Sách Trực Tuyến Tiki" },
            new QuangCaoItem { HinhAnh = "/images/QC03.gif", LinkUrl = "https://www.nxbtre.com.vn/", TenQuangCao = "Nhà Xuất Bản Trẻ" },
            new QuangCaoItem { HinhAnh = "/images/QC04.gif", LinkUrl = "https://ben.com.vn/", TenQuangCao = "Máy Tính & Thiết Bị Ben Computer" },
            new QuangCaoItem { HinhAnh = "/images/damsen.gif", LinkUrl = "https://damsenpark.vn/", TenQuangCao = "Công Viên Văn Hóa Đầm Sen" },
            new QuangCaoItem { HinhAnh = "/images/Trungthu.gif", LinkUrl = "https://phuongnambook.com/", TenQuangCao = "Phương Nam Book - Hội Sách Trung Thu" }
        };
        var randomBanners = allBannerAds.OrderBy(_ => Guid.NewGuid()).Take(3).ToList();

        // [Bước 7]: Đóng gói dữ liệu vào HomeViewModel và tái sử dụng giao diện Index.cshtml
        var viewModel = new HomeViewModel
        {
            SachMoi = sachMoi,
            SachBanChay = sachBanChay,
            DanhSachSach = dsSach,
            QuangCaoNgauNhien = randomBanners,
            TenTieuDe = selectedId > 0 ? $"Danh mục sách: {tenChuDe}" : "Tất cả sách trực tuyến"
        };

        return View("~/Views/Home/Index.cshtml", viewModel);
    }

    // =========================================================================
    // 2. TRANG CHI TIẾT SÁCH (CÂU 2 TRONG ĐỀ BÀI)
    // =========================================================================

    /// <summary>
    /// Hiển thị thông tin chi tiết của một cuốn sách: Bìa, Tên sách, Đơn vị tính,
    /// Đơn giá, Tác giả, Nhà xuất bản, Mô tả, và tự động tăng số lần xem.
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {
        // [Bước 1]: Truy vấn sách cùng thông tin Nhà xuất bản và Chủ đề liên kết
        var sach = await _context.Saches
            .Include(s => s.MnxbNavigation)
            .Include(s => s.McdNavigation)
            .FirstOrDefaultAsync(s => s.Ms == id);

        if (sach == null)
        {
            return NotFound();
        }

        // [Bước 2]: Tăng số lần xem của cuốn sách và lưu cập nhật
        sach.SoLanXem = (sach.SoLanXem ?? 0) + 1;
        await _context.SaveChangesAsync();

        // [Bước 3]: Truy vấn bảng THAM_GIA để lấy danh sách tên tất cả tác giả viết cuốn sách này
        var tacGias = await _context.ThamGia
            .Where(tg => tg.Ms == id)
            .Include(tg => tg.MtgNavigation)
            .Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : "")
            .Where(name => !string.IsNullOrEmpty(name))
            .ToListAsync();

        // [Bước 4]: Đóng gói dữ liệu vào SachViewModel để trả về View
        var model = new SachViewModel
        {
            Ms = sach.Ms,
            TenSach = sach.TenSach ?? "",
            DonGia = sach.DonGia,
            DonViTinh = sach.DonViTinh,
            MoTa = sach.MoTa,
            HinhMinhHoa = sach.HinhMinhHoa,
            TenChuDe = sach.McdNavigation?.TenChuDe,
            TenNXB = sach.MnxbNavigation?.TenNhaXuatBan,
            TenTacGia = tacGias.Any() ? string.Join(", ", tacGias) : "Đang cập nhật",
            NgayCapNhat = sach.NgayCapNhat,
            SoLuongBan = sach.SoLuongBan,
            SoLanXem = sach.SoLanXem
        };

        return View(model);
    }

    // =========================================================================
    // 3. DANH SÁCH SÁCH MỚI PHÁT HÀNH
    // =========================================================================

    /// <summary>
    /// Lấy danh sách 12 cuốn sách có ngày cập nhật mới nhất.
    /// </summary>
    public async Task<IActionResult> SachMoi()
    {
        var sachMoi = await _context.Saches
            .AsNoTracking()
            .OrderByDescending(s => s.NgayCapNhat ?? DateTime.MinValue)
            .Take(12)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : null,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault()
            })
            .ToListAsync();

        ViewData["TieuDe"] = "Sách Mới Phát Hành";
        return View("DanhSach", sachMoi);
    }

    // =========================================================================
    // 4. DANH SÁCH SÁCH BÁN CHẠY NHẤT
    // =========================================================================

    /// <summary>
    /// Lấy danh sách 12 cuốn sách có số lượng bán cao nhất (sắp xếp giảm dần theo SoLuongBan).
    /// </summary>
    public async Task<IActionResult> SachBanChay()
    {
        var sachBanChay = await _context.Saches
            .AsNoTracking()
            .OrderByDescending(s => s.SoLuongBan ?? 0)
            .Take(12)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : null,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault(),
                SoLuongBan = s.SoLuongBan
            })
            .ToListAsync();

        ViewData["TieuDe"] = "Top Sách Bán Chạy Nhất";
        return View("DanhSach", sachBanChay);
    }

    // =========================================================================
    // 5. TÌM KIẾM SÁCH (ĐIỀU HƯỚNG TƯƠNG THÍCH ROUTE /Sach/TimKiem)
    // =========================================================================
    [HttpGet]
    public IActionResult TimKiem(string? keyword)
    {
        return RedirectToAction("Index", "Home", new { keyword });
    }
}
