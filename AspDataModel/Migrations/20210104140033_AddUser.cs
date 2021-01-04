using Microsoft.EntityFrameworkCore.Migrations;

namespace AspDataModel.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "Password", "Role", "Username", "Valid" },
                values: new object[] { 2, "Huu Truong", "FHQwSU5ronM3lFxFeZqUGNC98UI=", 3, "admin", true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "Password", "Role", "Username", "Valid" },
                values: new object[] { 3, "Hoc Le", "majYDXpuwhSpOMxTe78bFa8w6m0=", 2, "nurse", true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "Password", "Role", "Username", "Valid" },
                values: new object[] { 4, "Man Nguyen", "DXC0+heieDbWRdCjE/Gm7SSDFls=", 1, "assistant", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
