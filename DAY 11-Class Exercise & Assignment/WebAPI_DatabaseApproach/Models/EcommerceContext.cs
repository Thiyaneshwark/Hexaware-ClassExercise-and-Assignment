using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI_DatabaseApproach.Models
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9O97IJJ;Database=Ecommerce;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId)
                    .ValueGeneratedNever()
                    .HasColumnName("cart_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__cart__customer_i__3C69FB99");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__cart__product_id__3D5E1FD2");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053480EE3542")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("customer_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.ShippingAddress)
                    .HasColumnType("text")
                    .HasColumnName("shipping_address");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__orders__shipping__403A8C7D");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");

                entity.Property(e => e.OrderItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_item_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderQuantity).HasColumnName("Order_quantity");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__Order__4316F928");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__order_ite__produ__440B1D61");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_id");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("productName");

                entity.Property(e => e.StockQuantity).HasColumnName("stockQuantity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
