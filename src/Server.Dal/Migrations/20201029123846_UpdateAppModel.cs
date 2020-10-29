using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Dal.Migrations
{
    public partial class UpdateAppModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VmId",
                table: "Apps",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "VmId",
                table: "Apps",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
