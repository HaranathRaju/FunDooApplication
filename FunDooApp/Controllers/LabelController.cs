using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System;
using System.Security.Claims;

namespace FunDooApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelsController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPost]
        public IActionResult AddLabel([FromBody] LabelRequest request)
        {
            var response = _labelService.AddLabel(request, GetUserId());
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetLabels()
        {
            return Ok(_labelService.GetLabels(GetUserId()));
        }

        [HttpDelete("{labelId}")]
        public IActionResult DeleteLabel(Guid labelId)
        {
            var result = _labelService.DeleteLabel(labelId, GetUserId());
            return result ? Ok("Label deleted") : NotFound();
        }

        [HttpPost("add-to-note")]
        public IActionResult AddLabelToNote(Guid noteId, Guid labelId)
        {
            return _labelService.AddLabelToNote(noteId, labelId)
                ? Ok("Label added to note")
                : BadRequest("Already added");
        }

        [HttpDelete("remove-from-note")]
        public IActionResult RemoveLabelFromNote(Guid noteId, Guid labelId)
        {
            return _labelService.RemoveLabelFromNote(noteId, labelId)
                ? Ok("Label removed from note")
                : NotFound();
        }
    }
}
