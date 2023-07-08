using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Tesoreria;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BackEnd.Api.Controllers.Tesoreria
{
    [Route("api/tesoreria/[controller]")]
    [ApiController]
    public class ReciboCtaCteController : ApiController<ReciboCtaCte,Guid>
    {
        private IReciboCtaCteService service;
        private IEmpresaService empresaService;
        ISessionService sessionService;
        IWebHostEnvironment hostingEnvironment;
        ILocalidadService localidadService;
        ISujetoService sujetoService;
        public ReciboCtaCteController(IReciboCtaCteService service, IAuthService authService, ISessionService sessionService, IEmpresaService empresaService,
            ISujetoService sujetoService,ILocalidadService localidadService, IWebHostEnvironment hostingEnvironment) : base(service, authService)
        {
            this.service = service;
            this.empresaService = empresaService;
            this.sessionService = sessionService;
            this.hostingEnvironment = hostingEnvironment;
            this.localidadService = localidadService;
            this.sujetoService = sujetoService;
            this.NombreRecurso = "Tesoreria.ReciboCtaCte";
        }
        [HttpGet("ComprobantesDisponibles/{id}")]
        public IActionResult ComprobantesDisponibles(string id, string idCuentaMayor)
        {          
            return Ok(this.service.ComprobantesDisponibles(id,idCuentaMayor));
        }
        [HttpGet("NextNumber/{id}")]
        public IActionResult NextNumber(string id)
        {
            var result = this.service.NextNumber(id);
            if (result == null) 
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/Print")]
        public async Task<IActionResult> Print(Guid id)
        {
            var empresa = this.empresaService.GetOne(this.sessionService.IdEmpresa);
            var tmpRecibo = this._service.GetOne(id);
            var tmpCuenta = sujetoService.GetOne(tmpRecibo.IdCuenta);
            

            Font font = new Font();
            font.Size = 7;
            Font fontTitulo = new Font();
            fontTitulo.Size = 9;
            Font fontD = new Font();
            fontD.Size = 7;
            Font fontDes = new Font();
            fontDes.Size = 5;

            float[] columnWidths = new float[] { 1.4F, 1.5F,7.4F, 1.7F };


            PdfPTable Table = new PdfPTable(columnWidths);
                  
            Table.HeaderRows = 1;
            // Header
            // Titulo a Vencido
            PdfPCell cell = new PdfPCell(new Phrase("COMPROBANTES", fontTitulo));
            cell.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.Colspan = 4;
            Table.AddCell(cell);

            cell = new PdfPCell(new Phrase("FECHA", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("NUMERO", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("CONCEPTO", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("IMPORTE", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);         
            foreach (var item in tmpRecibo.DetalleComprobante)
            {
                BaseColor colorBack = BaseColor.WHITE;
                int borderVenc = 0;               
                cell = new PdfPCell(new Phrase(item.Fecha.ToShortDateString(), font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Pe.ToString().PadLeft(4,'0') + "-" + item.Numero.ToString().PadLeft(8,'0'), font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Concepto, font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                decimal importe = item.IdTipo == "1" ? item.Importe : -item.Importe;
                cell = new PdfPCell(new Phrase(importe.ToString("N"), font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);                
            }
            //Totales           
            cell = new PdfPCell(new Phrase("Totales", fontTitulo));
            cell.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BackgroundColor = BaseColor.GRAY;
            cell.Colspan = 3;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(tmpRecibo.Importe.ToString("N"), font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;           
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;            
            Table.AddCell(cell);

            float[] columnWidthsValores = new float[] { 1.4F, 1.5F, 3, 1.3F, 1.3F, 1.3F };


            PdfPTable TableValores = new PdfPTable(columnWidthsValores);

            Table.HeaderRows = 1;
            // Header
            // Titulo a Vencido
            cell = new PdfPCell(new Phrase("DETALLE DE VALORES", fontTitulo));
            cell.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.Colspan = 6;
            TableValores.AddCell(cell);

            cell = new PdfPCell(new Phrase("FECHA", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);
            cell = new PdfPCell(new Phrase("NUMERO", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);
            cell = new PdfPCell(new Phrase("CONCEPTO", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);
            cell = new PdfPCell(new Phrase("BANCO", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);
            cell = new PdfPCell(new Phrase("FECHA VENC.", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);
            cell = new PdfPCell(new Phrase("IMPORTE", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);
            foreach (var item in tmpRecibo.DetalleValores)
            {
                BaseColor colorBack = BaseColor.WHITE;
                int borderVenc = 0;
                cell = new PdfPCell(new Phrase(item.Fecha.ToShortDateString(), font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                TableValores.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Numero.ToString().PadLeft(8, '0'), font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                TableValores.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Concepto, font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                TableValores.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Banco, font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                TableValores.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.FechaVencimiento.ToShortDateString(), font));
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                TableValores.AddCell(cell);
                decimal importe = item.IdTipo == "1" ? item.Importe : -item.Importe;
                cell = new PdfPCell(new Phrase(importe.ToString("N"), font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BackgroundColor = colorBack;
                cell.Border = borderVenc;
                cell.BorderColor = BaseColor.WHITE;
                TableValores.AddCell(cell);
            }
            //Totales           
            cell = new PdfPCell(new Phrase("Totales", fontTitulo));
            cell.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BackgroundColor = BaseColor.GRAY;
            cell.Colspan = 5;
            TableValores.AddCell(cell);
            cell = new PdfPCell(new Phrase(tmpRecibo.Importe.ToString("N"), font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;            
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            TableValores.AddCell(cell);

            var doc = new Document(PageSize.A4, 10f, 10f, 135f, 100f);

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.UNDERLINE, BaseColor.BLACK);
            var h1Font = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.NORMAL, BaseColor.DARK_GRAY);

            var strFilePath = hostingEnvironment.ContentRootPath + @"\Reports\";

            try
            {
                MemoryStream stream = new MemoryStream();
                var pdfWriter = PdfWriter.GetInstance(doc, stream);
                var textEvents = new ITextEvents();
                pdfWriter.PageEvent = textEvents;
                textEvents.Titulo = empresa.Nombre;
                textEvents.Titulo1 = "RECIBO DE CTA. CTE.";
                textEvents.Cuenta = tmpCuenta;
                textEvents.ReciboCtaCte = tmpRecibo;
                textEvents.ImagePath = strFilePath;
                doc.Open();
                doc.Add(Table);
                doc.Add(TableValores);

                doc.Close();
                var file = stream.ToArray();
                var output = new MemoryStream();
                output.Write(file, 0, file.Length);
                output.Position = 0;
                return new FileStreamResult(output, "application/pdf") { FileDownloadName = "ReciboCtaCte.pdf" };
            }
            catch (Exception ex)
            {
                return HandleErrorCondition(ex.Message);
            }
            finally
            {
                doc.Close();
            }

            //return new FileStreamResult(output, "application/pdf") { FileDownloadName = "ResumenCtaCte.pdf" };

        }

        [HttpGet("{id}/printByTransaccion")]
        public IActionResult PrintByTransaccion(Guid id)
        {
            Guid idEntity = service.GetByTransaction(id).Id;
            if (idEntity == Guid.Empty)
            {
                return NotFound();
            }
            string url = "/api/tesoreria/reciboctacte/" + idEntity + "/print";
            return new RedirectResult(url: url);
        }

        private ActionResult HandleErrorCondition(string message)
        {
            throw new NotImplementedException();
        }
    }
    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        public String Titulo { get; set; }
        public String Titulo1 { get; set; }
        public Sujeto Cuenta { get; set; }
        public ReciboCtaCte ReciboCtaCte { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string ImagePath { get; set; }



        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(400, 150);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Crear Header de Reporte
            base.OnEndPage(writer, document);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontLite = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            var Table = new PdfPTable(7);
            //Row 1
            PdfPCell cell = new PdfPCell(new Phrase(this.Titulo, baseFontBig));
            cell.Border = 0;
            cell.Colspan = 5;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("FOLIO", baseFontLite));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Table.AddCell(cell);

            cell = new PdfPCell(new Phrase(writer.PageNumber.ToString("000"), baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            //Row 2
            cell = new PdfPCell(new Phrase(this.Titulo1, baseFontBig));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.Colspan = 5;
            Table.AddCell(cell);
            // add a image
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(this.ImagePath + "logo.jpg");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell.Rowspan = 6;
            Table.AddCell(imageCell);
            //Row 3
            cell = new PdfPCell(new Phrase("R.Social:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.Cuenta.Nombre, baseFontLite));
            cell.Colspan = 2;
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Cuenta:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.Cuenta.Id, baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            //Row 4
            cell = new PdfPCell(new Phrase("Domicilio:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.Cuenta.Domicilio.Trim() +" " +  this.Cuenta.Altura.ToString("N0"), baseFontLite));
            cell.Colspan = 2;
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("CUIT:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.Cuenta.NumeroDocumento.ToString(), baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            //Row 5
            cell = new PdfPCell(new Phrase("Localidad:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            string cpostal = this.Cuenta.CodigoPostal != null ? this.Cuenta.CodigoPostal.Trim() : "";
            cell = new PdfPCell(new Phrase(cpostal + "-" + this.Cuenta.Localidad.Nombre.Trim() + "-" + this.Cuenta.Localidad.Provincia.Nombre, baseFontLite));
            cell.Colspan = 2;
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            //Row 6
            cell = new PdfPCell(new Phrase("email:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.Cuenta.email1, baseFontLite));
            cell.Colspan = 6;
            cell.Border = 0;
            Table.AddCell(cell);
            //Row 7
            cell = new PdfPCell(new Phrase("Fecha:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.ReciboCtaCte.Fecha.ToShortDateString(), baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Fecha Vencimiento:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.ReciboCtaCte.FechaVencimiento.ToShortDateString(), baseFontLite));
            cell.Border = 0;            
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Numero:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.ReciboCtaCte.Pe.ToString().PadLeft(4,'0') + "-" +this.ReciboCtaCte.Numero.ToString().PadLeft(8, '0'), baseFontLite));
            cell.Border = 0;
            cell.Colspan = 2;
            Table.AddCell(cell);

            //Table.TotalWidth = document.PageSize.Width - 80f;
            Table.TotalWidth = document.PageSize.Width - 95f;
            Table.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            //Table.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            Table.WriteSelectedRows(0, -1, 65, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            //cb.MoveTo(40, document.PageSize.Height - 100);
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            //cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();


        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(400, 150);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }
}

    