using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookingEventDateTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookingDate",
                table: "BookingView",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EventDate",
                table: "BookingView",
                nullable: true,
                oldClrType: typeof(int));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BookingDate",
                table: "BookingView",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "EventDate",
                table: "BookingView",
                nullable: false,
                oldClrType: typeof(string));

        }
    }
}
