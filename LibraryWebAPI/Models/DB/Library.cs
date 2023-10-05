using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models.DB;

public partial class Library
{
    public Guid LibraryId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public virtual List<Book> Books { get; set; } = new List<Book>();
}
