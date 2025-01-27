using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: new Guid("1f4e7986-d5f5-4336-b766-8cfd978f1a63"));

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: new Guid("25a698cd-9945-41ea-8046-1808018794fa"));

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: new Guid("91bc5ab7-42a3-4f16-8579-4a34b4d70fbb"));

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { new Guid("7eec7fc7-ed0c-4a57-928c-062d469258f1"), null, new DateTime(2025, 1, 27, 9, 31, 55, 506, DateTimeKind.Utc).AddTicks(9100), "Milk, Eggs, Bread", false, "Buy Groceries" },
                    { new Guid("a9e16d96-574b-44bc-b078-cd42da04e4fc"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exercise for 30 minutes", false, "Workout" },
                    { new Guid("dec2f8cf-f6d6-4e3d-b4a1-9e4c49d15900"), null, new DateTime(2025, 1, 27, 9, 31, 55, 506, DateTimeKind.Utc).AddTicks(9100), "Finish coding and testing", false, "Complete Project" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: new Guid("7eec7fc7-ed0c-4a57-928c-062d469258f1"));

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: new Guid("a9e16d96-574b-44bc-b078-cd42da04e4fc"));

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: new Guid("dec2f8cf-f6d6-4e3d-b4a1-9e4c49d15900"));

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { new Guid("1f4e7986-d5f5-4336-b766-8cfd978f1a63"), null, new DateTime(2025, 1, 25, 6, 26, 1, 504, DateTimeKind.Utc).AddTicks(3400), "Finish coding and testing", false, "Complete Project" },
                    { new Guid("25a698cd-9945-41ea-8046-1808018794fa"), null, new DateTime(2025, 1, 25, 6, 26, 1, 504, DateTimeKind.Utc).AddTicks(3400), "Milk, Eggs, Bread", false, "Buy Groceries" },
                    { new Guid("91bc5ab7-42a3-4f16-8579-4a34b4d70fbb"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exercise for 30 minutes", false, "Workout" }
                });
        }
    }
}
