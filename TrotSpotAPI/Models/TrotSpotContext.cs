using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrotSpotAPI.Models
{
    public class TrotSpotContext : DbContext
    {
        public TrotSpotContext(DbContextOptions<TrotSpotContext> options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>().ToTable("Class");
            modelBuilder.Entity<Division>().ToTable("Division");
            modelBuilder.Entity<Rider>().ToTable("Rider");
            modelBuilder.Entity<Show>().ToTable("Show");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
