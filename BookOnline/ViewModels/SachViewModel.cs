namespace BookOnline.ViewModels;

public class SachViewModel
{
    public int Ms { get; set; }
    public string TenSach { get; set; } = string.Empty;
    public decimal? DonGia { get; set; }
    public string? DonViTinh { get; set; }
    public string? MoTa { get; set; }
    public string? HinhMinhHoa { get; set; }
    public int? Mcd { get; set; }
    public string? TenChuDe { get; set; }
    public int? Mnxb { get; set; }
    public string? TenNXB { get; set; }
    public string? TenTacGia { get; set; }
    public DateTime? NgayCapNhat { get; set; }
    public int? SoLuongBan { get; set; }
    public int? SoLanXem { get; set; }

    // Helper tính đường dẫn ảnh chuẩn, tự bổ sung .jpg nếu CSDL chỉ lưu tên
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
