using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment_DBApproach.Models
{
    public partial class LibraryDBContext : DbContext
    {
        public LibraryDBContext()
        {
        }

        public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9O97IJJ;Database=LibraryDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Books__AuthorId__3B75D760");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Books__CategoryI__3C69FB99");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
