using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentBackend.Migrations
{
    /// <inheritdoc />
    public partial class CourseModelAddListInscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_CourseId",
                table: "Inscriptions",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscriptions_Courses_CourseId",
                table: "Inscriptions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscriptions_Courses_CourseId",
                table: "Inscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Inscriptions_CourseId",
                table: "Inscriptions");
        }
    }
}
