﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Task_Web_API.Migrations
{
    [DbContext(typeof(TaskDbContext))]
    [Migration("20250127111159_InitialMigrationV1")]
    partial class InitialMigrationV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Task_Web_API.Entities.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .HasDatabaseName("IX_ToDoItems_Title");

                    b.ToTable("ToDoItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a50114c9-c732-4956-a558-330a612b9f43"),
                            CreatedAt = new DateTime(2025, 1, 27, 11, 11, 59, 582, DateTimeKind.Utc).AddTicks(4720),
                            Description = "Milk, Eggs, Bread",
                            IsCompleted = false,
                            Title = "Buy Groceries"
                        },
                        new
                        {
                            Id = new Guid("1cfbc5de-3ee8-42d9-9ca0-9f675d9e5c8f"),
                            CreatedAt = new DateTime(2025, 1, 27, 11, 11, 59, 582, DateTimeKind.Utc).AddTicks(4720),
                            Description = "Finish coding and testing",
                            IsCompleted = false,
                            Title = "Complete Project"
                        },
                        new
                        {
                            Id = new Guid("ddb2c78a-5fc9-40b5-8a29-e75946f1f590"),
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Exercise for 30 minutes",
                            IsCompleted = false,
                            Title = "Workout"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
