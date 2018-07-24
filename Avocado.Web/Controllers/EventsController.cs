using System;
using System.Linq;
using Avocado.Domain.Services;
using Avocado.Domain.Entities;
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
            if (!_eventService.TryFindOne(id, out Event evnt))
            {
                return Unauthorized();
            }
            return Ok(evnt);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventModel model)
        {
            var errors = model.GetValidationErrors();
            if (errors.Any())
            {
                return BadRequest(errors);
            }

            if (!_eventService.TryCreate(model.Title, model.Description, out Event evnt))
            {
                return Unauthorized();
            }

            return Ok(evnt);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] EventModel model)
        {
            var errors = model.GetValidationErrors();
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            if (!_eventService.TryUpdate(id, model.Title, model.Description, out Event evnt))
            {
                return Unauthorized();
            }
            return Ok(evnt);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            if (!_eventService.TryDeleteEvent(id))
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}