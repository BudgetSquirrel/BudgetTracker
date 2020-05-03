using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetSquirrel.Data.EntityFramework.Migrations
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
                    NumberDays = table.Column<int>(nullable: true),
                    EndDayOfMonth = table.Column<int>(nullable: true),
                    RolloverEndDateOnSmallMonths = table.Column<bool>(nullable: true)
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
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
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
                    PercentAmount = table.Column<double>(nullable: true),
                    SetAmount = table.Column<decimal>(nullable: true),
                    FundBalance = table.Column<decimal>(nullable: false),
                    DurationId = table.Column<Guid>(nullable: false),
                    BudgetStart = table.Column<DateTime>(nullable: false),
                    ParentBudgetId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Budgets_Budgets_ParentBudgetId",
                        column: x => x.ParentBudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DurationId",
                table: "Budgets",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ParentBudgetId",
                table: "Budgets",
                column: "ParentBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "BudgetDurations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
