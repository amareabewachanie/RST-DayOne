using DayOne.API.Entities;
using FluentValidation;

namespace DayOne.API.Validations
{
    public class IngredientValidator:AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty();
        }
    }
}
