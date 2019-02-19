using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Add(T item)
        {
            await Task.Run(() => List.Add(item));
        }

        public async Task<bool> Update(T item)
        {
            await Remove(item);
            await Add(item);
            return true;
        }

        public async Task<bool> Remove(T item)
        {
            return await Task.Run(() => List.Remove(item));
        }

        public async Task<T> Find(ISpecification<T> spec)
        {
            return await Task.Run(() => List.AsQueryable().FirstOrDefault(spec.BuildExpression()));
        }

        public async Task<List<T>> Query(ISpecification<T> spec)
        {
            return await Task.Run(() => List.AsQueryable().Where(spec.BuildExpression()).ToList());
        }
    }
}