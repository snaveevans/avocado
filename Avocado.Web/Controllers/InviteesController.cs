using System;
using System.Linq;
using Avocado.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/events/{eventId}/members"), Authorize]
    public class MembersController : Controller
    {
        private readonly MemberService _memberService;

        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult Get(Guid eventId)
        {
            var members = _memberService.GetMembers(eventId);

            if (!members.Any())
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