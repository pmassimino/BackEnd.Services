using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class UpdateConfigFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AfipWs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    NombreServicio = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfipWs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoDigital",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoDigital", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuntoEmision",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAfipWsService = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdProvincia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Localidad = table.Column<string>(type: "TEXT", nullable: true),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Domicilio = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Altura = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntoEmision", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemPuntoEmision",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdPuntoEmision = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPuntoEmision", x => new { x.Id, x.IdPuntoEmision });
                    table.ForeignKey(
                        name: "FK_ItemPuntoEmision_ConfigFactura_Id",
                        column: x => x.Id,
                        principalTable: "ConfigFactura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPuntoEmision_PuntoEmision_IdPuntoEmision",
                        column: x => x.IdPuntoEmision,
                        principalTable: "PuntoEmision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPuntoEmision_IdPuntoEmision",
                table: "ItemPuntoEmision",
                column: "IdPuntoEmision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfipWs");

            migrationBuilder.DropTable(
                name: "CertificadoDigital");

            migrationBuilder.DropTable(
                name: "ItemPuntoEmision");

            migrationBuilder.DropTable(
                name: "PuntoEmision");
        }
    }
}
