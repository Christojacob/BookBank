using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookBankLibrary.Models
{
    public partial class BookBankContext : DbContext
    {
        public BookBankContext()
        {
        }

        public BookBankContext(DbContextOptions<BookBankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=bookbanksql.cjvyxbowhyzs.us-east-1.rds.amazonaws.com,1433;Initial Catalog=BookBank;User ID=christo;Password=Christo1995;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasMaxLength(50);

                entity.Property(e => e.AuthorCountry)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AuthorDescription).HasMaxLength(500);

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookIsbn);

                entity.ToTable("Book");

                entity.Property(e => e.BookIsbn).HasMaxLength(50);

                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookDescription).HasMaxLength(500);

                entity.Property(e => e.BookGenre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BookUrl)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Author");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
