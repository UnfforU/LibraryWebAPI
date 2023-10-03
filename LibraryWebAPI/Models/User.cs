using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid RoleId { get; set; }
    public bool? IsDeleted { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    public virtual UserRole Role { get; set; } = null!;
}
