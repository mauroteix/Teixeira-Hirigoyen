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
        public virtual DbSet<Track> Track { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
 

    public BetterCalmContext() { }
        public BetterCalmContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryTrack>()
                .HasKey(mc => new { mc.IdCategory, mc.IdTrack });
            modelBuilder.Entity<CategoryTrack>()
                .HasOne(mc => mc.Category)
                .WithMany(m => m.CategoryTrack)
                .HasForeignKey(mc => mc.IdCategory);
            modelBuilder.Entity<CategoryTrack>()
                .HasOne(mc => mc.Track)
                .WithMany(c => c.CategoryTrack)
                .HasForeignKey(mc => mc.IdTrack);

            modelBuilder.Entity<PlaylistCategory>()
               .HasKey(mc => new { mc.IdCategory, mc.IdPlaylist });
            modelBuilder.Entity<PlaylistCategory>()
                .HasOne(mc => mc.Category)
                .WithMany(m => m.PlaylistCategory)
                .HasForeignKey(mc => mc.IdCategory);
            modelBuilder.Entity<PlaylistCategory>()
                .HasOne(mc => mc.Playlist)
                .WithMany(c => c.PlaylistCategory)
                .HasForeignKey(mc => mc.IdPlaylist);

            modelBuilder.Entity<PlaylistTrack>()
               .HasKey(mc => new { mc.IdPlaylist, mc.IdTrack });
            modelBuilder.Entity<PlaylistTrack>()
                .HasOne(mc => mc.Playlist)
                .WithMany(m => m.PlaylistTrack)
                .HasForeignKey(mc => mc.IdPlaylist);
            modelBuilder.Entity<PlaylistTrack>()
                .HasOne(mc => mc.Track)
                .WithMany(c => c.PlaylistTrack)
                .HasForeignKey(mc => mc.IdTrack);
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
