using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entities
{
    public class Collaborator
    {
        [Key]
        public Guid CollaboratorId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Notes")]
        public Guid NoteId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }

 
        public Notes Notes { get; set; }
    }
}




