using Microsoft.EntityFrameworkCore.Migrations;

namespace UserDatabase.Migrations
{
    public partial class FixCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDAte",
                table: "User",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "User",
                newName: "CreatedDAte");
        }
    }
}
