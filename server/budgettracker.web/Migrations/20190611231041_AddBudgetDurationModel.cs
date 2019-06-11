using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace budgettracker.web.Migrations
{
    public partial class AddBudgetDurationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetDurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DurrationType = table.Column<string>(nullable: true),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetDurations");
        }
    }
}
