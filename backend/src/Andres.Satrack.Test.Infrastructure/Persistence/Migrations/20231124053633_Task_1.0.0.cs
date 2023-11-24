using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Andres.Satrack.Test.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Task_100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                schema: "Andres.Satrack.Test",
                table: "Task",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                schema: "Andres.Satrack.Test",
                table: "Task");
        }
    }
}
