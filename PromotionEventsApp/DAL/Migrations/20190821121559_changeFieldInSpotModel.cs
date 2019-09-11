using Microsoft.EntityFrameworkCore.Migrations;

namespace PromotionEventsApp.DAL.Migrations
{
    public partial class changeFieldInSpotModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Spots");

            migrationBuilder.AddColumn<string>(
                name: "Coords",
                table: "Spots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coords",
                table: "Spots");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Spots",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Spots",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
