using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("QUANG_CAO")]
public partial class QuangCao
{
    [Key]
    [Column("STT")]
    public int Stt { get; set; }

    [Column("TenCTy")]
    [StringLength(200)]
    public string? TenCty { get; set; }

    [Column("Hinh_Minh_Hoa")]
    [StringLength(100)]
    public string? HinhMinhHoa { get; set; }

    [Column("HREF")]
    [StringLength(100)]
    public string? Href { get; set; }

    [Column("Ngay_bat_dau", TypeName = "datetime")]
    public DateTime? NgayBatDau { get; set; }

    [Column("Ngay_het_han", TypeName = "datetime")]
    public DateTime? NgayHetHan { get; set; }
}
