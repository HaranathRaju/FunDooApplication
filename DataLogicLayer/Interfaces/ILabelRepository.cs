using ModelLayer.Entities;
using System;
using System.Collections.Generic;

namespace DataLogicLayer.Interfaces
{
    public interface ILabelRepository
    {
        Label AddLabel(Label label);
        IEnumerable<Label> GetLabelsByUserId(Guid userId);
        bool DeleteLabel(Guid labelId, Guid userId);

        bool AddLabelToNote(Guid noteId, Guid labelId);
        bool RemoveLabelFromNote(Guid noteId, Guid labelId);

        Label GetLabelByName(string labelName, Guid userId);
    }
}
