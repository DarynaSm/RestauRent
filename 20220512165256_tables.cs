using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingASP.Data.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tables",
                table: "Restaurant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tables",
                table: "Restaurant");
        }
    }
}
