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
using System.Threading.Tasks;

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
        public async Task<ActionResult> GetAllEvents()
        {
            List<Event> evnts = await _eventService.FindAuthorized();
            return Ok(evnts);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a specific event.", OperationId = "GetEvent")]
        [SwaggerResponse(200, "Returns the specified event.", typeof(Event))]
        [SwaggerResponse(403, "Event not found or no access to specific event.")]
        public async Task<ActionResult> GetEvent([SwaggerParameter("Event Id")] Guid id)
        {
            Event evnt = await _eventService.FindOne(id);
            if (evnt == null)
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
        public async Task<ActionResult> Create(
            [FromBody, Required]
            [SwaggerParameter("Event values")]
                EventModel model)
        {
            Event evnt = await _eventService.Create(model.Title, model.Description);
            if (evnt == null)
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
        public async Task<ActionResult> Update(
            [SwaggerParameter("Event Id")] Guid id,
            [FromBody] EventModel model)
        {
            Event evnt = await _eventService.Update(id, model.Title, model.Description);
            if (evnt == null)
            {
                return Unauthorized();
            }

            return Ok(evnt);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a specific event.", OperationId = "DeleteEvent")]
        [SwaggerResponse(204, "Event was successfully deleted.")]
        [SwaggerResponse(403, "No event found or no access to specific event.")]
        public async Task<ActionResult> DeleteEvent([SwaggerParameter("Event Id")] Guid id)
        {
            bool isDeleted = await _eventService.DeleteEvent(id);
            if (!isDeleted)
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
