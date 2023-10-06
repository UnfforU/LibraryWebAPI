namespace LibraryWebAPI.Models.DB;

public partial class AuthorBook
{
    public Guid AuthorBookId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Author Author { get; set; } = null!;
    public virtual Book Book { get; set; } = null!;
}
