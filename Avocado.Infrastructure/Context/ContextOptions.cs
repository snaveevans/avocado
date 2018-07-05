using Microsoft.EntityFrameworkCore;

namespace Avocado.Infrastructure.Context
{
    public class ContextOptions<TContext> where TContext : DbContext
    {
        public string ConnectionString { get; set; }
    }
}