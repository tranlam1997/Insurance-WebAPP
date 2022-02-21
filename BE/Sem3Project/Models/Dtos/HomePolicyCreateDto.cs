using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class HomePolicyCreateDto
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int AmountPaid { get; set; }
    }
}
