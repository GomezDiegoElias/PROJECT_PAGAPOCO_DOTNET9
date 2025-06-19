using com.project.pagapoco.core.business;
using com.project.pagapoco.core.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuración de la base de datos
var bdConnectionString = builder.Configuration.GetConnectionString("BDConnectionString")
    ?? throw new InvalidOperationException("No se encontró 'BDConnectionString' en la configuración.");

// Registrar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(bdConnectionString)
);

// Registrar servicios
builder.Services.AddScoped<UserRepository, UserRepositoryImp>();
builder.Services.AddScoped<UserService, UserServiceImp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
