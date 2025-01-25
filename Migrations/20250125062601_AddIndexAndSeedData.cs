using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ToDoItems",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "ToDoItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { new Guid("1f4e7986-d5f5-4336-b766-8cfd978f1a63"), null, new DateTime(2025, 1, 25, 6, 26, 1, 504, DateTimeKind.Utc).AddTicks(3400), "Finish coding and testing", false, "Complete Project" },
                    { new Guid("25a698cd-9945-41ea-8046-1808018794fa"), null, new DateTime(2025, 1, 25, 6, 26, 1, 504, DateTimeKind.Utc).AddTicks(3400), "Milk, Eggs, Bread", false, "Buy Groceries" },
                    { new Guid("91bc5ab7-42a3-4f16-8579-4a34b4d70fbb"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exercise for 30 minutes", false, "Workout" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_Title",
                table: "ToDoItems",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_Title",
                table: "ToDoItems");

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

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ToDoItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, "moring asdad", true, "Wake Up" },
                    { 2, "go washroom", false, "Answer the Nature" },
                    { 3, "eat healthy food", false, "Take Breakfast" }
                });
        }
    }
}
