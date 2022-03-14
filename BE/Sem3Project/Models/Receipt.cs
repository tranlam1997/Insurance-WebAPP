using Sem3Project.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem3Project.Models
{
    public class Receipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Minumum payment is required")]
        public int MinimumPayment { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        public int Balance { get; set; }

        public string PaymentType { get; set;}

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }  

        public string UserId { get; set; }

        public string InsuranceId { get; set; }

        [Required(ErrorMessage = "Insurance Type is required")]
        [EnumDataType(typeof(InsuranceType))]
        public InsuranceType InsuranceType { get; set; }

        public string VerifiedBy { get; set; }

        public DateTime? VerifiedTime { get; set; }

        public string PaymentId { get; set; }
    }
}
