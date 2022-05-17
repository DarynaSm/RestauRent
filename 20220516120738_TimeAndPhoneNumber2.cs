using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingASP.Data.Migrations
{
    public partial class TimeAndPhoneNumber2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosingTime",
                table: "Restaurant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningTime",
                table: "Restaurant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Restaurant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTime",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "OpeningTime",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Restaurant");
        }
    }
}
