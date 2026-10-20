using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("SACH")]
[Index("Mcd", Name = "CHU_DESACH")]
[Index("Mnxb", Name = "NHA_XUAT_BANSACH")]
public partial class Sach
{
    [Key]
    public int Ms { get; set; }

    [Column("Ten_sach")]
    [StringLength(100)]
    public string TenSach { get; set; } = null!;

    [Column("Don_gia", TypeName = "money")]
    public decimal? DonGia { get; set; }

    [Column("Don_vi_tinh")]
    [StringLength(10)]
    public string? DonViTinh { get; set; }

    [Column("Mo_ta", TypeName = "ntext")]
    public string? MoTa { get; set; }

    [Column("Hinh_minh_hoa")]
    [StringLength(50)]
    public string? HinhMinhHoa { get; set; }

    public int? Mcd { get; set; }

    public int? Mnxb { get; set; }

    [Column("Ngay_cap_nhat", TypeName = "datetime")]
    public DateTime? NgayCapNhat { get; set; }

    [Column("So_luong_ban")]
    public int? SoLuongBan { get; set; }

    [Column("So_lan_xem")]
    public int? SoLanXem { get; set; }

    [InverseProperty("MsNavigation")]
    public virtual ICollection<CtDatHang> CtDatHangs { get; set; } = new List<CtDatHang>();

    [ForeignKey("Mcd")]
    [InverseProperty("Saches")]
    public virtual ChuDe? McdNavigation { get; set; }

    [ForeignKey("Mnxb")]
    [InverseProperty("Saches")]
    public virtual NhaXuatBan? MnxbNavigation { get; set; }

    [InverseProperty("MsNavigation")]
    public virtual ICollection<ThamGium> ThamGia { get; set; } = new List<ThamGium>();
}
