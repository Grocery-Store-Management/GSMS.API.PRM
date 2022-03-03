using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GSMS.API.PRM.Models
{
    public partial class GsmsContext : DbContext
    {
        public GsmsContext()
        {
        }

        public GsmsContext(DbContextOptions<GsmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ImportOrder> ImportOrders { get; set; }
        public virtual DbSet<ImportOrderDetail> ImportOrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Stores");
            });

            modelBuilder.Entity<ImportOrder>(entity =>
            {
                entity.ToTable("ImportOrder");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ImportOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportOrder_Store");
            });

            modelBuilder.Entity<ImportOrderDetail>(entity =>
            {
                entity.ToTable("ImportOrderDetail");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.Distributor).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ImportOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportOrderDetails_ImportOrders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImportOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportOrderDetail_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ExpiringDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipt");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipt_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipt_Employee");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_Stores");
            });

            modelBuilder.Entity<ReceiptDetail>(entity =>
            {
                entity.ToTable("ReceiptDetail");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ReceiptId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReceiptDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptDetail_Product");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ReceiptDetails)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptDetails_Receipts");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasMaxLength(40);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
