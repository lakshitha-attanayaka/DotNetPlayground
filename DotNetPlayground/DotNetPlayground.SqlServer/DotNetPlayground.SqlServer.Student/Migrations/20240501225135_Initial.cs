using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetPlayground.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "playground");

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "playground",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FriendlyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_FriendlyId",
                schema: "playground",
                table: "Students",
                column: "FriendlyId",
                unique: true);
            
            migrationBuilder.Sql(sql: "DBCC CHECKIDENT ('playground.Students', RESEED, 1001);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(sql: "DBCC CHECKIDENT ('playground.Students', RESEED, 1);");
            migrationBuilder.DropTable(
                name: "Students",
                schema: "playground");
        }
    }
}
