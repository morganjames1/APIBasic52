using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addgender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_M_Education_tb_M_University_UniversityId",
                table: "tb_M_Education");

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "tb_M_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "tb_M_Education",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_M_Education_tb_M_University_UniversityId",
                table: "tb_M_Education",
                column: "UniversityId",
                principalTable: "tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_M_Education_tb_M_University_UniversityId",
                table: "tb_M_Education");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "tb_M_Employee");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "tb_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_M_Education_tb_M_University_UniversityId",
                table: "tb_M_Education",
                column: "UniversityId",
                principalTable: "tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
