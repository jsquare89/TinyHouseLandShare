using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class LandListingChanges_NoSmoking_NameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SmokingFriendly",
                table: "LandListings",
                newName: "NoSmoking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoSmoking",
                table: "LandListings",
                newName: "SmokingFriendly");
        }
    }
}
