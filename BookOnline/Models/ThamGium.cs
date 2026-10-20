using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[PrimaryKey("Ms", "Mtg")]
[Table("THAM_GIA")]
[Index("Ms", Name = "HANG_HOATAC_GIA_SACH")]
[Index("Mtg", Name = "TAC_GIATAC_GIA_SACH")]
public partial class ThamGium
{
    [Key]
    public int Ms { get; set; }

    [Key]
    public int Mtg { get; set; }

    [Column("Vai_tro")]
    [StringLength(50)]
    public string? VaiTro { get; set; }

    [ForeignKey("Ms")]
    [InverseProperty("ThamGia")]
    public virtual Sach MsNavigation { get; set; } = null!;

    [ForeignKey("Mtg")]
    [InverseProperty("ThamGia")]
    public virtual TacGium MtgNavigation { get; set; } = null!;
}
