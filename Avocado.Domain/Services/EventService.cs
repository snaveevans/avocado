using System;
using System.Collections.Generic;
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

        public bool TryCreate(string title, string description, out Event evnt)
        {
            var account = _accountAccessor.Account;
            if (account == null)
            {
                evnt = null;
                return false;
            }

            evnt = new Event(title, description);
            var member = new Member(account, evnt, Roles.Host);

            _eventRepo.Add(evnt);
            _memberRepo.Add(member);

            return true;
        }

        public bool TryUpdate(Guid id, string title, string description, out Event evnt)
        {
            if (!TryFindOne(id, out evnt))
            {
                evnt = null;
                return false;
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
                _eventRepo.Update(evnt);
            }

            return true;
        }

        public bool TryFindOne(Guid id, out Event evnt)
        {
            evnt = _eventRepo.Find(new EventById(id));
            if (evnt == null)
            {
                evnt = null;
                return false;
            }

            if (!_authorizationService.CanReadEvent(evnt))
            {
                evnt = null;
                return false;
            }

            return true;
        }

        public IEnumerable<Event> FindAuthorized()
        {
            var account = _accountAccessor.Account;
            if (account == null)
            {
                return null;
            }

            var members = _memberRepo.Query(new MembersForAccount(account));
            var events = _eventRepo.Query(new EventsForMembers(members));
            return events;
        }

        public bool TryDeleteEvent(Guid id)
        {
            if (!TryFindOne(id, out Event evnt))
            {
                return false;
            }

            return _eventRepo.Remove(evnt);
        }
    }
}