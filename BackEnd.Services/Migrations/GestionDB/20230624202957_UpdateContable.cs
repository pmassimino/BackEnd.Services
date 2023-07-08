using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class UpdateContable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComprobanteMayor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    IdComprobante = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobanteMayor", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ComprobanteMayor",
                columns: new[] { "Id", "IdComprobante", "IdTipo", "Nombre" },
                values: new object[] { "00001", null, null, "General" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprobanteMayor");
        }
    }
}
