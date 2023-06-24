using DayOne.API.Context;
using DayOne.API.Extensions;
using DayOne.API.Model;
using DayOne.API.Repository;
using DayOne.API.Services;
using DayOne.API.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<CateringContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthorizationService(builder.Configuration);

builder.Services.AddScoped<IIngredientService,IngredientService>();
builder.Services.AddScoped<IRecepieService, RecepieService>();
builder.Services.AddScoped<IAuthenticadtionRepositorty, AuthenticationRepository>();
builder.Services.AddScoped<IngredientValidator>();
builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ReportApiVersions=true;
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.ResolveConflictingActions(spec => spec.First());
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlCommentsPath);
    opt.AddSecurityDefinition("MySecurityBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this end point"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference=new OpenApiReference{ Type=ReferenceType.SecurityScheme,
                Id="MySecurityBearerAuth" }
                },
            new List<string>()
        }
});
});
builder.Services.AddControllers();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("MustBeGreaterThanEighteen", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>

            context.User.HasClaim(c => c.Type == "DateOfBirth" && Convert.ToDateTime(c.Value).AddYears(18).Year <= DateTime.Now.Year));

    });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.Run();
