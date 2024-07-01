using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPorfolio.Migrations
{
    /// <inheritdoc />
    public partial class project_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ProjectDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProjectPic = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ProjectInfo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
