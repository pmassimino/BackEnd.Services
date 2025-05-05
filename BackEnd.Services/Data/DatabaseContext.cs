using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Tesoreria;
using Microsoft.Extensions.Logging;
using BackEnd.Services.Data.EFLogging;
using BackEnd.Services.Models.Afip;
using BackEnd.Services.Models.Mail;
using BackEnd.Services.Services.Comun;

namespace BackEnd.Services.Data
{
    public class GestionDBContext : DbContext
    {
        public GestionDBContext(DbContextOptions<GestionDBContext> options)
        : base(options)
        {

        }
        //Almacen
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Familia> Familia { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<MovStock> MovStock { get; set; }

        //Comun

        public DbSet<Sujeto> Sujeto { get; set; }
        public DbSet<TipoRol> TipoRol { get; set; }
        public DbSet<Seccion> Seccion { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<NumeradorDocumento> NumeradorDocumento { get; set; }
        //Ventas
        public DbSet<Factura> Factura { get; set; }
        public DbSet<DetalleFactura> DetalleFactura { get; set; }
        public DbSet<ModeloAsientoFactura> ModeloAsientoFactura { get; set; }
        public DbSet<ConfigFactura> ConfigFactura { get; set; }
        public DbSet<AfipWs> AfipWs { get; set; }
        public DbSet<CertificadoDigital> CertificadoDigital { get; set; }
        public DbSet<PuntoEmision> PuntoEmision { get; set; }


        //Contabilidad
        public DbSet<CuentaMayor> CuentaMayor { get; set; }
        public DbSet<UsoCuentaMayor> UsoCuentaMayor { get; set; }
        public DbSet<TipoCuentaMayor> TipoCuentaMayor { get; set; }
        public DbSet<Mayor> Mayor { get; set; }
        public DbSet<MovCtaCte> MovCtaCte { get; set; }
        public DbSet<LibroIva> LibroIva { get; set; }
        public DbSet<ComprobanteMayor> ComprobanteMayor { get; set; }
        

        //Tesoreria
        public DbSet<ReciboCtaCte> ReciboCtaCte { get; set; }
        public DbSet<ConfigRecibo> ConfigRecibo { get; set; }
        //Core
        public DbSet<Transaccion> Transaccion { get; set; }
        //Mail
        public DbSet<MailServer> MailServer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var lf = new LoggerFactory();
            lf.AddProvider(new MyLoggerProvider());
            optionsBuilder.UseLoggerFactory(lf);
            optionsBuilder.EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Contacto Composite Key
            modelBuilder.Entity<Contacto>()
                .HasKey(e => new { e.Id, e.IdSujeto });
            //Contacto Composite Key
            modelBuilder.Entity<Domicilio>()
                .HasKey(e => new { e.Id, e.IdSujeto });
            //RolSujeto Composite Key
            modelBuilder.Entity<TipoRolSujeto>()
                .HasKey(e => new { e.IdTipoRol, e.IdSujeto });
            //Vehiculo Composite Key
            modelBuilder.Entity<Vehiculo>()
                .HasKey(e => new { e.Id, e.IdSujeto });
            //Detalle Factura Composite Key
            modelBuilder.Entity<DetalleFactura>()
                .HasKey(e => new { e.Id, e.Item });
            //Detalle Iva Composite Key
            modelBuilder.Entity<DetalleIva>()
                .HasKey(e => new { e.Id, e.Item });
            //Detalle Medio Pago Composite Key
            modelBuilder.Entity<MedioPago>()
                .HasKey(e => new { e.Id, e.Item });
            //Detalle Factura Composite Key
            modelBuilder.Entity<DetalleTributos>()
                .HasKey(e => new { e.Id, e.Item });
            //Detalle Factura Composite Key
            modelBuilder.Entity<ComprobanteAsociado>()
                .HasKey(e => new { e.Id, e.Item });
            //Factura Delete cascade
            modelBuilder.Entity<Factura>().HasMany<DetalleFactura>(m => m.Detalle).WithOne(f => f.Factura).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Factura>().HasMany<MedioPago>(m => m.MedioPago).WithOne(f => f.Factura).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Factura>().HasMany<DetalleIva>(m => m.Iva).WithOne(f => f.Factura).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Factura>().HasMany<DetalleTributos>(m => m.Tributos).WithOne(f => f.Factura).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Factura>().HasMany<ComprobanteAsociado>(m => m.ComprobanteAsociado).WithOne(f => f.Factura).OnDelete(DeleteBehavior.Cascade);
            //ConfigFactura  Composite Key
            modelBuilder.Entity<ItemNumerador>()
                .HasKey(e => new { e.Id, e.IdComprobante });
            modelBuilder.Entity<ConfigFactura>().HasMany<ItemNumerador>(m => m.Numeradores).WithOne(C => C.ConfigFactura).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemPuntoEmision>()
                .HasKey(e => new { e.Id, e.IdPuntoEmision });
            modelBuilder.Entity<ConfigFactura>().HasMany<ItemPuntoEmision>(m => m.PuntosEmision).WithOne(C => C.ConfigFactura).OnDelete(DeleteBehavior.Cascade);
            //Comun
            //NumeradoresPuntoVenta  Composite Key
            modelBuilder.Entity<NumeradorPuntoEmision>()
                .HasKey(e => new { e.Id,e.IdNumeradorDocumento});
            modelBuilder.Entity<PuntoEmision>().HasMany<NumeradorPuntoEmision>(m => m.Numeradores).WithOne(C => C.PuntoEmision).OnDelete(DeleteBehavior.Cascade);

            //Contable
            modelBuilder.Entity<DetalleMayor>()
               .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<DetalleRelacion>()
               .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<DetalleValores>()
               .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<DetalleComprobante>()
               .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<ItemIva>()
            .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<ItemTributo>()
            .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<Mayor>().HasMany<DetalleMayor>(m => m.Detalle).WithOne(f => f.Mayor).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LibroIva>().HasMany<ItemIva>(m => m.DetalleIva).WithOne(f => f.LibroIva).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LibroIva>().HasMany<ItemTributo>(m => m.DetalleTributo).WithOne(f => f.LibroIva).OnDelete(DeleteBehavior.Cascade);
            //Tesoreria

            modelBuilder.Entity<DetalleComprobante>()
                .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<DetalleValores>()
                .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<DetalleRelacion>()
                .HasKey(e => new { e.Id, e.Item });
            modelBuilder.Entity<ReciboCtaCte>().HasMany<DetalleComprobante>(m => m.DetalleComprobante).WithOne(f => f.ReciboCtaCte).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ReciboCtaCte>().HasMany<DetalleValores>(m => m.DetalleValores).WithOne(f => f.ReciboCtaCte).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ReciboCtaCte>().HasMany<DetalleRelacion>(m => m.DetalleRelacion).WithOne(f => f.ReciboCtaCte).OnDelete(DeleteBehavior.Cascade);
            //Comprobante
            modelBuilder.Entity<ComprobanteMayor>().HasData(
              new ComprobanteMayor { Id = "00001", Nombre = "General" });

            //Rol
            modelBuilder.Entity<TipoRol>().HasData(
              new TipoRol { Id = "1", Nombre = "CLIENTE" },
              new TipoRol { Id = "2", Nombre = "PROVEEDOR" },
              new TipoRol { Id = "3", Nombre = "PRODUCTOR" },
              new TipoRol { Id = "4", Nombre = "TRANSPORTISTA" },
              new TipoRol { Id = "5", Nombre = "DESTINATARIO" },
              new TipoRol { Id = "6", Nombre = "REPRESENTANTE" },
              new TipoRol { Id = "7", Nombre = "CORREDOR" },
              new TipoRol { Id = "8", Nombre = "ENTREGADOR" },
              new TipoRol { Id = "9", Nombre = "INTERMEDIARIO" });
            //Servicios Afip


            //Numerador Documentos
            modelBuilder.Entity<NumeradorDocumento>().HasData(
              new NumeradorDocumento { Id = "00001", Nombre = "FACTURA A", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00002", Nombre = "NOTA CREDITO A", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00003", Nombre = "NOTA DEBITO A", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00004", Nombre = "FACTURA B", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00005", Nombre = "NOTA CREDITO B", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00006", Nombre = "NOTA DEBITO B", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00007", Nombre = "FACTURA C", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00008", Nombre = "NOTA CREDITO C", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00009", Nombre = "NOTA DEBITO C", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00010", Nombre = "FACTURA M", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00011", Nombre = "NOTA CREDITO M", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00012", Nombre = "NOTA DEBITO M", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00013", Nombre = "FACTURA E", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00014", Nombre = "NOTA CREDITO E", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00015", Nombre = "NOTA DEBITO E", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00016", Nombre = "REMITO", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00017", Nombre = "PRESUPUESTO", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00018", Nombre = "PEDIDO", PuntoEmision = 1, Numero = 0 });
            modelBuilder.Entity<NumeradorDocumento>().HasData(
            new NumeradorDocumento { Id = "00019", Nombre = "RECIBO", PuntoEmision = 1, Numero = 0 });
            //Seccion
            modelBuilder.Entity<Seccion>().HasData(
              new Seccion { Id = "001", Nombre = "GENERAL" });
            //Area
            modelBuilder.Entity<Area>().HasData(
              new Area { Id = "001", Nombre = "GENERAL" });
            //Configuracion Factura
            ConfigFactura configFactura = new ConfigFactura();
            configFactura.Id = "001";
            modelBuilder.Entity<ConfigFactura>().HasData(new ConfigFactura { Id = "001" });
            modelBuilder.Entity<ItemNumerador>().HasData(
                    new ItemNumerador { Id = "001", IdComprobante = 1, IdNumeradorDocumento = "00001" },
                    new ItemNumerador { Id = "001", IdComprobante = 3, IdNumeradorDocumento = "00002"  },
                    new ItemNumerador { Id = "001", IdComprobante = 2, IdNumeradorDocumento = "00003"  },
                    new ItemNumerador { Id = "001", IdComprobante = 6, IdNumeradorDocumento = "00004"  },
                    new ItemNumerador { Id = "001", IdComprobante = 8, IdNumeradorDocumento = "00005"  },
                    new ItemNumerador { Id = "001", IdComprobante = 7, IdNumeradorDocumento = "00006"  },
                    new ItemNumerador { Id = "001", IdComprobante = 11, IdNumeradorDocumento = "00007" },
                    new ItemNumerador { Id = "001", IdComprobante = 13, IdNumeradorDocumento = "00008" },
                    new ItemNumerador { Id = "001", IdComprobante = 12, IdNumeradorDocumento = "00009" },
                    new ItemNumerador { Id = "001", IdComprobante = 51, IdNumeradorDocumento = "00010" },
                    new ItemNumerador { Id = "001", IdComprobante = 53, IdNumeradorDocumento = "00011" },
                    new ItemNumerador { Id = "001", IdComprobante = 52, IdNumeradorDocumento = "00012" },
                    new ItemNumerador { Id = "001", IdComprobante = 19, IdNumeradorDocumento = "00013" },
                    new ItemNumerador { Id = "001", IdComprobante = 21, IdNumeradorDocumento = "00014" },
                    new ItemNumerador { Id = "001", IdComprobante = 20, IdNumeradorDocumento = "00015" });

            //Contabilidad
            modelBuilder.Entity<TipoCuentaMayor>().HasData(
                new TipoCuentaMayor { Id = "1", Nombre = "ACTIVO" },
                new TipoCuentaMayor { Id = "2", Nombre = "PASIVO" },
                new TipoCuentaMayor { Id = "3", Nombre = "PATRIMONIO NETO" },
                new TipoCuentaMayor { Id = "4", Nombre = "RESULTADO DE INGRESOS" },
                new TipoCuentaMayor { Id = "5", Nombre = "RESULTADO DE EGRESOS" },
                new TipoCuentaMayor { Id = "6", Nombre = "ORDEN" });
            modelBuilder.Entity<UsoCuentaMayor>().HasData(
                new UsoCuentaMayor { Id = "1", Nombre = "INTEGRACION" },
                new UsoCuentaMayor { Id = "2", Nombre = "GENERAL" },
                new UsoCuentaMayor { Id = "3", Nombre = "CUENTA CORRIENTE" },
                new UsoCuentaMayor { Id = "4", Nombre = "CAJA" },
                new UsoCuentaMayor { Id = "5", Nombre = "BANCO" },
                new UsoCuentaMayor { Id = "6", Nombre = "CARTERA DE VALORES" },
                new UsoCuentaMayor { Id = "7", Nombre = "LIBRO DE IVA" });
            modelBuilder.Entity<CuentaMayor>().HasData(
                new CuentaMayor { Id = "1000", Nombre = "ACTIVO", IdUso = "1", IdTipo = "1" },
                new CuentaMayor { Id = "1100", Nombre = "DISPONIBILIDADES", IdSuperior = "1000", IdUso = "1", IdTipo = "1" },
                new CuentaMayor { Id = "1111", Nombre = "CAJA", IdSuperior = "1100", IdUso = "4", IdTipo = "1" },
                new CuentaMayor { Id = "1112", Nombre = "MONEDA EXTRANJERA", IdSuperior = "1100", IdUso = "2", IdTipo = "1" },
                new CuentaMayor { Id = "1113", Nombre = "BANCO", IdSuperior = "1100", IdUso = "5", IdTipo = "1" },
                new CuentaMayor { Id = "1114", Nombre = "VALORES A DEPOSITAR", IdSuperior = "1100", IdUso = "6", IdTipo = "1" },
                new CuentaMayor { Id = "1120", Nombre = "CUENTAS POR COBRAR", IdSuperior = "1000", IdUso = "1", IdTipo = "1" },
                new CuentaMayor { Id = "1121", Nombre = "CUENTAS CORRIENTES", IdSuperior = "1120", IdUso = "3", IdTipo = "1" },
                new CuentaMayor { Id = "1122", Nombre = "TARJETA DE CREDITO", IdSuperior = "1120", IdUso = "4", IdTipo = "2" },
                new CuentaMayor { Id = "1123", Nombre = "TARJETA DE DEBITO", IdSuperior = "1120", IdUso = "4", IdTipo = "1" },
                new CuentaMayor { Id = "1140", Nombre = "IVA", IdSuperior = "1000", IdUso = "1", IdTipo = "1" },
                new CuentaMayor { Id = "1141", Nombre = "IVA CREDITO FISCAL 21%", IdSuperior = "1140", IdUso = "7", IdTipo = "1" },
                new CuentaMayor { Id = "1142", Nombre = "IVA CREDITO FISCAL 10.5%", IdSuperior = "1140", IdUso = "7", IdTipo = "1" },
                new CuentaMayor { Id = "1143", Nombre = "IVA CREDITO FISCAL 27%", IdSuperior = "1140", IdUso = "7", IdTipo = "1" },
                new CuentaMayor { Id = "1144", Nombre = "IVA RETENCIÓN", IdSuperior = "1140", IdUso = "2", IdTipo = "1" },
                new CuentaMayor { Id = "1145", Nombre = "IVA PERCEPCIÓN", IdSuperior = "1140", IdUso = "2", IdTipo = "1" },
                new CuentaMayor { Id = "2000", Nombre = "PASIVO", IdUso = "1", IdTipo = "2" },
                new CuentaMayor { Id = "2110", Nombre = "DEUDAS COMERCIALES", IdSuperior = "2000", IdUso = "1", IdTipo = "2" },
                new CuentaMayor { Id = "2111", Nombre = "PROVEEDORES", IdSuperior = "2110", IdUso = "3", IdTipo = "2" },
                new CuentaMayor { Id = "2130", Nombre = "DEUDAS FISCALES", IdSuperior = "2000", IdUso = "1", IdTipo = "2" },
                new CuentaMayor { Id = "2131", Nombre = "IVA DEBITO FISCAL 21%", IdSuperior = "2130", IdUso = "2", IdTipo = "2" },
                new CuentaMayor { Id = "2132", Nombre = "IVA DEBITO FISCAL 10.5%", IdSuperior = "2130", IdUso = "2", IdTipo = "2" },
                new CuentaMayor { Id = "3000", Nombre = "PATRIMONIO NETO", IdUso = "1", IdTipo = "3" },
                new CuentaMayor { Id = "3111", Nombre = "CAPITAL SOCIAL", IdSuperior = "3000", IdUso = "2", IdTipo = "3" },
                new CuentaMayor { Id = "3200", Nombre = "RESULTADOS", IdSuperior = "3000", IdUso = "1", IdTipo = "3" },
                new CuentaMayor { Id = "3220", Nombre = "RESULTADOS DE EJERCICIO", IdSuperior = "3000", IdUso = "2", IdTipo = "3" },
                new CuentaMayor { Id = "4000", Nombre = "INGRESOS", IdUso = "1", IdTipo = "4" },
                new CuentaMayor { Id = "4111", Nombre = "VENTAS", IdSuperior = "4000", IdUso = "2", IdTipo = "4" },
                new CuentaMayor { Id = "4112", Nombre = "IMPUESTO INTERNO VENTAS", IdSuperior = "4000", IdUso = "2", IdTipo = "4" },
                new CuentaMayor { Id = "5000", Nombre = "EGRESOS", IdUso = "1", IdTipo = "5" },
                new CuentaMayor { Id = "5111", Nombre = "COMPRAS", IdSuperior = "5000", IdUso = "2", IdTipo = "5" },
                new CuentaMayor { Id = "5200", Nombre = "GASTOS", IdSuperior = "5000", IdUso = "1", IdTipo = "5" },
                new CuentaMayor { Id = "5211", Nombre = "GASTOS VARIOS", IdSuperior = "5200", IdUso = "2", IdTipo = "5" }
               );
            //Tesoreria
            modelBuilder.Entity<ConfigRecibo>().HasData(
              new ConfigRecibo { Id = "001", IdNumeradorDocumento = "00019" });
            modelBuilder.Entity<ModeloAsientoFactura>().HasData(
              new ModeloAsientoFactura { Id = 1, Nombre = "GENERAL", CtaIngresoDefault = "4111", CtaGastoDefault = "5211", CtaCajaDefault = "1111", CtaIvaGenDefault = "2131", CtaIvaRedDefault = "2132", CtaImpuestoDefault = "4112" });
            modelBuilder.Entity<MovCarteraValor>().HasKey(e => new { e.Id, e.Item });
            //Factura Delete cascade
            modelBuilder.Entity<CarteraValor>().HasMany<MovCarteraValor>(m => m.MovCarteraValor).WithOne(f => f.CarteraValor).OnDelete(DeleteBehavior.Cascade);
            //mail
            modelBuilder.Entity<Mail>().HasMany<Attachment>(m => m.Attachments).WithOne(f => f.Mail).OnDelete(DeleteBehavior.Cascade);
        }
        public class UnitOfWorkDb : IUnitOfWork
        {
            public DbContext Context { get; }

            public UnitOfWorkDb(GestionDBContext context)
            {
                Context = context;
            }
            public void Commit()
            {
                Context.SaveChanges();
            }
            public void Dispose()
            {
                Context.Dispose();

            }

        }



    }
}
