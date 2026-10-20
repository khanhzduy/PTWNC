using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("NHA_XUAT_BAN")]
public partial class NhaXuatBan
{
    [Key]
    public int Mnxb { get; set; }

    [Column("Ten_nha_xuat_ban")]
    [StringLength(100)]
    public string? TenNhaXuatBan { get; set; }

    [Column("Dia_chi")]
    [StringLength(150)]
    public string? DiaChi { get; set; }

    [Column("Dien_thoai")]
    [StringLength(15)]
    public string? DienThoai { get; set; }

    [InverseProperty("MnxbNavigation")]
    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
