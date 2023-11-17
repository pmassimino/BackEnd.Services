using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class NumeradorPuntoEmisionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision");

            migrationBuilder.DropColumn(
                name: "IdComprobante",
                table: "NumeradorPuntoEmision");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                type: "TEXT",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "IdComprobante",
                table: "NumeradorDocumento",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00001",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00002",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00003",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00004",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00005",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00006",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00007",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00008",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00009",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00010",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00011",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00012",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00013",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00014",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00015",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00016",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00017",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00018",
                column: "IdComprobante",
                value: 0);

            migrationBuilder.UpdateData(
                table: "NumeradorDocumento",
                keyColumn: "Id",
                keyValue: "00019",
                column: "IdComprobante",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision");

            migrationBuilder.DropColumn(
                name: "IdComprobante",
                table: "NumeradorDocumento");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumeradorDocumento",
                table: "NumeradorPuntoEmision",
                type: "TEXT",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 10,
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "IdComprobante",
                table: "NumeradorPuntoEmision",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeradorPuntoEmision",
                table: "NumeradorPuntoEmision",
                columns: new[] { "Id", "IdComprobante" });
        }
    }
}
