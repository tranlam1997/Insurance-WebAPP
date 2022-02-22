using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem3Project.Models
{
    public class HomeInsurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Land ownership certification id is required")]
        public string LandOwnershipId { get; set; }

        [Required(ErrorMessage = "Home characteristic is required")]
        public string HomeCharacteristic { get; set; }

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

        [Required(ErrorMessage = "Home Policy Id is required")]
        public HomePolicy HomePolicy { get; set; }

        [Required(ErrorMessage = "Receipt Id is required")]
        public Receipt Receipt { get; set; }
    }
}
