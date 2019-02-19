using Microsoft.EntityFrameworkCore.Migrations;

namespace Avocado.Infrastructure.Migrations
{
    public partial class LoginProviderKeyRefactor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Logins",
                newName: "ProviderKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "Logins",
                newName: "ProviderId");
        }
    }
}
