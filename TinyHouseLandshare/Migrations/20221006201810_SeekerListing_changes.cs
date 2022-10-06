using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class SeekerListing_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "SeekerListings");

            migrationBuilder.DropColumn(
                name: "Smoker",
                table: "SeekerListings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Privacy",
                table: "SeekerListings",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Smoker",
                table: "SeekerListings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
