using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni_Sphere.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AttendanceClassUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Attendance",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "SectionsId",
                table: "Attendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_SectionsId",
                table: "Attendance",
                column: "SectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Sections_SectionsId",
                table: "Attendance",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Sections_SectionsId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_SectionsId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "SectionsId",
                table: "Attendance");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Attendance",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
