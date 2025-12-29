using BusinessLogicLayer.Interfaces;
using ModelLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FunDooApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CollaboratorsController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorsController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        [HttpPost]
        public IActionResult AddCollaborator(
            [FromQuery] Guid noteId,
            [FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            try
            {
                CollaboratorResponse collaborator = _collaboratorService.AddCollaborator(noteId, email);
                return Ok(collaborator);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{noteId}")]
        public IActionResult GetCollaborators(Guid noteId)
        {
            IEnumerable<CollaboratorResponse> collaborators = _collaboratorService.GetCollaboratorsByNoteId(noteId);
            return Ok(collaborators);
        }

        [HttpDelete("{collaboratorId}")]
        public IActionResult RemoveCollaborator(Guid collaboratorId)
        {
            bool result = _collaboratorService.RemoveCollaboratorById(collaboratorId);
            if (!result)
                return NotFound("Collaborator not found");

            return Ok("Collaborator removed successfully");
        }

        [HttpGet("shared-notes")]
        public IActionResult GetSharedNotes([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var notes = _collaboratorService.GetSharedNotes(email); 
            return Ok(notes);
        }
    }
}




