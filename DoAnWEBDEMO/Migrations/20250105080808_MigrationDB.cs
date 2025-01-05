using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnWEBDEMO.Migrations
{
    /// <inheritdoc />
    public partial class MigrationDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Ma_DM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Trang_Thai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Ma_DM);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TENNGUOIDUNG = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MATKHAU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNCC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MA_NV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENNV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TENDANGNHAP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MATKHAU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TRANGTHAI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MA_NV);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNCC = table.Column<int>(type: "int", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TEN_SP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnhSP = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    MauSP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TGianBaoHanh = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KichThuoc_ManHinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CameraSau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Camera_Truoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Chipset = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gpu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Dung_Luong_Ram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bo_Nho_Trong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HeDieuHanh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TanSoQuet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "Ma_DM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_NhaCungCap_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaNVXL = table.Column<int>(type: "int", nullable: false),
                    NgayDatHang = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TongTienDonHang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    DiaChiNhanHang = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDH);
                    table.ForeignKey(
                        name: "FK_DonHang_KhachHang_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHang_NhanVien_MaNVXL",
                        column: x => x.MaNVXL,
                        principalTable: "NhanVien",
                        principalColumn: "MA_NV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LienHe",
                columns: table => new
                {
                    MA_LH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_NVXL = table.Column<int>(type: "int", nullable: false),
                    HO_TEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    NOI_DUNG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    THOI_GIAN_GUI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TRANG_THAI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LienHe", x => x.MA_LH);
                    table.ForeignKey(
                        name: "FK_LienHe_NhanVien_MA_NVXL",
                        column: x => x.MA_NVXL,
                        principalTable: "NhanVien",
                        principalColumn: "MA_NV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBinhLuan",
                columns: table => new
                {
                    MA_SP = table.Column<int>(type: "int", nullable: false),
                    MA_KH = table.Column<int>(type: "int", nullable: false),
                    SO_SAO = table.Column<int>(type: "int", nullable: false),
                    NOI_DUNG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NGAY = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBinhLuan", x => new { x.MA_KH, x.MA_SP });
                    table.ForeignKey(
                        name: "FK_ChiTietBinhLuan_KhachHang_MA_KH",
                        column: x => x.MA_KH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietBinhLuan_SanPham_MA_SP",
                        column: x => x.MA_SP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHang", x => new { x.MaKH, x.MaSP });
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_KhachHang_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_SanPham_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamKhuyenMaiId = table.Column<int>(type: "int", nullable: false),
                    MucGiamGia = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhuyenMai_SanPham_SanPhamKhuyenMaiId",
                        column: x => x.SanPhamKhuyenMaiId,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    ID_MauSac = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMauSac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HinhAnhSP_MauSac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    SoLuongTon_MS = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.ID_MauSac);
                    table.ForeignKey(
                        name: "FK_MauSac_SanPham_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamYeuThich",
                columns: table => new
                {
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamYeuThich", x => new { x.KhachHangId, x.SanPhamId });
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThich_KhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThich_SanPham_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MA_DH = table.Column<int>(type: "int", nullable: false),
                    MA_SP = table.Column<int>(type: "int", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    TONGTIENTUNGSANPHAM = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => new { x.MA_DH, x.MA_SP });
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_MA_DH",
                        column: x => x.MA_DH,
                        principalTable: "DonHang",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_SanPham_MA_SP",
                        column: x => x.MA_SP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBinhLuan_MA_SP",
                table: "ChiTietBinhLuan",
                column: "MA_SP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MA_SP",
                table: "ChiTietDonHang",
                column: "MA_SP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaSP",
                table: "ChiTietGioHang",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKH",
                table: "DonHang",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaNVXL",
                table: "DonHang",
                column: "MaNVXL");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMai_SanPhamKhuyenMaiId",
                table: "KhuyenMai",
                column: "SanPhamKhuyenMaiId");

            migrationBuilder.CreateIndex(
                name: "IX_LienHe_MA_NVXL",
                table: "LienHe",
                column: "MA_NVXL");

            migrationBuilder.CreateIndex(
                name: "IX_MauSac_MaSP",
                table: "MauSac",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaDanhMuc",
                table: "SanPham",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNCC",
                table: "SanPham",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThich_SanPhamId",
                table: "SanPhamYeuThich",
                column: "SanPhamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBinhLuan");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "LienHe");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPhamYeuThich");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "NhaCungCap");
        }
    }
}
