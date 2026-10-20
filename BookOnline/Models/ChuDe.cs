using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("CHU_DE")]
[Index("Pid", Name = "PID")]
public partial class ChuDe
{
    [Key]
    public int Mcd { get; set; }

    [Column("Ten_chu_de")]
    [StringLength(50)]
    public string? TenChuDe { get; set; }

    [Column("PID")]
    public int? Pid { get; set; }

    [InverseProperty("McdNavigation")]
    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
