using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEnd.Services.Migrations.GestionDB
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
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
                name: "Familia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    IdFamilia = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibroIva",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEmpresa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSucursal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdArea = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSeccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    CodigoOperacion = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaComp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVenc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdComprobante = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdMoneda = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CotizacionMoneda = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pe = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<long>(type: "INTEGER", nullable: false),
                    IdCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IdTipoDoc = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    NumeroDocumento = table.Column<long>(type: "INTEGER", nullable: false),
                    Origen = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Gravado = table.Column<decimal>(type: "TEXT", nullable: false),
                    Iva = table.Column<decimal>(type: "TEXT", nullable: false),
                    NoGravado = table.Column<decimal>(type: "TEXT", nullable: false),
                    Exento = table.Column<decimal>(type: "TEXT", nullable: false),
                    OtrosTributos = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Autorizado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroIva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mayor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEmpresa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSucursal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdArea = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSeccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaComp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVenc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    IdComprobante = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Pe = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<long>(type: "INTEGER", nullable: false),
                    Origen = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mayor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloAsientoFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CtaIngresoDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaGastoDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaIvaGenDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaIvaRedDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaPerIGDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaPerIvaDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaCajaDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CtaImpuestoDefault = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAsientoFactura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumeradorDocumento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    PuntoEmision = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeradorDocumento", x => x.Id);
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
                name: "Seccion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seccion", x => x.Id);
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
                name: "TipoCuentaMayor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCuentaMayor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoRol",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Owner = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsoCuentaMayor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsoCuentaMayor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IdFamilia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdUnidad = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    CostoVenta = table.Column<decimal>(type: "TEXT", nullable: false),
                    ImpuestoVenta = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "TEXT", nullable: false),
                    AlicuotaIva = table.Column<decimal>(type: "TEXT", nullable: false),
                    CondIva = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    PrecioVentaFinal = table.Column<decimal>(type: "TEXT", nullable: false),
                    MargenVenta = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockMinimo = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockActual = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockReposicion = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockMaximo = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observacion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulo_Familia_IdFamilia",
                        column: x => x.IdFamilia,
                        principalTable: "Familia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemIva",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    CondIva = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    BaseImponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    LibroIvaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemIva", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_ItemIva_LibroIva_LibroIvaId",
                        column: x => x.LibroIvaId,
                        principalTable: "LibroIva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTributo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTributo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BaseImponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tarifa = table.Column<decimal>(type: "TEXT", nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    LibroIvaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTributo", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_ItemTributo_LibroIva_LibroIvaId",
                        column: x => x.LibroIvaId,
                        principalTable: "LibroIva",
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
                name: "ConfigFactura",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    SeccionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigFactura_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigRecibo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdNumeradorDocumento = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigRecibo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigRecibo_NumeradorDocumento_IdNumeradorDocumento",
                        column: x => x.IdNumeradorDocumento,
                        principalTable: "NumeradorDocumento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigRecibo_Seccion_Id",
                        column: x => x.Id,
                        principalTable: "Seccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuentaMayor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IdSuperior = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdUso = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaMayor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentaMayor_TipoCuentaMayor_IdTipo",
                        column: x => x.IdTipo,
                        principalTable: "TipoCuentaMayor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CuentaMayor_UsoCuentaMayor_IdUso",
                        column: x => x.IdUso,
                        principalTable: "UsoCuentaMayor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sujeto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    NombreComercial = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IdTipoDoc = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    NumeroDocumento = table.Column<long>(type: "INTEGER", nullable: false),
                    IdProvincia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdLocalidad = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Domicilio = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Altura = table.Column<decimal>(type: "TEXT", nullable: false),
                    Piso = table.Column<decimal>(type: "TEXT", nullable: false),
                    Oficina = table.Column<decimal>(type: "TEXT", nullable: false),
                    Telefono1 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Telefono2 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Telefono3 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil1 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil2 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil3 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Fax1 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Fax2 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Fax3 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    email1 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    email2 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    email3 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    IdCondicionIva = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdCondicionGanancia = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdCondicionIB = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    NumeroIB = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdCondicionProductor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    LocalidadId = table.Column<string>(type: "TEXT", nullable: true),
                    CondIvaId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sujeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sujeto_CondIva_CondIvaId",
                        column: x => x.CondIvaId,
                        principalTable: "CondIva",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sujeto_Localidad_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemNumerador",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdComprobante = table.Column<int>(type: "INTEGER", nullable: false),
                    IdNumeradorDocumento = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNumerador", x => new { x.Id, x.IdComprobante });
                    table.ForeignKey(
                        name: "FK_ItemNumerador_ConfigFactura_Id",
                        column: x => x.Id,
                        principalTable: "ConfigFactura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemNumerador_NumeradorDocumento_IdNumeradorDocumento",
                        column: x => x.IdNumeradorDocumento,
                        principalTable: "NumeradorDocumento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarteraValor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEmpresa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSucursal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdArea = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSeccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdCuentaMayor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: true),
                    Banco = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Sucursal = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    Obs = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteraValor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarteraValor_CuentaMayor_IdCuentaMayor",
                        column: x => x.IdCuentaMayor,
                        principalTable: "CuentaMayor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarteraValor_Sujeto_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Sujeto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contacto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSujeto = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Telefono1 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Telefono2 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Telefono3 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil1 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil2 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Movil3 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    email1 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    email2 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    email3 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacto", x => new { x.Id, x.IdSujeto });
                    table.ForeignKey(
                        name: "FK_Contacto_Sujeto_IdSujeto",
                        column: x => x.IdSujeto,
                        principalTable: "Sujeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleMayor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaVenc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdCuentaMayor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    IdCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleMayor", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleMayor_CuentaMayor_IdCuentaMayor",
                        column: x => x.IdCuentaMayor,
                        principalTable: "CuentaMayor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleMayor_Mayor_Id",
                        column: x => x.Id,
                        principalTable: "Mayor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleMayor_Sujeto_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Sujeto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Domicilio",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdSujeto = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    idLocalidad = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Altura = table.Column<decimal>(type: "TEXT", nullable: true),
                    CodigoPostal = table.Column<string>(type: "TEXT", nullable: true),
                    CodigoPlanta = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilio", x => new { x.Id, x.IdSujeto });
                    table.ForeignKey(
                        name: "FK_Domicilio_Sujeto_IdSujeto",
                        column: x => x.IdSujeto,
                        principalTable: "Sujeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEmpresa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdSucursal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdArea = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSeccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Letra = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Pe = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<long>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaComp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdMoneda = table.Column<string>(type: "TEXT", nullable: true),
                    CotizacionMoneda = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Cae = table.Column<long>(type: "INTEGER", nullable: false),
                    IdConceptoAfip = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalNeto = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorDescuento = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalDescuento = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalExento = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalGravado = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalNoGravado = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalIva = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalOTributos = table.Column<decimal>(type: "TEXT", nullable: false),
                    Obs = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factura_Sujeto_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Sujeto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovCtaCte",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEmpresa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSucursal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdArea = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSeccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdCuentaMayor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdComprobante = table.Column<string>(type: "TEXT", nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaComp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVenc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pe = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<long>(type: "INTEGER", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", nullable: true),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    Origen = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovCtaCte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovCtaCte_CuentaMayor_IdCuentaMayor",
                        column: x => x.IdCuentaMayor,
                        principalTable: "CuentaMayor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovCtaCte_Sujeto_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Sujeto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReciboCtaCte",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEmpresa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSucursal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdArea = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSeccion = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdCuentaMayor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pe = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTipo = table.Column<string>(type: "TEXT", nullable: true),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    Obs = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReciboCtaCte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReciboCtaCte_CuentaMayor_IdCuentaMayor",
                        column: x => x.IdCuentaMayor,
                        principalTable: "CuentaMayor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReciboCtaCte_Sujeto_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Sujeto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TipoRolSujeto",
                columns: table => new
                {
                    IdSujeto = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdTipoRol = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRolSujeto", x => new { x.IdTipoRol, x.IdSujeto });
                    table.ForeignKey(
                        name: "FK_TipoRolSujeto_Sujeto_IdSujeto",
                        column: x => x.IdSujeto,
                        principalTable: "Sujeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoRolSujeto_TipoRol_IdTipoRol",
                        column: x => x.IdTipoRol,
                        principalTable: "TipoRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdSujeto = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    NombreChofer = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    NumeroDocumento = table.Column<decimal>(type: "TEXT", nullable: false),
                    PatenteChasis = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    PatenteAcoplado = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => new { x.Id, x.IdSujeto });
                    table.ForeignKey(
                        name: "FK_Vehiculo_Sujeto_IdSujeto",
                        column: x => x.IdSujeto,
                        principalTable: "Sujeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovCarteraValor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    IdTransaccion = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Concepto = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovCarteraValor", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_MovCarteraValor_CarteraValor_Id",
                        column: x => x.Id,
                        principalTable: "CarteraValor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFactura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdArticulo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdUnidadMedida = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorBonificacion = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bonificacion = table.Column<decimal>(type: "TEXT", nullable: false),
                    Gravado = table.Column<decimal>(type: "TEXT", nullable: false),
                    CondIva = table.Column<string>(type: "TEXT", nullable: true),
                    Iva = table.Column<decimal>(type: "TEXT", nullable: false),
                    NoGravado = table.Column<decimal>(type: "TEXT", nullable: false),
                    Exento = table.Column<decimal>(type: "TEXT", nullable: false),
                    OtroTributo = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Lote = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Serie = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    FacturaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFactura", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Articulo_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleIva",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    CondIva = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    BaseImponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    FacturaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleIva", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleIva_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleTributos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTributo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BaseImponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tarifa = table.Column<decimal>(type: "TEXT", nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    FacturaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleTributos", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleTributos_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedioPago",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCuentaMayor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Concepto = table.Column<string>(type: "TEXT", nullable: true),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaVenc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FacturaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedioPago", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_MedioPago_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleComprobante",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    IdTipoComp = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    IdMovCtaCte = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pe = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReciboCtaCteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleComprobante", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleComprobante_ReciboCtaCte_ReciboCtaCteId",
                        column: x => x.ReciboCtaCteId,
                        principalTable: "ReciboCtaCte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleRelacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMovCtaCte = table.Column<Guid>(type: "TEXT", maxLength: 10, nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReciboCtaCteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleRelacion", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleRelacion_ReciboCtaCte_ReciboCtaCteId",
                        column: x => x.ReciboCtaCteId,
                        principalTable: "ReciboCtaCte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleValores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Item = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTipo = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    IdCuentaMayor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdComp = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IdCarteraValor = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    Banco = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Sucursal = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    ReciboCtaCteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleValores", x => new { x.Id, x.Item });
                    table.ForeignKey(
                        name: "FK_DetalleValores_CuentaMayor_IdCuentaMayor",
                        column: x => x.IdCuentaMayor,
                        principalTable: "CuentaMayor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetalleValores_ReciboCtaCte_ReciboCtaCteId",
                        column: x => x.ReciboCtaCteId,
                        principalTable: "ReciboCtaCte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "001", "GENERAL" });

            migrationBuilder.InsertData(
                table: "ConfigFactura",
                columns: new[] { "Id", "SeccionId" },
                values: new object[] { "001", null });

            migrationBuilder.InsertData(
                table: "ModeloAsientoFactura",
                columns: new[] { "Id", "CtaCajaDefault", "CtaGastoDefault", "CtaImpuestoDefault", "CtaIngresoDefault", "CtaIvaGenDefault", "CtaIvaRedDefault", "CtaPerIGDefault", "CtaPerIvaDefault", "Nombre" },
                values: new object[] { 1, "1111", "5211", "4112", "4111", "2131", "2132", null, null, "GENERAL" });

            migrationBuilder.InsertData(
                table: "NumeradorDocumento",
                columns: new[] { "Id", "Nombre", "Numero", "PuntoEmision" },
                values: new object[,]
                {
                    { "00001", "FACTURA A", 0, 1 },
                    { "00002", "NOTA CREDITO A", 0, 1 },
                    { "00003", "NOTA DEBITO A", 0, 1 },
                    { "00004", "FACTURA B", 0, 1 },
                    { "00005", "NOTA CREDITO B", 0, 1 },
                    { "00006", "NOTA DEBITO B", 0, 1 },
                    { "00007", "FACTURA C", 0, 1 },
                    { "00008", "NOTA CREDITO C", 0, 1 },
                    { "00009", "NOTA DEBITO C", 0, 1 },
                    { "00010", "FACTURA M", 0, 1 },
                    { "00011", "NOTA CREDITO M", 0, 1 },
                    { "00012", "NOTA DEBITO M", 0, 1 },
                    { "00013", "FACTURA E", 0, 1 },
                    { "00014", "NOTA CREDITO E", 0, 1 },
                    { "00015", "NOTA DEBITO E", 0, 1 },
                    { "00016", "REMITO", 0, 1 },
                    { "00017", "PRESUPUESTO", 0, 1 },
                    { "00018", "PEDIDO", 0, 1 },
                    { "00019", "RECIBO", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Seccion",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "001", "GENERAL" });

            migrationBuilder.InsertData(
                table: "TipoCuentaMayor",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { "1", "ACTIVO" },
                    { "2", "PASIVO" },
                    { "3", "PATRIMONIO NETO" },
                    { "4", "RESULTADO DE INGRESOS" },
                    { "5", "RESULTADO DE EGRESOS" },
                    { "6", "ORDEN" }
                });

            migrationBuilder.InsertData(
                table: "TipoRol",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { "1", "CLIENTE" },
                    { "2", "PROVEEDOR" },
                    { "3", "PRODUCTOR" },
                    { "4", "TRANSPORTISTA" },
                    { "5", "DESTINATARIO" },
                    { "6", "REPRESENTANTE" },
                    { "7", "CORREDOR" },
                    { "8", "ENTREGADOR" },
                    { "9", "INTERMEDIARIO" }
                });

            migrationBuilder.InsertData(
                table: "UsoCuentaMayor",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { "1", "INTEGRACION" },
                    { "2", "GENERAL" },
                    { "3", "CUENTA CORRIENTE" },
                    { "4", "CAJA" },
                    { "5", "BANCO" },
                    { "6", "CARTERA DE VALORES" },
                    { "7", "LIBRO DE IVA" }
                });

            migrationBuilder.InsertData(
                table: "ConfigRecibo",
                columns: new[] { "Id", "IdNumeradorDocumento" },
                values: new object[] { "001", "00019" });

            migrationBuilder.InsertData(
                table: "CuentaMayor",
                columns: new[] { "Id", "IdSuperior", "IdTipo", "IdUso", "Nombre" },
                values: new object[,]
                {
                    { "1000", null, "1", "1", "ACTIVO" },
                    { "1100", "1000", "1", "1", "DISPONIBILIDADES" },
                    { "1111", "1100", "1", "4", "CAJA" },
                    { "1112", "1100", "1", "2", "MONEDA EXTRANJERA" },
                    { "1113", "1100", "1", "5", "BANCO" },
                    { "1114", "1100", "1", "6", "VALORES A DEPOSITAR" },
                    { "1120", "1000", "1", "1", "CUENTAS POR COBRAR" },
                    { "1121", "1120", "1", "3", "CUENTAS CORRIENTES" },
                    { "1122", "1120", "2", "4", "TARJETA DE CREDITO" },
                    { "1123", "1120", "1", "4", "TARJETA DE DEBITO" },
                    { "1140", "1000", "1", "1", "IVA" },
                    { "1141", "1140", "1", "7", "IVA CREDITO FISCAL 21%" },
                    { "1142", "1140", "1", "7", "IVA CREDITO FISCAL 10.5%" },
                    { "1143", "1140", "1", "7", "IVA CREDITO FISCAL 27%" },
                    { "1144", "1140", "1", "2", "IVA RETENCIÓN" },
                    { "1145", "1140", "1", "2", "IVA PERCEPCIÓN" },
                    { "2000", null, "2", "1", "PASIVO" },
                    { "2110", "2000", "2", "1", "DEUDAS COMERCIALES" },
                    { "2111", "2110", "2", "3", "PROVEEDORES" },
                    { "2130", "2000", "2", "1", "DEUDAS FISCALES" },
                    { "2131", "2130", "2", "2", "IVA DEBITO FISCAL 21%" },
                    { "2132", "2130", "2", "2", "IVA DEBITO FISCAL 10.5%" },
                    { "3000", null, "3", "1", "PATRIMONIO NETO" },
                    { "3111", "3000", "3", "2", "CAPITAL SOCIAL" },
                    { "3200", "3000", "3", "1", "RESULTADOS" },
                    { "3220", "3000", "3", "2", "RESULTADOS DE EJERCICIO" },
                    { "4000", null, "4", "1", "INGRESOS" },
                    { "4111", "4000", "4", "2", "VENTAS" },
                    { "4112", "4000", "4", "2", "IMPUESTO INTERNO VENTAS" },
                    { "5000", null, "5", "1", "EGRESOS" },
                    { "5111", "5000", "5", "2", "COMPRAS" },
                    { "5200", "5000", "5", "1", "GASTOS" },
                    { "5211", "5200", "5", "2", "GASTOS VARIOS" }
                });

            migrationBuilder.InsertData(
                table: "ItemNumerador",
                columns: new[] { "Id", "IdComprobante", "IdNumeradorDocumento" },
                values: new object[,]
                {
                    { "001", 1, "00001" },
                    { "001", 2, "00003" },
                    { "001", 3, "00002" },
                    { "001", 6, "00004" },
                    { "001", 7, "00006" },
                    { "001", 8, "00005" },
                    { "001", 11, "00007" },
                    { "001", 12, "00009" },
                    { "001", 13, "00008" },
                    { "001", 19, "00013" },
                    { "001", 20, "00015" },
                    { "001", 21, "00014" },
                    { "001", 51, "00010" },
                    { "001", 52, "00012" },
                    { "001", 53, "00011" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdFamilia",
                table: "Articulo",
                column: "IdFamilia");

            migrationBuilder.CreateIndex(
                name: "IX_CarteraValor_IdCuenta",
                table: "CarteraValor",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_CarteraValor_IdCuentaMayor",
                table: "CarteraValor",
                column: "IdCuentaMayor");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigFactura_SeccionId",
                table: "ConfigFactura",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigRecibo_IdNumeradorDocumento",
                table: "ConfigRecibo",
                column: "IdNumeradorDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Contacto_IdSujeto",
                table: "Contacto",
                column: "IdSujeto");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaMayor_IdTipo",
                table: "CuentaMayor",
                column: "IdTipo");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaMayor_IdUso",
                table: "CuentaMayor",
                column: "IdUso");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleComprobante_ReciboCtaCteId",
                table: "DetalleComprobante",
                column: "ReciboCtaCteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_FacturaId",
                table: "DetalleFactura",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdArticulo",
                table: "DetalleFactura",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleIva_FacturaId",
                table: "DetalleIva",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleMayor_IdCuenta",
                table: "DetalleMayor",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleMayor_IdCuentaMayor",
                table: "DetalleMayor",
                column: "IdCuentaMayor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRelacion_ReciboCtaCteId",
                table: "DetalleRelacion",
                column: "ReciboCtaCteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTributos_FacturaId",
                table: "DetalleTributos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleValores_IdCuentaMayor",
                table: "DetalleValores",
                column: "IdCuentaMayor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleValores_ReciboCtaCteId",
                table: "DetalleValores",
                column: "ReciboCtaCteId");

            migrationBuilder.CreateIndex(
                name: "IX_Domicilio_IdSujeto",
                table: "Domicilio",
                column: "IdSujeto");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdCuenta",
                table: "Factura",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIva_LibroIvaId",
                table: "ItemIva",
                column: "LibroIvaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNumerador_IdNumeradorDocumento",
                table: "ItemNumerador",
                column: "IdNumeradorDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTributo_LibroIvaId",
                table: "ItemTributo",
                column: "LibroIvaId");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_IdProvincia",
                table: "Localidad",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_MedioPago_FacturaId",
                table: "MedioPago",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovCtaCte_IdCuenta",
                table: "MovCtaCte",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_MovCtaCte_IdCuentaMayor",
                table: "MovCtaCte",
                column: "IdCuentaMayor");

            migrationBuilder.CreateIndex(
                name: "IX_ReciboCtaCte_IdCuenta",
                table: "ReciboCtaCte",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_ReciboCtaCte_IdCuentaMayor",
                table: "ReciboCtaCte",
                column: "IdCuentaMayor");

            migrationBuilder.CreateIndex(
                name: "IX_Sujeto_CondIvaId",
                table: "Sujeto",
                column: "CondIvaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sujeto_LocalidadId",
                table: "Sujeto",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoRolSujeto_IdSujeto",
                table: "TipoRolSujeto",
                column: "IdSujeto");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_IdSujeto",
                table: "Vehiculo",
                column: "IdSujeto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "ConfigRecibo");

            migrationBuilder.DropTable(
                name: "Contacto");

            migrationBuilder.DropTable(
                name: "DetalleComprobante");

            migrationBuilder.DropTable(
                name: "DetalleFactura");

            migrationBuilder.DropTable(
                name: "DetalleIva");

            migrationBuilder.DropTable(
                name: "DetalleMayor");

            migrationBuilder.DropTable(
                name: "DetalleRelacion");

            migrationBuilder.DropTable(
                name: "DetalleTributos");

            migrationBuilder.DropTable(
                name: "DetalleValores");

            migrationBuilder.DropTable(
                name: "Domicilio");

            migrationBuilder.DropTable(
                name: "ItemIva");

            migrationBuilder.DropTable(
                name: "ItemNumerador");

            migrationBuilder.DropTable(
                name: "ItemTributo");

            migrationBuilder.DropTable(
                name: "MedioPago");

            migrationBuilder.DropTable(
                name: "ModeloAsientoFactura");

            migrationBuilder.DropTable(
                name: "MovCarteraValor");

            migrationBuilder.DropTable(
                name: "MovCtaCte");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "TipoRolSujeto");

            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Mayor");

            migrationBuilder.DropTable(
                name: "ReciboCtaCte");

            migrationBuilder.DropTable(
                name: "ConfigFactura");

            migrationBuilder.DropTable(
                name: "NumeradorDocumento");

            migrationBuilder.DropTable(
                name: "LibroIva");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "CarteraValor");

            migrationBuilder.DropTable(
                name: "TipoRol");

            migrationBuilder.DropTable(
                name: "Familia");

            migrationBuilder.DropTable(
                name: "Seccion");

            migrationBuilder.DropTable(
                name: "CuentaMayor");

            migrationBuilder.DropTable(
                name: "Sujeto");

            migrationBuilder.DropTable(
                name: "TipoCuentaMayor");

            migrationBuilder.DropTable(
                name: "UsoCuentaMayor");

            migrationBuilder.DropTable(
                name: "CondIva");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Provincia");
        }
    }
}
