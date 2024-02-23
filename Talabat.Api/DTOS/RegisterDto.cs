using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.DTOS
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
