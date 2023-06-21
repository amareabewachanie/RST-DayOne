using DayOne.API.Context;
using DayOne.API.Services;
using DayOne.API.Validations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<CateringContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IIngredientService,IngredientService>();
builder.Services.AddScoped<IRecepieService, RecepieService>();
builder.Services.AddScoped<IngredientValidator>();
var app = builder.Build();


// Configure the HTTP request pipeline.
//app.Use(async (context, next) =>
//{
//    context.Response.ContentType = "application/json";
//    await context.Response.WriteAsync("HelLO World");
//    await next();
//});

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}