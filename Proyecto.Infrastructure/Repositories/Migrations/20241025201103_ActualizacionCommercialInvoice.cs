using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionCommercialInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_CommercialInvoices_CommercialInvoiceId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CommercialInvoiceId",
                table: "ShoppingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_CommercialInvoices_ShoppingCartId",
                table: "CommercialInvoices",
                column: "ShoppingCartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialInvoices_ShoppingCarts_ShoppingCartId",
                table: "CommercialInvoices",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "IdShoppingCart",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialInvoices_ShoppingCarts_ShoppingCartId",
                table: "CommercialInvoices");

            migrationBuilder.DropIndex(
                name: "IX_CommercialInvoices_ShoppingCartId",
                table: "CommercialInvoices");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CommercialInvoiceId",
                table: "ShoppingCarts",
                column: "CommercialInvoiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_CommercialInvoices_CommercialInvoiceId",
                table: "ShoppingCarts",
                column: "CommercialInvoiceId",
                principalTable: "CommercialInvoices",
                principalColumn: "IdOrden");
        }
    }
}
