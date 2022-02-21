using System;
using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class UserUpdateDto
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
