using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Dal.Migrations
{
    public partial class UpdateAppModelCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Hosts_HostId",
                table: "Apps");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Hosts_HostId",
                table: "Apps",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Hosts_HostId",
                table: "Apps");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Hosts_HostId",
                table: "Apps",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
