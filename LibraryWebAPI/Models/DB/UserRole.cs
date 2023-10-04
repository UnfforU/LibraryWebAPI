using LibraryWebAPI.Models.Extra;
using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models.DB;

public partial class UserRole
{
    public Guid UserRoleId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public EnumUserRoles RoleIndex { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
