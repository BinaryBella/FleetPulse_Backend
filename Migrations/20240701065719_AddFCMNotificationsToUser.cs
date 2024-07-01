using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class AddFCMNotificationsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPasswordReset",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPasswordReset",
                table: "Notifications");
        }
    }
}
