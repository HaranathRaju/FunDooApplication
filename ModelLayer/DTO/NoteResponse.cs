using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class NoteResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "title cannot be null")]
        [MaxLength(255, ErrorMessage = "length of characters should not exceed 255")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        public DateTime? Reminder { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Image { get; set; }

        [MaxLength(7)]
        public string Colour { get; set; } = "#FFFFFF";

        public bool IsArchive { get; set; } = false;

        public bool IsPin { get; set; } = false;

        public bool IsTrash { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
