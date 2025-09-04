using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class PlanProcedureUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanProcedureUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanProcedureUsers", x => new { x.UserId, x.ProcedureId, x.PlanId });
                    table.ForeignKey(
                        name: "FK_PlanProcedureUsers_PlanProcedures_ProcedureId_PlanId",
                        columns: x => new { x.ProcedureId, x.PlanId },
                        principalTable: "PlanProcedures",
                        principalColumns: new[] { "PlanId", "ProcedureId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanProcedureUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanProcedureUsers_ProcedureId_PlanId",
                table: "PlanProcedureUsers",
                columns: new[] { "ProcedureId", "PlanId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanProcedureUsers");
        }
    }
}
