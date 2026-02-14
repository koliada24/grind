using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashingWithPaginationDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Counter = table.Column<long>(type: "INTEGER", nullable: false),
                    Value1 = table.Column<string>(type: "TEXT", nullable: false),
                    Value2 = table.Column<string>(type: "TEXT", nullable: false),
                    Value3 = table.Column<string>(type: "TEXT", nullable: false),
                    Value4 = table.Column<string>(type: "TEXT", nullable: false),
                    Value5 = table.Column<string>(type: "TEXT", nullable: false),
                    Value6 = table.Column<string>(type: "TEXT", nullable: false),
                    Value7 = table.Column<string>(type: "TEXT", nullable: false),
                    Value8 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityModels");
        }
    }
}
