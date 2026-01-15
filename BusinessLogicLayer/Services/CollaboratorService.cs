using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Interfaces;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class CollaboratorService : ICollaboratorService
    {
    
        private readonly ICollaboratorRepository _repository;
        private readonly IUserRepository _userRepository;

        public CollaboratorService(
            ICollaboratorRepository repository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public CollaboratorResponse AddCollaborator(Guid noteId, string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new Exception("User with this email does not exist");

            var existing = _repository.GetCollaboratorsByNoteId(noteId)
                                      .FirstOrDefault(c => c.UserId == user.UserId);
            if (existing != null)
                throw new Exception("User is already a collaborator");

            var collaborator = new Collaborator
            {
                NoteId = noteId,
                UserId = user.UserId,
                Email = user.Email
            };

            var saved = _repository.AddCollaborator(collaborator);

            return new CollaboratorResponse
            {
                CollaboratorId = saved.CollaboratorId,
                NoteId = saved.NoteId,
                Email = saved.Email,
                CreatedAt = saved.CreatedAt
            };
        }

        public IEnumerable<CollaboratorResponse> GetCollaboratorsByNoteId(Guid noteId)
        {
            return _repository.GetCollaboratorsByNoteId(noteId)
                              .Select(c => new CollaboratorResponse
                              {
                                  CollaboratorId = c.CollaboratorId,
                                  NoteId = c.NoteId,
                                  Email = c.Email,
                                  CreatedAt = c.CreatedAt
                              })
                              .ToList();
        }

        public bool RemoveCollaboratorById(Guid collaboratorId)
        {
            return _repository.RemoveCollaboratorById(collaboratorId);
        }

        public IEnumerable<Notes> GetSharedNotes(string email)
        {
            return _repository.GetSharedNotes(email);
        }
    }
}
