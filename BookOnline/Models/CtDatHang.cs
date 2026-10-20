using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("CT_DAT_HANG")]
[Index("Sdh", Name = "DON_DAT_HANGCT_DAT_HANG")]
[Index("Ms", Name = "SACHCT_DAT_HANG")]
public partial class CtDatHang
{
    [Key]
    public int Mctddh { get; set; }

    public int Sdh { get; set; }

    public int Ms { get; set; }

    [Column("So_luong")]
    public int? SoLuong { get; set; }

    [Column("Don_gia")]
    public double? DonGia { get; set; }

    [Column("Thanh_tien")]
    public double? ThanhTien { get; set; }

    [ForeignKey("Ms")]
    [InverseProperty("CtDatHangs")]
    public virtual Sach MsNavigation { get; set; } = null!;

    [ForeignKey("Sdh")]
    [InverseProperty("CtDatHangs")]
    public virtual DonDatHang SdhNavigation { get; set; } = null!;
}
