using System;

namespace Sem3Project.Models.Dtos
{
    public class VehicleInsuranceDto
    {
        public Guid Id { get; set; }

        public string PlateNumber { get; set; }

        public string EngineNumber { get; set; }

        public string ChassisNumber { get; set; }

        public string Type { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }

        public bool IsVerified { get; set; }

        public ModifierInfoDto CreatedByInfo { get; set; }

        public ModifierInfoDto ModifiedByInfo { get; set; }

        // public User User { get; set; }

        public VehiclePolicy VehiclePolicy { get; set; }

        public Receipt Receipt { get; set; }
    }
}
