using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASE3040.Infrastructure.Data.Migrations.SqlServerMigrations;

/// <inheritdoc />
public partial class addDateTime : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "TripBudget",
            table: "ToDoLists",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<int>(
            name: "BudgetId",
            table: "ToDoItems",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTime>(
            name: "DateTime",
            table: "ToDoItems",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.CreateTable(
            name: "BudgetItem",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ActualExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ToDoListId = table.Column<int>(type: "int", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BudgetItem", x => x.Id);
                table.ForeignKey(
                    name: "FK_BudgetItem_ToDoLists_ToDoListId",
                    column: x => x.ToDoListId,
                    principalTable: "ToDoLists",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_ToDoItems_BudgetId",
            table: "ToDoItems",
            column: "BudgetId");

        migrationBuilder.CreateIndex(
            name: "IX_BudgetItem_ToDoListId",
            table: "BudgetItem",
            column: "ToDoListId");

        migrationBuilder.AddForeignKey(
            name: "FK_ToDoItems_BudgetItem_BudgetId",
            table: "ToDoItems",
            column: "BudgetId",
            principalTable: "BudgetItem",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_ToDoItems_BudgetItem_BudgetId",
            table: "ToDoItems");

        migrationBuilder.DropTable(
            name: "BudgetItem");

        migrationBuilder.DropIndex(
            name: "IX_ToDoItems_BudgetId",
            table: "ToDoItems");

        migrationBuilder.DropColumn(
            name: "TripBudget",
            table: "ToDoLists");

        migrationBuilder.DropColumn(
            name: "BudgetId",
            table: "ToDoItems");

        migrationBuilder.DropColumn(
            name: "DateTime",
            table: "ToDoItems");
    }
}