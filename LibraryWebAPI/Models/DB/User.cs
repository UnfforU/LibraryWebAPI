﻿namespace LibraryWebAPI.Models.DB;

public partial class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public byte UserRoleId { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    public virtual UserRole UserRole { get; set; } = null!;
}
