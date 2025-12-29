using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CollaboratorRequest
    {
        [Required]
        public Guid NoteId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
