using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace budgettracker.web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    NumberDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetDurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SetAmount = table.Column<decimal>(nullable: false),
                    BudgetStart = table.Column<DateTime>(nullable: false),
                    ParentBudgetId = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DurationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_BudgetDurations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "BudgetDurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DurationId",
                table: "Budgets",
                column: "DurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BudgetDurations");
        }
    }
}
