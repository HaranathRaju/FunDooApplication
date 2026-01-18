using ModelLayer.DTO;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICollaboratorService
    {
        CollaboratorResponse AddCollaborator(Guid noteId, string email);

        IEnumerable<CollaboratorResponse> GetCollaboratorsByNoteId(Guid noteId);

        bool RemoveCollaboratorById(Guid collaboratorId);

        IEnumerable<Notes> GetSharedNotes(string email);

    }

}


