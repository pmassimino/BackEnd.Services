using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class UpdateFamilia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CtaEgresoDefault",
                table: "Familia",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaIngresoDefault",
                table: "Familia",
                type: "TEXT",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CtaEgresoDefault",
                table: "Familia");

            migrationBuilder.DropColumn(
                name: "CtaIngresoDefault",
                table: "Familia");
        }
    }
}
