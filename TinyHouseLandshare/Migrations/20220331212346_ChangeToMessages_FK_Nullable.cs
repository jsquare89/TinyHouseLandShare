using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyHouseLandshare.Migrations
{
    public partial class ChangeToMessages_FK_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_OriginMessageId",
                table: "Messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "OriginMessageId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_OriginMessageId",
                table: "Messages",
                column: "OriginMessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_OriginMessageId",
                table: "Messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "OriginMessageId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_OriginMessageId",
                table: "Messages",
                column: "OriginMessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
