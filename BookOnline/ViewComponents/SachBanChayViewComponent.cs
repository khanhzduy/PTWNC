using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Models;
using BookOnline.ViewModels;

namespace BookOnline.ViewComponents;

public class SachBanChayViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public SachBanChayViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Lấy danh mục 5 đầu sách có số lượng bán nhiều nhất theo đặc tả Câu 1 đề bài
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
                SoLuongBan = s.SoLuongBan
            })
            .ToListAsync();

        return View(sachBanChay);
    }
}
