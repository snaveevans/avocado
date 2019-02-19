using Microsoft.EntityFrameworkCore.Migrations;

namespace Avocado.Infrastructure.Migrations
{
    public partial class AccountNormalizedUserNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_NormalizedUserName",
                table: "Accounts",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_NormalizedUserName",
                table: "Accounts");
        }
    }
}
