using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class SeekerListing_preferred_name_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferedLandType",
                table: "SeekerListings",
                newName: "PreferredLandType");

            migrationBuilder.RenameColumn(
                name: "PetFriendllyRequired",
                table: "SeekerListings",
                newName: "PetFriendlyRequired");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferredLandType",
                table: "SeekerListings",
                newName: "PreferedLandType");

            migrationBuilder.RenameColumn(
                name: "PetFriendlyRequired",
                table: "SeekerListings",
                newName: "PetFriendllyRequired");
        }
    }
}
