using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightsList.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    From = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    To = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Date", "FlightNumber", "From", "Price", "To" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "UA3321", "Chicago", 235m, "New York" },
                    { 2, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "QA1078", "Dubai", 590m, "London" },
                    { 3, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CA9087", "Hong Kong", 900m, "San Francisco" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightNumber",
                table: "Flights",
                column: "FlightNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
