using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServerApi.Data;
using System.Net.Sockets;
using System.Net;
using ServerApi.EndPoints;
using Microsoft.AspNetCore.Hosting.Server;

namespace ServerApi
{
    public class Startup
    {
        public string configurationString = "Server = localhost; Database = ricettariodb; User = root; Password = '';";

        // Configura i servizi necessari.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //var connection = Configuration.GetConnectionString("RicettarioDb");
            services.AddDbContext<RicettarioDbContext>(option =>
                option.UseMySql(configurationString, ServerVersion.AutoDetect(configurationString)));
        }

        // Configura la pipeline di richieste HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            Console.WriteLine(GetIpAddress().ToString());

            app.UseCors("AllowAll");
            app.UseRouting();
            //app.UseHttpsRedirection();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("ciao");
                });

                IngredientiEndPoints.MapIngredientiEndpoints(endpoints);
                RicettaEndPoints.MapRicetteEndPoints(endpoints);
                FotoEndPoints.MapFotoEndPoints(endpoints);
                UtenteEndPoints.MapUtenteEndPoints(endpoints);
                FotoUtenteEndPoints.MapFotoUtenteEndPoints(endpoints);
                RicettaIngEndPoints.MapRicIngEndPoints(endpoints);
                //endpoints.MapRicetteEndPoints();
            });
        }
        public static IPAddress GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.First(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork));
        }
    }
}
