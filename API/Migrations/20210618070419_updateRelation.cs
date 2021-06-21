using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_M_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_M_Education_tb_M_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_M_University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_T_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_T_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_T_Profiling_tb_M_Education_EducationId1",
                        column: x => x.EducationId1,
                        principalTable: "tb_M_Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_tb_T_Profiling_EducationId1",
                table: "tb_T_Profiling",
                column: "EducationId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_T_Profiling");

            migrationBuilder.DropTable(
                name: "tb_M_Education");

            migrationBuilder.DropTable(
                name: "tb_T_Account");

            migrationBuilder.DropTable(
                name: "tb_M_University");
        }
    }
}
