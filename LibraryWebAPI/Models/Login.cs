using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
