using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiFlowing.Data.Migrations
{
    public partial class BodyMassIndexRangeDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO BodyMassIndexRanges(MinimumBMI, MaximumBMI, Description, IsIdeal) VALUES" +
                     "(0,     16.00, 'Grave magrezza', 0)," +
                     "(16,    18.49, 'Sottopeso',      0)," +
                     "(18.50, 24.99, 'Normopeso',      1)," +
                     "(25,    29.99, 'Sovrappeso',     0)," +
                     "(30,    34.99, 'Obeso classe 1', 0)," +
                     "(35,    39.99, 'Obeso classe 2', 0)," +
                     "(40,    null,  'Obeso classe 3', 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM BodyMassIndexRanges");
        }
    }
}
