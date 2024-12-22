using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.Data.Migrations
{
    /// <inheritdoc />
    public partial class PriorityWindowsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriorityWindows",
                schema: "reservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleNames = table.Column<string>(type: "text", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityWindows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriorityWindows_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalSchema: "reservation",
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorityWindows_SemesterId",
                schema: "reservation",
                table: "PriorityWindows",
                column: "SemesterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorityWindows",
                schema: "reservation");
        }
    }
}
