using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GKLTNC.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPB = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenPB = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.MaPB);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoTen = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GioiTinh = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DiaChi = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Luong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaPB = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanVien_PhongBan_MaPB",
                        column: x => x.MaPB,
                        principalTable: "PhongBan",
                        principalColumn: "MaPB",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PhongBan",
                columns: new[] { "MaPB", "TenPB" },
                values: new object[,]
                {
                    { "PB01", "Phòng Kỹ thuật" },
                    { "PB02", "Phòng Kế toán" },
                    { "PB03", "Phòng Nhân sự" },
                    { "PB04", "Phòng Kinh doanh" },
                    { "PB05", "Phòng Hành chính" }
                });

            migrationBuilder.InsertData(
                table: "NhanVien",
                columns: new[] { "MaNV", "DiaChi", "GioiTinh", "HoTen", "Luong", "MaPB", "NgaySinh" },
                values: new object[,]
                {
                    { "NV01", "Hà Nội", true, "Nguyễn Văn An", 15000000m, "PB01", new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "NV02", "Đà Nẵng", false, "Trần Thị Bình", 12500000m, "PB02", new DateTime(1998, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "NV03", "TP. Hồ Chí Minh", true, "Lê Hoàng Cường", 18000000m, "PB04", new DateTime(1992, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaPB",
                table: "NhanVien",
                column: "MaPB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhongBan");
        }
    }
}
