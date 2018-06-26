using System.Collections.Generic;
using System.Linq;
using Avocado.Domain;
using Microsoft.EntityFrameworkCore;

namespace Avocado.Infrastructure
{
    public class ContextRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset;

        public ContextRepository(AvocadoContext context)
        {
            _dbset = context.Set<T>();
        }

        public void Add(T item)
        {
            _dbset.Add(item);
        }

        public T Find(ISpecification<T> spec)
        {
            return spec.Filter(_dbset).FirstOrDefault();
        }

        public IEnumerable<T> Query(ISpecification<T> spec)
        {
            return spec.Filter(_dbset);
        }

        public bool Remove(T Item)
        {
            var entityRemoved = _dbset.Remove(Item);
            return entityRemoved != null;
        }
    }
}