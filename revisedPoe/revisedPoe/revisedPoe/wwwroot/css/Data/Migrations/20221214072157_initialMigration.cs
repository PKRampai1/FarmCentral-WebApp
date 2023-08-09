using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyTime.Data.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "studentModules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moduleCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moduleCredit = table.Column<int>(type: "int", nullable: false),
                    semesterWeeks = table.Column<int>(type: "int", nullable: false),
                    moduleHours = table.Column<int>(type: "int", nullable: false),
                    moduleSelfStudy = table.Column<int>(type: "int", nullable: false),
                    SemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentModules", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentModules");
        }
    }
}
