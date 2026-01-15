using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTO
{
    public class NoteRequest
    {
        [Required(ErrorMessage = "Title cannot be null")]
        [MaxLength(255, ErrorMessage = "Length of characters should not exceed 255")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? Reminder { get; set; }

        public string? Image { get; set; }

        [MaxLength(7)]
        public string Colour { get; set; } = "#FFFFFF";

        public bool IsArchive { get; set; } = false;

        public bool IsPin { get; set; } = false;

        public bool IsTrash { get; set; } = false;
    }
}
