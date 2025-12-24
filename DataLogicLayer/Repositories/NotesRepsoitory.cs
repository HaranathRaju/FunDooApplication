using DataLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entities;
using DataLogicLayer.Context;

namespace DataLogicLayer.Repositories
{
    public class NotesRepsoitory : INotesRepository
    {
        private readonly FundooContext fundooContext;
        public NotesRepsoitory( FundooContext fundooContext)
        {
            this.fundooContext=fundooContext;
        }

        public  Notes AddNote(Notes notes)
        {
            fundooContext.Notes.Add(notes);
            fundooContext.SaveChanges();
            return notes;
        }

        public IEnumerable<Notes> GetNotesByUserId(Guid UserId)
        {
            return fundooContext.Notes
                .Where(n => n.UserId == UserId)
                .ToList();
        }

        public Notes GetNoteById(Guid Id)
        {
            return fundooContext.Notes.FirstOrDefault(n => n.Id == Id);

        }

        public Notes UpdateNote(Notes notes)
        {
            fundooContext.Notes.Update(notes);
            fundooContext.SaveChanges();
            return notes;
        }

        public void DeleteNote(Notes notes)
        {
            fundooContext.Remove(notes);
            fundooContext.SaveChanges();
        }
    }
}
