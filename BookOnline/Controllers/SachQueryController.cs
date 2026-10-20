using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookOnline.Models;

namespace BookOnline.Controllers;

public class SachQueryItem
{
    public int Id { get; set; }
    public string QueryName { get; set; } = string.Empty;
}

public class SachQueryController : Controller
{
    private readonly AppDbContext _context;

    public SachQueryController(AppDbContext context)
    {
        _context = context;
    }

    // Helper chuyển đổi List sang Dictionary để hiển thị động (theo slide TH03)
    private List<Dictionary<string, object>> ToDictionaryList<T>(IEnumerable<T> list)
    {
        var resultList = new List<Dictionary<string, object>>();
        var properties = typeof(T).GetProperties();

        foreach (var item in list)
        {
            var dict = new Dictionary<string, object>();
            foreach (var prop in properties)
            {
                dict[prop.Name] = prop.GetValue(item) ?? "NULL";
            }
            resultList.Add(dict);
        }
        return resultList;
    }

    public async Task<IActionResult> QueryDemo(int? id)
    {
        var queries = new List<SachQueryItem>
        {
            new SachQueryItem { Id = 1, QueryName = "1. Lấy tất cả danh mục chủ đề" },
            new SachQueryItem { Id = 2, QueryName = "2. Lấy danh mục chủ đề có sách kèm tổng số sách & lượt bán" },
            new SachQueryItem { Id = 3, QueryName = "3. Lấy danh mục sách có thông tin NXB" },
            new SachQueryItem { Id = 4, QueryName = "4. Lấy danh mục sách có thông tin NXB và tác giả" },
            new SachQueryItem { Id = 5, QueryName = "5. Lấy danh mục sách mới nhất" },
            new SachQueryItem { Id = 7, QueryName = "7. Lấy danh mục sách bán chạy nhất" },
            new SachQueryItem { Id = 9, QueryName = "9. Lấy danh mục quảng cáo còn hạn" }
        };

        ViewBag.Queries = new SelectList(queries, "Id", "QueryName", id);

        if (!id.HasValue)
        {
            return View(null);
        }

        switch (id.Value)
        {
            case 1:
                // 1. Lấy tất cả chủ đề
                var q1 = await _context.ChuDes.AsNoTracking()
                    .Select(c => new { c.Mcd, c.TenChuDe, c.Pid })
                    .ToListAsync();
                return View(ToDictionaryList(q1));

            case 2:
                // 2. Lấy danh mục chủ đề có sách kèm số lượng & tổng lượt bán (theo slide 9 & 22)
                var q2 = await _context.ChuDes.AsNoTracking()
                    .Select(c => new
                    {
                        TenChuDe = c.TenChuDe ?? "",
                        TongSoSach = c.Saches.Count(),
                        TongLuotBan = c.Saches.Sum(s => (int?)s.SoLuongBan) ?? 0
                    })
                    .Where(x => x.TongSoSach > 0)
                    .ToListAsync();
                return View(ToDictionaryList(q2));

            case 3:
                // 3. Sách có tt NXB
                var q3 = await _context.Saches.AsNoTracking()
                    .Include(s => s.MnxbNavigation)
                    .Select(s => new
                    {
                        s.Ms,
                        TenSach = s.TenSach ?? "",
                        DonGia = s.DonGia ?? 0,
                        NhaXuatBan = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : "N/A"
                    })
                    .Take(15)
                    .ToListAsync();
                return View(ToDictionaryList(q3));

            case 4:
                // 4. Sách có tt NXB và tác giả
                var q4 = await _context.Saches.AsNoTracking()
                    .Include(s => s.MnxbNavigation)
                    .Select(s => new
                    {
                        s.Ms,
                        TenSach = s.TenSach ?? "",
                        DonGia = s.DonGia ?? 0,
                        NhaXuatBan = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : "N/A",
                        TacGia = string.Join(", ", s.ThamGia.Select(tg => tg.MtgNavigation.TenTacGia))
                    })
                    .Take(15)
                    .ToListAsync();
                return View(ToDictionaryList(q4));

            case 5:
                // 5. Sách mới nhất
                var q5 = await _context.Saches.AsNoTracking()
                    .OrderByDescending(s => s.NgayCapNhat)
                    .Select(s => new
                    {
                        s.Ms,
                        TenSach = s.TenSach ?? "",
                        s.DonGia,
                        NgayCapNhat = s.NgayCapNhat != null ? s.NgayCapNhat.Value.ToString("dd/MM/yyyy") : "N/A"
                    })
                    .Take(10)
                    .ToListAsync();
                return View(ToDictionaryList(q5));

            case 7:
                // 7. Sách bán chạy nhất
                var q7 = await _context.Saches.AsNoTracking()
                    .OrderByDescending(s => s.SoLuongBan)
                    .Select(s => new
                    {
                        s.Ms,
                        TenSach = s.TenSach ?? "",
                        s.DonGia,
                        SoLuongBan = s.SoLuongBan ?? 0
                    })
                    .Take(10)
                    .ToListAsync();
                return View(ToDictionaryList(q7));

            case 9:
                // 9. Quảng cáo còn hạn
                var today = DateTime.Today;
                var q9 = await _context.QuangCaos.AsNoTracking()
                    .Where(q => q.NgayHetHan == null || q.NgayHetHan >= today)
                    .Select(q => new
                    {
                        q.Stt,
                        TenCongTy = q.TenCty ?? "",
                        HinhAnh = q.HinhMinhHoa ?? "",
                        LinkLienKet = q.Href ?? "",
                        NgayHetHan = q.NgayHetHan != null ? q.NgayHetHan.Value.ToString("dd/MM/yyyy") : "Vô thời hạn"
                    })
                    .ToListAsync();
                return View(ToDictionaryList(q9));

            default:
                return View(null);
        }
    }
}
