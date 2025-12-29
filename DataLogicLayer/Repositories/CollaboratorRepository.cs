using DataLogicLayer.Context;
using DataLogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLogicLayer.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly FundooContext _context;

        public CollaboratorRepository(FundooContext context)
        {
            _context = context;
        }

        public Collaborator AddCollaborator(Collaborator collaborator)
        {
            _context.Collaborators.Add(collaborator);
            _context.SaveChanges();
            return collaborator;
        }

        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(Guid noteId)
        {
            return _context.Collaborators
                           .Where(c => c.NoteId == noteId)
                           .ToList();
        }

        public bool RemoveCollaboratorById(Guid collaboratorId)
        {
            var collaborator = _context.Collaborators.Find(collaboratorId);
            if (collaborator == null)
                return false;

            _context.Collaborators.Remove(collaborator);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Notes> GetSharedNotes(string email)
        {
            var noteIds = _context.Collaborators
                                  .Where(c => c.User.Email == email) 
                                  .Select(c => c.NoteId)
                                  .ToList();

            return _context.Notes
                           .Where(n => noteIds.Contains(n.Id))
                           .ToList();
        }
    }
}
