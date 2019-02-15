using System;
using System.Linq;
using Avocado.Domain.Services;
using Avocado.Domain.Entities;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Avocado.Web.Controllers
{
    [Route("api/events"), Authorize, ApiController]
    [Produces("application/json")]
    [SwaggerTag("Create, read, update, and delete Events.")]
    public class EventsController : Controller
    {
        private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all the events that the user has access to.", OperationId = "GetEvents")]
        [SwaggerResponse(200, "All events for the user.", typeof(List<Event>))]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventService.FindAuthorized());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a specific event.", OperationId = "GetEvent")]
        [SwaggerResponse(200, "Returns the specified event.", typeof(Event))]
        [SwaggerResponse(403, "Event not found or no access to specific event.")]
        public IActionResult GetEvent([SwaggerParameter("Event Id")] Guid id)
        {
            if (!_eventService.TryFindOne(id, out Event evnt))
            {
                return Unauthorized();
            }
            return Ok(evnt);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/events
        ///     {
        ///        "title": "Some title",
        ///        "description": "Some description"
        ///     }
        ///
        /// </remarks> 
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Event.",
            OperationId = "CreateEvent",
            Consumes = new[] { "application/json" })]
        [SwaggerResponse(201, "The event was created.", typeof(Event))]
        [SwaggerResponse(403, "User account was not found.")]
        public IActionResult Create(
            [FromBody, Required]
            [SwaggerParameter("Event values")]
                EventModel model)
        {
            if (!_eventService.TryCreate(model.Title, model.Description, out Event evnt))
            {
                return Unauthorized();
            }

            return Ok(evnt);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/events/{id}
        ///     {
        ///        "title": "Some title",
        ///        "description": "Some description"
        ///     }
        ///
        /// </remarks> 
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates a specific event.",
            OperationId = "UpdateEvent",
            Consumes = new[] { "application/json" })]
        [SwaggerResponse(200, "Returns successfully updated event.", typeof(Event))]
        [SwaggerResponse(403, "No event found or no access to specific event.")]
        public IActionResult Update(
            [SwaggerParameter("Event Id")] Guid id,
            [FromBody] EventModel model)
        {
            if (!_eventService.TryUpdate(id, model.Title, model.Description, out Event evnt))
            {
                return Unauthorized();
            }
            return Ok(evnt);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a specific event.", OperationId = "DeleteEvent")]
        [SwaggerResponse(204, "Event was successfully deleted.")]
        [SwaggerResponse(403, "No event found or no access to specific event.")]
        public IActionResult DeleteEvent([SwaggerParameter("Event Id")] Guid id)
        {
            if (!_eventService.TryDeleteEvent(id))
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
