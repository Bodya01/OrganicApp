using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganicApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceIdToCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Cities");
        }
    }
}
