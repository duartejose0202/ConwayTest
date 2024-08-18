using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConwayGame.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentStateAndCurrentStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Boards",
                newName: "InitialState");

            migrationBuilder.AddColumn<string>(
                name: "CurrentState",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStep",
                table: "Boards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CurrentStep",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "InitialState",
                table: "Boards",
                newName: "State");
        }
    }
}
