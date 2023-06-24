using DayOne.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DayOne.API.Repository
{
    public class AuthenticationRepository:IAuthenticadtionRepositorty
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private User? _user;
        public AuthenticationRepository(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto userDto)
        {
            _user = new User(userDto.LastName,
userDto.FirstName
)
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                DateOfBirth = Convert.ToDateTime(userDto.DateOfBirth)
            };
            return await _userManager.CreateAsync(_user, userDto.Password);
        }
        public async Task<bool> ValidateUserAsync(Login login)
        {
            _user=await _userManager.FindByNameAsync(login.UserName);

            return _user !=null && await _userManager.CheckPasswordAsync(_user, login.Password); 
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("Name",_user.UserName),
                new Claim("DateOfBirth", _user.DateOfBirth.ToString()),

            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials,List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"])),
                signingCredentials: credentials
            );
            return jwtSecurityToken;
        }
        private SigningCredentials GetSigninCredentials()
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }

        public async Task<string> CreateTokenAsync()
        {
            var signInCredentials=GetSigninCredentials();
            var claims = await GetClaims();
            var tokenOptions=GenerateTokenOptions(signInCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> AddRoleAsync(string Name)
        {
            var success = await _roleManager.CreateAsync(new IdentityRole { Name = Name });
            return !!success.Succeeded;
        }

        public async Task<bool> AssignRoleAsync(UserRolesDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            var role =  _roleManager.Roles.Where(a=> dto.RoleName.Contains(a.Name));
            var roleNames=role.Select(a=>  a.Name).ToList();
            var result=await _userManager.AddToRolesAsync(user,roleNames);
            return !!result.Succeeded;
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            return await _roleManager.Roles.Select(a => 
            new RoleDto{
               Name= a.Name
            }).ToListAsync();
        }
    }
}
