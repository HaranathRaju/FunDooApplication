using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entities;

namespace DataLogicLayer.Interfaces
{
    public interface INotesRepository
    {
        Notes AddNote(Notes notes);

        IEnumerable<Notes> GetNotesByUserId(Guid UserId);

        Notes GetNoteById(Guid Id);

        Notes UpdateNote(Notes notes);

        void DeleteNote(Notes note);
    }
}
