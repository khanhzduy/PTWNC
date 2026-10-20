namespace BookOnline.ViewModels;

public class HomeViewModel
{
    public List<SachViewModel> SachMoi { get; set; } = new();
    public List<SachViewModel> SachBanChay { get; set; } = new();
    public List<SachViewModel> DanhSachSach { get; set; } = new();
    public List<QuangCaoItem> QuangCaoNgauNhien { get; set; } = new();
    public string TenTieuDe { get; set; } = "Sách nổi bật";
    public string? Keyword { get; set; }
}

public class QuangCaoItem
{
    public string HinhAnh { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = "#";
    public string TenQuangCao { get; set; } = string.Empty;
}
