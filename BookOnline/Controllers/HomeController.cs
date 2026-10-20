using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Helpers;
using BookOnline.Models;
using BookOnline.ViewModels;

namespace BookOnline.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? keyword)
    {
        // 1. Lấy danh sách 5 đầu sách mới nhất (Vùng Sách Mới)
        var sachMoi = await _context.Saches
            .AsNoTracking()
            .OrderByDescending(s => s.NgayCapNhat ?? DateTime.MinValue)
            .ThenByDescending(s => s.Ms)
            .Take(5)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "Chưa có tên",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : null,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault()
            })
            .ToListAsync();

        // 1.1 Lấy danh sách 5 đầu sách bán chạy nhất (Vùng Sách Bán Chạy theo đặc tả Câu 1)
        var sachBanChay = await _context.Saches
            .AsNoTracking()
            .OrderByDescending(s => s.SoLuongBan ?? 0)
            .ThenByDescending(s => s.Ms)
            .Take(5)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "Chưa có tên",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : null,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault(),
                SoLuongBan = s.SoLuongBan
            })
            .ToListAsync();

        // 2. Lấy danh sách sách hiển thị chính (Hỗ trợ TÌM KIẾM THÔNG MINH theo Tựa sách, Chủ đề, Tác giả, NXB)
        IQueryable<Sach> query = _context.Saches.AsNoTracking();
        string tenTieuDe = "Sách Nổi Bật Mới Nhất";
        string? cleanKw = keyword?.Trim();

        if (!string.IsNullOrWhiteSpace(cleanKw))
        {
            var kwNoSign = StringHelper.RemoveDiacritics(cleanKw);
            tenTieuDe = $"Kết quả tìm kiếm cho: \"{cleanKw}\"";

            // So khớp thông minh có dấu & không dấu (Case-Insensitive & Accent-Insensitive)
            query = query.Where(s =>
                EF.Functions.Collate(s.TenSach, "SQL_Latin1_General_CP1_CI_AI").Contains(cleanKw) ||
                EF.Functions.Collate(s.TenSach, "SQL_Latin1_General_CP1_CI_AI").Contains(kwNoSign) ||
                (s.McdNavigation != null && s.McdNavigation.TenChuDe != null && (
                    EF.Functions.Collate(s.McdNavigation.TenChuDe, "SQL_Latin1_General_CP1_CI_AI").Contains(cleanKw) ||
                    EF.Functions.Collate(s.McdNavigation.TenChuDe, "SQL_Latin1_General_CP1_CI_AI").Contains(kwNoSign)
                )) ||
                (s.MnxbNavigation != null && s.MnxbNavigation.TenNhaXuatBan != null && (
                    EF.Functions.Collate(s.MnxbNavigation.TenNhaXuatBan, "SQL_Latin1_General_CP1_CI_AI").Contains(cleanKw) ||
                    EF.Functions.Collate(s.MnxbNavigation.TenNhaXuatBan, "SQL_Latin1_General_CP1_CI_AI").Contains(kwNoSign)
                )) ||
                s.ThamGia.Any(tg => tg.MtgNavigation != null && tg.MtgNavigation.TenTacGia != null && (
                    EF.Functions.Collate(tg.MtgNavigation.TenTacGia, "SQL_Latin1_General_CP1_CI_AI").Contains(cleanKw) ||
                    EF.Functions.Collate(tg.MtgNavigation.TenTacGia, "SQL_Latin1_General_CP1_CI_AI").Contains(kwNoSign)
                )) ||
                (s.MoTa != null && (
                    EF.Functions.Collate(s.MoTa, "SQL_Latin1_General_CP1_CI_AI").Contains(cleanKw) ||
                    EF.Functions.Collate(s.MoTa, "SQL_Latin1_General_CP1_CI_AI").Contains(kwNoSign)
                ))
            );
        }
        else
        {
            query = query.OrderBy(s => s.Ms).Take(6);
        }

        var dsSach = await query
            .OrderBy(s => s.Ms)
            .Select(s => new SachViewModel
            {
                Ms = s.Ms,
                TenSach = s.TenSach ?? "Chưa có tên",
                DonGia = s.DonGia,
                HinhMinhHoa = s.HinhMinhHoa,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : null,
                TenTacGia = s.ThamGia.Select(tg => tg.MtgNavigation != null ? tg.MtgNavigation.TenTacGia : null).FirstOrDefault()
            })
            .ToListAsync();

        // 3. Vùng quảng cáo 1: Hiển thị ngẫu nhiên 3 ảnh quảng cáo cùng với liên kết trang đích cho mỗi lần load trang
        var allBannerAds = new List<QuangCaoItem>
        {
            new QuangCaoItem { HinhAnh = "/images/QC01.gif", LinkUrl = "https://www.fahasa.com/", TenQuangCao = "Hội Sách Fahasa Online" },
            new QuangCaoItem { HinhAnh = "/images/QC02.gif", LinkUrl = "https://tiki.vn/nha-sach-tiki", TenQuangCao = "Nhà Sách Trực Tuyến Tiki" },
            new QuangCaoItem { HinhAnh = "/images/QC03.gif", LinkUrl = "https://www.nxbtre.com.vn/", TenQuangCao = "Nhà Xuất Bản Trẻ" },
            new QuangCaoItem { HinhAnh = "/images/QC04.gif", LinkUrl = "https://ben.com.vn/", TenQuangCao = "Máy Tính & Thiết Bị Ben Computer" },
            new QuangCaoItem { HinhAnh = "/images/damsen.gif", LinkUrl = "https://damsenpark.vn/", TenQuangCao = "Công Viên Văn Hóa Đầm Sen" },
            new QuangCaoItem { HinhAnh = "/images/Trungthu.gif", LinkUrl = "https://phuongnambook.com/", TenQuangCao = "Phương Nam Book - Hội Sách Trung Thu" },
            new QuangCaoItem { HinhAnh = "/images/Thoitrang.gif", LinkUrl = "https://shopee.vn/", TenQuangCao = "Thời Trang & Phong Cách Sống" },
            new QuangCaoItem { HinhAnh = "/images/Omo.gif", LinkUrl = "https://www.unilever.com.vn/", TenQuangCao = "Unilever Việt Nam" },
            new QuangCaoItem { HinhAnh = "/images/Fristi.gif", LinkUrl = "https://www.dutchlady.com.vn/", TenQuangCao = "Sữa Tươi Fristi Cô Gái Hà Lan" }
        };
        var random = new Random();
        var randomBanners = allBannerAds.OrderBy(_ => random.Next()).Take(3).ToList();

        var viewModel = new HomeViewModel
        {
            SachMoi = sachMoi,
            SachBanChay = sachBanChay,
            DanhSachSach = dsSach,
            QuangCaoNgauNhien = randomBanners,
            TenTieuDe = tenTieuDe,
            Keyword = cleanKw
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
