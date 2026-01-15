using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.DTO;
using ModelLayer.Exceptions;
using System.Security.Claims;

namespace FunDooApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesService notesService;
        private readonly ILogger<NotesController> logger;

        public NotesController(
            INotesService notesService,
            ILogger<NotesController> logger)
        {
            this.notesService = notesService;
            this.logger = logger;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new AppException("Unauthorized", 401);

            return Guid.Parse(userIdClaim.Value);
        }

        [HttpPost]
        public IActionResult AddNote([FromBody] NoteRequest request)
        {
            var userId = GetUserId();
            var response = notesService.AddNote(request, userId);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            logger.LogInformation("GetAllNotes API called");

            var userId = GetUserId();
            var notes = notesService.GetNotesByUserId(userId);

            logger.LogInformation("Fetched notes for UserId: {UserId}", userId);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetNoteById(Guid id)
        {
            var userId = GetUserId();
            var note = notesService.GetNoteById(id, userId);

            if (note == null)
                throw new AppException("Note not found or access denied", 404);

            return Ok(note);
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteRequest request)
        {
            var userId = GetUserId();
            var updatedNote = notesService.UpdateNote(request, userId);

            if (updatedNote == null)
                throw new AppException("Note not found or access denied", 404);

            return Ok(updatedNote);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var userId = GetUserId();
            var result = notesService.DeleteNote(id, userId);

            if (!result)
                throw new AppException("Note not found or access denied", 404);

            return Ok(new { message = "Note deleted successfully" });
        }
    }
}
