using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Avocado.Infrastructure.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId1",
                table: "Members",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_EventId",
                table: "Members",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_EventId1",
                table: "Members",
                column: "EventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Events_EventId",
                table: "Members",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Events_EventId1",
                table: "Members",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Events_EventId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Events_EventId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_EventId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_EventId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Members");
        }
    }
}
