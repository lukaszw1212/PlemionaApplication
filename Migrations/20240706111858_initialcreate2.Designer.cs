﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlemionaApplication.Data;

#nullable disable

namespace PlemionaApplication.Migrations
{
    [DbContext(typeof(PlemionaApplicationContext))]
    [Migration("20240706111858_initialcreate2")]
    partial class initialcreate2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniProjekt.Armory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("Armory");
                });

            modelBuilder.Entity("MiniProjekt.Barracks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("Barracks");
                });

            modelBuilder.Entity("MiniProjekt.DefensiveWalls", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DefensiveValue")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxDefensiveValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("DefensiveWalls");
                });

            modelBuilder.Entity("MiniProjekt.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("AttackSpeed")
                        .HasColumnType("float");

                    b.Property<int>("CurrentHP")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("DamageType")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int?>("ExpeditionId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxHP")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhysicalResistance")
                        .HasColumnType("int");

                    b.Property<int>("RangeResistance")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExpeditionId");

                    b.HasIndex("VillageId");

                    b.ToTable("Entity");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Entity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MiniProjekt.Expedition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DefensiveWallsId")
                        .HasColumnType("int");

                    b.Property<int>("ExperienceGained")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DefensiveWallsId");

                    b.ToTable("Expedition");
                });

            modelBuilder.Entity("MiniProjekt.Fraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GuildMasterId")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuildMasterId");

                    b.ToTable("Fractions");
                });

            modelBuilder.Entity("MiniProjekt.GrainFarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenerateWheatPerTime")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxFarmPerTime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("GrainFarm");
                });

            modelBuilder.Entity("MiniProjekt.HorseStable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentHorses")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxHorses")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("HorseStable");
                });

            modelBuilder.Entity("MiniProjekt.IronMine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenerateIronPerTime")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxIronPerTime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("IronMine");
                });

            modelBuilder.Entity("MiniProjekt.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentExperience")
                        .HasColumnType("int");

                    b.Property<int?>("FractionId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FractionId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MiniProjekt.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("ExpeditionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExpeditionId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("MiniProjekt.Sawmill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenerateWoodPerTime")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxWoodPerTime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("Sawmill");
                });

            modelBuilder.Entity("MiniProjekt.Silo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("Silo");
                });

            modelBuilder.Entity("MiniProjekt.StoneMine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenerateStonePerTime")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxStonePerTime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("StoneMine");
                });

            modelBuilder.Entity("MiniProjekt.TownHall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenerateGoldPerTime")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxBuildingLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaxGoldPerTime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("TownHall");
                });

            modelBuilder.Entity("MiniProjekt.Village", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Villages");
                });

            modelBuilder.Entity("PlemionaApplication.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("PlemionaApplication.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MiniProjekt.Archer", b =>
                {
                    b.HasBaseType("MiniProjekt.Entity");

                    b.HasDiscriminator().HasValue("Archer");
                });

            modelBuilder.Entity("MiniProjekt.Hussar", b =>
                {
                    b.HasBaseType("MiniProjekt.Entity");

                    b.HasDiscriminator().HasValue("Hussar");
                });

            modelBuilder.Entity("MiniProjekt.Kamikadze", b =>
                {
                    b.HasBaseType("MiniProjekt.Entity");

                    b.HasDiscriminator().HasValue("Kamikadze");
                });

            modelBuilder.Entity("MiniProjekt.Trojan", b =>
                {
                    b.HasBaseType("MiniProjekt.Entity");

                    b.HasDiscriminator().HasValue("Trojan");
                });

            modelBuilder.Entity("MiniProjekt.Warrior", b =>
                {
                    b.HasBaseType("MiniProjekt.Entity");

                    b.HasDiscriminator().HasValue("Warrior");
                });

            modelBuilder.Entity("PlemionaApplication.Models.Building.Catapult", b =>
                {
                    b.HasBaseType("MiniProjekt.Entity");

                    b.HasDiscriminator().HasValue("Catapult");
                });

            modelBuilder.Entity("MiniProjekt.Armory", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.Barracks", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.DefensiveWalls", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.Entity", b =>
                {
                    b.HasOne("MiniProjekt.Expedition", null)
                        .WithMany("Entities")
                        .HasForeignKey("ExpeditionId");

                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.Expedition", b =>
                {
                    b.HasOne("MiniProjekt.DefensiveWalls", "DefensiveWalls")
                        .WithMany()
                        .HasForeignKey("DefensiveWallsId");

                    b.Navigation("DefensiveWalls");
                });

            modelBuilder.Entity("MiniProjekt.Fraction", b =>
                {
                    b.HasOne("MiniProjekt.Player", "GuildMaster")
                        .WithMany()
                        .HasForeignKey("GuildMasterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("GuildMaster");
                });

            modelBuilder.Entity("MiniProjekt.GrainFarm", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.HorseStable", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.IronMine", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.Player", b =>
                {
                    b.HasOne("MiniProjekt.Fraction", "Fraction")
                        .WithMany("Players")
                        .HasForeignKey("FractionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Fraction");
                });

            modelBuilder.Entity("MiniProjekt.Resource", b =>
                {
                    b.HasOne("MiniProjekt.Expedition", null)
                        .WithMany("Resources")
                        .HasForeignKey("ExpeditionId");

                    b.HasOne("MiniProjekt.Player", "Player")
                        .WithMany("Resources")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MiniProjekt.Sawmill", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.Silo", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.StoneMine", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.TownHall", b =>
                {
                    b.HasOne("MiniProjekt.Village", "Village")
                        .WithMany()
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("MiniProjekt.Village", b =>
                {
                    b.HasOne("MiniProjekt.Player", "Player")
                        .WithMany("Villages")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("PlemionaApplication.Entities.User", b =>
                {
                    b.HasOne("PlemionaApplication.Entities.Role", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("MiniProjekt.Expedition", b =>
                {
                    b.Navigation("Entities");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("MiniProjekt.Fraction", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("MiniProjekt.Player", b =>
                {
                    b.Navigation("Resources");

                    b.Navigation("Villages");
                });

            modelBuilder.Entity("PlemionaApplication.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
