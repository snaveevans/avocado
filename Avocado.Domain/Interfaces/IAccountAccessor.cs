using Avocado.Domain.Entities;

namespace Avocado.Domain.Interfaces
{
    public interface IAccountAccessor
    {
        Account Account { get; }
    }
}