using Avocado.Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Avocado.Infrastructure.Context
{
    public class IdentityContext : DbContext
    {
        private readonly string _connectionString;

        public IdentityContext(IOptions<ContextOptions<IdentityContext>> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        public DbSet<Login> Logins { get; set; }
    }
}