using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Dal.Migrations
{
    public partial class UpdateAppModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostId",
                table: "Apps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_HostId",
                table: "Apps",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Hosts_HostId",
                table: "Apps",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Hosts_HostId",
                table: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_Apps_HostId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Apps");
        }
    }
}
