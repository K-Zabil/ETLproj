using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETLproj.Migrations
{
    /// <inheritdoc />
    public partial class CreateTrips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupDatetime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DropoffDatetime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PassengerCount = table.Column<int>(type: "INT", nullable: false),
                    TripDistance = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    StoreAndFwdFlag = table.Column<string>(type: "VARCHAR(3)", nullable: false),
                    PULocationID = table.Column<int>(type: "INT", nullable: false),
                    DOLocationID = table.Column<int>(type: "INT", nullable: false),
                    FareAmount = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    TipAmount = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.CheckConstraint("CHK_StoreAndFwdFlag", "StoreAndFwdFlag IN ('Yes', 'No')");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DropoffDatetime",
                table: "Trips",
                column: "DropoffDatetime");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PickupDatetime",
                table: "Trips",
                column: "PickupDatetime");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PULocationID",
                table: "Trips",
                column: "PULocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripDistance",
                table: "Trips",
                column: "TripDistance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
