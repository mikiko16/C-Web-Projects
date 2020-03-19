using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAspNetProject.models;

namespace MyAspNetProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApp>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       //{
       //    base.OnConfiguring(optionsBuilder);
       //
       //    optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
       //}
    }
}
