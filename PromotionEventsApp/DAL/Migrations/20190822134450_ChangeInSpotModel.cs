using Microsoft.EntityFrameworkCore.Migrations;

namespace PromotionEventsApp.DAL.Migrations
{
    public partial class ChangeInSpotModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coords",
                table: "Spots",
                newName: "Image");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Spots",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Spots",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Spots");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Spots",
                newName: "Coords");
        }
    }
}
