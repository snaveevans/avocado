using System;
using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
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

        // get members
        public bool TryGetMembers(Guid eventId, out IEnumerable<Member> members)
        {
            if (!_eventService.TryFindOne(eventId, out Event evnt))
            {
                members = Enumerable.Empty<Member>();
                return false;
            }

            members = _memberRepo.Query(new MembersForEvent(evnt));
            return true;
        }

        // add members
        public bool TryAddMember(Guid eventId, Account account, out Member member)
        {
            if (!_eventService.TryFindOne(eventId, out Event evnt))
            {
                member = null;
                return false;
            }

            member = new Member(account, evnt, Roles.Guest);
            _memberRepo.Add(member);
            return true;
        }

        // remove members
        public bool TryRemoveMember(Guid eventId, Account account)
        {
            if (!_eventService.TryFindOne(eventId, out Event evnt))
            {
                return false;
            }

            var member = _memberRepo.Find(new FindMember(account, evnt));
            return _memberRepo.Remove(member);
        }

        // update member status
        public bool TryUpdateStatus(Guid eventId, AttendanceStatuses status, out Member member)
        {
            if (!_eventService.TryFindOne(eventId, out Event evnt))
            {
                member = null;
                return false;
            }

            member = _memberRepo.Find(new FindMember(_accountAccessor.Account, evnt));
            member.UpdateAttendanceStatus(status);
            _memberRepo.Update(member);
            return true;
        }
    }
}