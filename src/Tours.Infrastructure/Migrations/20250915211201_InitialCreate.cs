using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tours.Core.Domain.Entities;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;

#nullable disable

namespace Tours.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tours");

            migrationBuilder.CreateTable(
                name: "Equipment",
                schema: "tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourExecutions",
                schema: "tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    TouristId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Completion = table.Column<double>(type: "double precision", nullable: false),
                    Position = table.Column<TouristPosition>(type: "jsonb", nullable: false),
                    CompletedCheckpoints = table.Column<List<CompletedCheckpoint>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourExecutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                schema: "tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false),
                    Price = table.Column<Price>(type: "jsonb", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    TotalLength = table.Column<Distance>(type: "jsonb", nullable: false),
                    StatusChangeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Durations = table.Column<List<TourDuration>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checkpoints",
                schema: "tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<Location>(type: "jsonb", nullable: false),
                    Secret = table.Column<string>(type: "text", nullable: false),
                    EncounterIds = table.Column<List<long>>(type: "bigint[]", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkpoints_Tours_TourId",
                        column: x => x.TourId,
                        principalSchema: "tours",
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                schema: "tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<Location>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_Tours_TourId",
                        column: x => x.TourId,
                        principalSchema: "tours",
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredEquipments",
                schema: "tours",
                columns: table => new
                {
                    EquipmentId = table.Column<long>(type: "bigint", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredEquipments", x => new { x.EquipmentId, x.TourId });
                    table.ForeignKey(
                        name: "FK_RequiredEquipments_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalSchema: "tours",
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredEquipments_Tours_TourId",
                        column: x => x.TourId,
                        principalSchema: "tours",
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TouristId = table.Column<long>(type: "bigint", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    TourDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Images = table.Column<List<string>>(type: "text[]", nullable: false),
                    Completion = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Tours_TourId",
                        column: x => x.TourId,
                        principalSchema: "tours",
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoints_TourId",
                schema: "tours",
                table: "Checkpoints",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_TourId",
                schema: "tours",
                table: "Objects",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredEquipments_TourId",
                schema: "tours",
                table: "RequiredEquipments",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TourId",
                schema: "tours",
                table: "Reviews",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkpoints",
                schema: "tours");

            migrationBuilder.DropTable(
                name: "Objects",
                schema: "tours");

            migrationBuilder.DropTable(
                name: "RequiredEquipments",
                schema: "tours");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "tours");

            migrationBuilder.DropTable(
                name: "TourExecutions",
                schema: "tours");

            migrationBuilder.DropTable(
                name: "Equipment",
                schema: "tours");

            migrationBuilder.DropTable(
                name: "Tours",
                schema: "tours");
        }
    }
}
