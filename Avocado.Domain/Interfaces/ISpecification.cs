using System.Collections.Generic;

namespace Avocado.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items);
    }
}