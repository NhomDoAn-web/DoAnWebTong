﻿// <auto-generated />
using System;
using DoAnWEBDEMO.ApplicationDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoAnWEBDEMO.Migrations
{
    [DbContext(typeof(ApplicationDb))]
    partial class ApplicationDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DoAnWEBDEMO.Models.CHI_TIET_BINH_LUAN", b =>
                {
                    b.Property<int>("MA_KH")
                        .HasColumnType("int");

                    b.Property<int>("MA_SP")
                        .HasColumnType("int");

                    b.Property<DateTime>("NGAY")
                        .HasColumnType("datetime2");

                    b.Property<string>("NOI_DUNG")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SO_SAO")
                        .HasColumnType("int");

                    b.HasKey("MA_KH", "MA_SP");

                    b.HasIndex("MA_SP");

                    b.ToTable("CHI_TIET_BINH_LUAN");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.CHI_TIET_DON_HANG", b =>
                {
                    b.Property<int>("MA_DH")
                        .HasColumnType("int");

                    b.Property<int>("MA_SP")
                        .HasColumnType("int");

                    b.Property<int>("SOLUONG")
                        .HasColumnType("int");

                    b.Property<decimal>("TONGTIENTUNGSANPHAM")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MA_DH", "MA_SP");

                    b.HasIndex("MA_SP");

                    b.ToTable("CHI_TIET_DON_HANG");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.DanhMuc", b =>
                {
                    b.Property<int>("Ma_DM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ma_DM"));

                    b.Property<string>("TenDM")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Trang_Thai")
                        .HasColumnType("int");

                    b.HasKey("Ma_DM");

                    b.ToTable("DanhMuc");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.DonHang", b =>
                {
                    b.Property<int>("MaDH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDH"));

                    b.Property<string>("DiaChiNhanHang")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MaKH")
                        .HasColumnType("int");

                    b.Property<int>("MaNVXL")
                        .HasColumnType("int");

                    b.Property<string>("NgayDatHang")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("TongTienDonHang")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaDH");

                    b.HasIndex("MaKH");

                    b.HasIndex("MaNVXL");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.KhachHang", b =>
                {
                    b.Property<int>("MaKH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKH"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HoKH")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MATKHAU")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TENNGUOIDUNG")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenKH")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaKH");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.LienHe", b =>
                {
                    b.Property<int>("MA_LH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MA_LH"));

                    b.Property<string>("EMAIL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HO_TEN")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("MA_NVXL")
                        .HasColumnType("int");

                    b.Property<string>("NOI_DUNG")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("THOI_GIAN_GUI")
                        .HasColumnType("datetime2");

                    b.Property<int>("TRANG_THAI")
                        .HasColumnType("int");

                    b.HasKey("MA_LH");

                    b.HasIndex("MA_NVXL");

                    b.ToTable("LienHe");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.NhaCungCap", b =>
                {
                    b.Property<int>("MaNCC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNCC"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TenNCC")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaNCC");

                    b.ToTable("NhaCungCap");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.NhanVien", b =>
                {
                    b.Property<int>("MA_NV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MA_NV"));

                    b.Property<string>("EMAIL")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MATKHAU")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TENDANGNHAP")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TENNV")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TRANGTHAI")
                        .HasColumnType("int");

                    b.HasKey("MA_NV");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.SanPham", b =>
                {
                    b.Property<int>("MaSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSP"));

                    b.Property<string>("Bo_Nho_Trong")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CameraSau")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Camera_Truoc")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Chipset")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Dung_Luong_Ram")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gpu")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HeDieuHanh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HinhAnhSP")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KichThuoc_ManHinh")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MaDanhMuc")
                        .HasColumnType("int");

                    b.Property<int>("MaNCC")
                        .HasColumnType("int");

                    b.Property<string>("MauSP")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MoTa")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TEN_SP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TGianBaoHanh")
                        .HasColumnType("int");

                    b.Property<string>("TanSoQuet")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaSP");

                    b.HasIndex("MaDanhMuc");

                    b.HasIndex("MaNCC");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.CHI_TIET_BINH_LUAN", b =>
                {
                    b.HasOne("DoAnWEBDEMO.Models.KhachHang", "KhachHang")
                        .WithMany("ChiTietBinhLuans")
                        .HasForeignKey("MA_KH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAnWEBDEMO.Models.SanPham", "SanPham")
                        .WithMany("ChiTietBinhLuans")
                        .HasForeignKey("MA_SP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.CHI_TIET_DON_HANG", b =>
                {
                    b.HasOne("DoAnWEBDEMO.Models.DonHang", "DonHang")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("MA_DH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAnWEBDEMO.Models.SanPham", "SanPham")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("MA_SP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.DonHang", b =>
                {
                    b.HasOne("DoAnWEBDEMO.Models.KhachHang", "KhachHang")
                        .WithMany("DonHang")
                        .HasForeignKey("MaKH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAnWEBDEMO.Models.NhanVien", "NhanVien")
                        .WithMany("DonHang")
                        .HasForeignKey("MaNVXL")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.LienHe", b =>
                {
                    b.HasOne("DoAnWEBDEMO.Models.NhanVien", "NhanVien")
                        .WithMany("LienHe")
                        .HasForeignKey("MA_NVXL")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.SanPham", b =>
                {
                    b.HasOne("DoAnWEBDEMO.Models.DanhMuc", "DanhMuc")
                        .WithMany("SanPham")
                        .HasForeignKey("MaDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAnWEBDEMO.Models.NhaCungCap", "NhaCungCap")
                        .WithMany("SanPham")
                        .HasForeignKey("MaNCC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DanhMuc");

                    b.Navigation("NhaCungCap");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.DanhMuc", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.DonHang", b =>
                {
                    b.Navigation("ChiTietDonHangs");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.KhachHang", b =>
                {
                    b.Navigation("ChiTietBinhLuans");

                    b.Navigation("DonHang");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.NhaCungCap", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.NhanVien", b =>
                {
                    b.Navigation("DonHang");

                    b.Navigation("LienHe");
                });

            modelBuilder.Entity("DoAnWEBDEMO.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietBinhLuans");

                    b.Navigation("ChiTietDonHangs");
                });
#pragma warning restore 612, 618
        }
    }
}