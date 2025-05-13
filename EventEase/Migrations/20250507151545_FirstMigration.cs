using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingView",
                columns: table => new
                {
                    BookingViewid = table.Column<int>(type: "int", nullable: true)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(max)",nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)",nullable: true),
                    Capacity = table.Column<int>(type: "int",nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)",nullable: true),
                    EventName = table.Column<string>(type: "nvarchar(max)",nullable: true),
                    EventDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)",nullable: true),
                    BookingDate = table.Column<string>(type: "nvarchar(max)", nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingView", x => x.BookingViewid);
                });
        }

            //migrationBuilder.CreateTable(
            //    name: "Event",
            //    columns: table => new
            //    {
            //        EventID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EventName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
            //        EventDate = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Event__7944C8709092EA68", x => x.EventID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Venue",
            //    columns: table => new
            //    {
            //        VenueID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        VenueName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        Capacity = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        ImageURL = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Venue__3C57E5D241D319EB", x => x.VenueID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Booking",
            //    columns: table => new
            //    {
            //        BookingID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EventID = table.Column<int>(type: "int", nullable: true),
            //        VenueID = table.Column<int>(type: "int", nullable: true),
            //        BookingDate = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Booking__73951ACD2B51BAE1", x => x.BookingID);
            //        table.ForeignKey(
            //            name: "FK__Booking__EventID__3C69FB99",
            //            column: x => x.EventID,
            //            principalTable: "Event",
            //            principalColumn: "EventID");
            //        table.ForeignKey(
            //            name: "FK__Booking__VenueID__3D5E1FD2",
            //            column: x => x.VenueID,
            //            principalTable: "Venue",
            //            principalColumn: "VenueID");
            //    });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Booking_EventID",
        //        table: "Booking",
        //        column: "EventID");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Booking_VenueID",
        //        table: "Booking",
        //        column: "VenueID");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Venue__E55D3B104C6EE3E7",
        //        table: "Venue",
        //        column: "Location",
        //        unique: true);
        //}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.DropTable(
                name: "BookingView");
        }
    }
}
