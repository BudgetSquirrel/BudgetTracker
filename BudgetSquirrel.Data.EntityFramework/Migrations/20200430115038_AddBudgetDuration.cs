using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetSquirrel.Data.EntityFramework.Migrations
{
    public partial class AddBudgetDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    NumberDays = table.Column<int>(nullable: true),
                    EndDayOfMonth = table.Column<int>(nullable: true),
                    RolloverEndDateOnSmallMonths = table.Column<bool>(nullable: true)
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
        }
    }
}
