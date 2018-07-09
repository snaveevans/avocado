using System.Collections.Generic;

namespace Avocado.Domain
{
    public interface IRepository<T>
    {
        void Add(T item);
        bool Remove(T item);
        T Find(ISpecification<T> spec);
        IEnumerable<T> Query(ISpecification<T> spec);
    }
}