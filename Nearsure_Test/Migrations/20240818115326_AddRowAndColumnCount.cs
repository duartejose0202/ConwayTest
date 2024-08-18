using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConwayGame.Migrations
{
    /// <inheritdoc />
    public partial class AddRowAndColumnCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColumnCount",
                table: "Boards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowCount",
                table: "Boards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnCount",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "RowCount",
                table: "Boards");
        }
    }
}
