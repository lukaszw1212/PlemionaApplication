using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlemionaApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Armory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefensiveWalls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DefensiveValue = table.Column<int>(type: "int", nullable: false),
                    MaxDefensiveValue = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefensiveWalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expedition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ExperienceGained = table.Column<int>(type: "int", nullable: false),
                    DefensiveWallsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expedition_DefensiveWalls_DefensiveWallsId",
                        column: x => x.DefensiveWallsId,
                        principalTable: "DefensiveWalls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentHP = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    AttackSpeed = table.Column<double>(type: "float", nullable: false),
                    DamageType = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    PhysicalResistance = table.Column<int>(type: "int", nullable: false),
                    RangeResistance = table.Column<int>(type: "int", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ExpeditionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entity_Expedition_ExpeditionId",
                        column: x => x.ExpeditionId,
                        principalTable: "Expedition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    GuildMasterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fractions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CurrentExperience = table.Column<int>(type: "int", nullable: false),
                    FractionId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Fractions_FractionId",
                        column: x => x.FractionId,
                        principalTable: "Fractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Players_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ExpeditionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Expedition_ExpeditionId",
                        column: x => x.ExpeditionId,
                        principalTable: "Expedition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Resource_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Villages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villages_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrainFarm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    GenerateWheatPerTime = table.Column<int>(type: "int", nullable: false),
                    MaxFarmPerTime = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrainFarm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrainFarm_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorseStable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CurrentHorses = table.Column<int>(type: "int", nullable: false),
                    MaxHorses = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorseStable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorseStable_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IronMine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    GenerateIronPerTime = table.Column<int>(type: "int", nullable: false),
                    MaxIronPerTime = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronMine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IronMine_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sawmill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    GenerateWoodPerTime = table.Column<int>(type: "int", nullable: false),
                    MaxWoodPerTime = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sawmill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sawmill_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Silo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Silo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Silo_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoneMine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    GenerateStonePerTime = table.Column<int>(type: "int", nullable: false),
                    MaxStonePerTime = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoneMine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoneMine_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownHall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MaxBuildingLevel = table.Column<int>(type: "int", nullable: false),
                    GenerateGoldPerTime = table.Column<int>(type: "int", nullable: false),
                    MaxGoldPerTime = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownHall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TownHall_Villages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Armory_VillageId",
                table: "Armory",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Barracks_VillageId",
                table: "Barracks",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_DefensiveWalls_VillageId",
                table: "DefensiveWalls",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ExpeditionId",
                table: "Entity",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_VillageId",
                table: "Entity",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedition_DefensiveWallsId",
                table: "Expedition",
                column: "DefensiveWallsId");

            migrationBuilder.CreateIndex(
                name: "IX_Fractions_GuildMasterId",
                table: "Fractions",
                column: "GuildMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_GrainFarm_VillageId",
                table: "GrainFarm",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseStable_VillageId",
                table: "HorseStable",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_IronMine_VillageId",
                table: "IronMine",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_FractionId",
                table: "Players",
                column: "FractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ExpeditionId",
                table: "Resource",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_PlayerId",
                table: "Resource",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sawmill_VillageId",
                table: "Sawmill",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Silo_VillageId",
                table: "Silo",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_StoneMine_VillageId",
                table: "StoneMine",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_TownHall_VillageId",
                table: "TownHall",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Villages_PlayerId",
                table: "Villages",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Armory_Villages_VillageId",
                table: "Armory",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Barracks_Villages_VillageId",
                table: "Barracks",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DefensiveWalls_Villages_VillageId",
                table: "DefensiveWalls",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Villages_VillageId",
                table: "Entity",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fractions_Players_GuildMasterId",
                table: "Fractions",
                column: "GuildMasterId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fractions_Players_GuildMasterId",
                table: "Fractions");

            migrationBuilder.DropTable(
                name: "Armory");

            migrationBuilder.DropTable(
                name: "Barracks");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "GrainFarm");

            migrationBuilder.DropTable(
                name: "HorseStable");

            migrationBuilder.DropTable(
                name: "IronMine");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Sawmill");

            migrationBuilder.DropTable(
                name: "Silo");

            migrationBuilder.DropTable(
                name: "StoneMine");

            migrationBuilder.DropTable(
                name: "TownHall");

            migrationBuilder.DropTable(
                name: "Expedition");

            migrationBuilder.DropTable(
                name: "DefensiveWalls");

            migrationBuilder.DropTable(
                name: "Villages");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Fractions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
