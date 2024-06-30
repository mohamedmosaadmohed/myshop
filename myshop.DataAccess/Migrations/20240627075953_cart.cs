using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "TbProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbShoppingCarts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbShoppingCarts_TbProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TbProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbProduct_ShoppingCartId",
                table: "TbProduct",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShoppingCarts_ApplicationUserId",
                table: "TbShoppingCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShoppingCarts_ProductId",
                table: "TbShoppingCarts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProduct_TbShoppingCarts_ShoppingCartId",
                table: "TbProduct",
                column: "ShoppingCartId",
                principalTable: "TbShoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProduct_TbShoppingCarts_ShoppingCartId",
                table: "TbProduct");

            migrationBuilder.DropTable(
                name: "TbShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_TbProduct_ShoppingCartId",
                table: "TbProduct");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "TbProduct");
        }
    }
}
