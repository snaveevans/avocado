using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("events/{eventId}/members"), Authorize]
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
            IEnumerable<Member> members;
            if (!_memberService.TryGetMembers(eventId, out members))
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