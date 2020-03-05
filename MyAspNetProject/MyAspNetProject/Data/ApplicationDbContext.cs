﻿using Microsoft.EntityFrameworkCore;
using MyAspNetProject.Models;

namespace MyAspNetProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserTrip>()
            //    .HasKey(k => new { k.TripId, k.UserId });
        }

        public DbSet<User> Users { get; set; }
    }
}
