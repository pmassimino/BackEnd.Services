using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class FacturaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdPuntoEmision",
                table: "Factura",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPuntoEmision",
                table: "Factura");
        }
    }
}
