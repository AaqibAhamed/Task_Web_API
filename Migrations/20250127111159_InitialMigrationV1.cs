using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { new Guid("1cfbc5de-3ee8-42d9-9ca0-9f675d9e5c8f"), null, new DateTime(2025, 1, 27, 11, 11, 59, 582, DateTimeKind.Utc).AddTicks(4720), "Finish coding and testing", false, "Complete Project" },
                    { new Guid("a50114c9-c732-4956-a558-330a612b9f43"), null, new DateTime(2025, 1, 27, 11, 11, 59, 582, DateTimeKind.Utc).AddTicks(4720), "Milk, Eggs, Bread", false, "Buy Groceries" },
                    { new Guid("ddb2c78a-5fc9-40b5-8a29-e75946f1f590"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exercise for 30 minutes", false, "Workout" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_Title",
                table: "ToDoItems",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItems");
        }
    }
}
