using System;
using System.Linq.Expressions;

namespace Avocado.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<System.Func<T, bool>> BuildExpression();
    }
}