using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Test
{
    public class TestRepo<T> : IRepository<T>
    {
        public List<T> List { get; set; }
        
        public TestRepo()
        {
            List = new List<T>();
        }
        public void Add(T item)
        {
            List.Add(item);
        }

        public T Find(ISpecification<T> spec)
        {
            return spec.Filter(List).FirstOrDefault();
        }

        public IEnumerable<T> Query(ISpecification<T> spec)
        {
            return spec.Filter(List);
        }

        public bool Remove(T item)
        {
            return List.Remove(item);
        }

        public void Update(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}