using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class PuntoEmisionUpdate_ver1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeradorPuntoEmision_NumeradorDocumento_IdNumeradorDocumento",
                table: "NumeradorPuntoEmision");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeradorPuntoEmision_NumeradorDocumento_IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                column: "IdNumeradorDocumento",
                principalTable: "NumeradorDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeradorPuntoEmision_NumeradorDocumento_IdNumeradorDocumento",
                table: "NumeradorPuntoEmision");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                type: "TEXT",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeradorPuntoEmision_NumeradorDocumento_IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                column: "IdNumeradorDocumento",
                principalTable: "NumeradorDocumento",
                principalColumn: "Id");
        }
    }
}
