using ModelLayer.DTO;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface ILabelService
    {
        LabelResponse AddLabel(LabelRequest request, Guid userId);
        IEnumerable<LabelResponse> GetLabels(Guid userId);
        bool DeleteLabel(Guid labelId, Guid userId);

        bool AddLabelToNote(Guid noteId, Guid labelId);
        bool RemoveLabelFromNote(Guid noteId, Guid labelId);
    }
}
