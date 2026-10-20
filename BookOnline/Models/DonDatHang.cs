using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("DON_DAT_HANG")]
[Index("Mkh", Name = "KHACH_HANGDON_DAT_HANG")]
public partial class DonDatHang
{
    [Key]
    public int Sdh { get; set; }

    public int? Mkh { get; set; }

    [Column("Ngay_dat_hang", TypeName = "datetime")]
    public DateTime NgayDatHang { get; set; }

    [Column("Tri_gia", TypeName = "money")]
    public decimal? TriGia { get; set; }

    [Column("Da_giao_hang")]
    public bool DaGiaoHang { get; set; }

    [Column("Ngay_giao_hang", TypeName = "datetime")]
    public DateTime? NgayGiaoHang { get; set; }

    [InverseProperty("SdhNavigation")]
    public virtual ICollection<CtDatHang> CtDatHangs { get; set; } = new List<CtDatHang>();

    [ForeignKey("Mkh")]
    [InverseProperty("DonDatHangs")]
    public virtual KhachHang? MkhNavigation { get; set; }
}
