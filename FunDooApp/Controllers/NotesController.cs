using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Security.Claims;
using Microsoft.Extensions.Logging;


namespace FunDooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesService notesService;
        private readonly ILogger<NotesController> ilogger;

        public NotesController(INotesService notesService, ILogger<NotesController> ilogger)
        {
            this.notesService = notesService;
            this.ilogger = ilogger; 
        }
        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId);
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
            ilogger.LogInformation("GetAllNotes API called");

            try
            {
                var userId = GetUserId();
                var notes = notesService.GetNotesByUserId(userId);

                ilogger.LogInformation("Fetched notes for UserId: {UserId}", userId);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, "Error while fetching notes");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetNoteById(Guid id)
        {
            var userId = GetUserId();
            var note = notesService.GetNoteById(id, userId);

            if (note == null)
                return NotFound("Note not found or access denied");

            return Ok(note);
        }


        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteRequest request)
        {
            var userId = GetUserId();
            var updatedNote = notesService.UpdateNote(request, userId);

            if (updatedNote == null)
                return NotFound("Note not found or access denied");

            return Ok(updatedNote);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var userId = GetUserId();
            var result = notesService.DeleteNote(id, userId);

            if (!result)
                return NotFound("Note not found or access denied");

            return Ok(new { message = "Note deleted successfully" });
        }
    }
}
