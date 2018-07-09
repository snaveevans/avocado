using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Avocado.Infrastructure.Specifications;
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
        private readonly EventService _eventService;
        private readonly AccountService _accountService;

        public EventsController(IRepository<Event> eventRepo,
            EventService eventService,
            AccountService accountService)
        {
            _eventRepo = eventRepo;
            _eventService = eventService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventRepo.Query(new AllEventsSpecification()));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEventModel model)
        {
            var errors = model.GetValidationErrors();
            if (errors.Any()) { return BadRequest(errors); };
            if (!_accountService.TryGetCurrentAccount(out Account account))
            {
                return Unauthorized();
            }
            var evnt = _eventService.Create(account, model.Title, model.Description);
            return Ok(evnt);
        }
    }
}