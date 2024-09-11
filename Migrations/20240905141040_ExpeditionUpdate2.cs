using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlemionaApplication.Migrations
{
    /// <inheritdoc />
    public partial class ExpeditionUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Expedition",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Expedition");
        }
    }
}
