using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pr_4._4.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    MasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Имя = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Фамилия = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Опыт = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Пол = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Описание = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    Адреспочты = table.Column<string>(name: "Адрес почты", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.MasterId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Название = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Цена = table.Column<decimal>(type: "decimal(18,2)", maxLength: 10, nullable: false),
                    Описание = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Времязаписи = table.Column<DateTime>(name: "Время записи", type: "datetime2", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptions_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "MasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receptions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Masters",
                columns: new[] { "MasterId", "Описание", "Адрес почты", "Опыт", "Пол", "Фамилия", "Имя" },
                values: new object[,]
                {
                    { 1, "по умолчанию 1", "YGGDRASIL@gmail.com", "100 Лет", "М", "Судзуки", "Сатор" },
                    { 2, "по умолчанию 2", "Overlord@gmail.com", "100 Лет", "М", "ОалГоун", "Айнз" },
                    { 3, "по умолчанию 3", "Degurechaff@gmail.com", "100 Лет", "Ж", "Дёгурешафф", "Таня" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Описание", "Название", "Цена" },
                values: new object[,]
                {
                    { 1, "по умолчанию 1", "Энри", 1000m },
                    { 2, "по умолчанию 2", "Момон", 2000m },
                    { 3, "по умолчанию 3", "Энфри", 3000m }
                });

            migrationBuilder.InsertData(
                table: "Receptions",
                columns: new[] { "Id", "MasterId", "ServiceId", "Время записи" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2138, 12, 1, 23, 59, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, new DateTime(2138, 12, 2, 23, 59, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, new DateTime(2138, 12, 3, 23, 59, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_MasterId_Время записи",
                table: "Receptions",
                columns: new[] { "MasterId", "Время записи" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_ServiceId",
                table: "Receptions",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
