using DayOne.API.Model;
using DayOne.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DayOne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticadtionRepositorty _repository;

        public AuthController(IAuthenticadtionRepositorty authenticationRepository)
        {
            _repository = authenticationRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            var userResult=await _repository.RegisterUserAsync(userDto);
            return userResult.Succeeded ? Ok(userResult) : BadRequest();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            return !await _repository.ValidateUserAsync(login) ? Unauthorized()
                : Ok(new { Token = await _repository.CreateTokenAsync() });
        }
    }
}
