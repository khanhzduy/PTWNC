using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

[Table("KHACH_HANG")]
public partial class KhachHang
{
    [Key]
    public int Mkh { get; set; }

    [Column("Ho_ten")]
    [StringLength(50)]
    public string HoTen { get; set; } = null!;

    [Column("Dia_chi")]
    [StringLength(50)]
    public string? DiaChi { get; set; }

    [Column("Dien_thoai")]
    [StringLength(10)]
    public string? DienThoai { get; set; }

    [Column("Ten_dang_nhap")]
    [StringLength(15)]
    public string TenDangNhap { get; set; } = null!;

    [Column("Mat_khau")]
    [StringLength(15)]
    public string MatKhau { get; set; } = null!;

    [Column("Ngay_sinh", TypeName = "datetime")]
    public DateTime? NgaySinh { get; set; }

    [Column("Gioi_tinh")]
    public bool GioiTinh { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [InverseProperty("MkhNavigation")]
    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
}
