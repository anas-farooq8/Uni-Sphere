using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni_Sphere.Migrations
{
    /// <inheritdoc />
    public partial class StudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    RollNo = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    Section = table.Column<string>(type: "char(1)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    DegreeProgram = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Batch = table.Column<int>(type: "int", nullable: false),
                    CurrentSemester = table.Column<short>(type: "smallint", nullable: false),
                    Gpa = table.Column<float>(type: "real", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
