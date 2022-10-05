using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class LandListingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundationSize",
                table: "LandListings");

            migrationBuilder.DropColumn(
                name: "LotSize",
                table: "LandListings");

            migrationBuilder.DropColumn(
                name: "PayPeriod",
                table: "LandListings");

            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "LandListings");

            migrationBuilder.RenameColumn(
                name: "SmokingPermitted",
                table: "LandListings",
                newName: "SmokingFriendly");

            migrationBuilder.RenameColumn(
                name: "Pets",
                table: "LandListings",
                newName: "Private");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "SeekerListings",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "MapLocation",
                table: "LandListings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "LandListings",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AddColumn<bool>(
                name: "PetFriendly",
                table: "LandListings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetFriendly",
                table: "LandListings");

            migrationBuilder.RenameColumn(
                name: "SmokingFriendly",
                table: "LandListings",
                newName: "SmokingPermitted");

            migrationBuilder.RenameColumn(
                name: "Private",
                table: "LandListings",
                newName: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "SeekerListings",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "MapLocation",
                table: "LandListings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "LandListings",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<string>(
                name: "FoundationSize",
                table: "LandListings",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LotSize",
                table: "LandListings",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PayPeriod",
                table: "LandListings",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Privacy",
                table: "LandListings",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
