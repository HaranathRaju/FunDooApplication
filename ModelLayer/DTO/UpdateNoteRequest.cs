using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UpdateNoteRequest
    {
        [Required]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "title cannot be null")]
        [MaxLength(255, ErrorMessage = "length of characters should not exceed 255")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string Colour { get; set; } = "#FFFFFF";
    }
}
