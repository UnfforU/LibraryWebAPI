namespace LibraryWebAPI.Models.DB;

public partial class Book
{
    public Guid BookId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public byte[]? Cover { get; set; }
    public Guid LibraryId { get; set; }
    public bool IsDeleted { get; set; }

    public virtual List<File> Files { get; set; } = new List<File>();
    public virtual List<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    public virtual Library Library { get; set; } = null!;
    public virtual List<Order> Orders { get; set; } = new List<Order>();
}
