using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EGAH.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class EventsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FirstEventId",
                table: "Incidents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SecondEventId",
                table: "Incidents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_FirstEventId",
                table: "Incidents",
                column: "FirstEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_SecondEventId",
                table: "Incidents",
                column: "SecondEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Events_FirstEventId",
                table: "Incidents",
                column: "FirstEventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Events_SecondEventId",
                table: "Incidents",
                column: "SecondEventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Events_FirstEventId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Events_SecondEventId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_FirstEventId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_SecondEventId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "FirstEventId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "SecondEventId",
                table: "Incidents");
        }
    }
}
