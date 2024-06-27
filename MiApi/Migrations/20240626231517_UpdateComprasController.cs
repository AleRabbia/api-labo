using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComprasController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompra_Compras_CompraId",
                table: "DetalleCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleCompra",
                table: "DetalleCompra");

            migrationBuilder.RenameTable(
                name: "DetalleCompra",
                newName: "DetallesCompra");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCompra_CompraId",
                table: "DetallesCompra",
                newName: "IX_DetallesCompra_CompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesCompra",
                table: "DetallesCompra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesCompra",
                table: "DetallesCompra");

            migrationBuilder.RenameTable(
                name: "DetallesCompra",
                newName: "DetalleCompra");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesCompra_CompraId",
                table: "DetalleCompra",
                newName: "IX_DetalleCompra_CompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleCompra",
                table: "DetalleCompra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompra_Compras_CompraId",
                table: "DetalleCompra",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");
        }
    }
}
