using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eProject3.Models
{
    public partial class C2108L_Nhom6Context : DbContext
    {
        public C2108L_Nhom6Context()
        {
        }

        public C2108L_Nhom6Context(DbContextOptions<C2108L_Nhom6Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ChucVu> ChucVus { get; set; } = null!;
        public virtual DbSet<DichVu> DichVus { get; set; } = null!;
        public virtual DbSet<FeedBack> FeedBacks { get; set; } = null!;
        public virtual DbSet<Loai> Loais { get; set; } = null!;
        public virtual DbSet<Provider> Providers { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<ThanhToan> ThanhToans { get; set; } = null!;
        public virtual DbSet<ThongTin> ThongTins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=103.184.112.82;Database=C2108L_Nhom6;User ID=C2108L_Nhom6;Password=1q2w3E*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__ChucVu__8AFACE1A13BB8DAE");

                entity.ToTable("ChucVu");

                entity.Property(e => e.Rol)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__DichVu__C51BB00AEA0AB192");

                entity.ToTable("DichVu");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.SerName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Thumb)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("Category");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("SerProvider");
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.HasKey(e => e.FeedBack1)
                    .HasName("PK__FeedBack__7EE9E5B51D1FF1D1");

                entity.ToTable("FeedBack");

                entity.Property(e => e.FeedBack1).HasColumnName("FeedBack");

                entity.Property(e => e.Contents)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("AccFB");
            });

            modelBuilder.Entity<Loai>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__Loai__6A1C8AFA7DC999F9");

                entity.ToTable("Loai");

                entity.Property(e => e.CatName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.ToTable("Provider");

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__TaiKhoan__349DA5A63F800DBD");

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Role");
            });

            modelBuilder.Entity<ThanhToan>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__ThanhToa__11F2FC6AB9E447A2");

                entity.ToTable("ThanhToan");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("Acc");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.DetailId)
                    .HasConstraintName("Detail");

                entity.HasOne(d => d.DichVu)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Service");
            });

            modelBuilder.Entity<ThongTin>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__ThongTin__135C316DFB946033");

                entity.ToTable("ThongTin");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ThongTins)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Ser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
