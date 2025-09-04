using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class ChangeCompositekeyOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanProcedureUsers_PlanProcedures_ProcedureId_PlanId",
                table: "PlanProcedureUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanProcedureUsers",
                table: "PlanProcedureUsers");

            migrationBuilder.DropIndex(
                name: "IX_PlanProcedureUsers_ProcedureId_PlanId",
                table: "PlanProcedureUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanProcedureUsers",
                table: "PlanProcedureUsers",
                columns: new[] { "PlanId", "ProcedureId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanProcedureUsers_UserId",
                table: "PlanProcedureUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanProcedureUsers_PlanProcedures_PlanId_ProcedureId",
                table: "PlanProcedureUsers",
                columns: new[] { "PlanId", "ProcedureId" },
                principalTable: "PlanProcedures",
                principalColumns: new[] { "PlanId", "ProcedureId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanProcedureUsers_PlanProcedures_PlanId_ProcedureId",
                table: "PlanProcedureUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanProcedureUsers",
                table: "PlanProcedureUsers");

            migrationBuilder.DropIndex(
                name: "IX_PlanProcedureUsers_UserId",
                table: "PlanProcedureUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanProcedureUsers",
                table: "PlanProcedureUsers",
                columns: new[] { "UserId", "ProcedureId", "PlanId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanProcedureUsers_ProcedureId_PlanId",
                table: "PlanProcedureUsers",
                columns: new[] { "ProcedureId", "PlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanProcedureUsers_PlanProcedures_ProcedureId_PlanId",
                table: "PlanProcedureUsers",
                columns: new[] { "ProcedureId", "PlanId" },
                principalTable: "PlanProcedures",
                principalColumns: new[] { "PlanId", "ProcedureId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
