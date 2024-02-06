using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class ConfigTripUserFuelRefillUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelRefillUser_FuelRefills_FuelRefillId",
                table: "FuelRefillUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FuelRefillUser_User_UserId",
                table: "FuelRefillUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuelRefillUser",
                table: "FuelRefillUser");

            migrationBuilder.RenameTable(
                name: "FuelRefillUser",
                newName: "FuelRefillUsers");

            migrationBuilder.RenameIndex(
                name: "IX_FuelRefillUser_UserId",
                table: "FuelRefillUsers",
                newName: "IX_FuelRefillUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuelRefillUsers",
                table: "FuelRefillUsers",
                columns: new[] { "FuelRefillId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FuelRefillUsers_FuelRefills_FuelRefillId",
                table: "FuelRefillUsers",
                column: "FuelRefillId",
                principalTable: "FuelRefills",
                principalColumn: "FuelRefillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FuelRefillUsers_User_UserId",
                table: "FuelRefillUsers",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelRefillUsers_FuelRefills_FuelRefillId",
                table: "FuelRefillUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FuelRefillUsers_User_UserId",
                table: "FuelRefillUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuelRefillUsers",
                table: "FuelRefillUsers");

            migrationBuilder.RenameTable(
                name: "FuelRefillUsers",
                newName: "FuelRefillUser");

            migrationBuilder.RenameIndex(
                name: "IX_FuelRefillUsers_UserId",
                table: "FuelRefillUser",
                newName: "IX_FuelRefillUser_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuelRefillUser",
                table: "FuelRefillUser",
                columns: new[] { "FuelRefillId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FuelRefillUser_FuelRefills_FuelRefillId",
                table: "FuelRefillUser",
                column: "FuelRefillId",
                principalTable: "FuelRefills",
                principalColumn: "FuelRefillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FuelRefillUser_User_UserId",
                table: "FuelRefillUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
