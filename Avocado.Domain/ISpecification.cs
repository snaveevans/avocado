using System.Collections.Generic;

namespace Avocado.Domain
{
    public interface ISpecification<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items);
    }
}