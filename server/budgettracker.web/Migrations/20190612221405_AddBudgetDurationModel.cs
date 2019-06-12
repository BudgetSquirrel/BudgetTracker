using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace budgettracker.web.Migrations
{
    public partial class AddBudgetDurationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Budgets");

            migrationBuilder.AddColumn<Guid>(
                name: "DurationId",
                table: "Budgets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BudgetDurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DurationType = table.Column<string>(nullable: true),
                    StartDayOfMonth = table.Column<int>(nullable: false),
                    EndDayOfMonth = table.Column<int>(nullable: false),
                    RolloverStartDateOnSmallMonths = table.Column<bool>(nullable: false),
                    RolloverEndDateOnSmallMonths = table.Column<bool>(nullable: false),
                    NumDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetDurations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DurationId",
                table: "Budgets",
                column: "DurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_BudgetDurations_DurationId",
                table: "Budgets",
                column: "DurationId",
                principalTable: "BudgetDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_BudgetDurations_DurationId",
                table: "Budgets");

            migrationBuilder.DropTable(
                name: "BudgetDurations");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_DurationId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DurationId",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Budgets",
                nullable: false,
                defaultValue: 0);
        }
    }
}
