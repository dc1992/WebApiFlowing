using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiFlowing.Data.Migrations
{
    public partial class IndexAndUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ_DateAndUserId",
                table: "WeightHistory",
                columns: new[] { "DateOfMeasurement", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guid",
                table: "Users",
                column: "Guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_DateAndUserId",
                table: "WeightHistory");

            migrationBuilder.DropIndex(
                name: "IX_Guid",
                table: "Users");
        }
    }
}
