using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models.DB;

public partial class UserRole
{
    public byte UserRoleId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
