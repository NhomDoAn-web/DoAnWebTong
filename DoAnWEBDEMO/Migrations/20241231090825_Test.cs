using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnWEBDEMO.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DON_HANGs",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    NgayDatHang = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TongTienDonHang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    DiaChiNhanHang = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DON_HANGs", x => x.MaDH);
                });

            migrationBuilder.CreateTable(
                name: "kHACH_HANGs",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kHACH_HANGs", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "San_Phams",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNCC = table.Column<int>(type: "int", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    HinhAnhSP = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    MauSP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TGianBaoHanh = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_San_Phams", x => x.MaSP);
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_BINH_LUANs",
                columns: table => new
                {
                    MA_SP = table.Column<int>(type: "int", nullable: false),
                    MA_KH = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    SO_SAO = table.Column<int>(type: "int", nullable: false),
                    NOI_DUNG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NGAY = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHI_TIET_BINH_LUANs", x => new { x.MA_KH, x.MA_SP });
                    table.ForeignKey(
                        name: "FK_CHI_TIET_BINH_LUANs_San_Phams_MA_SP",
                        column: x => x.MA_SP,
                        principalTable: "San_Phams",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHI_TIET_BINH_LUANs_kHACH_HANGs_MA_KH",
                        column: x => x.MA_KH,
                        principalTable: "kHACH_HANGs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_DON_HANGs",
                columns: table => new
                {
                    MA_DH = table.Column<int>(type: "int", nullable: false),
                    MA_SP = table.Column<int>(type: "int", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    TONGTIENTUNGSANPHAM = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHI_TIET_DON_HANGs", x => new { x.MA_DH, x.MA_SP });
                    table.ForeignKey(
                        name: "FK_CHI_TIET_DON_HANGs_DON_HANGs_MA_DH",
                        column: x => x.MA_DH,
                        principalTable: "DON_HANGs",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHI_TIET_DON_HANGs_San_Phams_MA_SP",
                        column: x => x.MA_SP,
                        principalTable: "San_Phams",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_BINH_LUANs_MA_SP",
                table: "CHI_TIET_BINH_LUANs",
                column: "MA_SP");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_DON_HANGs_MA_SP",
                table: "CHI_TIET_DON_HANGs",
                column: "MA_SP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHI_TIET_BINH_LUANs");

            migrationBuilder.DropTable(
                name: "CHI_TIET_DON_HANGs");

            migrationBuilder.DropTable(
                name: "kHACH_HANGs");

            migrationBuilder.DropTable(
                name: "DON_HANGs");

            migrationBuilder.DropTable(
                name: "San_Phams");
        }
    }
}
