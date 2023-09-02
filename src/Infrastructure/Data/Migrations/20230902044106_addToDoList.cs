using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASE3040.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addToDoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoListId",
                table: "ToDoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "ToDoListId",
                table: "ToDoItems");
        }
    }
}
