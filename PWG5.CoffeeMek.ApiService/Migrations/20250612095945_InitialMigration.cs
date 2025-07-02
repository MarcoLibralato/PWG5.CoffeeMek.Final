using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWG5.CoffeeMek.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "coffee_mek");

            migrationBuilder.CreateSequence(
                name: "AssemblyLineLogSequence",
                schema: "coffee_mek");

            migrationBuilder.CreateSequence(
                name: "CutterCNCLogSequence",
                schema: "coffee_mek");

            migrationBuilder.CreateSequence(
                name: "LatheLogSequence",
                schema: "coffee_mek");

            migrationBuilder.CreateSequence(
                name: "TestLineLogSequence",
                schema: "coffee_mek");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "coffee_mek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "coffee_mek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyLineLogs",
                schema: "coffee_mek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [coffee_mek].[AssemblyLineLogSequence]"),
                    MeanStationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    MachineStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimestampLocal = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimestampUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsMachineBlocked = table.Column<bool>(type: "bit", nullable: false),
                    BlockDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyLineLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssemblyLineLogs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "coffee_mek",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CutterCNCLogs",
                schema: "coffee_mek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [coffee_mek].[CutterCNCLogSequence]"),
                    CycleTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CutDepth = table.Column<double>(type: "float", nullable: false),
                    Vibration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimestampLocal = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimestampUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsMachineBlocked = table.Column<bool>(type: "bit", nullable: false),
                    BlockDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CutterCNCLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CutterCNCLogs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "coffee_mek",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LatheLogs",
                schema: "coffee_mek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [coffee_mek].[LatheLogSequence]"),
                    MachineStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotationSpeed = table.Column<int>(type: "int", nullable: false),
                    SpindleTemperature = table.Column<double>(type: "float", nullable: false),
                    LotCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimestampLocal = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimestampUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsMachineBlocked = table.Column<bool>(type: "bit", nullable: false),
                    BlockDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatheLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LatheLogs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "coffee_mek",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                schema: "coffee_mek",
                columns: table => new
                {
                    LotCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScheduledStartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProducedItems = table.Column<long>(type: "bigint", nullable: false),
                    ProductionStarted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProductionFinished = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.LotCode);
                    table.ForeignKey(
                        name: "FK_Lots_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "coffee_mek",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lots_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "coffee_mek",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestLineLogs",
                schema: "coffee_mek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [coffee_mek].[TestLineLogSequence]"),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoilerPressure = table.Column<double>(type: "float", nullable: false),
                    BoilerTemperature = table.Column<double>(type: "float", nullable: false),
                    EnergyConsumption = table.Column<double>(type: "float", nullable: false),
                    LotCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimestampLocal = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimestampUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsMachineBlocked = table.Column<bool>(type: "bit", nullable: false),
                    BlockDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestLineLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestLineLogs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "coffee_mek",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyLineLogs_LocationId",
                schema: "coffee_mek",
                table: "AssemblyLineLogs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CutterCNCLogs_LocationId",
                schema: "coffee_mek",
                table: "CutterCNCLogs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LatheLogs_LocationId",
                schema: "coffee_mek",
                table: "LatheLogs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_CustomerId",
                schema: "coffee_mek",
                table: "Lots",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_LocationId",
                schema: "coffee_mek",
                table: "Lots",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestLineLogs_LocationId",
                schema: "coffee_mek",
                table: "TestLineLogs",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssemblyLineLogs",
                schema: "coffee_mek");

            migrationBuilder.DropTable(
                name: "CutterCNCLogs",
                schema: "coffee_mek");

            migrationBuilder.DropTable(
                name: "LatheLogs",
                schema: "coffee_mek");

            migrationBuilder.DropTable(
                name: "Lots",
                schema: "coffee_mek");

            migrationBuilder.DropTable(
                name: "TestLineLogs",
                schema: "coffee_mek");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "coffee_mek");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "coffee_mek");

            migrationBuilder.DropSequence(
                name: "AssemblyLineLogSequence",
                schema: "coffee_mek");

            migrationBuilder.DropSequence(
                name: "CutterCNCLogSequence",
                schema: "coffee_mek");

            migrationBuilder.DropSequence(
                name: "LatheLogSequence",
                schema: "coffee_mek");

            migrationBuilder.DropSequence(
                name: "TestLineLogSequence",
                schema: "coffee_mek");
        }
    }
}
