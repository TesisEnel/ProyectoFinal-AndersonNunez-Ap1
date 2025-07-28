using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalAp1.Migrations
{
    /// <inheritdoc />
    public partial class CampoNombre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fabricante",
                table: "Avions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fabricante",
                table: "Avions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
