using System.Collections.Generic;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Infrastructure.Specifications
{
    public class AllEventsSpecification : ISpecification<Event>
    {
        public IEnumerable<Event> Filter(IEnumerable<Event> items)
        {
            return items;
        }
    }
}