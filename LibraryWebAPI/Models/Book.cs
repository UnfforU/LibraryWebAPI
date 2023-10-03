using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class Book
{
    public Guid BookId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public byte[]? Cover { get; set; }
    public Guid LibraryId { get; set; }
    public bool? IsDeleted { get; set; }
    public Guid? OwnerId { get; set; }
    public DateTime? BookedDate { get; set; }
    public bool? IsBooked { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    public virtual Library Library { get; set; } = null!;
    public virtual User? Owner { get; set; }
}
