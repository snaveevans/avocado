using System.Linq;
using Avocado.Domain;
using Avocado.Infrastructure.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/events"), Authorize]
    public class EventsController : Controller
    {
        private readonly IRepository<Event> _eventRepo;

        public EventsController(IRepository<Event> eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventRepo.Query(new AllEventsSpecification()));
        }
    }
}