using Avocado.Domain.Entities;

namespace Avocado.Infrastructure.Authentication
{
    public interface IJwtFactory
    {
        string GenerateToken(Account account);
    }
}