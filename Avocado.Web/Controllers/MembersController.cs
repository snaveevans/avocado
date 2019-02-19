using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avocado.Web.Controllers
{
    [Route("api/events/{eventId}/members"), Authorize, ApiController]
    [Produces("application/json")]
    [SwaggerTag("Create, read, update, and delete event members.")]
    public class MembersController : Controller
    {
        private readonly MemberService _memberService;

        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all the events that the user has access to.", OperationId = "GetEventMembers")]
        [SwaggerResponse(200, "All events for the user.", typeof(List<Member>))]
        public async Task<ActionResult> Get([SwaggerParameter("Event Id")] Guid eventId)
        {
            List<Member> members = await _memberService.GetMembers(eventId);
            if (members == null)
            {
                return Unauthorized();
            }

            return Ok(members);
        }

        // add members


        // remove members
        // update member status
    }
}