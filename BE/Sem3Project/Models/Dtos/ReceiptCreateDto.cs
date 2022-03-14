using Sem3Project.Enum;
using System;

namespace Sem3Project.Models.Dtos
{
    public class ReceiptCreateDto
    {
        public int MinimumPayment { get; set; }

        public int Balance { get; set; }

        public string PaymentType { get; set; }

        public string UserId { get; set; }

        public string InsuranceId { get; set; }

        public InsuranceType InsuranceType { get; set; }

        public string PaymentId { get; set; }
    }
}
