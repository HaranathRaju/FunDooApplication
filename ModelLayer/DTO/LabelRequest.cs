using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTO
{
    public class LabelRequest
    {
        public Guid NoteId { get; set; }
        public string LabelName { get; set; }
    }

}
