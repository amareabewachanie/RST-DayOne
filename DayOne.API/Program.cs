using DayOne.API.Context;
using DayOne.API.Extensions;
using DayOne.API.Model;
using DayOne.API.Repository;
using DayOne.API.Services;
using DayOne.API.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<CateringContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthorizationService();
builder.Services.AddScoped<IIngredientService,IngredientService>();
builder.Services.AddScoped<IRecepieService, RecepieService>();
builder.Services.AddScoped<IAuthenticadtionRepositorty, AuthenticationRepository>();
builder.Services.AddScoped<IngredientValidator>();
builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.Run();
