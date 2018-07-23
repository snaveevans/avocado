using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Events
{
    public class EventsForMembers : ISpecification<Event>
    {
        private readonly IEnumerable<Member> _members;
        
        public EventsForMembers(IEnumerable<Member> members)
        {
            _members = members;
        }
        public IEnumerable<Event> Filter(IEnumerable<Event> items)
        {
            var eventIds = _members.Select(i => i.EventId);
            return items.Where(e => eventIds.Contains(e.Id));
        }
    }
}