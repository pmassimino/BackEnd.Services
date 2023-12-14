using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class PuntoEmisionUpdate_ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision",
                columns: new[] { "Id", "IdNumeradorDocumento" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision",
                column: "Id");
        }
    }
}
