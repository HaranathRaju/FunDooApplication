using ModelLayer.Entities;
using System;
using System.Collections.Generic;

namespace DataLogicLayer.Interfaces
{
    public interface ICollaboratorRepository
    {
        Collaborator AddCollaborator(Collaborator collaborator);

        IEnumerable<Collaborator> GetCollaboratorsByNoteId(Guid noteId);

        bool RemoveCollaboratorById(Guid collaboratorId);

        IEnumerable<Notes> GetSharedNotes(string email);
    }

}
