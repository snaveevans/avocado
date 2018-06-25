﻿using System;
using Avocado.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Avocado.Infrastructure
{
    public class AvocadoContext : DbContext
    {
        private readonly string _connectionString;

        public AvocadoContext(IOptions<ContextOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        public DbSet<Event> Events { get; set; }
    }
}
