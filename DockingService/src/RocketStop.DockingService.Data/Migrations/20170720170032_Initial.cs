using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RocketStop.DockingService.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bays",
                columns: table => new
                {
                    BayId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Depth = table.Column<int>(type: "int4", nullable: false),
                    Height = table.Column<int>(type: "int4", nullable: false),
                    Number = table.Column<int>(type: "int4", nullable: false),
                    Section = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bays", x => x.BayId);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Depth = table.Column<int>(type: "int4", nullable: false),
                    Height = table.Column<int>(type: "int4", nullable: false),
                    Registration = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipId);
                });

            migrationBuilder.CreateTable(
                name: "Dockings",
                columns: table => new
                {
                    DockingId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BayId = table.Column<int>(type: "int4", nullable: false),
                    DockedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    ExpectedHours = table.Column<int>(type: "int4", nullable: false),
                    ShipId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockings", x => x.DockingId);
                    table.ForeignKey(
                        name: "FK_Dockings_Bays_BayId",
                        column: x => x.BayId,
                        principalTable: "Bays",
                        principalColumn: "BayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dockings_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "ShipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dockings_BayId",
                table: "Dockings",
                column: "BayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dockings_ShipId",
                table: "Dockings",
                column: "ShipId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dockings");

            migrationBuilder.DropTable(
                name: "Bays");

            migrationBuilder.DropTable(
                name: "Ships");
        }
    }
}
