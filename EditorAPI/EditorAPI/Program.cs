using EditorAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using Repositories.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Task6DbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
    options.UseLazyLoadingProxies();
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddScoped<PresentationRepository>();
builder.Services.AddScoped<PresentationSlideRepoitory>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
