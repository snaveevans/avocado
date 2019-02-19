using System.Threading.Tasks;
using Avocado.Domain.Entities;

namespace Avocado.Domain.Interfaces
{
    public interface IAccountAccessor
    {
        Task<Account> GetAccount();
    }
}