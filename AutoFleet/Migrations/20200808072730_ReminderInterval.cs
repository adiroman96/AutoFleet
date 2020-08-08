using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoFleet.Migrations
{
    public partial class ReminderInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "Insurances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ReminderInterval",
                table: "Insurances",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "ReminderInterval",
                table: "Insurances");
        }
    }
}
