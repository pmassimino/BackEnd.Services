using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEnd.Services.Migrations.GlobalDB
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    IdGrupo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comprobante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    CodAfip = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comprobante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CondIva",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    CodAfip = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondIva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CondIvaOperacion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Alicuota = table.Column<decimal>(type: "TEXT", nullable: false),
                    CodAfip = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondIvaOperacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moneda",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moneda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recurso",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    CodAfip = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoFactura",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    CodAfip = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoFactura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    CodAfip = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    NombreComercial = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IdTipoDoc = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    NumeroDocumento = table.Column<long>(type: "INTEGER", nullable: false),
                    IdProvincia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdLocalidad = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Domicilio = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Altura = table.Column<decimal>(type: "TEXT", nullable: false),
                    Piso = table.Column<decimal>(type: "TEXT", nullable: false),
                    Oficina = table.Column<decimal>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    TelefonoSec = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MovilSec = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Fax = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    IdCondicionIva = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdCondicionGanancia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdCondicionIB = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    NumeroIB = table.Column<decimal>(type: "TEXT", nullable: false),
                    DatabaseName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    IdOrganizacion = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdOwner = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresa_Organizacion_IdOrganizacion",
                        column: x => x.IdOrganizacion,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdOrganizacion = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rol_Organizacion_IdOrganizacion",
                        column: x => x.IdOrganizacion,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    IdProvincia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidad_Provincia_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IdAccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdRecurso = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permiso_Accion_IdAccion",
                        column: x => x.IdAccion,
                        principalTable: "Accion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permiso_Recurso_IdRecurso",
                        column: x => x.IdRecurso,
                        principalTable: "Recurso",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpresaAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 10, nullable: false),
                    IdAccount = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaAccount", x => new { x.Id, x.IdAccount });
                    table.ForeignKey(
                        name: "FK_EmpresaAccount_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolAccount",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAccount = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolAccount", x => new { x.IdRol, x.IdAccount });
                    table.ForeignKey(
                        name: "FK_RolAccount_Account_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolAccount_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOrganizacion = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdPermiso = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => new { x.IdRol, x.IdOrganizacion, x.IdPermiso });
                    table.ForeignKey(
                        name: "FK_RolPermiso_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accion",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { "Add", "Nuevo" },
                    { "Delete", "Eliminar" },
                    { "Edit", "Editar" },
                    { "Execute", "Ejecutar" },
                    { "GetAll", "Listar" },
                    { "Update", "Actualizar" }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Email", "Estado", "IdGrupo", "Nombre", "Password" },
                values: new object[] { "0000000001", "pmassimino@hotmail.com", null, null, "admin", "JrCdgs9+JFZYDic1e30pk0iDjwE=" });

            migrationBuilder.InsertData(
                table: "Comprobante",
                columns: new[] { "Id", "CodAfip", "Nombre" },
                values: new object[,]
                {
                    { 1, "1", "FACTURA A" },
                    { 2, "2", "NOTAS DE DEBITO A" },
                    { 3, "3", "NOTAS DE CREDITO A" },
                    { 4, "4", "RECIBOS A" },
                    { 5, "5", "NOTAS DE VENTA AL CONTADO A" },
                    { 6, "6", "FACTURA B" },
                    { 7, "5", "NOTAS DE DEBITO B" },
                    { 8, "8", "NOTAS DE CREDITO B" },
                    { 9, "9", "RECIBOS B" },
                    { 10, "10", "NOTAS DE VENTA AL CONTADO B" },
                    { 11, "11", "FACTURA C" },
                    { 12, "12", "NOTAS DE DEBITO C" },
                    { 13, "13", "NOTAS DE CREDITO C" },
                    { 15, "15", "RECIBOS C" },
                    { 16, "16", "NOTAS DE VENTA AL CONTADO C" },
                    { 17, "17", "LIQUIDACION DE SERVICIOS PUBLICOS CLASE A" },
                    { 18, "18", "LIQUIDACION DE SERVICIOS PUBLICOS CLASE B" },
                    { 19, "19", "FACTURAS DE EXPORTACION" },
                    { 20, "20", "NOTAS DE DEBITO POR OPERACIONES CON EL EXTERIOR" },
                    { 21, "21", "NOTAS DE CREDITO POR OPERACIONES CON EL EXTERIOR" },
                    { 22, "23", "FACTURAS - PERMISO EXPORTACION SIMPLIFICADO - DTO. 855/97" },
                    { 24, "24", "COMPROBANTES “A” DE CONSIGNACION PRIMARIA PARA EL SECTOR PESQUERO MARITIMO" },
                    { 25, "25", "COMPROBANTES “B” DE COMPRA PRIMARIA PARA EL SECTOR PESQUERO MARITIMO" },
                    { 26, "26", "COMPROBANTES “B” DE CONSIGNACION PRIMARIA PARA EL SECTOR PESQUERO MARITIMO" },
                    { 27, "27", "LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE A" },
                    { 28, "28", "LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE B" },
                    { 29, "29", "LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE C" },
                    { 30, "30", "COMPROBANTES DE COMPRA DE BIENES USADOS" },
                    { 31, "31", "MANDATO - CONSIGNACION" },
                    { 32, "32", "COMPROBANTES PARA RECICLAR MATERIALES" },
                    { 33, "33", "LIQUIDACION PRIMARIA DE GRANOS" },
                    { 34, "34", "COMPROBANTES A DEL APARTADO A  INCISO F)  R.G. N°  1415" },
                    { 35, "35", "COMPROBANTES B DEL ANEXO I, APARTADO A, INC. F), R.G. N° 1415" },
                    { 36, "36", "COMPROBANTES C DEL Anexo I, Apartado A, INC.F), R.G. N° 1415" },
                    { 37, "37", "NOTAS DE DEBITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415" },
                    { 38, "38", "NOTAS DE CREDITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415" },
                    { 39, "39", "OTROS COMPROBANTES A QUE CUMPLEN CON LA R G  1415" },
                    { 40, "40", "OTROS COMPROBANTES B QUE CUMPLAN CON LA R.G. N° 1415" },
                    { 41, "41", "OTROS COMPROBANTES C QUE CUMPLAN CON LA R.G. N° 1415" },
                    { 43, "43", "NOTA DE CREDITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE B" },
                    { 44, "44", "NOTA DE CREDITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE C" },
                    { 45, "45", "NOTA DE DEBITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE A" },
                    { 46, "46", "NOTA DE DEBITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE B" },
                    { 47, "47", "NOTA DE DEBITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE C" },
                    { 48, "48", "NOTA DE CREDITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE A" },
                    { 49, "49", "COMPROBANTES DE COMPRA DE BIENES NO REGISTRABLES A CONSUMIDORES FINALES" },
                    { 50, "50", "RECIBO FACTURA A  REGIMEN DE FACTURA DE CREDITO " },
                    { 51, "51", "FACTURAS M" },
                    { 52, "52", "NOTAS DE DEBITO M" },
                    { 53, "53", "NOTAS DE CREDITO M" },
                    { 54, "54", "RECIBOS M" },
                    { 55, "55", "NOTAS DE VENTA AL CONTADO M" },
                    { 56, "56", "COMPROBANTES M DEL ANEXO I  APARTADO A  INC F) R.G. N° 1415" },
                    { 57, "57", "OTROS COMPROBANTES M QUE CUMPLAN CON LA R.G. N° 1415" },
                    { 58, "58", "CUENTAS DE VENTA Y LIQUIDO PRODUCTO M" },
                    { 59, "59", "LIQUIDACIONES M" },
                    { 60, "60", "CUENTAS DE VENTA Y LIQUIDO PRODUCTO A" },
                    { 61, "61", "CUENTAS DE VENTA Y LIQUIDO PRODUCTO B" },
                    { 63, "63", "LIQUIDACIONES A" },
                    { 64, "64", "LIQUIDACIONES B" },
                    { 66, "66", "DESPACHO DE IMPORTACION" },
                    { 68, "68", "LIQUIDACION C" },
                    { 70, "70", "RECIBOS FACTURA DE CREDITO" },
                    { 80, "80", "INFORME DIARIO DE CIERRE (ZETA) - CONTROLADORES FISCALES" },
                    { 81, "81", "TIQUE FACTURA A" },
                    { 82, "82", "TIQUE FACTURA B" },
                    { 83, "83", "TIQUE" },
                    { 88, "88", "REMITO ELECTRONICO" },
                    { 89, "89", "RESUMEN DE DATOS" },
                    { 90, "90", "OTROS COMPROBANTES - DOCUMENTOS EXCEPTUADOS - NOTAS DE CREDITO" },
                    { 91, "91", "REMITOS R" },
                    { 99, "99", "OTROS COMPROBANTES QUE NO CUMPLEN O ESTÁN EXCEPTUADOS DE LA R.G. 1415 Y SUS MODIF " },
                    { 110, "110", "TIQUE NOTA DE CREDITO " },
                    { 111, "111", "TIQUE FACTURA C" },
                    { 112, "112", " TIQUE NOTA DE CREDITO A" },
                    { 113, "113", "TIQUE NOTA DE CREDITO B" },
                    { 114, "114", "TIQUE NOTA DE CREDITO CB" },
                    { 115, "115", "TIQUE NOTA DE DEBITO A" },
                    { 116, "116", "TIQUE NOTA DE DEBITO B" },
                    { 117, "117", "TIQUE NOTA DE DEBITO C" },
                    { 118, "118", "TIQUE FACTURA M" },
                    { 119, "119", "TIQUE NOTA DE CREDITO M" },
                    { 120, "120", "TIQUE NOTA DE DEBITO M" },
                    { 201, "201", "FACTURA DE CRÉDITO ELECTRÓNICA MiPyMEs (FCE) A" },
                    { 202, "202", "NOTA DE DEBITO ELECTRÓNICA MiPyMEs (FCE) A" },
                    { 203, "203", "NOTA DE CREDITO ELECTRÓNICA MiPyMEs (FCE) A" },
                    { 206, "206", "FACTURA DE CRÉDITO ELECTRÓNICA MiPyMEs (FCE) B" },
                    { 207, "207", "NOTA DE DEBITO ELECTRÓNICA MiPyMEs (FCE) B" },
                    { 208, "208", "NOTA DE CREDITO ELECTRÓNICA MiPyMEs (FCE) B" },
                    { 211, "211", "FACTURA DE CRÉDITO ELECTRÓNICA MiPyMEs (FCE) C" },
                    { 212, "212", "NOTA DE DEBITO ELECTRÓNICA MiPyMEs (FCE) C" },
                    { 213, "213", "NOTA DE CREDITO ELECTRÓNICA MiPyMEs (FCE) C" },
                    { 331, "331", "LIQUIDACION SECUNDARIA DE GRANOS" },
                    { 332, "332", "CERTIFICACION ELECTRONICA (GRANOS)" },
                    { 995, "995", "REMITO ELECTRÓNICO CÁRNICO" }
                });

            migrationBuilder.InsertData(
                table: "CondIva",
                columns: new[] { "Id", "CodAfip", "Nombre" },
                values: new object[,]
                {
                    { "01", "1", "RESPONSABLE INSCRIPTO" },
                    { "04", "4", "EXENTO" },
                    { "05", "5", "RESPONSABLE MONOTRIBUTO" },
                    { "06", "6", "NO CATEGORIZADO" },
                    { "08", "8", "EXPORTACION" },
                    { "O3", "3", "CONSUMIDOR FINAL" }
                });

            migrationBuilder.InsertData(
                table: "CondIvaOperacion",
                columns: new[] { "Id", "Alicuota", "CodAfip", "Nombre" },
                values: new object[,]
                {
                    { "001", 0m, "1", "No Gravado" },
                    { "002", 0m, "2", "Exento" },
                    { "003", 0m, "3", "0%" },
                    { "004", 10.5m, "4", "10.5%" },
                    { "005", 21m, "5", "21%" },
                    { "006", 27m, "6", "27%" },
                    { "008", 5m, "8", "5%" },
                    { "009", 2.5m, "9", "2.5%" }
                });

            migrationBuilder.InsertData(
                table: "EmpresaAccount",
                columns: new[] { "Id", "IdAccount", "EmpresaId" },
                values: new object[,]
                {
                    { new Guid("bdd05dab-3ebb-44e5-ba5d-1f413506dbb1"), "0000000001", null },
                    { new Guid("f51d9987-6ee8-4b10-971e-c306df44b95b"), "0000000001", null }
                });

            migrationBuilder.InsertData(
                table: "Organizacion",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Organizacion General" });

            migrationBuilder.InsertData(
                table: "Provincia",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { "00", "CIUDAD AUTÓNOMA BUENOS AIRES" },
                    { "01", "BUENOS AIRES" },
                    { "02", " CATAMARCA" },
                    { "03", "CÓRDOBA" },
                    { "04", "CORRIENTES" },
                    { "05", "ENTRE RIOS" },
                    { "06", "JUJUY" },
                    { "07", "MENDOZA" },
                    { "08", "LA RIOJA" },
                    { "09", "SALTA" },
                    { "10", "SAN JUAN" },
                    { "11", "SAN LUIS" },
                    { "12", "SANTA FE" },
                    { "13", "SANTIAGO DEL ESTERO" },
                    { "14", "TUCUMÁN" },
                    { "16", "CHACO" },
                    { "17", "CHUBUT" },
                    { "18", "FORMOSA" },
                    { "19", "MISIONES" },
                    { "20", "NEUQUÉN" },
                    { "21", "LA PAMPA" },
                    { "22", "RIO NEGRO" },
                    { "23", "SANTA CRUZ" },
                    { "24", "TIERRA DEL FUEGO" },
                    { "DOL", "Dólar ESTADOUNIDENSE" },
                    { "PES", "PESOS" }
                });

            migrationBuilder.InsertData(
                table: "Recurso",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { "Almacen.Articulo", "Articulo" },
                    { "Comun.Sujeto", "Sujetos (Clientes y Proveedores)" },
                    { "Contable.CuentaMayor", "CuentaMayor" },
                    { "Contable.LibroIva", "Libro Iva" },
                    { "Contable.Mayor", "Mayor" },
                    { "Contable.MovCtaCte", "Cuentas Corrientes" },
                    { "Tesoreria.ReciboCtaCte", "Recibo Cta.Cte." },
                    { "Venta.Factura", "Factura" }
                });

            migrationBuilder.InsertData(
                table: "TipoDocumento",
                columns: new[] { "Id", "CodAfip", "Nombre" },
                values: new object[,]
                {
                    { "80", "80", "CUIT" },
                    { "86", "86", "CUIL" },
                    { "87", "87", "CDI" },
                    { "89", "89", "LIBRETA DE ENROLAMIENTO" },
                    { "90", "90", "LIBRETA CIVICA" },
                    { "96", "96", "DNI" }
                });

            migrationBuilder.InsertData(
                table: "TipoFactura",
                columns: new[] { "Id", "CodAfip", "Nombre" },
                values: new object[,]
                {
                    { "1", "1", "FACTURA" },
                    { "2", "2", "NOTA DE CREDITO" },
                    { "3", "3", "NOTA DE DEBITO" }
                });

            migrationBuilder.InsertData(
                table: "UnidadMedida",
                columns: new[] { "Id", "CodAfip", "Nombre" },
                values: new object[,]
                {
                    { "001", "001", "KILOGRAMO" },
                    { "002", "002", "METROS" },
                    { "003", "003", "METRO CUADRADO" },
                    { "004", "004", "METRO CUBICO" },
                    { "005", "005", "LITROS" },
                    { "006", "006", "1000 KILOWATT HORA" },
                    { "007", "007", "UNIDAD" },
                    { "014", "014", "GRAMO" },
                    { "017", "017", "KILOMETRO" },
                    { "020", "020", "CENTIMETRO" },
                    { "029", "029", "TONELADA" }
                });

            migrationBuilder.InsertData(
                table: "Empresa",
                columns: new[] { "Id", "Altura", "DatabaseName", "Domicilio", "Email", "Fax", "IdCondicionGanancia", "IdCondicionIB", "IdCondicionIva", "IdLocalidad", "IdOrganizacion", "IdOwner", "IdProvincia", "IdTipoDoc", "Movil", "MovilSec", "Nombre", "NombreComercial", "NumeroDocumento", "NumeroIB", "Oficina", "Piso", "Telefono", "TelefonoSec" },
                values: new object[,]
                {
                    { new Guid("bdd05dab-3ebb-44e5-ba5d-1f413506dbb1"), 100m, "AltDb", "CALLE S/N", "nombre@sudominio.com", null, null, null, "01", null, new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), null, "03", "80", null, null, "EMPRESA ALTERNATIVA", "EMPRESA ALTERNATIVA", 1111111111L, 0m, 0m, 0m, "999-999999", null },
                    { new Guid("f51d9987-6ee8-4b10-971e-c306df44b95b"), 810m, "HerediaDB", "QUINTANA ", "lorenaheredia@live.com.ar", null, null, null, "05", null, new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), null, "03", "80", null, null, "HEREDIA MARIA LORENA", "LA TRIGUEÑA SUCURSAL SUR", 23255953044L, 0m, 0m, 0m, "999-999999", null }
                });

            migrationBuilder.InsertData(
                table: "Permiso",
                columns: new[] { "Id", "IdAccion", "IdRecurso" },
                values: new object[,]
                {
                    { "Almacen.Articulo.Add", "Add", "Almacen.Articulo" },
                    { "Almacen.Articulo.Delete", "Delete", "Almacen.Articulo" },
                    { "Almacen.Articulo.Edit", "Edit", "Almacen.Articulo" },
                    { "Almacen.Articulo.GetAll", "GetAll", "Almacen.Articulo" },
                    { "Almacen.Articulo.Update", "Update", "Almacen.Articulo" },
                    { "Almacen.Articulo.Update.Precio", "Execute", "Almacen.Articulo" },
                    { "Comun.Sujeto.Add", "Add", "Comun.Sujeto" },
                    { "Comun.Sujeto.Delete", "Delete", "Comun.Sujeto" },
                    { "Comun.Sujeto.Edit", "Edit", "Comun.Sujeto" },
                    { "Comun.Sujeto.GetAll", "GetAll", "Comun.Sujeto" },
                    { "Comun.Sujeto.Update", "Update", "Comun.Sujeto" },
                    { "Contable.CuentaMayor.Add", "Add", "Contable.Mayor" },
                    { "Contable.CuentaMayor.Delete", "Delete", "Contable.Mayor" },
                    { "Contable.CuentaMayor.Edit", "Edit", "Contable.Mayor" },
                    { "Contable.CuentaMayor.GetAll", "GetAll", "Contable.Mayor" },
                    { "Contable.CuentaMayor.Update", "Update", "Contable.Mayor" },
                    { "Contable.LibroIva.List", "GetAll", "Contable.LibroIva" },
                    { "Contable.LibroIva.Print", "Execute", "Contable.LibroIva" },
                    { "Contable.Mayor.Add", "Add", "Contable.Mayor" },
                    { "Contable.Mayor.Delete", "Delete", "Contable.Mayor" },
                    { "Contable.Mayor.Edit", "Edit", "Contable.Mayor" },
                    { "Contable.Mayor.GetAll", "GetAll", "Contable.Mayor" },
                    { "Contable.Mayor.Update", "Update", "Contable.Mayor" },
                    { "Tesoreria.ReciboCtaCte.Add", "Add", "Tesoreria.ReciboCtaCte" },
                    { "Tesoreria.ReciboCtaCte.Delete", "Delete", "Tesoreria.ReciboCtaCte" },
                    { "Tesoreria.ReciboCtaCte.Edit", "Edit", "Tesoreria.ReciboCtaCte" },
                    { "Tesoreria.ReciboCtaCte.GetAll", "GetAll", "Tesoreria.ReciboCtaCte" },
                    { "Tesoreria.ReciboCtaCte.Update", "Update", "Tesoreria.ReciboCtaCte" },
                    { "Venta.Factura.Add", "Add", "Venta.Factura" },
                    { "Venta.Factura.Delete", "Delete", "Venta.Factura" },
                    { "Venta.Factura.Edit", "Edit", "Venta.Factura" },
                    { "Venta.Factura.GetAll", "GetAll", "Venta.Factura" },
                    { "Venta.Factura.Print", "Execute", "Venta.Factura" },
                    { "Venta.Factura.Update", "Update", "Venta.Factura" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "IdOrganizacion", "Nombre" },
                values: new object[] { 1, new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Admin" });

            migrationBuilder.InsertData(
                table: "RolAccount",
                columns: new[] { "IdAccount", "IdRol" },
                values: new object[] { "0000000001", 1 });

            migrationBuilder.InsertData(
                table: "RolPermiso",
                columns: new[] { "IdOrganizacion", "IdPermiso", "IdRol" },
                values: new object[,]
                {
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Almacen.Articulo.Add", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Almacen.Articulo.Delete", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Almacen.Articulo.Edit", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Almacen.Articulo.GetAll", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Almacen.Articulo.Update", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Almacen.Articulo.Update.Precio", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Comun.Sujeto.Add", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Comun.Sujeto.Delete", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Comun.Sujeto.Edit", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Comun.Sujeto.GetAll", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Comun.Sujeto.Update", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.CuentaMayor.Add", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.CuentaMayor.Delete", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.CuentaMayor.Edit", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.CuentaMayor.GetAll", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.CuentaMayor.Update", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.LibroIva.List", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.LibroIva.Print", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.Mayor.Add", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.Mayor.Delete", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.Mayor.Edit", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.Mayor.GetAll", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Contable.Mayor.Update", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Tesoreria.ReciboCtaCte.Add", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Tesoreria.ReciboCtaCte.Delete", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Tesoreria.ReciboCtaCte.Edit", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Tesoreria.ReciboCtaCte.GetAll", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Tesoreria.ReciboCtaCte.Update", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Venta.Factura.Add", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Venta.Factura.Delete", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Venta.Factura.Edit", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Venta.Factura.GetAll", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Venta.Factura.Print", 1 },
                    { new Guid("3f2db00c-939b-466b-813a-d01be3ddb836"), "Venta.Factura.Update", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_IdOrganizacion",
                table: "Empresa",
                column: "IdOrganizacion");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaAccount_EmpresaId",
                table: "EmpresaAccount",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_IdProvincia",
                table: "Localidad",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Permiso_IdAccion",
                table: "Permiso",
                column: "IdAccion");

            migrationBuilder.CreateIndex(
                name: "IX_Permiso_IdRecurso",
                table: "Permiso",
                column: "IdRecurso");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_IdOrganizacion",
                table: "Rol",
                column: "IdOrganizacion");

            migrationBuilder.CreateIndex(
                name: "IX_RolAccount_IdAccount",
                table: "RolAccount",
                column: "IdAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comprobante");

            migrationBuilder.DropTable(
                name: "CondIva");

            migrationBuilder.DropTable(
                name: "CondIvaOperacion");

            migrationBuilder.DropTable(
                name: "EmpresaAccount");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Moneda");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "RolAccount");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "TipoFactura");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Accion");

            migrationBuilder.DropTable(
                name: "Recurso");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Organizacion");
        }
    }
}
