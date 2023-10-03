using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class UserRole
{
    public Guid UserRoleId { get; set; }
    public string Name { get; set; } = null!;
    public bool? IsDeleted { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
