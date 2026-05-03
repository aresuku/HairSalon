using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pr_4._4.Migrations
{
    /// <inheritdoc />
    public partial class Seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Masters",
                keyColumn: "MasterId",
                keyValue: 1,
                column: "Имя",
                value: "Сатору");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Masters",
                keyColumn: "MasterId",
                keyValue: 1,
                column: "Имя",
                value: "Сатор");
        }
    }
}
