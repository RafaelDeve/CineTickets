using Microsoft.EntityFrameworkCore;
using proyecto.Infrastructure.Persistence;
using Proyecto.Application.Services;
using Proyecto.Domain.Repositories;
using Proyecto.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configurar el contexto de base de datos
builder.Services.AddDbContext<CineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CineDbConnection")));

// Agregar servicios de autenticación y autorización
builder.Services.AddAuthorization();

// Agregar controladores
builder.Services.AddControllers();

// Registrar servicios y repositorios
builder.Services.AddScoped<IProyeccionRepository, ProyeccionRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<TicketService>();

// Agregar Swagger para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el middleware para la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
