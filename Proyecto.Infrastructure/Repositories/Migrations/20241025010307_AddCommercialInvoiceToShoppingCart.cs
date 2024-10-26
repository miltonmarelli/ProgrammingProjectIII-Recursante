using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommercialInvoiceToShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialInvoices_ShoppingCarts_ShoppingCartIdShoppingCart",
                table: "CommercialInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_CommercialInvoices_Users_ClientId",
                table: "CommercialInvoices");

            migrationBuilder.DropIndex(
                name: "IX_CommercialInvoices_ShoppingCartIdShoppingCart",
                table: "CommercialInvoices");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartIdShoppingCart",
                table: "CommercialInvoices",
                newName: "ShoppingCartId");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "CommercialInvoices",
                newName: "FechaEmision");

            migrationBuilder.AddColumn<Guid>(
                name: "CommercialInvoiceId",
                table: "ShoppingCarts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CommercialInvoices",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "CommercialInvoices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "CommercialInvoices",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CommercialInvoiceId",
                table: "ShoppingCarts",
                column: "CommercialInvoiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialInvoices_Users_ClientId",
                table: "CommercialInvoices",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_CommercialInvoices_CommercialInvoiceId",
                table: "ShoppingCarts",
                column: "CommercialInvoiceId",
                principalTable: "CommercialInvoices",
                principalColumn: "IdOrden");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialInvoices_Users_ClientId",
                table: "CommercialInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_CommercialInvoices_CommercialInvoiceId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CommercialInvoiceId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CommercialInvoiceId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "CommercialInvoices");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "CommercialInvoices");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "CommercialInvoices",
                newName: "ShoppingCartIdShoppingCart");

            migrationBuilder.RenameColumn(
                name: "FechaEmision",
                table: "CommercialInvoices",
                newName: "Estado");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CommercialInvoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommercialInvoices_ShoppingCartIdShoppingCart",
                table: "CommercialInvoices",
                column: "ShoppingCartIdShoppingCart");

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialInvoices_ShoppingCarts_ShoppingCartIdShoppingCart",
                table: "CommercialInvoices",
                column: "ShoppingCartIdShoppingCart",
                principalTable: "ShoppingCarts",
                principalColumn: "IdShoppingCart",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialInvoices_Users_ClientId",
                table: "CommercialInvoices",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
