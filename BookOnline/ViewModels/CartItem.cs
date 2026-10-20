namespace BookOnline.ViewModels;

public class CartItem
{
    public int Ms { get; set; }
    public string TenSach { get; set; } = string.Empty;
    public string? HinhMinhHoa { get; set; }
    public decimal DonGia { get; set; }
    public int SoLuong { get; set; }

    public decimal ThanhTien => SoLuong * DonGia;

    public string ImageUrl
    {
        get
        {
            if (string.IsNullOrWhiteSpace(HinhMinhHoa))
                return "/images/Upload/Sample.jpg";

            var trimmed = HinhMinhHoa.Trim();
            if (trimmed.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                trimmed.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                trimmed.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return $"/images/Upload/{trimmed}";
            }
            return $"/images/Upload/{trimmed}.jpg";
        }
    }
}

public class CartSummaryViewModel
{
    public int TongSoLuong { get; set; }
    public decimal TongTien { get; set; }
}
