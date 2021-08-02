using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedGoals.Data.Migrations
{
    public partial class RemovedProgressProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDoneInPercents",
                table: "GoalWorks");

            migrationBuilder.DropColumn(
                name: "ProgressInPercents",
                table: "Goals");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Goals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "WorkDoneInPercents",
                table: "GoalWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgressInPercents",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
