using System;

namespace Sem3Project.Models.Dtos
{
    public class UserForAdminDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
