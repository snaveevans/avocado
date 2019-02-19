using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<List<Member>> GetMembers(Guid eventId)
        {
            Event evnt = await _eventService.FindOne(eventId);
            if (evnt == null)
            {
                return new List<Member>();
            }

            var members = await _memberRepo.Query(new MembersForEvent(evnt));
            return members;
        }

        // add members
        public async Task<Member> AddMember(Guid eventId, Account account)
        {
            Event evnt = await _eventService.FindOne(eventId);
            if (evnt == null)
            {
                return null;
            }

            Member member = new Member(account, evnt, Roles.Guest);
            await _memberRepo.Add(member);
            return member;
        }

        // remove members
        public async Task<bool> RemoveMember(Guid eventId, Account account)
        {
            Event evnt = await _eventService.FindOne(eventId);
            if (evnt == null)
            {
                return false;
            }

            Member member = await _memberRepo.Find(new FindMember(account, evnt));
            return await _memberRepo.Remove(member);
        }

        // update member status
        public async Task<Member> UpdateStatus(Guid eventId, AttendanceStatuses status)
        {
            Event evnt = await _eventService.FindOne(eventId);
            if (evnt == null)
            {
                return null;
            }

            Account account = await _accountAccessor.GetAccount();
            Member member = await _memberRepo.Find(new FindMember(account, evnt));
            member.UpdateAttendanceStatus(status);
            bool isUpdated = await _memberRepo.Update(member);
            // TODO: check for error
            return member;
        }
    }
}