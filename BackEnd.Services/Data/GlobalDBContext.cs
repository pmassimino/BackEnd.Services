using Microsoft.EntityFrameworkCore;
using BackEnd.Services.Models.Global;
using System;
using System.Text;
using System.Security.Cryptography;



namespace BackEnd.Services.Data
{
    public class GlobalDBContext: DbContext
    {
        public GlobalDBContext(DbContextOptions<GlobalDBContext> options)
       : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //Unidad de Medida
            modelBuilder.Entity<UnidadMedida>().HasData(
            new UnidadMedida { Id = "001", Nombre = "KILOGRAMO", CodAfip = "001" },
            new UnidadMedida { Id = "002", Nombre = "METROS", CodAfip = "002" },
            new UnidadMedida { Id = "003", Nombre = "METRO CUADRADO", CodAfip = "003" },
            new UnidadMedida { Id = "004", Nombre = "METRO CUBICO", CodAfip = "004" },
            new UnidadMedida { Id = "005", Nombre = "LITROS", CodAfip = "005" },
            new UnidadMedida { Id = "006", Nombre = "1000 KILOWATT HORA", CodAfip = "006" },
            new UnidadMedida { Id = "007", Nombre = "UNIDAD", CodAfip = "007" },
            new UnidadMedida { Id = "014", Nombre = "GRAMO", CodAfip = "014" },
            new UnidadMedida { Id = "017", Nombre = "KILOMETRO", CodAfip = "017" },
            new UnidadMedida { Id = "020", Nombre = "CENTIMETRO", CodAfip = "020" },
            new UnidadMedida { Id = "029", Nombre = "TONELADA", CodAfip = "029" });
            //Condicion Iva Operacion
            modelBuilder.Entity<CondIvaOperacion>().HasData(
            new CondIvaOperacion { Id = "001", Nombre = "No Gravado", Alicuota = 0, CodAfip = "1" },
            new CondIvaOperacion { Id = "002", Nombre = "Exento", Alicuota = 0, CodAfip = "2" },
            new CondIvaOperacion { Id = "003", Nombre = "0%", Alicuota = 0, CodAfip = "3" },
            new CondIvaOperacion { Id = "004", Nombre = "10.5%", Alicuota = Convert.ToDecimal(10.5), CodAfip = "4" },
            new CondIvaOperacion { Id = "005", Nombre = "21%", Alicuota = Convert.ToDecimal(21), CodAfip = "5" },
            new CondIvaOperacion { Id = "006", Nombre = "27%", Alicuota = Convert.ToDecimal(27), CodAfip = "6" },
            new CondIvaOperacion { Id = "008", Nombre = "5%", Alicuota = Convert.ToDecimal(5), CodAfip = "8" },
            new CondIvaOperacion { Id = "009", Nombre = "2.5%", Alicuota = Convert.ToDecimal(2.5), CodAfip = "9" });
            //Tipo Documento
            modelBuilder.Entity<TipoDocumento>().HasData(
            new TipoDocumento { Id = "80", Nombre = "CUIT", CodAfip = "80" },
            new TipoDocumento { Id = "86", Nombre = "CUIL", CodAfip = "86" },
            new TipoDocumento { Id = "87", Nombre = "CDI", CodAfip = "87" },
            new TipoDocumento { Id = "89", Nombre = "LIBRETA DE ENROLAMIENTO", CodAfip = "89" },
            new TipoDocumento { Id = "90", Nombre = "LIBRETA CIVICA", CodAfip = "90" },
            new TipoDocumento { Id = "96", Nombre = "DNI", CodAfip = "96" });
            //Cond Iva
            modelBuilder.Entity<CondIva>().HasData(
            new CondIva { Id = "01", Nombre = "RESPONSABLE INSCRIPTO", CodAfip = "1" },
            new CondIva { Id = "O3", Nombre = "CONSUMIDOR FINAL", CodAfip = "3" },
            new CondIva { Id = "04", Nombre = "EXENTO", CodAfip = "4" },
            new CondIva { Id = "05", Nombre = "RESPONSABLE MONOTRIBUTO", CodAfip = "5" },
            new CondIva { Id = "06", Nombre = "NO CATEGORIZADO", CodAfip = "6" },
            new CondIva { Id = "08", Nombre = "EXPORTACION", CodAfip = "8" });
            //Tipo Factura
            modelBuilder.Entity<TipoFactura>().HasData(
            new TipoFactura { Id = "1", Nombre = "FACTURA", CodAfip = "1" },
            new TipoFactura { Id = "2", Nombre = "NOTA DE CREDITO", CodAfip = "2" },
            new TipoFactura { Id = "3", Nombre = "NOTA DE DEBITO", CodAfip = "3" });

            //Comprobantes
            modelBuilder.Entity<Comprobante>().HasData(
           new Comprobante { Id = 1, Nombre = "FACTURA A", CodAfip = "1" },
           new Comprobante { Id = 2, Nombre = "NOTAS DE DEBITO A", CodAfip = "2" },
           new Comprobante { Id = 3, Nombre = "NOTAS DE CREDITO A", CodAfip = "3" },
           new Comprobante { Id = 4, Nombre = "RECIBOS A", CodAfip = "4" },
           new Comprobante { Id = 5, Nombre = "NOTAS DE VENTA AL CONTADO A", CodAfip = "5" },
           new Comprobante { Id = 6, Nombre = "FACTURA B", CodAfip = "6" },
           new Comprobante { Id = 7, Nombre = "NOTAS DE DEBITO B", CodAfip = "5" },
           new Comprobante { Id = 8, Nombre = "NOTAS DE CREDITO B", CodAfip = "8" },
           new Comprobante { Id = 9, Nombre = "RECIBOS B", CodAfip = "9" },
           new Comprobante { Id = 10, Nombre = "NOTAS DE VENTA AL CONTADO B", CodAfip = "10" },
           new Comprobante { Id = 11, Nombre = "FACTURA C", CodAfip = "11" },
           new Comprobante { Id = 12, Nombre = "NOTAS DE DEBITO C", CodAfip = "12" },
           new Comprobante { Id = 13, Nombre = "NOTAS DE CREDITO C", CodAfip = "13" },
           new Comprobante { Id = 15, Nombre = "RECIBOS C", CodAfip = "15" },
           new Comprobante { Id = 16, Nombre = "NOTAS DE VENTA AL CONTADO C", CodAfip = "16" },
           new Comprobante { Id = 17, Nombre = "LIQUIDACION DE SERVICIOS PUBLICOS CLASE A", CodAfip = "17" },
           new Comprobante { Id = 18, Nombre = "LIQUIDACION DE SERVICIOS PUBLICOS CLASE B", CodAfip = "18" },
           new Comprobante { Id = 19, Nombre = "FACTURAS DE EXPORTACION", CodAfip = "19" },
           new Comprobante { Id = 20, Nombre = "NOTAS DE DEBITO POR OPERACIONES CON EL EXTERIOR", CodAfip = "20" },
           new Comprobante { Id = 21, Nombre = "NOTAS DE CREDITO POR OPERACIONES CON EL EXTERIOR", CodAfip = "21" },
           new Comprobante { Id = 22, Nombre = "FACTURAS - PERMISO EXPORTACION SIMPLIFICADO - DTO. 855/97", CodAfip = "23" },
           new Comprobante { Id = 24, Nombre = "COMPROBANTES “A” DE CONSIGNACION PRIMARIA PARA EL SECTOR PESQUERO MARITIMO", CodAfip = "24" },
           new Comprobante { Id = 25, Nombre = "COMPROBANTES “B” DE COMPRA PRIMARIA PARA EL SECTOR PESQUERO MARITIMO", CodAfip = "25" },
           new Comprobante { Id = 26, Nombre = "COMPROBANTES “B” DE CONSIGNACION PRIMARIA PARA EL SECTOR PESQUERO MARITIMO", CodAfip = "26" },
           new Comprobante { Id = 27, Nombre = "LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE A", CodAfip = "27" },
           new Comprobante { Id = 28, Nombre = "LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE B", CodAfip = "28" },
           new Comprobante { Id = 29, Nombre = "LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE C", CodAfip = "29" },
           new Comprobante { Id = 30, Nombre = "COMPROBANTES DE COMPRA DE BIENES USADOS", CodAfip = "30" },
           new Comprobante { Id = 31, Nombre = "MANDATO - CONSIGNACION", CodAfip = "31" },
           new Comprobante { Id = 32, Nombre = "COMPROBANTES PARA RECICLAR MATERIALES", CodAfip = "32" },
           new Comprobante { Id = 33, Nombre = "LIQUIDACION PRIMARIA DE GRANOS", CodAfip = "33" },
           new Comprobante { Id = 34, Nombre = "COMPROBANTES A DEL APARTADO A  INCISO F)  R.G. N°  1415", CodAfip = "34" },
           new Comprobante { Id = 35, Nombre = "COMPROBANTES B DEL ANEXO I, APARTADO A, INC. F), R.G. N° 1415", CodAfip = "35" },
           new Comprobante { Id = 36, Nombre = "COMPROBANTES C DEL Anexo I, Apartado A, INC.F), R.G. N° 1415", CodAfip = "36" },
           new Comprobante { Id = 37, Nombre = "NOTAS DE DEBITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415", CodAfip = "37" },
           new Comprobante { Id = 38, Nombre = "NOTAS DE CREDITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415", CodAfip = "38" },
           new Comprobante { Id = 39, Nombre = "OTROS COMPROBANTES A QUE CUMPLEN CON LA R G  1415", CodAfip = "39" },
           new Comprobante { Id = 40, Nombre = "OTROS COMPROBANTES B QUE CUMPLAN CON LA R.G. N° 1415", CodAfip = "40" },
           new Comprobante { Id = 41, Nombre = "OTROS COMPROBANTES C QUE CUMPLAN CON LA R.G. N° 1415", CodAfip = "41" },
           new Comprobante { Id = 43, Nombre = "NOTA DE CREDITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE B", CodAfip = "43" },
           new Comprobante { Id = 44, Nombre = "NOTA DE CREDITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE C", CodAfip = "44" },
           new Comprobante { Id = 45, Nombre = "NOTA DE DEBITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE A", CodAfip = "45" },
           new Comprobante { Id = 46, Nombre = "NOTA DE DEBITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE B", CodAfip = "46" },
           new Comprobante { Id = 47, Nombre = "NOTA DE DEBITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE C", CodAfip = "47" },
           new Comprobante { Id = 48, Nombre = "NOTA DE CREDITO LIQUIDACION UNICA COMERCIAL IMPOSITIVA CLASE A", CodAfip = "48" },
           new Comprobante { Id = 49, Nombre = "COMPROBANTES DE COMPRA DE BIENES NO REGISTRABLES A CONSUMIDORES FINALES", CodAfip = "49" },
           new Comprobante { Id = 50, Nombre = "RECIBO FACTURA A  REGIMEN DE FACTURA DE CREDITO ", CodAfip = "50" },
           new Comprobante { Id = 51, Nombre = "FACTURAS M", CodAfip = "51" },
           new Comprobante { Id = 52, Nombre = "NOTAS DE DEBITO M", CodAfip = "52" },
           new Comprobante { Id = 53, Nombre = "NOTAS DE CREDITO M", CodAfip = "53" },
           new Comprobante { Id = 54, Nombre = "RECIBOS M", CodAfip = "54" },
           new Comprobante { Id = 55, Nombre = "NOTAS DE VENTA AL CONTADO M", CodAfip = "55" },
           new Comprobante { Id = 56, Nombre = "COMPROBANTES M DEL ANEXO I  APARTADO A  INC F) R.G. N° 1415", CodAfip = "56" },
           new Comprobante { Id = 57, Nombre = "OTROS COMPROBANTES M QUE CUMPLAN CON LA R.G. N° 1415", CodAfip = "57" },
           new Comprobante { Id = 58, Nombre = "CUENTAS DE VENTA Y LIQUIDO PRODUCTO M", CodAfip = "58" },
           new Comprobante { Id = 59, Nombre = "LIQUIDACIONES M", CodAfip = "59" },
           new Comprobante { Id = 60, Nombre = "CUENTAS DE VENTA Y LIQUIDO PRODUCTO A", CodAfip = "60" },
           new Comprobante { Id = 61, Nombre = "CUENTAS DE VENTA Y LIQUIDO PRODUCTO B", CodAfip = "61" },
           new Comprobante { Id = 63, Nombre = "LIQUIDACIONES A", CodAfip = "63" },
           new Comprobante { Id = 64, Nombre = "LIQUIDACIONES B", CodAfip = "64" },
           new Comprobante { Id = 66, Nombre = "DESPACHO DE IMPORTACION", CodAfip = "66" },
           new Comprobante { Id = 68, Nombre = "LIQUIDACION C", CodAfip = "68" },
           new Comprobante { Id = 70, Nombre = "RECIBOS FACTURA DE CREDITO", CodAfip = "70" },
           new Comprobante { Id = 80, Nombre = "INFORME DIARIO DE CIERRE (ZETA) - CONTROLADORES FISCALES", CodAfip = "80" },
           new Comprobante { Id = 81, Nombre = "TIQUE FACTURA A", CodAfip = "81" },
           new Comprobante { Id = 82, Nombre = "TIQUE FACTURA B", CodAfip = "82" },
           new Comprobante { Id = 83, Nombre = "TIQUE", CodAfip = "83" },
           new Comprobante { Id = 88, Nombre = "REMITO ELECTRONICO", CodAfip = "88" },
           new Comprobante { Id = 89, Nombre = "RESUMEN DE DATOS", CodAfip = "89" },
           new Comprobante { Id = 90, Nombre = "OTROS COMPROBANTES - DOCUMENTOS EXCEPTUADOS - NOTAS DE CREDITO", CodAfip = "90" },
           new Comprobante { Id = 91, Nombre = "REMITOS R", CodAfip = "91" },
           new Comprobante { Id = 99, Nombre = "OTROS COMPROBANTES QUE NO CUMPLEN O ESTÁN EXCEPTUADOS DE LA R.G. 1415 Y SUS MODIF ", CodAfip = "99" },
           new Comprobante { Id = 110, Nombre = "TIQUE NOTA DE CREDITO ", CodAfip = "110" },
           new Comprobante { Id = 111, Nombre = "TIQUE FACTURA C", CodAfip = "111" },
           new Comprobante { Id = 112, Nombre = " TIQUE NOTA DE CREDITO A", CodAfip = "112" },
           new Comprobante { Id = 113, Nombre = "TIQUE NOTA DE CREDITO B", CodAfip = "113" },
           new Comprobante { Id = 114, Nombre = "TIQUE NOTA DE CREDITO CB", CodAfip = "114" },
           new Comprobante { Id = 115, Nombre = "TIQUE NOTA DE DEBITO A", CodAfip = "115" },
           new Comprobante { Id = 116, Nombre = "TIQUE NOTA DE DEBITO B", CodAfip = "116" },
           new Comprobante { Id = 117, Nombre = "TIQUE NOTA DE DEBITO C", CodAfip = "117" },
           new Comprobante { Id = 118, Nombre = "TIQUE FACTURA M", CodAfip = "118" },
           new Comprobante { Id = 119, Nombre = "TIQUE NOTA DE CREDITO M", CodAfip = "119" },
           new Comprobante { Id = 120, Nombre = "TIQUE NOTA DE DEBITO M", CodAfip = "120" },
           new Comprobante { Id = 201, Nombre = "FACTURA DE CRÉDITO ELECTRÓNICA MiPyMEs (FCE) A", CodAfip = "201" },
           new Comprobante { Id = 202, Nombre = "NOTA DE DEBITO ELECTRÓNICA MiPyMEs (FCE) A", CodAfip = "202" },
           new Comprobante { Id = 203, Nombre = "NOTA DE CREDITO ELECTRÓNICA MiPyMEs (FCE) A", CodAfip = "203" },
           new Comprobante { Id = 206, Nombre = "FACTURA DE CRÉDITO ELECTRÓNICA MiPyMEs (FCE) B", CodAfip = "206" },
           new Comprobante { Id = 207, Nombre = "NOTA DE DEBITO ELECTRÓNICA MiPyMEs (FCE) B", CodAfip = "207" },
           new Comprobante { Id = 208, Nombre = "NOTA DE CREDITO ELECTRÓNICA MiPyMEs (FCE) B", CodAfip = "208" },
           new Comprobante { Id = 211, Nombre = "FACTURA DE CRÉDITO ELECTRÓNICA MiPyMEs (FCE) C", CodAfip = "211" },
           new Comprobante { Id = 212, Nombre = "NOTA DE DEBITO ELECTRÓNICA MiPyMEs (FCE) C", CodAfip = "212" },
           new Comprobante { Id = 213, Nombre = "NOTA DE CREDITO ELECTRÓNICA MiPyMEs (FCE) C", CodAfip = "213" },
           new Comprobante { Id = 331, Nombre = "LIQUIDACION SECUNDARIA DE GRANOS", CodAfip = "331" },
           new Comprobante { Id = 332, Nombre = "CERTIFICACION ELECTRONICA (GRANOS)", CodAfip = "332" },
           new Comprobante { Id = 995, Nombre = "REMITO ELECTRÓNICO CÁRNICO", CodAfip = "995" });

            //Provincia
            //Tipo Documento
            modelBuilder.Entity<Provincia>().HasData(
            new Provincia { Id = "00", Nombre = "CIUDAD AUTÓNOMA BUENOS AIRES"},
            new Provincia { Id = "01", Nombre = "BUENOS AIRES" },
            new Provincia { Id = "02", Nombre = " CATAMARCA" },
            new Provincia { Id = "03", Nombre = "CÓRDOBA" },
            new Provincia { Id = "04", Nombre = "CORRIENTES" },
            new Provincia { Id = "05", Nombre = "ENTRE RIOS" },
            new Provincia { Id = "06", Nombre = "JUJUY" },
            new Provincia { Id = "07", Nombre = "MENDOZA" },
            new Provincia { Id = "08", Nombre = "LA RIOJA" },
            new Provincia { Id = "09", Nombre = "SALTA" },
            new Provincia { Id = "10", Nombre = "SAN JUAN" },
            new Provincia { Id = "11", Nombre = "SAN LUIS"},
            new Provincia { Id = "12", Nombre = "SANTA FE" },
            new Provincia { Id = "13", Nombre = "SANTIAGO DEL ESTERO" },
            new Provincia { Id = "14", Nombre = "TUCUMÁN" },
            new Provincia { Id = "16", Nombre = "CHACO" },
            new Provincia { Id = "17", Nombre = "CHUBUT" },
            new Provincia { Id = "18", Nombre = "FORMOSA" },
            new Provincia { Id = "19", Nombre = "MISIONES" },
            new Provincia { Id = "20", Nombre = "NEUQUÉN" },
            new Provincia { Id = "21", Nombre = "LA PAMPA" },
            new Provincia { Id = "22", Nombre = "RIO NEGRO" },
            new Provincia { Id = "23", Nombre = "SANTA CRUZ" },
            new Provincia { Id = "24", Nombre = "TIERRA DEL FUEGO" });
            //Moneda
            modelBuilder.Entity<Provincia>().HasData(new Moneda { Id = "PES", Nombre="PESOS" }, new Moneda { Id = "DOL", Nombre="Dólar ESTADOUNIDENSE" });

            //Organizacion
            Guid idOrganizacion = Guid.NewGuid();
            modelBuilder.Entity<Organizacion>().HasData(new Organizacion { Id = idOrganizacion, Nombre = "Organizacion General" });
            //Empresa
            Guid idEmpresa = Guid.NewGuid();
            modelBuilder.Entity<Empresa>().HasData(
            new Empresa
            {
                Id = idEmpresa,
                Nombre = "HEREDIA MARIA LORENA",
                NombreComercial = "LA TRIGUEÑA SUCURSAL SUR",
                NumeroDocumento = 23255953044,
                IdTipoDoc = "80",
                IdCondicionIva = "05",
                IdProvincia = "03",
                Domicilio = "QUINTANA ",
                Altura = 810,
                Telefono = "999-999999",
                Email = "lorenaheredia@live.com.ar",
                DatabaseName = "HerediaDB",
                IdOrganizacion = idOrganizacion
            });
            Guid idEmpresa1 = Guid.NewGuid();
            modelBuilder.Entity<Empresa>().HasData(
            new Empresa
            {
                Id = idEmpresa1,
                Nombre = "EMPRESA ALTERNATIVA",
                NombreComercial = "EMPRESA ALTERNATIVA",
                NumeroDocumento = 1111111111,
                IdTipoDoc = "80",
                IdCondicionIva = "01",
                IdProvincia = "03",
                Domicilio = "CALLE S/N",
                Altura = 100,
                Telefono = "999-999999",
                Email = "nombre@sudominio.com",
                DatabaseName = "AltDb",
                IdOrganizacion = idOrganizacion
            });
            //Empresa Account
            modelBuilder.Entity<EmpresaAccount>().HasData(
            new EmpresaAccount { Id=idEmpresa,IdAccount= "0000000001" },
            new EmpresaAccount { Id = idEmpresa1, IdAccount = "0000000001" });
            //Accion
            modelBuilder.Entity<Accion>().HasData(
            new Accion { Id = "Add", Nombre = "Nuevo", },
            new Accion { Id = "Edit", Nombre = "Editar" },
            new Accion { Id = "Update", Nombre = "Actualizar" },
            new Accion { Id = "Delete", Nombre = "Eliminar" },
            new Accion { Id = "GetAll", Nombre = "Listar" },
            new Accion { Id = "Execute", Nombre = "Ejecutar" }
            );
            //Recurso
            modelBuilder.Entity<Recurso>().HasData(
            new Recurso { Id = "Almacen.Articulo", Nombre = "Articulo" },
            new Recurso { Id = "Comun.Sujeto", Nombre = "Sujetos (Clientes y Proveedores)" },
            new Recurso { Id = "Venta.Factura", Nombre = "Factura" },
            new Recurso { Id = "Contable.CuentaMayor", Nombre = "CuentaMayor" },
            new Recurso { Id = "Contable.Mayor", Nombre = "Mayor" },
            new Recurso { Id = "Contable.MovCtaCte", Nombre = "Cuentas Corrientes" },
            new Recurso { Id = "Contable.LibroIva", Nombre = "Libro Iva" },
            new Recurso { Id = "Tesoreria.ReciboCtaCte", Nombre = "Recibo Cta.Cte." }
            );
            //Permiso
            //Recurso
            modelBuilder.Entity<Permiso>().HasData(
            new Permiso { Id = "Almacen.Articulo.Add", IdAccion = "Add", IdRecurso = "Almacen.Articulo" },
            new Permiso { Id = "Almacen.Articulo.Edit", IdAccion = "Edit", IdRecurso = "Almacen.Articulo" },
            new Permiso { Id = "Almacen.Articulo.Update", IdAccion = "Update", IdRecurso = "Almacen.Articulo" },
            new Permiso { Id = "Almacen.Articulo.Delete", IdAccion = "Delete", IdRecurso = "Almacen.Articulo" },
            new Permiso { Id = "Almacen.Articulo.GetAll", IdAccion = "GetAll", IdRecurso = "Almacen.Articulo" },
            new Permiso { Id = "Almacen.Articulo.Update.Precio", IdAccion = "Execute", IdRecurso = "Almacen.Articulo" },
            new Permiso { Id = "Contable.CuentaMayor.Add", IdAccion = "Add", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.CuentaMayor.Edit", IdAccion = "Edit", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.CuentaMayor.Update", IdAccion = "Update", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.CuentaMayor.Delete", IdAccion = "Delete", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.CuentaMayor.GetAll", IdAccion = "GetAll", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.Mayor.Add", IdAccion = "Add", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.Mayor.Edit", IdAccion = "Edit", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.Mayor.Update", IdAccion = "Update", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.Mayor.Delete", IdAccion = "Delete", IdRecurso = "Contable.Mayor" },
            new Permiso { Id = "Contable.Mayor.GetAll", IdAccion = "GetAll", IdRecurso = "Contable.Mayor" },            
            new Permiso { Id = "Comun.Sujeto.Add", IdAccion = "Add", IdRecurso = "Comun.Sujeto" },
            new Permiso { Id = "Comun.Sujeto.Edit", IdAccion = "Edit", IdRecurso = "Comun.Sujeto" },
            new Permiso { Id = "Comun.Sujeto.Update", IdAccion = "Update", IdRecurso = "Comun.Sujeto" },
            new Permiso { Id = "Comun.Sujeto.Delete", IdAccion = "Delete", IdRecurso = "Comun.Sujeto" },
            new Permiso { Id = "Comun.Sujeto.GetAll", IdAccion = "GetAll", IdRecurso = "Comun.Sujeto" },            
            new Permiso { Id = "Contable.LibroIva.List", IdAccion = "GetAll", IdRecurso = "Contable.LibroIva" },
            new Permiso { Id = "Contable.LibroIva.Print", IdAccion = "Execute", IdRecurso = "Contable.LibroIva" },
            new Permiso { Id = "Venta.Factura.Add", IdAccion = "Add", IdRecurso = "Venta.Factura" },
            new Permiso { Id = "Venta.Factura.Edit", IdAccion = "Edit", IdRecurso = "Venta.Factura" },
            new Permiso { Id = "Venta.Factura.Update", IdAccion = "Update", IdRecurso = "Venta.Factura" },
            new Permiso { Id = "Venta.Factura.Delete", IdAccion = "Delete", IdRecurso = "Venta.Factura" },
            new Permiso { Id = "Venta.Factura.GetAll", IdAccion = "GetAll", IdRecurso = "Venta.Factura" },
            new Permiso { Id = "Venta.Factura.Print", IdAccion = "Execute", IdRecurso = "Venta.Factura" },            
            new Permiso { Id = "Tesoreria.ReciboCtaCte.Add", IdAccion = "Add", IdRecurso = "Tesoreria.ReciboCtaCte" },
            new Permiso { Id = "Tesoreria.ReciboCtaCte.Edit", IdAccion = "Edit", IdRecurso = "Tesoreria.ReciboCtaCte" },
            new Permiso { Id = "Tesoreria.ReciboCtaCte.Update", IdAccion = "Update", IdRecurso = "Tesoreria.ReciboCtaCte" },
            new Permiso { Id = "Tesoreria.ReciboCtaCte.Delete", IdAccion = "Delete", IdRecurso = "Tesoreria.ReciboCtaCte" },
            new Permiso { Id = "Tesoreria.ReciboCtaCte.GetAll", IdAccion = "GetAll", IdRecurso = "Tesoreria.ReciboCtaCte" }
            );
            //ConfigFactura  Composite Key
            modelBuilder.Entity<Rol>()
               .HasKey(e => new { e.Id});
            modelBuilder.Entity<RolPermiso>()
                .HasKey(e => new { e.IdRol, e.IdOrganizacion,e.IdPermiso });
            modelBuilder.Entity<Rol>().HasMany<RolPermiso>(m => m.Permisos).WithOne(C => C.Rol).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Rol>().HasData(
            new Rol { Id = 1,IdOrganizacion = idOrganizacion, Nombre = "Admin" });

            modelBuilder.Entity<RolPermiso>().HasData(
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Almacen.Articulo.Add" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Almacen.Articulo.Edit" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Almacen.Articulo.Update" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Almacen.Articulo.Delete" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Almacen.Articulo.GetAll" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Almacen.Articulo.Update.Precio" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.CuentaMayor.Add" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.CuentaMayor.Edit" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.CuentaMayor.Update" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.CuentaMayor.Delete" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.CuentaMayor.GetAll" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.Mayor.Add" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.Mayor.Edit" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.Mayor.Update" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.Mayor.Delete" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.Mayor.GetAll" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Comun.Sujeto.Add" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Comun.Sujeto.Edit" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Comun.Sujeto.Update" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Comun.Sujeto.Delete" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Comun.Sujeto.GetAll" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Venta.Factura.Add" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Venta.Factura.Edit" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Venta.Factura.Update" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Venta.Factura.Delete" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Venta.Factura.GetAll" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Venta.Factura.Print" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Tesoreria.ReciboCtaCte.Add" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Tesoreria.ReciboCtaCte.Edit" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Tesoreria.ReciboCtaCte.Update" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Tesoreria.ReciboCtaCte.Delete" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Tesoreria.ReciboCtaCte.GetAll" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.LibroIva.List" },
                    new RolPermiso { IdRol = 1, IdOrganizacion = idOrganizacion, IdPermiso = "Contable.LibroIva.Print" });

            //EntityAccount Composite Key
            modelBuilder.Entity<EmpresaAccount>()
                .HasKey(e => new { e.Id, e.IdAccount });
            //Account
            //Crear Password
            string password = "activasol";
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] MessageBytes = UE.GetBytes(password);

            dynamic hashedPwd = sha.ComputeHash(MessageBytes);
            var passEncript = Convert.ToBase64String(hashedPwd);
            string idAccount = "0000000001";
            modelBuilder.Entity<Account>().HasData(            
            new Account
            {
                Id = idAccount,
                Nombre = "admin",
                Password = passEncript,
                Email = "pmassimino@hotmail.com"
            });
            modelBuilder.Entity<RolAccount>().HasKey(ra => new { ra.IdRol, ra.IdAccount });

            modelBuilder.Entity<RolAccount>()
             .HasOne(pt => pt.Account)
             .WithMany(p => p.Roles)
             .HasForeignKey(pt => pt.IdAccount);

            modelBuilder.Entity<RolAccount>()
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.Accounts)
                .HasForeignKey(pt => pt.IdRol);
            modelBuilder.Entity<RolAccount>().HasData(new RolAccount { IdRol = 1, IdAccount = idAccount });



        }
            
      
        public DbSet<Account> Account { get; set; }
        public DbSet<UnidadMedida> UnidadMedida { get; set; }
        public DbSet<CondIvaOperacion> CondIvaOperacion { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<CondIva> CondIva { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<EmpresaAccount> EmpresaAccount { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<Accion> Accion { get; set; }
        public DbSet<Recurso> Recurso { get; set; }
        public DbSet<Permiso> Permiso { get; set; }
    }
}
