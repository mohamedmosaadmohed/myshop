using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "TbOrderHeaders",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "TbOrderHeaders",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "TbOrderHeaders",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TbOrderHeaders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "Region");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "TbOrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalPhoneNumber",
                table: "TbOrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TbOrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "TbOrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "TbOrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "TbOrderHeaders");

            migrationBuilder.DropColumn(
                name: "AdditionalPhoneNumber",
                table: "TbOrderHeaders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TbOrderHeaders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "TbOrderHeaders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "TbOrderHeaders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "TbOrderHeaders",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TbOrderHeaders",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "TbOrderHeaders",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "TbOrderHeaders",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
