using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace budgettracker.web.Migrations
{
    public partial class CreateBudgetModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SetAmount = table.Column<decimal>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    BudgetStart = table.Column<DateTime>(nullable: false),
                    ParentBudgetId = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
