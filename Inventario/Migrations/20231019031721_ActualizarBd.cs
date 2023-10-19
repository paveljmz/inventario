using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarBd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TipoProducto_IdTipoProducto",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "IdTipoProducto",
                table: "Productos",
                newName: "IdTProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_IdTipoProducto",
                table: "Productos",
                newName: "IX_Productos_IdTProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TipoProducto_IdTProducto",
                table: "Productos",
                column: "IdTProducto",
                principalTable: "TipoProducto",
                principalColumn: "IdTipoProducto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TipoProducto_IdTProducto",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "IdTProducto",
                table: "Productos",
                newName: "IdTipoProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_IdTProducto",
                table: "Productos",
                newName: "IX_Productos_IdTipoProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TipoProducto_IdTipoProducto",
                table: "Productos",
                column: "IdTipoProducto",
                principalTable: "TipoProducto",
                principalColumn: "IdTipoProducto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
