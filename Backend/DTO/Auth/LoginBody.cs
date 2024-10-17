using System.ComponentModel.DataAnnotations;

namespace Backend.DTO.Auth
{
    public class LoginBody
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
