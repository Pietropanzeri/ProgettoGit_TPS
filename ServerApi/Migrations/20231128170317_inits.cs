using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApi.Migrations
{
    /// <inheritdoc />
    public partial class inits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 1,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 18, 3, 17, 139, DateTimeKind.Local).AddTicks(8618));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 2,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 18, 3, 17, 139, DateTimeKind.Local).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 3,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 18, 3, 17, 139, DateTimeKind.Local).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 4,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 18, 3, 17, 139, DateTimeKind.Local).AddTicks(8692));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 5,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 18, 3, 17, 139, DateTimeKind.Local).AddTicks(8698));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 1,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4708));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 2,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 3,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 4,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4771));

            migrationBuilder.UpdateData(
                table: "Ricette",
                keyColumn: "RicettaId",
                keyValue: 5,
                column: "DataAggiunta",
                value: new DateTime(2023, 11, 28, 13, 56, 1, 732, DateTimeKind.Local).AddTicks(4774));
        }
    }
}
