﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI_HoaDon.Entities;

#nullable disable

namespace WebAPI_HoaDon.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240701075755_minhv2")]
    partial class minhv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPI_HoaDon.Entities.ChiTietHoaDon", b =>
                {
                    b.Property<int>("ChiTietHoaDonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChiTietHoaDonId"), 1L, 1);

                    b.Property<string>("DVT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoaDonId")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<double?>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("ChiTietHoaDonId");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("ChiTietHoaDons");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.HoaDon", b =>
                {
                    b.Property<int>("HoaDonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HoaDonId"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KhachHangId")
                        .HasColumnType("int");

                    b.Property<string>("MaGiaoDich")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHoaDon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ThoiGianCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.Property<double?>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("HoaDonId");

                    b.HasIndex("KhachHangId");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.KhachHang", b =>
                {
                    b.Property<int>("KhachHangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KhachHangId"), 1L, 1);

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KhachHangId");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.LoaiSanPham", b =>
                {
                    b.Property<int>("LoaiSanPhamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoaiSanPhamId"), 1L, 1);

                    b.Property<string>("TenLoaiSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoaiSanPhamId");

                    b.ToTable("LoaiSanPhams");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.SanPham", b =>
                {
                    b.Property<int>("SanPhamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SanPhamId"), 1L, 1);

                    b.Property<double>("GiaThanh")
                        .HasColumnType("float");

                    b.Property<string>("KiHieuSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoaiSanPhamId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SanPhamId");

                    b.HasIndex("LoaiSanPhamId");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.ChiTietHoaDon", b =>
                {
                    b.HasOne("WebAPI_HoaDon.Entities.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI_HoaDon.Entities.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.HoaDon", b =>
                {
                    b.HasOne("WebAPI_HoaDon.Entities.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("KhachHangId");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.SanPham", b =>
                {
                    b.HasOne("WebAPI_HoaDon.Entities.LoaiSanPham", "LoaiSanPham")
                        .WithMany()
                        .HasForeignKey("LoaiSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiSanPham");
                });

            modelBuilder.Entity("WebAPI_HoaDon.Entities.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}