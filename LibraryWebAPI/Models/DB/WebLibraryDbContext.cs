using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Models.DB;

public partial class WebLibraryDbContext : DbContext
{
    public WebLibraryDbContext(DbContextOptions<WebLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<AuthorBook> AuthorBooks { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Library> Libraries { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(600);
        });

        modelBuilder.Entity<AuthorBook>(entity =>
        {
            entity.ToTable("Author_Book");

            entity.Property(e => e.AuthorBookId).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Author_Book_Author");

            entity.HasOne(d => d.Book).WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Author_Book_Book");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.BookId).ValueGeneratedNever();
            entity.Property(e => e.Cover).HasColumnType("image");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Library).WithMany(p => p.Books)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Library");

            entity.HasOne(d => d.Owner).WithMany(p => p.Books)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_Book_User");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.ToTable("Library");

            entity.Property(e => e.LibraryId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(1000);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salt).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(40);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserRole");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.Property(e => e.UserRoleId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
