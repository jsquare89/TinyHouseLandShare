using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class SeekerListing_wifi_changed_to_internet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WifiConnectionRequired",
                table: "SeekerListings",
                newName: "InternetConnectionRequired");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternetConnectionRequired",
                table: "SeekerListings",
                newName: "WifiConnectionRequired");
        }
    }
}
