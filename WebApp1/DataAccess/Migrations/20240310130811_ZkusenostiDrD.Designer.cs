﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp1.DataAccess;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    [DbContext(typeof(LitTextyDbContext))]
    [Migration("20240310130811_ZkusenostiDrD")]
    partial class ZkusenostiDrD
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApp1.Models.Database.HrdinaPovolani", b =>
                {
                    b.Property<int>("HrdinaId")
                        .HasColumnType("int")
                        .HasColumnName("HRDINA_ID");

                    b.Property<int>("PovolaniId")
                        .HasColumnType("int")
                        .HasColumnName("POVOLANI_ID");

                    b.Property<int>("Level")
                        .HasColumnType("int")
                        .HasColumnName("LEVEL");

                    b.HasKey("HrdinaId", "PovolaniId");

                    b.HasIndex("PovolaniId");

                    b.ToTable("HRDINA_POVOLANI");
                });

            modelBuilder.Entity("WebApp1.Models.Database.HrdinaSchopnost", b =>
                {
                    b.Property<int>("HrdinaId")
                        .HasColumnType("int")
                        .HasColumnName("HRDINA_ID");

                    b.Property<int>("SchopnostId")
                        .HasColumnType("int")
                        .HasColumnName("SCHOPNOST_ID");

                    b.HasKey("HrdinaId", "SchopnostId");

                    b.HasIndex("SchopnostId");

                    b.ToTable("HRDINA_SCHOPNOST");
                });

            modelBuilder.Entity("WebApp1.Models.Database.Povolani", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("ID");

                    b.ToTable("POVOLANI");
                });

            modelBuilder.Entity("WebApp1.Models.Doba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CelyNazev")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CELY_NAZEV");

                    b.Property<string>("DobaDef")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DOBA_POPIS");

                    b.HasKey("Id");

                    b.ToTable("DOBA");
                });

            modelBuilder.Entity("WebApp1.Models.Hrdina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Duse")
                        .HasColumnType("int")
                        .HasColumnName("DUSE");

                    b.Property<int>("DuseJizva")
                        .HasColumnType("int")
                        .HasColumnName("DUSE_JIZVA");

                    b.Property<int>("DuseLimit")
                        .HasColumnType("int")
                        .HasColumnName("DUSE_LIMIT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("NAME");

                    b.Property<int>("Ohrozeni")
                        .HasColumnType("int")
                        .HasColumnName("OHROZENI");

                    b.Property<decimal>("Penize")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PENIZE");

                    b.Property<string>("Rasa")
                        .HasColumnType("NVARCHAR(10)")
                        .HasColumnName("RASA");

                    b.Property<int>("Suroviny")
                        .HasColumnType("int")
                        .HasColumnName("SUROVINY");

                    b.Property<int>("Telo")
                        .HasColumnType("int")
                        .HasColumnName("TELO");

                    b.Property<int>("TeloJizva")
                        .HasColumnType("int")
                        .HasColumnName("TELO_JIZVA");

                    b.Property<int>("TeloLimit")
                        .HasColumnType("int")
                        .HasColumnName("TELO_LIMIT");

                    b.Property<int>("Vliv")
                        .HasColumnType("int")
                        .HasColumnName("VLIV");

                    b.Property<int>("VlivJizva")
                        .HasColumnType("int")
                        .HasColumnName("VLIV_JIZVA");

                    b.Property<int>("VlivLimit")
                        .HasColumnType("int")
                        .HasColumnName("VLIV_LIMIT");

                    b.Property<string>("Vybaveni")
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("VYBAVENI");

                    b.Property<int>("Vyhoda")
                        .HasColumnType("int")
                        .HasColumnName("VYHODA");

                    b.Property<string>("Zbrane")
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("ZBRANE");

                    b.Property<int>("Zkusenosti")
                        .HasColumnType("int")
                        .HasColumnName("ZKUSENOSTI");

                    b.HasKey("ID");

                    b.ToTable("HRDINA");
                });

            modelBuilder.Entity("WebApp1.Models.LitText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cyklus")
                        .HasColumnType("NVARCHAR(1)")
                        .HasColumnName("CYKLUS");

                    b.Property<string>("Nazev_dne")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("NAZEV_DNE");

                    b.Property<int?>("SvateId")
                        .HasColumnType("int")
                        .HasColumnName("SVATEK_ID");

                    b.Property<string>("Text")
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SvateId");

                    b.ToTable("LIT_TEXT");
                });

            modelBuilder.Entity("WebApp1.Models.Schopnost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(500)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(20)")
                        .HasColumnName("NAME");

                    b.Property<int>("PovolaniId")
                        .HasColumnType("int")
                        .HasColumnName("POVOLANI_ID");

                    b.Property<string>("Pravidlo")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("PRAVIDLO");

                    b.Property<string>("Vlastnost")
                        .HasColumnType("NVARCHAR(5)")
                        .HasColumnName("VLASTNOST");

                    b.HasKey("ID");

                    b.HasIndex("PovolaniId");

                    b.ToTable("SCHOPNOST");
                });

            modelBuilder.Entity("WebApp1.Models.Svatek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Barva")
                        .HasColumnType("decimal(10:0)")
                        .HasColumnName("BARVA");

                    b.Property<string>("Cely_nazev")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("CELY_NAZEV");

                    b.Property<string>("Cteni")
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("CTENI");

                    b.Property<bool?>("Cykly")
                        .HasColumnType("bit")
                        .HasColumnName("CYKLY");

                    b.Property<int?>("DobaId")
                        .HasColumnType("int")
                        .HasColumnName("DOBA_ID");

                    b.Property<string>("Nazev_dne")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("NAZEV_DNE");

                    b.HasKey("Id");

                    b.HasIndex("DobaId");

                    b.ToTable("SVATEK");
                });

            modelBuilder.Entity("WebApp1.Models.Database.HrdinaPovolani", b =>
                {
                    b.HasOne("WebApp1.Models.Hrdina", "Hrdina")
                        .WithMany()
                        .HasForeignKey("HrdinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp1.Models.Database.Povolani", "Povolani")
                        .WithMany()
                        .HasForeignKey("PovolaniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hrdina");

                    b.Navigation("Povolani");
                });

            modelBuilder.Entity("WebApp1.Models.Database.HrdinaSchopnost", b =>
                {
                    b.HasOne("WebApp1.Models.Hrdina", "Hrdina")
                        .WithMany()
                        .HasForeignKey("HrdinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp1.Models.Schopnost", "Schopnost")
                        .WithMany()
                        .HasForeignKey("SchopnostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hrdina");

                    b.Navigation("Schopnost");
                });

            modelBuilder.Entity("WebApp1.Models.LitText", b =>
                {
                    b.HasOne("WebApp1.Models.Svatek", "Svatek")
                        .WithMany()
                        .HasForeignKey("SvateId");

                    b.Navigation("Svatek");
                });

            modelBuilder.Entity("WebApp1.Models.Schopnost", b =>
                {
                    b.HasOne("WebApp1.Models.Database.Povolani", "Povolani")
                        .WithMany()
                        .HasForeignKey("PovolaniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Povolani");
                });

            modelBuilder.Entity("WebApp1.Models.Svatek", b =>
                {
                    b.HasOne("WebApp1.Models.Doba", "Doba")
                        .WithMany()
                        .HasForeignKey("DobaId");

                    b.Navigation("Doba");
                });
#pragma warning restore 612, 618
        }
    }
}
