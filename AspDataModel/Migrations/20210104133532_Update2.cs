using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspDataModel.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FirstVaccinateDateTime",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstVaccinateDescription",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemindFirstVaccinateDateTime",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemindSecondVaccinateDateTime",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecondVaccinateDateTime",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondVaccinateDescription",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstVaccinateDateTime",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FirstVaccinateDescription",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RemindFirstVaccinateDateTime",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RemindSecondVaccinateDateTime",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SecondVaccinateDateTime",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SecondVaccinateDescription",
                table: "Patients");
        }
    }
}
