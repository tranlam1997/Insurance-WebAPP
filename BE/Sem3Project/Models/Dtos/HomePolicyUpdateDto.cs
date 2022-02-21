using System;
using System.ComponentModel.DataAnnotations;

namespace Sem3Project.Models.Dtos
{
    public class HomePolicyUpdateDto
    {
        public string Type { get; set; }

        public string Content { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int AmountPaid { get; set; }

        public Boolean IsReleased { get; set; }
    }
}
