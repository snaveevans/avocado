using System.Threading.Tasks;
using Avocado.Domain.Entities;

namespace Avocado.Infrastructure.Providers
{
    public interface IProvider
    {
        string Provider { get; }
        Task<string> GetProviderKey(string accessToken);
    }
}