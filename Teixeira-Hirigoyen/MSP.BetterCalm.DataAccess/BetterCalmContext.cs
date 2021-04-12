using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MSP.BetterCalm.Domain;
using System;
using System.IO;

namespace MSP.BetterCalm.DataAccess
{
    public class BetterCalmContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public BetterCalmContext() { }
        public BetterCalmContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString(@"MSP.BetterCalmDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
