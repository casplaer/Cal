using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cal.Migrations
{
    /// <inheritdoc />
    public partial class SharedEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharedEventsId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SharedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SharedEventsId",
                table: "Events",
                column: "SharedEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SharedEvents_SharedEventsId",
                table: "Events",
                column: "SharedEventsId",
                principalTable: "SharedEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_SharedEvents_SharedEventsId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "SharedEvents");

            migrationBuilder.DropIndex(
                name: "IX_Events_SharedEventsId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SharedEventsId",
                table: "Events");
        }
    }
}
