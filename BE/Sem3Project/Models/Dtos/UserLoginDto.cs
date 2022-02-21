using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$",
           ErrorMessage = "Password must be between 6 and 20; containts at least one uppercase character, one number.")]
        public string Password { get; set; }
    }
}
