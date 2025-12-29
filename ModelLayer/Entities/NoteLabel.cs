using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class NoteLabel
    {
        public Guid NoteId { get; set; }

        public Notes Notes { get; set; }

        public Guid LabelId { get; set; }
        public Label Label { get; set; }
    }
}
