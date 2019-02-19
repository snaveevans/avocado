using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Events;
using Avocado.Domain.Specifications.Members;

namespace Avocado.Domain.Services
{
    public class EventService
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IRepository<Member> _memberRepo;
        private readonly AuthorizationService _authorizationService;
        private readonly IAccountAccessor _accountAccessor;

        public EventService(IRepository<Event> eventRepo,
            IRepository<Member> memberRepo,
            AuthorizationService authorizationService,
            IAccountAccessor accountAccessor)
        {
            _eventRepo = eventRepo;
            _memberRepo = memberRepo;
            _authorizationService = authorizationService;
            _accountAccessor = accountAccessor;
        }

        public async Task<Event> Create(string title, string description)
        {
            Account account = await _accountAccessor.GetAccount();
            if (account == null)
            {
                return null;
            }

            Event evnt = new Event(title, description);
            var member = new Member(account, evnt, Roles.Host);

            await _eventRepo.Add(evnt);
            await _memberRepo.Add(member);

            return evnt;
        }

        public async Task<Event> Update(Guid id, string title, string description)
        {
            Event evnt = await FindOne(id);
            if (evnt == null)
            {
                return evnt;
            }

            var hasChange = false;
            if (!string.IsNullOrWhiteSpace(title) && evnt.Title != title)
            {
                evnt.UpdateTitle(title);
                hasChange = true;
            }

            if (!string.IsNullOrWhiteSpace(description) && evnt.Description != description)
            {
                evnt.UpdateDescription(description);
                hasChange = true;
            }

            if (hasChange)
            {
                bool isUpdated = await _eventRepo.Update(evnt);
                if (!isUpdated)
                {
                    // TODO: report error
                }
            }

            return evnt;
        }

        public async Task<Event> FindOne(Guid id)
        {
            Event evnt = await _eventRepo.Find(new EventById(id));
            if (evnt == null)
            {
                return evnt;
            }

            if (!await _authorizationService.CanReadEvent(evnt))
            {
                evnt = null;
                return evnt;
            }

            return evnt;
        }

        public async Task<List<Event>> FindAuthorized()
        {
            Account account = await _accountAccessor.GetAccount();
            if (account == null)
            {
                return null;
            }

            var members = await _memberRepo.Query(new MembersForAccount(account));
            var events = await _eventRepo.Query(new EventsForMembers(members));
            return events;
        }

        public async Task<bool> DeleteEvent(Guid id)
        {
            Event evnt = await FindOne(id);
            if (evnt == null)
            {
                return false;
            }

            return await _eventRepo.Remove(evnt);
        }
    }
}