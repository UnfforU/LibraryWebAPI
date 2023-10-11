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
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");
            entity.HasQueryFilter(author => EF.Property<bool>(author, "IsDeleted") == false);

            entity.Property(e => e.Name).HasMaxLength(600);
        });

        modelBuilder.Entity<AuthorBook>(entity =>
        {
            entity.ToTable("Author_Book");
            entity.HasQueryFilter(ab => EF.Property<bool>(ab, "IsDeleted") == false);

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
            entity.HasQueryFilter(book => EF.Property<bool>(book, "IsDeleted") == false);

            entity.Property(e => e.Cover).HasColumnType("image");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Library).WithMany(p => p.Books)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Library");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.ToTable("Library");
            entity.HasQueryFilter(library => EF.Property<bool>(library, "IsDeleted") == false);

            entity.Property(e => e.Name).HasMaxLength(1000);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasQueryFilter(author => EF.Property<bool>(author, "IsDeleted") == false);

            entity.HasOne(d => d.Book).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false);

            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salt).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(40);

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserRole");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");
            entity.HasQueryFilter(ur => EF.Property<bool>(ur, "IsDeleted") == false);

            entity.Property(e => e.UserRoleId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
