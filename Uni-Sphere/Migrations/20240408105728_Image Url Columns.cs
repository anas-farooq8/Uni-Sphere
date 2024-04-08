using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni_Sphere.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Students");
        }
    }
}
