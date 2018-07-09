using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Avocado.Infrastructure.Specifications.Events;
using Avocado.Web.Models;
using Avocado.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/events"), Authorize]
    public class EventsController : Controller
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IRepository<Invitee> _inviteeRepo;
        private readonly EventService _eventService;
        private readonly AccountService _accountService;

        public EventsController(IRepository<Event> eventRepo,
            IRepository<Invitee> inviteeRepo,
            EventService eventService,
            AccountService accountService)
        {
            _eventRepo = eventRepo;
            _inviteeRepo = inviteeRepo;
            _eventService = eventService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            if (!_accountService.TryGetCurrentAccount(out Account account))
            {
                return Unauthorized();
            }

            return Ok(_eventRepo.Query(new AllAuthorizedEvents(account, _inviteeRepo)));
        }

        [HttpGet("{id}")]
        public IActionResult GetEvent(Guid id)
        {
            var evnt = _eventRepo.Find(new EventById(id));
            if (evnt == null) { return BadRequest(); }
            return Ok(evnt);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEventModel model)
        {
            if (!_accountService.TryGetCurrentAccount(out Account account))
            {
                return Unauthorized();
            }
            var errors = model.GetValidationErrors();
            if (errors.Any()) { return BadRequest(errors); };
            var evnt = _eventService.Create(account, model.Title, model.Description);
            return Ok(evnt);
        }
    }
}