using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookWeb.Migrations
{
    public partial class addCompanyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_PostalCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_StreetAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUser_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_StreetAddress",
                table: "AspNetUsers");
        }
    }
}
