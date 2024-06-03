using AuthExample.Model;
using System.ComponentModel.DataAnnotations;

namespace AuthExample.DTO
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; } = Role.User;
    }
}
