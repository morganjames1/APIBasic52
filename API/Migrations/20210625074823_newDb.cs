using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class newDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_M_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_M_Employee", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "tb_M_University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_M_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_T_Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_T_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tb_T_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_T_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_T_Account_tb_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_M_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_M_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_M_Education_tb_M_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_M_University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_T_AccountRole",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_T_AccountRole", x => new { x.NIK, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_T_AccountRole_tb_T_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_T_AccountRole_tb_T_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_T_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_T_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_T_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_T_Profiling_tb_M_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_M_Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_T_Profiling_tb_T_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_M_Education_UniversityId",
                table: "tb_M_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_T_AccountRole_RoleId",
                table: "tb_T_AccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_T_Profiling_EducationId",
                table: "tb_T_Profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_T_AccountRole");

            migrationBuilder.DropTable(
                name: "tb_T_Profiling");

            migrationBuilder.DropTable(
                name: "tb_T_Role");

            migrationBuilder.DropTable(
                name: "tb_M_Education");

            migrationBuilder.DropTable(
                name: "tb_T_Account");

            migrationBuilder.DropTable(
                name: "tb_M_University");

            migrationBuilder.DropTable(
                name: "tb_M_Employee");
        }
    }
}
