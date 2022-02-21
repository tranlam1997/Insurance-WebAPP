using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$",
            ErrorMessage = "Password must be between 6 and 20; containts at least one uppercase character, one number.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$",
            ErrorMessage = "New password must be between 6 and 20; containts at least one uppercase character, one number.")]
        public string NewPassword { get; set; }
    }
}
