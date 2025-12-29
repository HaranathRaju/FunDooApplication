using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BusinessLogicLayer.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public NotesService(
            INotesRepository notesRepository,
            IMapper mapper,
            IDistributedCache cache)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public NoteResponse AddNote(NoteRequest dto, Guid userId)
        {
            var note = _mapper.Map<Notes>(dto);
            note.Id = Guid.NewGuid();
            note.UserId = userId;
            note.CreatedAt = DateTime.UtcNow;

            var savedNote = _notesRepository.AddNote(note);

            _cache.Remove($"NotesList_{userId}");

            return _mapper.Map<NoteResponse>(savedNote);
        }

        public IEnumerable<NoteResponse> GetNotesByUserId(Guid userId)
        {
            string cacheKey = $"NotesList_{userId}";

            var cachedData = _cache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<NoteResponse>>(cachedData);
            }

            var notes = _notesRepository.GetNotesByUserId(userId);
            var response = _mapper.Map<IEnumerable<NoteResponse>>(notes);

            _cache.SetString(cacheKey, JsonSerializer.Serialize(response));

            return response;
        }

        public NoteResponse GetNoteById(Guid noteId, Guid userId)
        {
            string cacheKey = $"Note_{noteId}";

            var cachedData = _cache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<NoteResponse>(cachedData);
            }

            var note = _notesRepository.GetNoteById(noteId);
            if (note == null || note.UserId != userId)
                return null;

            var response = _mapper.Map<NoteResponse>(note);

            _cache.SetString(cacheKey, JsonSerializer.Serialize(response));

            return response;
        }

        public NoteResponse UpdateNote(UpdateNoteRequest dto, Guid userId)
        {
            var note = _notesRepository.GetNoteById(dto.Id);
            if (note == null || note.UserId != userId)
                return null;

            _mapper.Map(dto, note);
            note.UpdatedAt = DateTime.Now;

            var updatedNote = _notesRepository.UpdateNote(note);

            _cache.Remove($"NotesList_{userId}");
            _cache.Remove($"Note_{dto.Id}");

            return _mapper.Map<NoteResponse>(updatedNote);
        }

        public bool DeleteNote(Guid noteId, Guid userId)
        {
            var note = _notesRepository.GetNoteById(noteId);
            if (note == null || note.UserId != userId)
                return false;

            _notesRepository.DeleteNote(note);

       
            _cache.Remove($"NotesList_{userId}");
            _cache.Remove($"Note_{noteId}");

            return true;
        }
    }
}
