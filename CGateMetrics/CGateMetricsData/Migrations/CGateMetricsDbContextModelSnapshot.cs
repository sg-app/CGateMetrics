﻿// <auto-generated />
using System;
using CGateMetricsData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CGateMetricsData.Migrations
{
    [DbContext(typeof(CGateMetricsDbContext))]
    partial class CGateMetricsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CGateMetricsData.Models.Buchung", b =>
                {
                    b.Property<int>("BuchungsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuchungsId"));

                    b.Property<string>("AusweisId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FahrerAusweisId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Fahrgestellnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FahrzeugFahrgestellnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gefahrgut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GewichtIn")
                        .HasColumnType("int");

                    b.Property<int?>("GewichtOut")
                        .HasColumnType("int");

                    b.Property<string>("Standort")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UhrzeitIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UhrzeitOut")
                        .HasColumnType("datetime2");

                    b.HasKey("BuchungsId");

                    b.HasIndex("FahrerAusweisId");

                    b.HasIndex("FahrzeugFahrgestellnummer");

                    b.ToTable("Buchungen");
                });

            modelBuilder.Entity("CGateMetricsData.Models.Fahrer", b =>
                {
                    b.Property<string>("AusweisId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Anrede")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Firma")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Geburtsort")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Geburtstag")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AusweisId");

                    b.ToTable("Fahrer");
                });

            modelBuilder.Entity("CGateMetricsData.Models.Fahrzeug", b =>
                {
                    b.Property<string>("Fahrgestellnummer")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Hersteller")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Kennzeichen")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ZulGesamtGewicht")
                        .HasColumnType("int");

                    b.HasKey("Fahrgestellnummer");

                    b.ToTable("Fahrzeuge");
                });

            modelBuilder.Entity("CGateMetricsData.Models.Buchung", b =>
                {
                    b.HasOne("CGateMetricsData.Models.Fahrer", "Fahrer")
                        .WithMany()
                        .HasForeignKey("FahrerAusweisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CGateMetricsData.Models.Fahrzeug", "Fahrzeug")
                        .WithMany()
                        .HasForeignKey("FahrzeugFahrgestellnummer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fahrer");

                    b.Navigation("Fahrzeug");
                });
#pragma warning restore 612, 618
        }
    }
}
