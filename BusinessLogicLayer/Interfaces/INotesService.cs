using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;
using ModelLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface INotesService
    {
        NoteResponse AddNote(NoteRequest dto, Guid userId);

    
        IEnumerable<NoteResponse> GetNotesByUserId(Guid userId);

       
        NoteResponse GetNoteById(Guid noteId, Guid userId);

        NoteResponse UpdateNote(UpdateNoteRequest dto, Guid userId);

        bool DeleteNote(Guid noteId, Guid userId);

    }
}
