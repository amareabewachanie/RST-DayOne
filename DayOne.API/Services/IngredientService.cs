
using System.Linq;
using DayOne.API.Context;
using DayOne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayOne.API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly CateringContext _context;
        public IngredientService(CateringContext context)
        {
            _context = context; 
        }
       
        public async  Task<List<Ingredient>> GetIngredients()
        => await _context.Ingredients.ToListAsync(); 
        
        public async Task<Ingredient> GetIngredient(int id)
        => await _context.Ingredients.FirstOrDefaultAsync(a => a.Id == id) ;
        
        public bool AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
           
            return _context.SaveChanges()>0;
        }
    }

}
