using System.Globalization;
using System.Text;

namespace BookOnline.Helpers;

public static class StringHelper
{
    /// <summary>
    /// Chuyển chuỗi tiếng Việt có dấu thành không dấu (Ví dụ: "Công nghệ thông tin" -> "Cong nghe thong tin")
    /// </summary>
    public static string RemoveDiacritics(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        return sb.ToString().Normalize(NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
    }
}
