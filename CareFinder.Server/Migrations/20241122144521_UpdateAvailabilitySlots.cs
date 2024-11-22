using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareFinder.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvailabilitySlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BreakInterval",
                table: "AvailabilitySlots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakInterval",
                table: "AvailabilitySlots");
        }
    }
}
