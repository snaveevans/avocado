using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Events
{
    public class EventsForInvitees : ISpecification<Event>
    {
        private readonly IEnumerable<Invitee> _invitees;
        
        public EventsForInvitees(IEnumerable<Invitee> invitees)
        {
            _invitees = invitees;
        }
        public IEnumerable<Event> Filter(IEnumerable<Event> items)
        {
            var eventIds = _invitees.Select(i => i.EventId);
            return items.Where(e => eventIds.Contains(e.Id));
        }
    }
}