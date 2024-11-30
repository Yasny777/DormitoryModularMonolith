using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoomInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomInfo_Capacity",
                schema: "reservation",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomInfo_Number",
                schema: "reservation",
                table: "Reservations",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RoomInfo_Price",
                schema: "reservation",
                table: "Reservations",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomInfo_Capacity",
                schema: "reservation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomInfo_Number",
                schema: "reservation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomInfo_Price",
                schema: "reservation",
                table: "Reservations");
        }
    }
}
