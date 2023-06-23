using DayOne.API.Entities;
using DayOne.API.Services;
using DayOne.API.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DayOne.API.Controllers
{
    [ApiController]
    [Route("Ingredients")]
    
    public class IngredientsController : ControllerBase
    {
        private ILogger<IngredientsController> _logger;
        private IIngredientService _ingredientService;
        private IngredientValidator _validator;
        public IngredientsController(ILogger<IngredientsController> logger, IIngredientService ingredientService, IngredientValidator validator)
        {
            _logger = logger;
            _ingredientService = ingredientService;
            _validator = validator;

        }
        [HttpGet]
        [Authorize(Roles = "Admin,Guest")]
        public async Task<ActionResult> Get()
        {

            return Ok(await _ingredientService.GetIngredients());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Find([FromRoute] int id)
        {

            return Ok(await _ingredientService.GetIngredient(id));
        }
        [HttpPost]
        public ActionResult AddIngredient([FromBody] Ingredient ingredient)
        {
            var result = _validator.Validate(ingredient);
            if (!result.IsValid) return BadRequest(result.Errors.Select(a => a.ErrorMessage).ToList());
            var success = _ingredientService.AddIngredient(ingredient);
            if (success)
            {
                return CreatedAtRoute("Find", new {id=ingredient.Id},ingredient);
            }
            return BadRequest();

        }
        //[HttpPut("{id}")]
        //public ActionResult UpdateIngredient([FromRoute]int id,Ingredient ingredient)
        //{
        //    var ingredientToBeUpdate=DataStore.Instance.Ingredients.Find(a=>a.Id==id);
        //    if(ingredientToBeUpdate is null)
        //            return NotFound();
        //    ingredientToBeUpdate.Name=ingredient.Name;
        //    ingredientToBeUpdate.Description=ingredient.Description;    
        //    ingredientToBeUpdate.Category=ingredient.Category;
        //    return Ok();
        //}
        //[HttpPatch("{id}")]
        //public ActionResult PatchIngredient([FromRoute]int id,[FromBody]JsonPatchDocument<Ingredients> patchDocument)
        //{
        //    var ingredient = DataStore.Instance.Ingredients.Find(a => a.Id == id);
        //    if(ingredient is null ) return NotFound();
        //    var ingredeintTobeUpdate = new Ingredients
        //    {
        //        Name
        //        =ingredient.Name,
        //        Description=ingredient.Description,
        //        Category=ingredient.Category,
        //    };
        //    patchDocument.ApplyTo(ingredeintTobeUpdate,ModelState);
        //    ingredient.Name=ingredeintTobeUpdate.Name;
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public ActionResult Delete([FromRoute] int id)
        //{
        //    var ingredientToBeDelete = DataStore.Instance.Ingredients.Find(a => a.Id == id);
        //    if (ingredientToBeDelete is null)
        //        return NotFound();
        //    DataStore.Instance.Ingredients.Remove(ingredientToBeDelete);
        //    return NoContent();
        //}
 
    }
}
