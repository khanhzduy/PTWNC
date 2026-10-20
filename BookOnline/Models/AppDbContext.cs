using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookOnline.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<CtDatHang> CtDatHangs { get; set; }

    public virtual DbSet<DonDatHang> DonDatHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<QuangCao> QuangCaos { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TacGium> TacGia { get; set; }

    public virtual DbSet<ThamGium> ThamGia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=QLBanSach;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.Mcd)
                .HasName("aaaaaCHU_DE_PK")
                .IsClustered(false);

            entity.Property(e => e.Pid).HasDefaultValue(0);
        });

        modelBuilder.Entity<CtDatHang>(entity =>
        {
            entity.HasKey(e => e.Mctddh)
                .HasName("aaaaaCT_DAT_HANG_PK")
                .IsClustered(false);

            entity.HasOne(d => d.MsNavigation).WithMany(p => p.CtDatHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CT_DAT_HANG_FK01");

            entity.HasOne(d => d.SdhNavigation).WithMany(p => p.CtDatHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CT_DAT_HANG_FK00");
        });

        modelBuilder.Entity<DonDatHang>(entity =>
        {
            entity.HasKey(e => e.Sdh)
                .HasName("aaaaaDON_DAT_HANG_PK")
                .IsClustered(false);

            entity.HasOne(d => d.MkhNavigation).WithMany(p => p.DonDatHangs).HasConstraintName("DON_DAT_HANG_FK00");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Mkh)
                .HasName("aaaaaKHACH_HANG_PK")
                .IsClustered(false);
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.Mnxb)
                .HasName("aaaaaNHA_XUAT_BAN_PK")
                .IsClustered(false);
        });

        modelBuilder.Entity<QuangCao>(entity =>
        {
            entity.HasKey(e => e.Stt)
                .HasName("aaaaaQUANG_CAO_PK")
                .IsClustered(false);
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Ms)
                .HasName("aaaaaSACH_PK")
                .IsClustered(false);

            entity.Property(e => e.DonViTinh).HasDefaultValue("Cu?n");
            entity.Property(e => e.SoLanXem).HasDefaultValue(0);
            entity.Property(e => e.SoLuongBan).HasDefaultValue(0);

            entity.HasOne(d => d.McdNavigation).WithMany(p => p.Saches).HasConstraintName("SACH_FK00");

            entity.HasOne(d => d.MnxbNavigation).WithMany(p => p.Saches).HasConstraintName("SACH_FK01");
        });

        modelBuilder.Entity<TacGium>(entity =>
        {
            entity.HasKey(e => e.Mtg)
                .HasName("aaaaaTAC_GIA_PK")
                .IsClustered(false);
        });

        modelBuilder.Entity<ThamGium>(entity =>
        {
            entity.HasKey(e => new { e.Ms, e.Mtg })
                .HasName("aaaaaTHAM_GIA_PK")
                .IsClustered(false);

            entity.HasOne(d => d.MsNavigation).WithMany(p => p.ThamGia).HasConstraintName("THAM_GIA_FK00");

            entity.HasOne(d => d.MtgNavigation).WithMany(p => p.ThamGia).HasConstraintName("THAM_GIA_FK01");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
