using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem3Project.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public List<VehicleInsurance> VehicleInsurances { get; set; }

        public List<LifeInsurance> LifeInsurances { get; set; }

        public List<HomeInsurance> HomeInsurances { get; set; }

        public List<MedicalInsurance> MedicalInsurances { get; set; }
    }
}
