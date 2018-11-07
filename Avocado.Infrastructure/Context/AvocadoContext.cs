using System;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Avocado.Infrastructure.Context
{
    public class AvocadoContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Event> Events { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Login> Logins { get; set; }

        public AvocadoContext(IOptions<ContextOptions<AvocadoContext>> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasKey(i => new { i.AccountId, i.EventId });
        }
    }
}
