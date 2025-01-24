using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Address_Street = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    Address_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Address_Country = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    Address_Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Address_Longitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rule = table.Column<string>(type: "text", nullable: true),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    Movement = table.Column<int>(type: "integer", nullable: false),
                    Range = table.Column<decimal>(type: "numeric", nullable: false),
                    Accuracy = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BattleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleGroups_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rule = table.Column<string>(type: "text", nullable: true),
                    Attack = table.Column<int>(type: "integer", nullable: true),
                    Points = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleGroupUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    BattleGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rule = table.Column<string>(type: "text", nullable: true),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    Movement = table.Column<int>(type: "integer", nullable: false),
                    Range = table.Column<decimal>(type: "numeric", nullable: false),
                    Accuracy = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleGroupUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleGroupUnits_BattleGroups_BattleGroupId",
                        column: x => x.BattleGroupId,
                        principalTable: "BattleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleGroupUnitEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    BattleGroupUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rule = table.Column<string>(type: "text", nullable: true),
                    Attack = table.Column<int>(type: "integer", nullable: true),
                    Points = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleGroupUnitEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleGroupUnitEquipments_BattleGroupUnits_BattleGroupUnitId",
                        column: x => x.BattleGroupUnitId,
                        principalTable: "BattleGroupUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "BattleGroups",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleGroupUnitEquipment_EquipmentId",
                table: "BattleGroupUnitEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleGroupUnitEquipments_BattleGroupUnitId",
                table: "BattleGroupUnitEquipments",
                column: "BattleGroupUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleGroupUnit_UnitId",
                table: "BattleGroupUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleGroupUnits_BattleGroupId",
                table: "BattleGroupUnits",
                column: "BattleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitId",
                table: "Equipments",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleGroupUnitEquipments");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "BattleGroupUnits");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "BattleGroups");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
