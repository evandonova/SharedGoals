using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedGoals.Data.Migrations
{
    public partial class GoalGoalWorkDeleteBehaviorCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalWorks_Goals_GoalId",
                table: "GoalWorks");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalWorks_Goals_GoalId",
                table: "GoalWorks",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalWorks_Goals_GoalId",
                table: "GoalWorks");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalWorks_Goals_GoalId",
                table: "GoalWorks",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
