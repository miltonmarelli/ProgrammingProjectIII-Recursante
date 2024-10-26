using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Descuento",
                table: "Productos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Productos",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    IdShoppingCart = table.Column<Guid>(type: "TEXT", nullable: false),
                    Impuesto = table.Column<double>(type: "REAL", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.IdShoppingCart);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommercialInvoices",
                columns: table => new
                {
                    IdOrden = table.Column<Guid>(type: "TEXT", nullable: false),
                    ShoppingCartIdShoppingCart = table.Column<Guid>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialInvoices", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK_CommercialInvoices_ShoppingCarts_ShoppingCartIdShoppingCart",
                        column: x => x.ShoppingCartIdShoppingCart,
                        principalTable: "ShoppingCarts",
                        principalColumn: "IdShoppingCart",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommercialInvoices_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartProducto",
                columns: table => new
                {
                    ProductoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartProducto", x => new { x.ProductoId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_ShoppingCartProducto_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartProducto_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "IdShoppingCart",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommercialInvoices_ClientId",
                table: "CommercialInvoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CommercialInvoices_ShoppingCartIdShoppingCart",
                table: "CommercialInvoices",
                column: "ShoppingCartIdShoppingCart");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducto_ShoppingCartId",
                table: "ShoppingCartProducto",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ClientId",
                table: "ShoppingCarts",
                column: "ClientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommercialInvoices");

            migrationBuilder.DropTable(
                name: "ShoppingCartProducto");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Productos");
        }
    }
}
