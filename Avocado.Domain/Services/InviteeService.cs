using System;
using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Members;

namespace Avocado.Domain.Services
{
    public class MemberService
    {
        private readonly EventService _eventService;
        private readonly IRepository<Member> _memberRepo;
        private readonly AuthorizationService _authorizationService;
        private readonly IAccountAccessor _accountAccessor;

        public MemberService(EventService eventService,
            IRepository<Member> memberRepo,
            AuthorizationService authorizationService,
            IAccountAccessor accountAccessor)
        {
            _eventService = eventService;
            _memberRepo = memberRepo;
            _authorizationService = authorizationService;
            _accountAccessor = accountAccessor;
        }

        public IEnumerable<Member> GetMembers(Guid eventId)
        {
            var evnt = _eventService.FindOne(eventId);

            if (evnt == null)
            {
                return Enumerable.Empty<Member>();
            }

            return _memberRepo.Query(new MembersForEvent(evnt));
        }

        // get members
        // add members
        // remove members
        // update member status
    }
}