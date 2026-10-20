using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("TAC_GIA")]
public partial class TacGium
{
    [Key]
    public int Mtg { get; set; }

    [Column("Ten_tac_gia")]
    [StringLength(50)]
    public string? TenTacGia { get; set; }

    [Column("Dia_chi")]
    [StringLength(100)]
    public string? DiaChi { get; set; }

    [Column("Dien_thoai")]
    [StringLength(15)]
    public string? DienThoai { get; set; }

    [InverseProperty("MtgNavigation")]
    public virtual ICollection<ThamGium> ThamGia { get; set; } = new List<ThamGium>();
}
