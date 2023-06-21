using DayOne.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DayOne.API.Context
{
    public class CateringContext:DbContext
    {
        public CateringContext(DbContextOptions<CateringContext> options):base(options) { } 
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<Recepie> Recepie { get; set;}  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasMany(e => e.Recepie);
            modelBuilder.Entity<Recepie>().
                HasMany(e => e.Ingredients);
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    Id=1,
                    Name="Initally Create",
                   Description= "Initially created desc"
                });
        }
    }
}
