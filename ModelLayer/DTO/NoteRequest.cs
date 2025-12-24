using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class NoteRequest
    {
        [Required(ErrorMessage = "title cannot be null")]
        [MaxLength(255, ErrorMessage = "length of characters should not exceed 255")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
        
        public string Colour { get; set; }

    }
}
