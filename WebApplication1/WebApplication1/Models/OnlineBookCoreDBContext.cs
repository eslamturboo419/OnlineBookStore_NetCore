using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models
{
    public partial class OnlineBookCoreDBContext : DbContext
    {
        public OnlineBookCoreDBContext()
        {
        }

        public OnlineBookCoreDBContext(DbContextOptions<OnlineBookCoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Usersaccount> Usersaccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=OnlineBookCoreDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .HasColumnName("author");

                entity.Property(e => e.Bookquantity).HasColumnName("bookquantity");

                entity.Property(e => e.Cataid).HasColumnName("cataid");

                entity.Property(e => e.Imgfile)
                    .HasMaxLength(50)
                    .HasColumnName("imgfile");

                entity.Property(e => e.Info).HasColumnName("info");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("date")
                    .HasColumnName("orderdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Usersaccount>(entity =>
            {
                entity.ToTable("usersaccounts");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .HasColumnName("pass");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
