using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class VehicleInsuranceCreateDto : IValidatableObject
    {
        [Required(ErrorMessage = "Plate number is required")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "Engine number is required")]
        public string EngineNumber { get; set; }

        [Required(ErrorMessage = "Chassis number is required")]
        public string ChassisNumber { get; set; }

        [Required(ErrorMessage = "Vehicle type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Effective date is required")]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "Expire date is required")]
        public DateTime ExpireDate { get; set; }


        [Required(ErrorMessage = "User id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Vehicle policy id is required")]
        public string VehiclePolicyId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (ExpireDate <= EffectiveDate)
            {
                validationResults.Add(new ValidationResult(
                    errorMessage : "ExpireDate must be greater than EffectiveDate",
                    memberNames: new string[] { "DateValidation" }
                ));
            }

            if (ExpireDate <= DateTime.Now)
            {
                validationResults.Add(new ValidationResult(
                   errorMessage : "ExpireDate must be greater than now",
                   memberNames: new string[] { "DateValidation" }
                ));
            }

            if (EffectiveDate <= DateTime.Now)
            { 
                validationResults.Add(new ValidationResult(
                   errorMessage: "EffectiveDate must be greater than now",
                   memberNames: new string[] { "DateValidation" }
                ));
            }
            return validationResults;
        }
    }
}
