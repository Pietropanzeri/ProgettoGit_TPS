using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApi.Migrations
{
    /// <inheritdoc />
    public partial class aggiuntaDataRicetta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAggiunta",
                table: "Ricette",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAggiunta",
                table: "Ricette");
        }
    }
}
