using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
