using System;
using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Infrastructure.Specifications.Events
{
    public class EventById : ISpecification<Event>
    {
        private readonly Guid _eventId;
        public EventById(Guid eventId)
        {
            _eventId = eventId;
        }
        public IEnumerable<Event> Filter(IEnumerable<Event> items)
        {
            return items.Where(evnt => evnt.Id == _eventId);
        }
    }
}