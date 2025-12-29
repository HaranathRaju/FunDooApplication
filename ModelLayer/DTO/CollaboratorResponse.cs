using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CollaboratorResponse
    {
        public Guid CollaboratorId { get; set; }
        public Guid NoteId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
