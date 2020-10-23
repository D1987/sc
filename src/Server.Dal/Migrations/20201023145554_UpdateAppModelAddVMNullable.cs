using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Dal.Migrations
{
    public partial class UpdateAppModelAddVMNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_VMs_VmId",
                table: "Apps");

            migrationBuilder.AlterColumn<int>(
                name: "VmId",
                table: "Apps",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_VMs_VmId",
                table: "Apps",
                column: "VmId",
                principalTable: "VMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_VMs_VmId",
                table: "Apps");

            migrationBuilder.AlterColumn<int>(
                name: "VmId",
                table: "Apps",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_VMs_VmId",
                table: "Apps",
                column: "VmId",
                principalTable: "VMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
