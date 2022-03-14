using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class PaymentMethodDto
    {
        [Required]
        public string Method { get; set; }
    }
}
