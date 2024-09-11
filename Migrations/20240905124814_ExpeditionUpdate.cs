using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlemionaApplication.Migrations
{
    /// <inheritdoc />
    public partial class ExpeditionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armory_Villages_VillageId",
                table: "Armory");

            migrationBuilder.DropForeignKey(
                name: "FK_Barracks_Villages_VillageId",
                table: "Barracks");

            migrationBuilder.DropForeignKey(
                name: "FK_DefensiveWalls_Villages_VillageId",
                table: "DefensiveWalls");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Villages_VillageId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedition_DefensiveWalls_DefensiveWallsId",
                table: "Expedition");

            migrationBuilder.DropForeignKey(
                name: "FK_GrainFarm_Villages_VillageId",
                table: "GrainFarm");

            migrationBuilder.DropForeignKey(
                name: "FK_HorseStable_Villages_VillageId",
                table: "HorseStable");

            migrationBuilder.DropForeignKey(
                name: "FK_IronMine_Villages_VillageId",
                table: "IronMine");

            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Players_PlayerId",
                table: "Resource");

            migrationBuilder.DropForeignKey(
                name: "FK_Sawmill_Villages_VillageId",
                table: "Sawmill");

            migrationBuilder.DropForeignKey(
                name: "FK_Silo_Villages_VillageId",
                table: "Silo");

            migrationBuilder.DropForeignKey(
                name: "FK_StoneMine_Villages_VillageId",
                table: "StoneMine");

            migrationBuilder.DropForeignKey(
                name: "FK_TownHall_Villages_VillageId",
                table: "TownHall");

            migrationBuilder.DropIndex(
                name: "IX_Expedition_DefensiveWallsId",
                table: "Expedition");

            migrationBuilder.DropColumn(
                name: "DefensiveWallsId",
                table: "Expedition");

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "TownHall",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "TownHall",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "StoneMine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "StoneMine",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Silo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "Silo",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Sawmill",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "Sawmill",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Resource",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "IronMine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "IronMine",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "HorseStable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "HorseStable",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "GrainFarm",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "GrainFarm",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cooldown",
                table: "Expedition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Entity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "DefensiveWalls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "DefensiveWalls",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Barracks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "Barracks",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Armory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExpeditionId",
                table: "Armory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TownHall_ExpeditionId",
                table: "TownHall",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_StoneMine_ExpeditionId",
                table: "StoneMine",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Silo_ExpeditionId",
                table: "Silo",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sawmill_ExpeditionId",
                table: "Sawmill",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_IronMine_ExpeditionId",
                table: "IronMine",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseStable_ExpeditionId",
                table: "HorseStable",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_GrainFarm_ExpeditionId",
                table: "GrainFarm",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_DefensiveWalls_ExpeditionId",
                table: "DefensiveWalls",
                column: "ExpeditionId",
                unique: true,
                filter: "[ExpeditionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Barracks_ExpeditionId",
                table: "Barracks",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Armory_ExpeditionId",
                table: "Armory",
                column: "ExpeditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Armory_Expedition_ExpeditionId",
                table: "Armory",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Armory_Villages_VillageId",
                table: "Armory",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Barracks_Expedition_ExpeditionId",
                table: "Barracks",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Barracks_Villages_VillageId",
                table: "Barracks",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DefensiveWalls_Expedition_ExpeditionId",
                table: "DefensiveWalls",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DefensiveWalls_Villages_VillageId",
                table: "DefensiveWalls",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Villages_VillageId",
                table: "Entity",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GrainFarm_Expedition_ExpeditionId",
                table: "GrainFarm",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GrainFarm_Villages_VillageId",
                table: "GrainFarm",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HorseStable_Expedition_ExpeditionId",
                table: "HorseStable",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HorseStable_Villages_VillageId",
                table: "HorseStable",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IronMine_Expedition_ExpeditionId",
                table: "IronMine",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IronMine_Villages_VillageId",
                table: "IronMine",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Players_PlayerId",
                table: "Resource",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sawmill_Expedition_ExpeditionId",
                table: "Sawmill",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sawmill_Villages_VillageId",
                table: "Sawmill",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Silo_Expedition_ExpeditionId",
                table: "Silo",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Silo_Villages_VillageId",
                table: "Silo",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoneMine_Expedition_ExpeditionId",
                table: "StoneMine",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoneMine_Villages_VillageId",
                table: "StoneMine",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TownHall_Expedition_ExpeditionId",
                table: "TownHall",
                column: "ExpeditionId",
                principalTable: "Expedition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TownHall_Villages_VillageId",
                table: "TownHall",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armory_Expedition_ExpeditionId",
                table: "Armory");

            migrationBuilder.DropForeignKey(
                name: "FK_Armory_Villages_VillageId",
                table: "Armory");

            migrationBuilder.DropForeignKey(
                name: "FK_Barracks_Expedition_ExpeditionId",
                table: "Barracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Barracks_Villages_VillageId",
                table: "Barracks");

            migrationBuilder.DropForeignKey(
                name: "FK_DefensiveWalls_Expedition_ExpeditionId",
                table: "DefensiveWalls");

            migrationBuilder.DropForeignKey(
                name: "FK_DefensiveWalls_Villages_VillageId",
                table: "DefensiveWalls");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Villages_VillageId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_GrainFarm_Expedition_ExpeditionId",
                table: "GrainFarm");

            migrationBuilder.DropForeignKey(
                name: "FK_GrainFarm_Villages_VillageId",
                table: "GrainFarm");

            migrationBuilder.DropForeignKey(
                name: "FK_HorseStable_Expedition_ExpeditionId",
                table: "HorseStable");

            migrationBuilder.DropForeignKey(
                name: "FK_HorseStable_Villages_VillageId",
                table: "HorseStable");

            migrationBuilder.DropForeignKey(
                name: "FK_IronMine_Expedition_ExpeditionId",
                table: "IronMine");

            migrationBuilder.DropForeignKey(
                name: "FK_IronMine_Villages_VillageId",
                table: "IronMine");

            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Players_PlayerId",
                table: "Resource");

            migrationBuilder.DropForeignKey(
                name: "FK_Sawmill_Expedition_ExpeditionId",
                table: "Sawmill");

            migrationBuilder.DropForeignKey(
                name: "FK_Sawmill_Villages_VillageId",
                table: "Sawmill");

            migrationBuilder.DropForeignKey(
                name: "FK_Silo_Expedition_ExpeditionId",
                table: "Silo");

            migrationBuilder.DropForeignKey(
                name: "FK_Silo_Villages_VillageId",
                table: "Silo");

            migrationBuilder.DropForeignKey(
                name: "FK_StoneMine_Expedition_ExpeditionId",
                table: "StoneMine");

            migrationBuilder.DropForeignKey(
                name: "FK_StoneMine_Villages_VillageId",
                table: "StoneMine");

            migrationBuilder.DropForeignKey(
                name: "FK_TownHall_Expedition_ExpeditionId",
                table: "TownHall");

            migrationBuilder.DropForeignKey(
                name: "FK_TownHall_Villages_VillageId",
                table: "TownHall");

            migrationBuilder.DropIndex(
                name: "IX_TownHall_ExpeditionId",
                table: "TownHall");

            migrationBuilder.DropIndex(
                name: "IX_StoneMine_ExpeditionId",
                table: "StoneMine");

            migrationBuilder.DropIndex(
                name: "IX_Silo_ExpeditionId",
                table: "Silo");

            migrationBuilder.DropIndex(
                name: "IX_Sawmill_ExpeditionId",
                table: "Sawmill");

            migrationBuilder.DropIndex(
                name: "IX_IronMine_ExpeditionId",
                table: "IronMine");

            migrationBuilder.DropIndex(
                name: "IX_HorseStable_ExpeditionId",
                table: "HorseStable");

            migrationBuilder.DropIndex(
                name: "IX_GrainFarm_ExpeditionId",
                table: "GrainFarm");

            migrationBuilder.DropIndex(
                name: "IX_DefensiveWalls_ExpeditionId",
                table: "DefensiveWalls");

            migrationBuilder.DropIndex(
                name: "IX_Barracks_ExpeditionId",
                table: "Barracks");

            migrationBuilder.DropIndex(
                name: "IX_Armory_ExpeditionId",
                table: "Armory");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "TownHall");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "StoneMine");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "Silo");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "Sawmill");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "IronMine");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "HorseStable");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "GrainFarm");

            migrationBuilder.DropColumn(
                name: "Cooldown",
                table: "Expedition");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "DefensiveWalls");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "Barracks");

            migrationBuilder.DropColumn(
                name: "ExpeditionId",
                table: "Armory");

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "TownHall",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "StoneMine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Silo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Sawmill",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Resource",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "IronMine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "HorseStable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "GrainFarm",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefensiveWallsId",
                table: "Expedition",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Entity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "DefensiveWalls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Barracks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Armory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedition_DefensiveWallsId",
                table: "Expedition",
                column: "DefensiveWallsId");

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
                name: "FK_Expedition_DefensiveWalls_DefensiveWallsId",
                table: "Expedition",
                column: "DefensiveWallsId",
                principalTable: "DefensiveWalls",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GrainFarm_Villages_VillageId",
                table: "GrainFarm",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HorseStable_Villages_VillageId",
                table: "HorseStable",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IronMine_Villages_VillageId",
                table: "IronMine",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Players_PlayerId",
                table: "Resource",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sawmill_Villages_VillageId",
                table: "Sawmill",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Silo_Villages_VillageId",
                table: "Silo",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoneMine_Villages_VillageId",
                table: "StoneMine",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownHall_Villages_VillageId",
                table: "TownHall",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
