using DayOne.API.Entities;
using DayOne.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DayOne.API.Controllers
{
    [ApiController]
    [Route("reciepe")]
    public class RecepiesController:ControllerBase
    {
        private readonly IRecepieService _recepieService;
        public RecepiesController(IRecepieService recepieService)
        {
            _recepieService = recepieService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Recepie>>> Get()=>Ok(await _recepieService.GetAll());
        [HttpGet("{id}",Name ="Find")]
        public async Task<ActionResult<List<Recepie>>> Find(int id) => Ok(await _recepieService.GetOne(id));
        [HttpPost]
        public ActionResult Add(Recepie recepie)
        {
            var savedRecepie = _recepieService.Add(recepie);
            if (savedRecepie is not null)
                return CreatedAtRoute("Find", new
                {
                    id=savedRecepie.Id
                }, recepie);
            return BadRequest();
        }
    }
}
