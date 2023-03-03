using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenidad", "DateCreate", "DateUpdate", "Detalle", "ImageUrl", "MetrosCuadrados", "Name", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 3, 2, 21, 56, 0, 544, DateTimeKind.Local).AddTicks(9978), new DateTime(2023, 3, 2, 21, 56, 0, 544, DateTimeKind.Local).AddTicks(9991), "Detalle de la Villa", "", 50, "Vila Real", 5, 200.0 },
                    { 2, "", new DateTime(2023, 3, 2, 21, 56, 0, 544, DateTimeKind.Local).AddTicks(9994), new DateTime(2023, 3, 2, 21, 56, 0, 544, DateTimeKind.Local).AddTicks(9995), "Detalle de la Villa", "", 50, "Premium Vista a la piscina", 5, 200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
