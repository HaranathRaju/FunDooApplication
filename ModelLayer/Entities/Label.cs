using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entities
{
    public class Label
    {
        [Key]
        public Guid LabelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string LabelName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<NoteLabel> NoteLabels { get; set; }
    }
}
