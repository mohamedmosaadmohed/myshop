using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbOrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    shippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    orderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbOrderHeaders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    orderHeaderId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbOrderDetails_TbOrderHeaders_orderHeaderId",
                        column: x => x.orderHeaderId,
                        principalTable: "TbOrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbOrderDetails_TbProduct_productId",
                        column: x => x.productId,
                        principalTable: "TbProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbOrderDetails_orderHeaderId",
                table: "TbOrderDetails",
                column: "orderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TbOrderDetails_productId",
                table: "TbOrderDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TbOrderHeaders_ApplicationUserId",
                table: "TbOrderHeaders",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbOrderDetails");

            migrationBuilder.DropTable(
                name: "TbOrderHeaders");
        }
    }
}
