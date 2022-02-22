using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem3Project.Models
{
    public class MedicalInsurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirtsName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Relationship is required")]
        public string Relationship { get; set; }

        public string Image { get; set; } = string.Empty;

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }

        public bool IsVerified { get; set; } = false;

        public string Token { get; set; }

        // Navigation Properties
        [Required(ErrorMessage = "User Id is required")]
        public User User { get; set; }

        [Required(ErrorMessage = "Medical Policy Id is required")]
        public MedicalPolicy MedicalPolicy { get; set; }

        [Required(ErrorMessage = "Hospital Id is required")]
        public Hospital Hospital { get; set; }

        [Required(ErrorMessage = "Receipt Id is required")]
        public Receipt Receipt { get; set; }
    }
}
