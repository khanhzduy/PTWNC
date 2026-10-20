using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Models;

namespace BookOnline.ViewComponents;

public class QuangCaoSidebarViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public QuangCaoSidebarViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var today = DateTime.Today;
        var ads = await _context.QuangCaos
            .AsNoTracking()
            .Where(q => q.NgayHetHan == null || q.NgayHetHan >= today)
            .OrderByDescending(q => q.Stt)
            .Take(6)
            .ToListAsync();

        // Tự động chuẩn hóa font chữ tiếng Việt nếu CSDL gốc bị lỗi mã hóa UTF-8/Latin-1
        foreach (var ad in ads)
        {
            if (!string.IsNullOrEmpty(ad.TenCty) && (ad.TenCty.Contains("Chá»") || ad.TenCty.Contains("khoÃ¡n")))
            {
                ad.TenCty = "Chứng khoán Biển Việt (CBV)";
            }
        }

        return View(ads);
    }
}
