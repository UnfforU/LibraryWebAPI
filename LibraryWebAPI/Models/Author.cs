using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class Author
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
