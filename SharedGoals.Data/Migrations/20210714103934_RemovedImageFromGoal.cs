using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedGoals.Data.Migrations
{
    public partial class RemovedImageFromGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Goals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
