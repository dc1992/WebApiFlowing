using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiFlowing.Data.Migrations
{
    public partial class NullableMaximumBMI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MaximumBMI",
                table: "BodyMassIndexRanges",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MaximumBMI",
                table: "BodyMassIndexRanges",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
