using System;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
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
            modelBuilder.Entity<Account>(a =>
            {
                a.HasIndex(i => i.NormalizedUserName)
                    .IsUnique();
            });
            modelBuilder.Entity<Member>(m =>
            {
                m.HasKey(i => new { i.AccountId, i.EventId });
                m.Property(i => i.AttendanceStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<AttendanceStatuses>(v)
                );
                m.Property(i => i.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<Roles>(v)
                );
                m.HasOne<Event>()
                    .WithMany()
                    .HasForeignKey(i => i.EventId)
                    .IsRequired();
            });
            modelBuilder.Entity<Event>(e =>
            {
                e.HasMany<Member>()
                    .WithOne();
            });
        }
    }
}
