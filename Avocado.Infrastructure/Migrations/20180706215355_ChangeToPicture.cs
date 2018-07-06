using Microsoft.EntityFrameworkCore.Migrations;

namespace Avocado.Infrastructure.Migrations
{
    public partial class ChangeToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Accounts",
                newName: "Picture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Accounts",
                newName: "Avatar");
        }
    }
}
