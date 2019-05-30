using Microsoft.EntityFrameworkCore.Migrations;

namespace PromotionEventsApp.DAL.Migrations
{
    public partial class VisitedPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSpots",
                columns: table => new
                {
                    SpotId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventId1 = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpots", x => new { x.UserId, x.SpotId });
                    table.ForeignKey(
                        name: "FK_UserSpots_AspNetUsers_EventId",
                        column: x => x.EventId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSpots_Events_EventId1",
                        column: x => x.EventId1,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSpots_Spots_SpotId",
                        column: x => x.SpotId,
                        principalTable: "Spots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSpots_EventId",
                table: "UserSpots",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpots_EventId1",
                table: "UserSpots",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpots_SpotId",
                table: "UserSpots",
                column: "SpotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSpots");
        }
    }
}
