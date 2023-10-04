using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models.DB;

public partial class Author
{
    public Guid AuthorId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
}
