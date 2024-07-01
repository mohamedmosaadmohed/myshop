using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class solvebugsM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbOrderDetails_TbOrderHeaders_orderHeaderId",
                table: "TbOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderDetails",
                table: "TbOrderDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TbOrderDetails");

            migrationBuilder.RenameColumn(
                name: "orderHeaderId",
                table: "TbOrderDetails",
                newName: "OrderHeaderId");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "TbOrderDetails",
                newName: "OrderDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrderDetails_orderHeaderId",
                table: "TbOrderDetails",
                newName: "IX_TbOrderDetails_OrderHeaderId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailsId",
                table: "TbOrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderDetails",
                table: "TbOrderDetails",
                column: "OrderDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrderDetails_TbOrderHeaders_OrderHeaderId",
                table: "TbOrderDetails",
                column: "OrderHeaderId",
                principalTable: "TbOrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbOrderDetails_TbOrderHeaders_OrderHeaderId",
                table: "TbOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderDetails",
                table: "TbOrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderHeaderId",
                table: "TbOrderDetails",
                newName: "orderHeaderId");

            migrationBuilder.RenameColumn(
                name: "OrderDetailsId",
                table: "TbOrderDetails",
                newName: "orderId");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrderDetails_OrderHeaderId",
                table: "TbOrderDetails",
                newName: "IX_TbOrderDetails_orderHeaderId");

            migrationBuilder.AlterColumn<int>(
                name: "orderId",
                table: "TbOrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TbOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderDetails",
                table: "TbOrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrderDetails_TbOrderHeaders_orderHeaderId",
                table: "TbOrderDetails",
                column: "orderHeaderId",
                principalTable: "TbOrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
