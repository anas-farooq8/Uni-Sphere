using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni_Sphere.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedTitleattribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionPosts_Classrooms_ClassRoomsId",
                table: "DiscussionPosts");

            migrationBuilder.RenameColumn(
                name: "ClassRoomsId",
                table: "DiscussionPosts",
                newName: "ClassroomsId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionPosts_ClassRoomsId",
                table: "DiscussionPosts",
                newName: "IX_DiscussionPosts_ClassroomsId");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DiscussionPosts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionPosts_Classrooms_ClassroomsId",
                table: "DiscussionPosts",
                column: "ClassroomsId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionPosts_Classrooms_ClassroomsId",
                table: "DiscussionPosts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "DiscussionPosts");

            migrationBuilder.RenameColumn(
                name: "ClassroomsId",
                table: "DiscussionPosts",
                newName: "ClassRoomsId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionPosts_ClassroomsId",
                table: "DiscussionPosts",
                newName: "IX_DiscussionPosts_ClassRoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionPosts_Classrooms_ClassRoomsId",
                table: "DiscussionPosts",
                column: "ClassRoomsId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
