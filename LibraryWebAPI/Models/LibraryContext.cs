using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .HasDefaultValue(new Guid())
                .HasColumnName("authorId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("book");

            entity.HasIndex(e => e.AuthorId, "IXFK_book_author");

            entity.HasIndex(e => e.LibraryId, "IXFK_book_library");

            entity.HasIndex(e => e.OwnerId, "IXFK_book_user");

            entity.Property(e => e.BookId)
                .HasDefaultValue(new Guid())
                .HasColumnName("bookId");
            entity.Property(e => e.AuthorId).HasColumnName("author");
            entity.Property(e => e.BookedDate).HasColumnName("bookedDate");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IsBooked).HasColumnName("isBooked");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.LibraryId).HasColumnName("library");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_book_author");

            entity.HasOne(d => d.LibraryNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_book_library");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_book_user");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.ToTable("library");

            entity.Property(e => e.LibraryId)
                .HasDefaultValue(new Guid())
                .HasColumnName("libraryId");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
