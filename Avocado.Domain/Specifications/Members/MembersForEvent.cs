using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public Expression<Func<Member, bool>> BuildExpression() => member => member.EventId == _event.Id;
    }
}

