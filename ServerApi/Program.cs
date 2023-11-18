using MySqlConnector;
using ServerApi.Data;
using Microsoft.EntityFrameworkCore;
using ServerApi.EndPoints;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;

namespace ServerApi
{
    public class Program
    {
        public static IPAddress GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.First(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork));
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://0.0.0.0:5000", "https://0.0.0.0:5001");
            });

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            //builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //});

            //var connection = builder.Configuration.GetConnectionString("RicettarioDb");

            //builder.Services.AddDbContext<RicettarioDbContext>(option =>
            //                        option.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //Console.WriteLine(GetIpAddress().ToString());

            //app.UseCors("AllowAll");
            //app.UseRouting();
            //app.UseHttpsRedirection();
            //app.UseAuthorization();

            //app.MapGet("/" , () =>  "ciao");
            //app.MapIngredientiEndpoints();
            //app.MapRicetteEndPoints();

            CreateHostBuilder(args).Build().Run();

            //app.Run();

            
        }
    }
}