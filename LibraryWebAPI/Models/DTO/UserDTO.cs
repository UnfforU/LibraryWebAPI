using LibraryWebAPI.Models.Extra;

namespace LibraryWebAPI.Models.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public EnumUserRoles UserRole { get; set; } 
    }
}
