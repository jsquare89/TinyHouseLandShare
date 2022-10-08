using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class SeekerListing_petfriendly_field_name_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PetsRequired",
                table: "SeekerListings",
                newName: "PetFriendllyRequired");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PetFriendllyRequired",
                table: "SeekerListings",
                newName: "PetsRequired");
        }
    }
}
