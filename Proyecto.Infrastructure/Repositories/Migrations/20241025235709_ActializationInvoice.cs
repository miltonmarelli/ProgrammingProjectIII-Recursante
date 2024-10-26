using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ActializationInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialInvoices_ShoppingCarts_ShoppingCartId",
                table: "CommercialInvoices");

            migrationBuilder.DropIndex(
                name: "IX_CommercialInvoices_ShoppingCartId",
                table: "CommercialInvoices");

            migrationBuilder.DropColumn(
                name: "CommercialInvoiceId",
                table: "ShoppingCarts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommercialInvoiceId",
                table: "ShoppingCarts",
                type: "TEXT",
                nullable: true);

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
    }
}
