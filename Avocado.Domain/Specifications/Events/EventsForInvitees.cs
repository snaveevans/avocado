using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Events
{
    public class EventsForMembers : ISpecification<Event>
    {
        private readonly IEnumerable<Guid> _eventIds;

        public EventsForMembers(IEnumerable<Member> members)
        {
            _eventIds = members.Select(m => m.EventId);
        }

        public Expression<Func<Event, bool>> BuildExpression() => evnt => _eventIds.Contains(evnt.Id);
    }
}
