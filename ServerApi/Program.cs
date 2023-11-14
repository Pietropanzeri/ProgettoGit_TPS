using MySqlConnector;

namespace ServerApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<MySqlConnection>(_ =>
                                new MySqlConnection(builder.Configuration.GetConnectionString("RicettarioDb")));

            using var connection = new MySqlConnection(builder.Configuration.GetConnectionString("RicettarioDb"));

            await connection.OpenAsync();


            #region CreazioneTabelle
            using var CREATE_TABLE_INGREDIENTI = new MySqlCommand("""
                                                         CREATE TABLE IF NOT EXISTS ingredienti (
                                                             ingrediente_id int PRIMARY KEY AUTO_INCREMENT,
                                                             nome varchar(16) not null,
                                                             descrizione varchar(200),
                                                             dataInizio date,
                                                             dataFine date
                                                         );
                                                         """, connection);

            using var CREATE_RICETTE_TABLE = new MySqlCommand("""
                                                        CREATE TABLE IF NOT EXISTS ricette (
                                                            ricetta_id int PRIMARY KEY AUTO_INCREMENT,
                                                            nome varchar(16) not null,
                                                            preparazione varchar(200),
                                                            tempo int,
                                                            difficolta int CHECK (difficolta >= 1 AND difficolta <= 5)
                                                        );
                                                        """, connection);

            using var CREATE_FOTO_TABLE = new MySqlCommand("""
                                                        CREATE TABLE IF NOT EXISTS foto (
                                                            foto_id int PRIMARY KEY AUTO_INCREMENT,
                                                            descrizione varchar(50),
                                                            foto blob,
                                                            ricetta_id int,
                                                            foreign key (ricetta_id) references ricette(ricetta_id)
                                                        );
                                                        """, connection);

            using var CREATE_RICETTAINGREDIENTI_TABLE = new MySqlCommand("""
                                                        CREATE TABLE RicetteIngredienti (
                                                            ricetta_id INT not null,
                                                            ingrediente_id INT not null,
                                                            FOREIGN KEY (ricetta_id) REFERENCES ricette(ricetta_id),
                                                            FOREIGN KEY (ingrediente_id) REFERENCES ingredienti(ingrediente_id),
                                                            PRIMARY KEY(ricetta_id, ingrediente_id)
                                                        );
                                                        """, connection);

            await CREATE_TABLE_INGREDIENTI.ExecuteNonQueryAsync();
            await CREATE_RICETTE_TABLE.ExecuteNonQueryAsync();
            await CREATE_FOTO_TABLE.ExecuteNonQueryAsync();
            await CREATE_RICETTAINGREDIENTI_TABLE.ExecuteNonQueryAsync();
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.Run();
        }
    }
}