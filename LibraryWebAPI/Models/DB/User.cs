namespace LibraryWebAPI.Models.DB;

public partial class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public byte UserRoleId { get; set; }
    public bool IsDeleted { get; set; }

    public virtual List<Order> Orders { get; set; } = new List<Order>();
    public virtual UserRole UserRole { get; set; } = null!;
}
