using DayOne.API.Context;
using DayOne.API.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace DayOne.API.Extensions
{
    public static class AuthenticationServiceExtension
    {
        public static void AddAuthorizationService(this IServiceCollection services)
        {
             services.AddIdentity<User, IdentityRole>(option => {
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<CateringContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidIssuer = builder["Jwt:Issuer"],
                    //ValidAudience = builder["Jwt:Audience"],
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        }
       
    }
}
