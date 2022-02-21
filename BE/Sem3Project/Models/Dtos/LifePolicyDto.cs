using System;

namespace Sem3Project.Models.Dtos
{
    public class LifePolicyDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public int PersonClaim { get; set; }

        public int AmountPaid { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime? ReleasedDate { get; set; }

        public Boolean IsReleased { get; set; }

        public ModifierInfoDto CreatedByInfo { get; set; }

        public ModifierInfoDto ModifiedByInfo { get; set; }
    }
}
