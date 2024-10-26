using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionEnRelaciondeEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShoppingCarts_ShoppingCartId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShoppingCartId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ClientId",
                table: "ShoppingCarts",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Users_ClientId",
                table: "ShoppingCarts",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Users_ClientId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_ClientId",
                table: "ShoppingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShoppingCartId",
                table: "Users",
                column: "ShoppingCartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShoppingCarts_ShoppingCartId",
                table: "Users",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "IdShoppingCart",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
