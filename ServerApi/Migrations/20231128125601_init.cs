using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredienti",
                columns: table => new
                {
                    IngredienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(800)", nullable: false),
                    DataInizio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataFine = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredienti", x => x.IngredienteId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.UtenteId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FotosUtenti",
                columns: table => new
                {
                    FotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FotoData = table.Column<byte[]>(type: "longblob", nullable: false),
                    UtenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosUtenti", x => x.FotoId);
                    table.ForeignKey(
                        name: "FK_FotosUtenti_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ricette",
                columns: table => new
                {
                    RicettaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preparazione = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tempo = table.Column<int>(type: "int", nullable: true),
                    Difficolta = table.Column<int>(type: "int", nullable: true),
                    Piatto = table.Column<int>(type: "int", nullable: false),
                    DataAggiunta = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ricette", x => x.RicettaId);
                    table.ForeignKey(
                        name: "FK_Ricette_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UtenteUtentiSeguiti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    UtenteSeguitoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteUtentiSeguiti", x => new { x.UtenteId, x.UtenteSeguitoId });
                    table.ForeignKey(
                        name: "FK_UtenteUtentiSeguiti_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UtenteUtentiSeguiti_Utenti_UtenteSeguitoId",
                        column: x => x.UtenteSeguitoId,
                        principalTable: "Utenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    FotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descrizione = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FotoData = table.Column<byte[]>(type: "longblob", nullable: false),
                    RicettaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.FotoId);
                    table.ForeignKey(
                        name: "FK_Fotos_Ricette_RicettaId",
                        column: x => x.RicettaId,
                        principalTable: "Ricette",
                        principalColumn: "RicettaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RicetteIngredienti",
                columns: table => new
                {
                    RicettaId = table.Column<int>(type: "int", nullable: false),
                    IngredienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RicetteIngredienti", x => new { x.RicettaId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_RicetteIngredienti_Ingredienti_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredienti",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RicetteIngredienti_Ricette_RicettaId",
                        column: x => x.RicettaId,
                        principalTable: "Ricette",
                        principalColumn: "RicettaId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UtentiRicetteSalvate",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    RicettaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtentiRicetteSalvate", x => new { x.RicettaId, x.UtenteId });
                    table.ForeignKey(
                        name: "FK_UtentiRicetteSalvate_Ricette_RicettaId",
                        column: x => x.RicettaId,
                        principalTable: "Ricette",
                        principalColumn: "RicettaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UtentiRicetteSalvate_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Ingredienti",
                columns: new[] { "IngredienteId", "DataFine", "DataInizio", "Descrizione", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto derivato dalla macinazione dei cereali.", "Farina" },
                    { 2, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frutto comunemente utilizzato in cucina.", "Pomodoro" },
                    { 3, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vegetale dal sapore caratteristico.", "Cipolla" },
                    { 4, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spezia aromatica utilizzata in molti piatti.", "Aglio" },
                    { 5, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spezia piccante usata in diverse cucine del mondo.", "Peperoncino" },
                    { 6, new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pesce ricco di omega-3.", "Salmone" },
                    { 7, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vegetale verde e versatile in cucina.", "Zucchine" },
                    { 8, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spezia comune per condire i piatti.", "Pepe nero" },
                    { 9, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Varietà di riso aromatica.", "Riso basmati" },
                    { 10, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vegetale viola ampiamente utilizzato in cucina.", "Melanzane" },
                    { 11, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carne di pollame comunemente consumata.", "Pollo" },
                    { 12, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miscela di spezie utilizzata nella cucina indiana.", "Curry in polvere" },
                    { 13, new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vegetale a foglia verde ricco di nutrienti.", "Spinaci" },
                    { 14, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Condimento utilizzato in molte cucine asiatiche.", "Salsa di soia" },
                    { 15, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pasta fermentata utilizzata nella cucina giapponese.", "Pasta di miso" },
                    { 16, new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Varietà di pepe meno maturo, spesso utilizzato fresco.", "Pepe verde" },
                    { 17, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Legume ricco di proteine.", "Lenticchie" },
                    { 18, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Varietà di funghi apprezzata in cucina.", "Funghi porcini" },
                    { 19, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frutto agrumato ricco di vitamina C.", "Arancia" },
                    { 20, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Varietà di cioccolato con alto contenuto di cacao.", "Cioccolato fondente" }
                });

            migrationBuilder.InsertData(
                table: "Utenti",
                columns: new[] { "UtenteId", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "pIbvhgmpVHahDBTYUgQvew==", "admin" },
                    { 2, "pIbvhgmpVHahDBTYUgQvew", "Bob456" },
                    { 3, "pIbvhgmpVHahDBTYUgQvew", "Charlie789" }
                });

            migrationBuilder.InsertData(
                table: "FotosUtenti",
                columns: new[] { "FotoId", "FotoData", "UtenteId" },
                values: new object[,]
                {
                    { 1, new byte[0], 1 },
                    { 2, new byte[0], 2 },
                    { 3, new byte[0], 3 }
                });

            migrationBuilder.InsertData(
                table: "Ricette",
                columns: new[] { "RicettaId", "DataAggiunta", "Difficolta", "Nome", "Piatto", "Preparazione", "Tempo", "UtenteId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4708), 2, "Pasta al Pomodoro", 0, "Cuocere la pasta e condirla con pomodoro fresco, olio e basilico.", 30, 1 },
                    { 2, new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4765), 3, "Salmone alla Griglia", 1, "Marinare il salmone e cuocerlo alla griglia con limone e erbe aromatiche.", 45, 1 },
                    { 3, new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4768), 1, "Insalata di Riso", 7, "Cuocere il riso, mescolarlo con verdure e condire con olio e aceto.", 20, 2 },
                    { 4, new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4771), 3, "Pollo al Curry", 1, "Cuocere il pollo e aggiungere una salsa di curry speziata.", 40, 2 },
                    { 5, new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4774), 2, "Muffin al Cioccolato", 2, "Preparare l'impasto con farina, cioccolato fondente e zucchero.", 25, 3 }
                });

            migrationBuilder.InsertData(
                table: "UtenteUtentiSeguiti",
                columns: new[] { "UtenteId", "UtenteSeguitoId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Fotos",
                columns: new[] { "FotoId", "Descrizione", "FotoData", "RicettaId" },
                values: new object[,]
                {
                    { 1, "Immagine della Pasta al Pomodoro", new byte[0], 1 },
                    { 2, "Immagine del Salmone alla Griglia", new byte[0], 2 },
                    { 3, "Immagine dell'Insalata di Riso", new byte[0], 3 },
                    { 4, "Immagine del Pollo al Curry", new byte[0], 4 },
                    { 5, "Immagine del Muffin al Cioccolato", new byte[0], 5 },
                    { 6, "Immagine del Muffin al Cioccolato 2", new byte[0], 5 }
                });

            migrationBuilder.InsertData(
                table: "RicetteIngredienti",
                columns: new[] { "IngredienteId", "RicettaId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 3 },
                    { 9, 3 },
                    { 10, 3 },
                    { 11, 4 },
                    { 12, 4 },
                    { 20, 5 }
                });

            migrationBuilder.InsertData(
                table: "UtentiRicetteSalvate",
                columns: new[] { "RicettaId", "UtenteId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_RicettaId",
                table: "Fotos",
                column: "RicettaId");

            migrationBuilder.CreateIndex(
                name: "IX_FotosUtenti_UtenteId",
                table: "FotosUtenti",
                column: "UtenteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ricette_UtenteId",
                table: "Ricette",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RicetteIngredienti_IngredienteId",
                table: "RicetteIngredienti",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_UtenteUtentiSeguiti_UtenteSeguitoId",
                table: "UtenteUtentiSeguiti",
                column: "UtenteSeguitoId");

            migrationBuilder.CreateIndex(
                name: "IX_UtentiRicetteSalvate_UtenteId",
                table: "UtentiRicetteSalvate",
                column: "UtenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "FotosUtenti");

            migrationBuilder.DropTable(
                name: "RicetteIngredienti");

            migrationBuilder.DropTable(
                name: "UtenteUtentiSeguiti");

            migrationBuilder.DropTable(
                name: "UtentiRicetteSalvate");

            migrationBuilder.DropTable(
                name: "Ingredienti");

            migrationBuilder.DropTable(
                name: "Ricette");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
