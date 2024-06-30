using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addprimary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProduct_TbShoppingCarts_ShoppingCartId",
                table: "TbProduct");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TbShoppingCarts",
                newName: "shoppingId");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "TbProduct",
                newName: "ShoppingCartshoppingId");

            migrationBuilder.RenameIndex(
                name: "IX_TbProduct_ShoppingCartId",
                table: "TbProduct",
                newName: "IX_TbProduct_ShoppingCartshoppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProduct_TbShoppingCarts_ShoppingCartshoppingId",
                table: "TbProduct",
                column: "ShoppingCartshoppingId",
                principalTable: "TbShoppingCarts",
                principalColumn: "shoppingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProduct_TbShoppingCarts_ShoppingCartshoppingId",
                table: "TbProduct");

            migrationBuilder.RenameColumn(
                name: "shoppingId",
                table: "TbShoppingCarts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartshoppingId",
                table: "TbProduct",
                newName: "ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_TbProduct_ShoppingCartshoppingId",
                table: "TbProduct",
                newName: "IX_TbProduct_ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProduct_TbShoppingCarts_ShoppingCartId",
                table: "TbProduct",
                column: "ShoppingCartId",
                principalTable: "TbShoppingCarts",
                principalColumn: "Id");
        }
    }
}
