using System;
using System.Collections.Generic;

namespace Sem3Project.Models.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        //public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        //public List<VehicleInsurance> VehicleInsurances { get; set; }

        //public List<Bill> Bills { get; set; }
    }
}
