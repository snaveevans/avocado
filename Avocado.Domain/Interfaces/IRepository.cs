using System.Collections.Generic;

namespace Avocado.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        bool Remove(T item);
        T Find(ISpecification<T> spec);
        IEnumerable<T> Query(ISpecification<T> spec);
    }
}