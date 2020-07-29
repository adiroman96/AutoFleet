using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoFleet.Data.Migrations
{
    public partial class AddInsuranceTypeColumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfInsurance",
                table: "Insurances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Insurances",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfInsurance",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Insurances");
        }
    }
}
