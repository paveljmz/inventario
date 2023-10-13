using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class TablasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    IdArea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.IdArea);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NCategoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caracteristicas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                   
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Area", "CorreoUsuario", "FechaCreacion", "NombreUsuario", "PasswordUsuario", "TipoUsuario" },
                values: new object[,]
                {
                    { 1, "tics", "jpavel202@gmail.com", new DateTime(2023, 10, 3, 23, 44, 55, 449, DateTimeKind.Local).AddTicks(4970), "Pavel", "pavel12345", "administrador" },
                    { 2, "tics", "Alex@gmail.com", new DateTime(2023, 10, 3, 23, 44, 55, 449, DateTimeKind.Local).AddTicks(4984), "Alejandro", "alex123", "administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
