using DayOne.API.Model;
using DayOne.API.Repository;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Registers the user to the database
        /// </summary>
        /// <param name="userDto">Provide the lsit of the fields for user dto</param>
        /// <returns>200 ok if it succeeds</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      
        public async Task<IActionResult> Register([FromBody]RegisterUserDto userDto)
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
        [HttpPost("roles")]
        [Authorize]
        public async Task<ActionResult> AddRole([FromBody]RoleDto roleDto)
        {
            var result=await _repository.AddRoleAsync(roleDto.Name);
            return result?Ok():BadRequest();
        }
        [HttpGet("roles")]
        [Authorize]
        public async Task<ActionResult> Roles()
        {
            
            return Ok(await _repository.GetRolesAsync());
        }
        [HttpPost("assign")]
        [Authorize]
        public async Task<ActionResult> AssignRoles([FromBody] UserRolesDto userRolesDto)
        {

            return Ok(await _repository.AssignRoleAsync(userRolesDto));
        }
    }
}
