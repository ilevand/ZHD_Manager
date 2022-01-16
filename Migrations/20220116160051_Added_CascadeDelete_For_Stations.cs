using Microsoft.EntityFrameworkCore.Migrations;

namespace ZHD_Manager.Migrations
{
    public partial class Added_CascadeDelete_For_Stations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Stations_StationOfArrivalID",
                table: "Trains");

            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Stations_StationOfDepartureID",
                table: "Trains");

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Stations_StationOfArrivalID",
                table: "Trains",
                column: "StationOfArrivalID",
                principalTable: "Stations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Stations_StationOfDepartureID",
                table: "Trains",
                column: "StationOfDepartureID",
                principalTable: "Stations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Stations_StationOfArrivalID",
                table: "Trains");

            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Stations_StationOfDepartureID",
                table: "Trains");

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Stations_StationOfArrivalID",
                table: "Trains",
                column: "StationOfArrivalID",
                principalTable: "Stations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Stations_StationOfDepartureID",
                table: "Trains",
                column: "StationOfDepartureID",
                principalTable: "Stations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
