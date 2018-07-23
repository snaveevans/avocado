using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Members
{
    public class MembersForEvent : ISpecification<Member>
    {
        private readonly Event _event;

        public MembersForEvent(Event evnt)
        {
            _event = evnt;
        }
        public IEnumerable<Member> Filter(IEnumerable<Member> items)
        {
            return items.Where(member => member.EventId == _event.Id);
        }
    }
}