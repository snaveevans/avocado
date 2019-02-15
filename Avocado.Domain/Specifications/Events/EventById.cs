using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Events
{
    public class EventById : ISpecification<Event>
    {
        private readonly Guid _eventId;
        public EventById(Guid eventId)
        {
            _eventId = eventId;
        }

        public Expression<Func<Event, bool>> BuildExpression() => evnt => evnt.Id == _eventId;
    }
}
