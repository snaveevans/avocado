using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Invitees
{
    public class InviteesForEvent : ISpecification<Invitee>
    {
        private readonly Event _event;

        public InviteesForEvent(Event evnt)
        {
            _event = evnt;
        }
        public IEnumerable<Invitee> Filter(IEnumerable<Invitee> items)
        {
            return items.Where(invitee => invitee.EventId == _event.Id);
        }
    }
}