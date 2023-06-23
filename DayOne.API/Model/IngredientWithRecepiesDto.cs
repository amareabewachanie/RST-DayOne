using DayOne.API.Entities;

namespace DayOne.API.Model
{
    public class IngredientWithRecepiesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<RecepieDto> Recepies { get; set; } =new List<RecepieDto>();
    }
}
