using MySqlConnector;
using ServerApi.Data;
using Microsoft.EntityFrameworkCore;
using ServerApi.EndPoints;

namespace ServerApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();



            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connection = builder.Configuration.GetConnectionString("RicettarioDb");

            builder.Services.AddDbContext<RicettarioDbContext>(option =>
                                    option.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapGet("/" , () =>  "ciao");
            app.MapIngredientiEndpoints();
            app.MapRicetteEndPoints();
            app.MapFotoEndPoints();

            app.Run();

            
        }
    }
}