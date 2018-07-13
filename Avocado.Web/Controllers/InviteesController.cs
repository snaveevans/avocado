using System;
using System.Linq;
using Avocado.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/events/{eventId}/invitees"), Authorize]
    public class InviteesController : Controller
    {
        private readonly InviteeService _inviteeService;

        public InviteesController(InviteeService inviteeService)
        {
            _inviteeService = inviteeService;
        }

        [HttpGet]
        public IActionResult Get(Guid eventId)
        {
            var invitees = _inviteeService.GetInvitees(eventId);

            if (!invitees.Any())
            {
                return Unauthorized();
            }

            return Ok(invitees);
        }

        // add invitees


        // remove invitees
        // update invitee status
    }
}