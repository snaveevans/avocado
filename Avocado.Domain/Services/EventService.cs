using System;
using System.Collections.Generic;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Events;
using Avocado.Domain.Specifications.Invitees;

namespace Avocado.Domain.Services
{
    public class EventService
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IRepository<Invitee> _inviteeRepo;
        private readonly AuthorizationService _authorizationService;
        private readonly IAccountAccessor _accountAccessor;

        public EventService(IRepository<Event> eventRepo,
            IRepository<Invitee> inviteeRepo,
            AuthorizationService authorizationService,
            IAccountAccessor accountAccessor)
        {
            _eventRepo = eventRepo;
            _inviteeRepo = inviteeRepo;
            _authorizationService = authorizationService;
            _accountAccessor = accountAccessor;
        }

        public Event Create(string title, string description)
        {
            var account = _accountAccessor.Account;
            if (account == null)
                return null;

            var evnt = new Event(title, description);
            var invitee = new Invitee(account, evnt);

            _eventRepo.Add(evnt);
            _inviteeRepo.Add(invitee);

            return evnt;
        }

        public Event Update(Guid id, string title, string description)
        {
            var evnt = FindOne(id);
            if (evnt == null)
                return null;

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

            return evnt;
        }

        public Event FindOne(Guid id)
        {
            var evnt = _eventRepo.Find(new EventById(id));
            if (evnt == null)
                return null;

            if (!_authorizationService.CanReadEvent(evnt))
                return null;

            return evnt;
        }

        public IEnumerable<Event> FindAuthorized()
        {
            var account = _accountAccessor.Account;
            if (account == null)
                return null;

            var invitees = _inviteeRepo.Query(new InviteesForAccount(account));
            var events = _eventRepo.Query(new EventsForInvitees(invitees));
            return events;
        }

        public bool DeleteEvent(Guid id)
        {
            var evnt = FindOne(id);
            if (evnt == null)
                return false;

            return _eventRepo.Remove(evnt);
        }
    }
}