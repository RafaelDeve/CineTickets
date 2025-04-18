using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using proyecto.Infrastructure.Persistence;
using Proyecto.Domain.Repositories;
using Proyecto.Infrastructure.Repositories;
using Proyecto.Application.Services;

namespace Proyecto.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método se llama en tiempo de ejecución. Se usa para agregar servicios al contenedor de DI.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar el DbContext con SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("CineDbConnection")));

            // Registrar los servicios de aplicación y repositorios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPeliculaRepository, PeliculaRepository>();
            services.AddScoped<IProyeccionRepository, ProyeccionRepository>();
            services.AddScoped<IEntradaRepository, EntradaRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();

            services.AddScoped<UsuarioService>();
            services.AddScoped<PeliculaService>();
            services.AddScoped<ProyeccionService>();
            services.AddScoped<EntradaService>();
            services.AddScoped<PagoService>();

            // Configurar controladores con vistas
            services.AddControllersWithViews();

            // Configuración de CORS si es necesario
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        // Este método se llama en tiempo de ejecución. Se usa para configurar la canalización de solicitudes HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}