using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class UpdateMovStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "MovStock",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_IdArticulo",
                table: "Stocks",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_IdDeposito",
                table: "Stocks",
                column: "IdDeposito");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Articulo_IdArticulo",
                table: "Stocks",
                column: "IdArticulo",
                principalTable: "Articulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Deposito_IdDeposito",
                table: "Stocks",
                column: "IdDeposito",
                principalTable: "Deposito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Articulo_IdArticulo",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Deposito_IdDeposito",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_IdArticulo",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_IdDeposito",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "MovStock");
        }
    }
}
