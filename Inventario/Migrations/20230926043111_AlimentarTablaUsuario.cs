using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Area", "CorreoUsuario", "FechaCreacion", "NombreUsuario", "PasswordUsuario", "TipoUsuario" },
                values: new object[,]
                {
                    { 1, "tics", "jpavel202@gmail.com", new DateTime(2023, 9, 25, 22, 31, 11, 343, DateTimeKind.Local).AddTicks(6804), "Pavel", "pavel12345", "administrador" },
                    { 2, "tics", "Alex@gmail.com", new DateTime(2023, 9, 25, 22, 31, 11, 343, DateTimeKind.Local).AddTicks(6817), "Alejandro", "alex123", "administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2);
        }
    }
}
