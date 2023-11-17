using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class UpdateConfigFactura08112023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdSeccion",
                table: "ConfigFactura",
                type: "TEXT",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "ConfigFactura",
                type: "TEXT",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reporte",
                table: "ConfigFactura",
                type: "TEXT",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporteFiscal",
                table: "ConfigFactura",
                type: "TEXT",
                maxLength: 60,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ConfigFactura",
                keyColumn: "Id",
                keyValue: "001",
                columns: new[] { "IdSeccion", "Nombre", "Reporte", "ReporteFiscal" },
                values: new object[] { null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigFactura_IdSeccion",
                table: "ConfigFactura",
                column: "IdSeccion");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigFactura_Seccion_IdSeccion",
                table: "ConfigFactura",
                column: "IdSeccion",
                principalTable: "Seccion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigFactura_Seccion_IdSeccion",
                table: "ConfigFactura");

            migrationBuilder.DropIndex(
                name: "IX_ConfigFactura_IdSeccion",
                table: "ConfigFactura");

            migrationBuilder.DropColumn(
                name: "IdSeccion",
                table: "ConfigFactura");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "ConfigFactura");

            migrationBuilder.DropColumn(
                name: "Reporte",
                table: "ConfigFactura");

            migrationBuilder.DropColumn(
                name: "ReporteFiscal",
                table: "ConfigFactura");
        }
    }
}
