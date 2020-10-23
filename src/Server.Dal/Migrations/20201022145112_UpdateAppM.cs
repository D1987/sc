using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Dal.Migrations
{
    public partial class UpdateAppM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Apps_VMs_VMId",
            //    table: "Apps");

            migrationBuilder.DropForeignKey(
                name: "FK_VMs_Hosts_HostId",
                table: "VMs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "VMs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Apps");

            //migrationBuilder.RenameColumn(
            //    name: "OS",
            //    table: "VMs",
            //    newName: "Os");

            //migrationBuilder.RenameColumn(
            //    name: "IP",
            //    table: "VMs",
            //    newName: "Ip");

            //migrationBuilder.RenameColumn(
            //    name: "OS",
            //    table: "Hosts",
            //    newName: "Os");

            //migrationBuilder.RenameColumn(
            //    name: "IP",
            //    table: "Hosts",
            //    newName: "Ip");

            //migrationBuilder.RenameColumn(
            //    name: "VMId",
            //    table: "Apps",
            //    newName: "VmId");

            //migrationBuilder.RenameColumn(
            //    name: "IP",
            //    table: "Apps",
            //    newName: "Ip");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Apps_VMId",
            //    table: "Apps",
            //    newName: "IX_Apps_VmId");

            migrationBuilder.AlterColumn<int>(
                name: "HostId",
                table: "VMs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            //migrationBuilder.AddColumn<bool>(
            //    name: "Enabled",
            //    table: "VMs",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "VMs",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "Enabled",
            //    table: "Hosts",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "Hosts",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "Enabled",
            //    table: "Apps",
            //    nullable: false,
            //    defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HostId",
                table: "Apps",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "Apps",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Project",
            //    table: "Apps",
            //    nullable: true);

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

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Apps_VMs_VmId",
            //    table: "Apps",
            //    column: "VmId",
            //    principalTable: "VMs",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VMs_Hosts_HostId",
                table: "VMs",
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

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Apps_VMs_VmId",
            //    table: "Apps");

            migrationBuilder.DropForeignKey(
                name: "FK_VMs_Hosts_HostId",
                table: "VMs");

            migrationBuilder.DropIndex(
                name: "IX_Apps_HostId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "VMs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "VMs");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "Project",
                table: "Apps");

            migrationBuilder.RenameColumn(
                name: "Os",
                table: "VMs",
                newName: "OS");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "VMs",
                newName: "IP");

            migrationBuilder.RenameColumn(
                name: "Os",
                table: "Hosts",
                newName: "OS");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "Hosts",
                newName: "IP");

            migrationBuilder.RenameColumn(
                name: "VmId",
                table: "Apps",
                newName: "VMId");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "Apps",
                newName: "IP");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Apps_VmId",
            //    table: "Apps",
            //    newName: "IX_Apps_VMId");

            migrationBuilder.AlterColumn<int>(
                name: "HostId",
                table: "VMs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VMs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Hosts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Apps",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_VMs_VMId",
                table: "Apps",
                column: "VMId",
                principalTable: "VMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VMs_Hosts_HostId",
                table: "VMs",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
