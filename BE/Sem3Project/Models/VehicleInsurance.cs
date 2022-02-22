using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem3Project.Models
{
    public class VehicleInsurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Plate number is required")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "Engine number is required")]
        public string EngineNumber { get; set; }

        [Required(ErrorMessage = "Chassis number is required")]
        public string ChassisNumber { get; set; }

        [Required(ErrorMessage = "Vehicle type is required")]
        public string Type { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }

        public bool IsVerified { get; set; } = false;

        public string Token { get; set; }

        // Navigation Properties
        [Required(ErrorMessage = "User Id is required")]
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Vehicle Policy Id is required")]
        public virtual VehiclePolicy VehiclePolicy { get; set; }

        [Required(ErrorMessage = "Receipt Id is required")]
        public virtual Receipt Receipt { get; set; }
    }
}
