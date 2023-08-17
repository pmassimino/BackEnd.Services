using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Afip.Services;
using Afip.Services.Model;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Afip;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Venta;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Ventas
{
    [Route("api/ventas/[controller]")]
    [ApiController]
    public class FacturaController : ApiController<Services.Models.Ventas.Factura, Guid>
    {
        private IWebHostEnvironment _hostingEnvironment;
        private ISessionService sessionService;
        private IEmpresaService empresaService;
        private IAFIPHelperService afipHelperService;
        private IFacturaService service;
        private IGlobalService GlobalService;
        private ISujetoService sujetoService;
        public FacturaController(IFacturaService service, IEmpresaService empresaService,ISujetoService sujetoService, 
            IWebHostEnvironment environment, ISessionService sessionService,IAuthService authService, IAFIPHelperService afipHelperService,IGlobalService globalService) : base(service, authService)
        {
            _hostingEnvironment = environment;
            this.sessionService = sessionService;
            this.empresaService = empresaService;
            this.afipHelperService = afipHelperService;
            this.service = service;
            this.GlobalService = globalService;
            this.sujetoService = sujetoService;
            this.NombreRecurso = "Venta.Factura";
        }
        [HttpGet("listView")]
        public  IActionResult ListView(DateTime? fecha, DateTime? fechaHasta)
        {
            DateTime pfecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime pfechaHasta = pfecha.AddMonths(1).AddDays(-1);
            if (fecha.HasValue)
            {
                pfecha = (DateTime)fecha;
            }
            if (fechaHasta.HasValue)
            {
                pfechaHasta = (DateTime)fechaHasta;
            }
            if (pfecha > pfechaHasta) 
            {
                return BadRequest("Rango de fecha incorrecto");
            }
            try
            {
                var result = this.service.GetAll().Where(w=>w.Fecha >= fecha && w.Fecha <= fechaHasta).ToList();
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ByCuenta/{id}")]
        public IActionResult GetByIdCuenta(string id)
        {
           
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("IdCuenta Requerido");
            }
            
            try
            {
                var result = this.service.GetAll().Where(w => w.IdCuenta==id).ToList();
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}/print")]
        public IActionResult Print(Guid id)
        {
            if (!this.authService.Authorize(this.NombreRecurso + ".Print")) 
            {
                return BadRequest("Permiso Denegado");
            }
            var entity = this._service.GetOne(id);
            var Empresa = empresaService.GetOne(sessionService.IdEmpresa);
            string pdfTemplate = "";
            var path = _hostingEnvironment.ContentRootPath;
            if (entity.Cae == 0)
                pdfTemplate = path + @"\Reports\TemplateFacturaCFiscal.pdf";
            else
                pdfTemplate = path + @"\Reports\TemplateFactura.pdf";

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            MemoryStream stream = new MemoryStream();
            PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
            AcroFields Form = pdfStamper.AcroFields;            
            // add a image            
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path + @"\Reports\logo.jpg");
            PushbuttonField ad = Form.GetNewPushbuttonFromField("logo");
            if (ad != null)
            {
                ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                ad.ProportionalIcon = true;
                ad.Image = image;
                Form.ReplacePushbuttonField("logo", ad.Field);
            }
            Form.SetField("nombreEmpresa", Empresa.Nombre);
            Form.SetField("domicilioEmpresa", Empresa.Domicilio + "-Tel.: " + Empresa.Telefono);
            var tmpCuenta = sujetoService.GetOne(entity.IdCuenta);
            //Form.SetField("domicilioEmpresa1", Empresa + "-" + Empresa.Localidad.Provincia);
            Form.SetField("emailEmpresa", Empresa.Email);
            string condIva = Empresa.IdCondicionIva == "05" ? "RESPONSABLE MONOTRIBUTISTA" : "RESPONSABLE INSCRIPTO";
            Form.SetField("condIvaEmpresa", condIva);
            Form.SetField("cuitEmpresa", Empresa.NumeroDocumento.ToString());
            Form.SetField("numeroIBEmpresa", Empresa.NumeroIB == 0 ? "" : Empresa.NumeroIB.ToString());
            Form.SetField("fechaInicioAct","");
            Form.SetField("fecha", entity.FechaComp.ToShortDateString());
            string numero = entity.Pe.ToString().PadLeft(4, '0') + "-" +  entity.Numero.ToString().PadLeft(8, '0');
            Form.SetField("numero", numero);
            Form.SetField("nombre", tmpCuenta.Nombre);
            Form.SetField("domicilio", tmpCuenta.Domicilio + " " + tmpCuenta.Altura.ToString("N0"));
            string cpostal = tmpCuenta.CodigoPostal != null ? tmpCuenta.CodigoPostal.Trim() + "-" : "";
            string localidadProvincia = cpostal + tmpCuenta.Localidad.Nombre.Trim() + "-" + tmpCuenta.Localidad.Provincia.Nombre;
            Form.SetField("localidadProvincia", localidadProvincia);
            Form.SetField("condIva",tmpCuenta.CondIva.Nombre);
            Form.SetField("cuit", tmpCuenta.NumeroDocumento.ToString());
            Form.SetField("cuentaNumero", entity.IdCuenta);
            Form.SetField("letra", entity.Letra);
            string tipoComp = "FACTURA";
            if (entity.Tipo == "2")
                tipoComp = "NOTA DE CREDITO";
            else if (entity.Tipo == "3")
                tipoComp = "NOTA DE DEBITO";
            else if (entity.Tipo == "4")
                tipoComp = "TICKET";
            Form.SetField("tipoComprobante", tipoComp);
            var i = 1;
            foreach (var item in entity.Detalle)
            {
               Form.SetField("codigo" + i.ToString().Trim(), item.IdArticulo);
              Form.SetField("cantidad" + i.ToString().Trim(), item.Cantidad.ToString());
              Form.SetField("detalle" + i.ToString().Trim(), item.Concepto);
              Form.SetField("precioUnitario" + i.ToString().Trim(), item.Precio.ToString());
              Form.SetField("importe" + i.ToString().Trim(), item.Total.ToString());
              i += 1;
            }
            Form.SetField("obs", entity.Obs);
            Form.SetField("importeSubTotal", entity.TotalNeto.ToString());
            Form.SetField("importeDescuento", entity.TotalDescuento.ToString());
            Form.SetField("importeSubTotal2", (entity.TotalNeto - entity.TotalDescuento).ToString());
            Form.SetField("importeImp", entity.TotalOTributos.ToString());
            decimal ivaGeneral = entity.Iva.Where(w=>w.CondIva == "005").Sum(s => s.Importe);
            decimal ivaOtro = entity.Iva.Where(w => w.CondIva == "004").Sum(s => s.Importe);
            Form.SetField("importeIvaOtro", ivaOtro.ToString());
            Form.SetField("importeIvaG", ivaGeneral.ToString());
            Form.SetField("importeTotal", entity.Total.ToString());

            Form.SetField("cae", entity.Cae.ToString());
            Form.SetField("fechaVencCae", entity.FechaComp.AddDays(10).ToShortDateString());
            Form.SetField("codBarra", "0");
            Form.SetField("codBarraNumero", "0");
            Form.SetField("remito", "0");
            //QR
            var year = entity.FechaComp.Year.ToString();
            var month = entity.FechaComp.Month.ToString().PadLeft(2, '0');
            var day = entity.FechaComp.Day.ToString().PadLeft(2,'0');
            var fecha = year + "-" + month + "-" + day;
            var cuit = Empresa.NumeroDocumento;
            var ptoVa = entity.Pe;
            var letra = entity.Letra;
            var nroCmp = entity.Numero;
            var tipoCmp = this.afipHelperService.GetIdComprobanteAfip(entity.Letra, entity.Tipo);
            var importe = entity.Total.ToString().Replace(",", ".");
            var moneda = entity.IdMoneda;
            var ctz = entity.IdMoneda == "PES" ? "1" : entity.CotizacionMoneda.ToString();
            var tipoDocRec = tmpCuenta.IdTipoDoc;
            var nroDocRec = tmpCuenta.NumeroDocumento.ToString();
            var tipoCodAut = 'E';
            var codAut = entity.Cae;
            var qrStr = "{'ver':1,'fecha':'" + fecha + "','cuit':" + cuit + ",'ptoVta':" + ptoVa + ",'tipoCmp':" + tipoCmp + ",'nroCmp':" + nroCmp + ",'importe':" + importe + ",'moneda':'" + moneda + "','ctz':" + ctz + ",'tipoDocRec':" + tipoDocRec + ",'nroDocRec':" + nroDocRec + ",'tipoCodAut':'" + "E" + "','codAut':" + codAut + "}";

            qrStr = qrStr.Replace(@"'", @"""");

            byte[] byt = System.Text.Encoding.UTF8.GetBytes(qrStr);
            var qrBase64 = Convert.ToBase64String(byt);

            // Dim qrBase64 = Convert.ToBase64String(qrStr)

            var url = "https://www.afip.gob.ar/fe/qr/?p=" + qrBase64;
            // Insertar qr
            iTextSharp.text.pdf.BarcodeQRCode qrcode = new BarcodeQRCode(url, 50, 50, null);
            iTextSharp.text.Image img = qrcode.GetImage();

            img.SetAbsolutePosition(50, 85);
            if (entity.Cae !=0)
                pdfStamper.GetOverContent(1).AddImage(img);


            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
            var file = stream.ToArray();
            var output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;

            //HttpContext.Response.AddHeader("content-disposition", "inline; filename=factura.pdf");
            return new FileStreamResult(output, "application/pdf") { FileDownloadName="Factura"};
            
        }

        [HttpGet("{id}/printByTransaccion")]
        public IActionResult PrintByTransaction(Guid id) 
        {
            Guid idEntity = service.GetByTransaction(id).Id;
            if (idEntity == Guid.Empty) 
            {
                return NotFound();
            }
            string url = "/api/ventas/factura/" + idEntity + "/print";
            return new RedirectResult(url: url);
        }

        [HttpGet("letrasdisponibles")]
        public IActionResult LetrasDisponibles(string idCondIva = "")
        {
            string[] letras = this.GetLetrasDisponibles(idCondIva);
            return Ok(letras);
        }
        public override IActionResult NewDefault()
        {
            var result = this._service.NewDefault();
            result.Letra = this.GetLetrasDisponibles()[0];
            return Ok(result);
        }
        private string[] GetLetrasDisponibles(string idSeccion = "")
        {
            var Empresa = empresaService.GetOne(sessionService.IdEmpresa);
            string[] letras = new string[1];
                   
            if (Empresa != null)
            {
                //Monotributo     
                if (Empresa.IdCondicionIva == "05")
                {
                    letras = new string[1] { "C" };
                }
                //Empresa Responsable Incripto - sujeto responsable insc
                if (Empresa.IdCondicionIva == "01")
                {
                    letras = new string[2] { "A", "B" };
                }
            }
            return letras;
        }
        [HttpGet("NextNumber")]
        public IActionResult NextNumber(string idSeccion,string letra,string tipo)
        {
            var result = this.service.NextNumber(idSeccion,letra,tipo);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/autorizar")]
        public IActionResult Autorizar(Guid id)
        {
            //configurar
            var tmpEmpresa = empresaService.GetOne(sessionService.IdEmpresa);

            var ws = new FacturaWebService_V1();
            ws.UrlServicio = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";
            ws.UrlLogin = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
            //ws.DNDestino = "CN=ws, O=AFIP, C=AR, SERIALNUMBER=CUIT 33693450239";
            var path = _hostingEnvironment.ContentRootPath + @"\tmp\";
            ws.PathTicket = path;
            var empresa = new EmpresaInfo();
            empresa.Cuit = tmpEmpresa.NumeroDocumento;            
            empresa.NombreEmpresa = tmpEmpresa.Nombre;
            empresa.PathCertificado = "C:/clientes/lorenaheredia/Certificado/Heredia2023.pfx";
            ws.DNDestino = "cn=wsaa,o=afip,c=ar,serialNumber=CUIT 33693450239";

            ws.Empresa = empresa;

            var tmpFactura = this._service.GetOne(id);
            var factura = new FacturaWebService_V1.Factura();
            //Numero y tipo
            factura.Punto_Emision = tmpFactura.Pe;
            int tipo = this.afipHelperService.GetIdComprobanteAfip(tmpFactura.Letra, tmpFactura.Tipo);
            var loginResult = ws.Login();
            int numero = ws.GetUltimoNumeroComprobante(tmpFactura.Pe, tipo) + 1;
            factura.Numero = numero;
            factura.id_Concepto = 3;
            factura.Tipo_Cbte = tipo;
            //Fechas
            factura.Fecha_cbte = tmpFactura.FechaComp;
            factura.Fecha_Vencimiento = tmpFactura.FechaVencimiento;
            //Cliente
            factura.Numero_Doc = tmpFactura.Sujeto.NumeroDocumento;
            factura.Tipo_Doc = Convert.ToInt32(tmpFactura.Sujeto.IdTipoDoc);
            factura.NombreCliente = tmpFactura.Sujeto.Nombre;
            factura.DomicilioCliente = tmpFactura.Sujeto.Domicilio;
            //Importes 
            factura.Importe_Neto = Convert.ToDouble(tmpFactura.TotalNeto);
            if (tmpFactura.Letra != "C")
            {
                factura.Importe_CNG = Convert.ToDouble(tmpFactura.TotalNoGravado);
                factura.Importe_Exento = Convert.ToDouble(tmpFactura.TotalExento);
            }            
            factura.Importe_Iva = Math.Round(Convert.ToDouble(tmpFactura.TotalIva),2);
            factura.Importe_OTributos = Convert.ToDouble(tmpFactura.TotalOTributos);
            factura.Importe_Total = Math.Round(Convert.ToDouble(tmpFactura.Total),2);
            //Moneda
            factura.MondedaId = tmpFactura.IdMoneda;
            factura.MonedaCotizacion = Convert.ToDouble(tmpFactura.CotizacionMoneda);
            //Iva
            if (tmpFactura.Letra != "C") {
                foreach (var item in tmpFactura.Iva)
                {
                    if (item.Importe > 0)
                    {
                        factura.AddIva("", Convert.ToInt16(item.CondIva), Convert.ToDouble(item.BaseImponible), Math.Round(Convert.ToDouble(item.Importe),2));
                    }
                }
            }
            //Otros
            foreach (var item in tmpFactura.Tributos)
            {
                factura.AddTributo(item.Nombre, Convert.ToDouble(item.BaseImponible), Convert.ToDouble(item.Tarifa), Convert.ToInt32(item.IdTributo), Convert.ToDouble(item.Importe));
            }
            var result = ws.Autorizar(factura);

            if (result.Result == true)
            {
                //Actualizar CAE  y estado
                long cae = Convert.ToInt64(result.Factura.cae);
                this.service.UpdateAfip(tmpFactura.Id, cae, result.Factura.Punto_Emision, result.Factura.Numero);
                return Ok(result);
            }
            else
            {
                Dictionary<string, string> error = new Dictionary<string, string>();              
                error.Add("General", result.Message);
                return BadRequest(error);
            }
        }
        [HttpGet("{id}/recuperar")]
        public IActionResult Recuperar(Guid id)
        {
            Dictionary<string, string> errorValidacion = new Dictionary<string, string>();
            var tmpFactura = this._service.GetOne(id);
            if (tmpFactura == null) 
            {
                errorValidacion.Add("Factura", "Factura no existe");
            }
            if (tmpFactura.Cae != 0) 
            {
                errorValidacion.Add("Factura1", "Factura con cae no se puede recuperar");
            }
            if (errorValidacion.Count > 0) 
            {
                return BadRequest(errorValidacion);
            }
            //configurar
            var tmpEmpresa = empresaService.GetOne(sessionService.IdEmpresa);

            var ws = new FacturaWebService_V1();
            ws.UrlServicio = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";
            ws.UrlLogin = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
            //ws.DNDestino = "CN=ws, O=AFIP, C=AR, SERIALNUMBER=CUIT 33693450239";
            var path = _hostingEnvironment.ContentRootPath + @"\tmp\";
            ws.PathTicket = path;
            var empresa = new EmpresaInfo();
            empresa.Cuit = tmpEmpresa.NumeroDocumento;
            empresa.NombreEmpresa = tmpEmpresa.Nombre;
            empresa.PathCertificado = "C:/clientes/lorenaheredia/Certificado/Heredia2023.pfx";
            ws.DNDestino = "cn=wsaa,o=afip,c=ar,serialNumber=CUIT 33693450239";

            ws.Empresa = empresa;

            
            var factura = new FacturaWebService_V1.Factura();
            //Numero y tipo
            factura.Punto_Emision = tmpFactura.Pe;
            int tipo = this.afipHelperService.GetIdComprobanteAfip(tmpFactura.Letra, tmpFactura.Tipo);
            var loginResult = ws.Login();
            int numero = Convert.ToInt16(tmpFactura.Numero);
            factura.Numero = numero;
            factura.id_Concepto = 3;
            factura.Tipo_Cbte = tipo;
            
            var result = ws.GetComprobante(tipo,tmpFactura.Pe,numero);
            Dictionary<string, string> error = new Dictionary<string, string>();
            decimal importeTotal = Convert.ToDecimal(factura?.Importe_Total ?? 0);
            long numeroDocumento = tmpFactura.Sujeto?.NumeroDocumento ?? 0;

            if (result.Result && (importeTotal != tmpFactura.Total || numeroDocumento != tmpFactura.Sujeto.NumeroDocumento))
            {
                error.Add("Afip", "La Factura registrada en AFIP no coincide con la registrada en el sistema");
            }
            else 
            {
                error.Add("General", result.Message);
            }
            //Actualizar CAE  y estado
            long cae = Convert.ToInt64(result.Factura.cae);
            this.service.UpdateAfip(tmpFactura.Id, cae, result.Factura.Punto_Emision, result.Factura.Numero);
            return Ok(result);
}


    }
    public class EFacturaAdic
    {
        public string IdConcepto { get; set; }
        public string IdOpcional { get; set; }
        public string ValorOpcional { get; set; }
        public IList<CompAfip> CompAdicional {get;set;} 
    }
    public class CompAfip
    {
        public string idTipo { get; set; }
        public int pe { get; set; }
        public int numero { get; set; }
        public DateTime fecha { get; set; }

    }
}