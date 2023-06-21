using DayOne.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DayOne.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        { 
            return Ok("Good "+(DateTime.UtcNow.Hour>12?"Morning":"After noon") );
        }
    }
}
