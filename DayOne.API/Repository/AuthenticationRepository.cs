using DayOne.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DayOne.API.Repository
{
    public class AuthenticationRepository:IAuthenticadtionRepositorty
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;
        public AuthenticationRepository(UserManager<User> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto userDto)
        {
            _user = new User(userDto.LastName,
userDto.FirstName
)
            {
                UserName = userDto.UserName,
                Email = userDto.Email
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
                new Claim(ClaimTypes.Name,_user.UserName),

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
            var key = Encoding.UTF8.GetBytes("MyPrivateToken");
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
    }
}
