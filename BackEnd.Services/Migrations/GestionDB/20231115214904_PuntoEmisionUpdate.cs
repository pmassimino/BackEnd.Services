using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class PuntoEmisionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeradorPuntoEmision",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdComprobante = table.Column<int>(type: "INTEGER", nullable: false),
                    IdNumeradorDocumento = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeradorPuntoEmision", x => new { x.Id, x.IdComprobante });
                    table.ForeignKey(
                        name: "FK_NumeradorPuntoEmision_NumeradorDocumento_IdNumeradorDocumento",
                        column: x => x.IdNumeradorDocumento,
                        principalTable: "NumeradorDocumento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NumeradorPuntoEmision_PuntoEmision_Id",
                        column: x => x.Id,
                        principalTable: "PuntoEmision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumeradorPuntoEmision_IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                column: "IdNumeradorDocumento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeradorPuntoEmision");
        }
    }
}
