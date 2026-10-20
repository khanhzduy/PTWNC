using Microsoft.AspNetCore.Mvc;
using BookOnline.Helpers;
using BookOnline.ViewModels;

namespace BookOnline.ViewComponents;

public class CartSummaryViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        var voucher = HttpContext.Session.GetString("Cart_Voucher");
        var subtotal = cart.Sum(i => i.ThanhTien);

        decimal discount = 0;
        if (!string.IsNullOrWhiteSpace(voucher) && subtotal > 0)
        {
            var code = voucher.Trim().ToUpper();
            if (code == "VIETHUNG" || code == "VHU") discount = Math.Round(subtotal * 0.20m, 0);
            else if (code == "PN10" || code == "GIAM10") discount = Math.Round(subtotal * 0.10m, 0);
            else if (code == "FREESHIP") discount = Math.Min(subtotal, 20000m);
            else if (code == "BANMOI") discount = Math.Min(subtotal, 15000m);
            else if (code == "TRIENTRIC" && subtotal >= 100000m) discount = 30000m;
            else if (code == "FLASH50" && subtotal >= 200000m) discount = 50000m;
            else if (code == "CUOITUAN") discount = Math.Round(subtotal * 0.15m, 0);
        }

        var summary = new CartSummaryViewModel
        {
            TongSoLuong = cart.Sum(i => i.SoLuong),
            TongTien = Math.Max(0, subtotal - discount)
        };

        return View(summary);
    }
}
