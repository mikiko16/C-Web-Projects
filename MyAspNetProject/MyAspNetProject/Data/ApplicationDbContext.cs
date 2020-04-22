using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAspNetProject.models;
using MyAspNetProject.Models;

namespace MyAspNetProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApp>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        public DbSet<TeamBuilding> TeamBuilding { get; set; }

        public DbSet<ThingsNeeded> ThingsNedded { get; set; }

        public DbSet<Pictures> Pictures { get; set; }

        public DbSet<Ad> Ad { get; set; }
    }
}
