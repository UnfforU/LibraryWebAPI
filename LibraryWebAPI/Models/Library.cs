using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class Library
{
    public Guid LibraryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
