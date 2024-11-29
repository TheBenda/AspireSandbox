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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShipmentAddress_Street = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    ShipmentAddress_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShipmentAddress_State = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    ShipmentAddress_Country = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    ShipmentAddress_ZipCode = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    ShipmentAddress_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    ShipmentAddress_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    OrderFulfilled = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OrderPayed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryStated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryCompleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
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
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderToppingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ToppingId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderToppingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderToppingItems_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitId",
                table: "Equipments",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderToppingItem_ToppingId",
                table: "OrderToppingItems",
                column: "ToppingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderToppingItems_OrderItemId",
                table: "OrderToppingItems",
                column: "OrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "OrderToppingItems");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
