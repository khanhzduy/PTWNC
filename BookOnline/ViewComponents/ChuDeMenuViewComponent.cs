using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookOnline.Models;
using BookOnline.ViewModels;

namespace BookOnline.ViewComponents;

public class ChuDeMenuViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public ChuDeMenuViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var chuDes = await _context.ChuDes
            .AsNoTracking()
            .Where(c => c.Saches.Any()) // Chỉ lấy các danh mục chủ đề có số lượng sách lớn hơn 0 (> 0)
            .Select(c => new ChuDeViewModel
            {
                Mcd = c.Mcd,
                TenChuDe = c.TenChuDe ?? "Không tên",
                TongSoSach = c.Saches.Count()
            })
            .OrderBy(c => c.Mcd)
            .ToListAsync();

        return View(chuDes);
    }
}
