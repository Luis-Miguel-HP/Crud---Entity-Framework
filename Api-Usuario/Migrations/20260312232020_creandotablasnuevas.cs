using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Usuario.Migrations
{
    /// <inheritdoc />
    public partial class creandotablasnuevas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodcuto_Categoría_IdCategoria",
                table: "Prodcuto");

            migrationBuilder.DropForeignKey(
                name: "FK_Prodcuto_Proveedor_IdProveedor",
                table: "Prodcuto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prodcuto",
                table: "Prodcuto");

            migrationBuilder.RenameTable(
                name: "Prodcuto",
                newName: "Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Prodcuto_IdProveedor",
                table: "Producto",
                newName: "IX_Producto_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_Prodcuto_IdCategoria",
                table: "Producto",
                newName: "IX_Producto_IdCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Categoría_IdCategoria",
                table: "Producto",
                column: "IdCategoria",
                principalTable: "Categoría",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Proveedor_IdProveedor",
                table: "Producto",
                column: "IdProveedor",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Categoría_IdCategoria",
                table: "Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Proveedor_IdProveedor",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Prodcuto");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_IdProveedor",
                table: "Prodcuto",
                newName: "IX_Prodcuto_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_IdCategoria",
                table: "Prodcuto",
                newName: "IX_Prodcuto_IdCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prodcuto",
                table: "Prodcuto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodcuto_Categoría_IdCategoria",
                table: "Prodcuto",
                column: "IdCategoria",
                principalTable: "Categoría",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodcuto_Proveedor_IdProveedor",
                table: "Prodcuto",
                column: "IdProveedor",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
