using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni_Sphere.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class teachercoursesection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Batch",
                table: "TeacherCourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batch",
                table: "TeacherCourseSections");
        }
    }
}
