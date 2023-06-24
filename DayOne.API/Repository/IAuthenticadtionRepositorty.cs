using DayOne.API.Model;
using Microsoft.AspNetCore.Identity;

namespace DayOne.API.Repository
{
    public interface IAuthenticadtionRepositorty
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto dto);
        Task<bool> ValidateUserAsync(Login login);
        Task<string> CreateTokenAsync();
        Task<bool> AddRoleAsync(string Name);
        Task<bool> AssignRoleAsync(UserRolesDto userRolesDto);
        Task<List<RoleDto>> GetRolesAsync();

    }
}
