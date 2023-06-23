using DayOne.API.Entities;
using DayOne.API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DayOne.API.Context
{
    public class CateringContext:IdentityDbContext<User>
    {
        public CateringContext(DbContextOptions<CateringContext> options):base(options) { } 
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<Recepie> Recepie { get; set;}  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ingredient>()
                .HasMany(e => e.Recepie);
            modelBuilder.Entity<Recepie>().
                HasMany(e => e.Ingredients);
              
        }
    }
}
