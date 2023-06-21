using DayOne.API.Entities;

namespace DayOne.API.Services
{
    public interface IIngredientService
    {
        public Task<List<Ingredient>> GetIngredients();
        public Task<Ingredient> GetIngredient(int id);
        public bool AddIngredient(Ingredient ingredients);
    }
}
