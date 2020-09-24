using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prj1.Models
{
    public partial class Prj1DBContext : DbContext
    {
        public Prj1DBContext(DbContextOptions<Prj1DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<UserData> UsersData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Goods__B40CC6ED750EF595");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductCategory)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserData__1788CCAC2312EE06");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
