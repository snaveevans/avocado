using System;
using System.Linq;
using Avocado.Domain.Services;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/events"), Authorize]
    public class EventsController : Controller
    {
        private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventService.FindAuthorized());
        }

        [HttpGet("{id}")]
        public IActionResult GetEvent(Guid id)
        {
            var evnt = _eventService.FindOne(id);
            if (evnt == null)
                return Unauthorized();
            return Ok(evnt);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventModel model)
        {
            var errors = model.GetValidationErrors();
            if (errors.Any())
                return BadRequest(errors);
            var evnt = _eventService.Create(model.Title, model.Description);
            return Ok(evnt);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] EventModel model)
        {
            var errors = model.GetValidationErrors();
            if (errors.Any())
                return BadRequest(errors);
            var evnt = _eventService.Update(id, model.Title, model.Description);
            if (evnt == null)
                return Unauthorized();
            return Ok(evnt);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            var isDeleted = _eventService.DeleteEvent(id);

            if (!isDeleted)
                return Unauthorized();

            return NoContent();
        }
    }
}