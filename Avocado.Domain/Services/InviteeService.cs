using System;
using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Invitees;

namespace Avocado.Domain.Services
{
    public class InviteeService
    {
        private readonly EventService _eventService;
        private readonly IRepository<Invitee> _inviteeRepo;
        private readonly AuthorizationService _authorizationService;
        private readonly IAccountAccessor _accountAccessor;

        public InviteeService(EventService eventService,
            IRepository<Invitee> inviteeRepo,
            AuthorizationService authorizationService,
            IAccountAccessor accountAccessor)
        {
            _eventService = eventService;
            _inviteeRepo = inviteeRepo;
            _authorizationService = authorizationService;
            _accountAccessor = accountAccessor;
        }

        public IEnumerable<Invitee> GetInvitees(Guid eventId)
        {
            var evnt = _eventService.FindOne(eventId);

            if (evnt == null)
            {
                return Enumerable.Empty<Invitee>();
            }

            return _inviteeRepo.Query(new InviteesForEvent(evnt));
        }

        // get invitees
        // add invitees
        // remove invitees
        // update invitee status
    }
}