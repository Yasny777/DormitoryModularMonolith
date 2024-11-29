using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dormitories.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssociateTableForOccupants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomOccupants",
                schema: "dormitory",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOccupants", x => new { x.RoomId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_RoomOccupants_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "dormitory",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomOccupants",
                schema: "dormitory");
        }
    }
}
