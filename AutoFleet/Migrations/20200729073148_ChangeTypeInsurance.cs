using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoFleet.Migrations
{
    public partial class ChangeTypeInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ITP_TypeOfInsurance",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Rca_TypeOfInsurance",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Rovinieta_TypeOfInsurance",
                table: "Insurances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ITP_TypeOfInsurance",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rca_TypeOfInsurance",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rovinieta_TypeOfInsurance",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
