using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class solve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDetailsId",
                table: "TbOrderDetails",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TbOrderDetails",
                newName: "OrderDetailsId");
        }
    }
}
