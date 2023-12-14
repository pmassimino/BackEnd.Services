using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Global;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using BackEnd.Services.Services.Global;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Contable
{
    [Route("api/contable/[controller]")]
    [ApiController]
    public class LibroIvaController : ApiController<LibroIva, Guid> {


        ILibroIvaService service;
        ISessionService sessionService { get; set; }
        IEmpresaService empresaService { get; set; }
        private IWebHostEnvironment hostingEnvironment { get; set; }
        ISujetoService sujetoService { get; set; }
        public LibroIvaController(ILibroIvaService service, ISessionService sessionService, 
            IEmpresaService empresaService,IAuthService authService,
            ISujetoService sujetoService, IWebHostEnvironment hostingEnvironment) : base(service,authService)
        {
            this.service= service;
            this.sessionService = sessionService;
            this.hostingEnvironment = hostingEnvironment;
            this.empresaService = empresaService;
            this.sujetoService = sujetoService;            
        }
        [HttpGet("Ventas")]
        public IActionResult Ventas(DateTime? fecha, DateTime? fechaHasta,bool filtrarAuto = true,bool autorizado = true )
        {
            string tipo = "V";
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
            try
            {
                return Ok(this.service.GetAllView(tipo, pfecha, pfechaHasta, filtrarAuto, autorizado));
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
        [HttpGet("Compras")]
        public IActionResult Compras(DateTime? fecha, DateTime? fechaHasta, bool filtrarAuto = true, bool autorizado = true)
        {
            string tipo = "C";
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
            try
            {
                return Ok(this.service.GetAllView(tipo, pfecha, pfechaHasta, filtrarAuto, autorizado));
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
        [HttpGet("print")]
        public async Task<IActionResult> Print(string tipo,DateTime? fecha, DateTime? fechaHasta, bool filtrarAuto = true, bool autorizado = true)
        {
            if (tipo == null) tipo = "V";
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
            var tmpLibroIva = this.service.GetAllView(tipo, pfecha, pfechaHasta, filtrarAuto, autorizado);

            var empresa = this.empresaService.GetOne(this.sessionService.IdEmpresa);
            

            Font font = new Font();
            font.Size = 7;
            Font fontTitulo = new Font();
            fontTitulo.Size = 7;
            Font fontD = new Font();
            fontD.Size = 7;
            Font fontDes = new Font();
            fontDes.Size =7;

            float[] columnWidths = new float[] { 1.8F, 2.4F, 1.8F, 4F, 2F, 2F, 1.1F, 1.8F, 1.8F, 1.1F, 1.1F, 1.8F, 2.4F };


            PdfPTable Table = new PdfPTable(columnWidths);
            // Conf
            // Table.SkipFirstHeader = True
            // Table.SkipLastFooter = True
            Table.HeaderRows = 1;
            // Header
            PdfPCell cell = new PdfPCell(new Phrase("Fecha", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Numero", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("N°Doc", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Nombre", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Gravado", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("No Gravado", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Exento", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Iva 10.5", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Iva 21", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Iva 27", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Iva Otro", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Otros.Trib.", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Total", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);           
            bool headerAVencer = false;
            foreach (var item in tmpLibroIva)
            {
                BaseColor colorBack;
                colorBack = BaseColor.WHITE;

                cell = new PdfPCell(new Phrase(item.FechaComprobante.ToShortDateString(), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Pe.ToString().PadLeft(4,'0') + "-" + item.Numero.ToString().PadLeft(8, '0'), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.NumeroDocumento.ToString(), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);               
                cell = new PdfPCell(new Phrase(item.Nombre, font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Gravado.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.NoGravado.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Exento.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Iva105.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);               
                cell = new PdfPCell(new Phrase(item.Iva21.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Iva27.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.IvaOtro.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.OtrosTributos.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Total.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
            }
            // Agregar Totales
            // Linea en Blanco
            decimal totalGravado = tmpLibroIva.Sum(s => s.Gravado);
            decimal totalNoGravado = tmpLibroIva.Sum(s => s.NoGravado);
            decimal totalExento = tmpLibroIva.Sum(s => s.Exento);
            decimal totalIva105 = tmpLibroIva.Sum(s => s.Iva105);
            decimal totalIva21 = tmpLibroIva.Sum(s => s.Iva21);
            decimal totalIva27 = tmpLibroIva.Sum(s => s.Iva27);
            decimal totalIvaOtro = tmpLibroIva.Sum(s => s.IvaOtro);
            decimal totalOtrosTributos = tmpLibroIva.Sum(s => s.OtrosTributos);
            decimal total = tmpLibroIva.Sum(s => s.Total);

            cell = new PdfPCell(new Phrase("Totales",font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            cell.Colspan = 4;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalGravado.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;            
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalNoGravado.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalExento.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalIva105.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalIva21.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalIva27.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalIvaOtro.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalOtrosTributos.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(total.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);

            var doc = new Document(PageSize.A4.Rotate(), -20f, 10f, 100f, 100f);
            

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.UNDERLINE, BaseColor.BLACK);
            var h1Font = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.NORMAL, BaseColor.DARK_GRAY);

            var strFilePath = hostingEnvironment.ContentRootPath + @"\Reports\";

            try
            {
                MemoryStream stream = new MemoryStream();
                var pdfWriter = PdfWriter.GetInstance(doc, stream);
                var templateGeneral = new TemplateGeneral();
                pdfWriter.PageEvent = templateGeneral;                
                string tipoLibro = tipo == "V" ? "Ventas" : "Compras";
                templateGeneral.Empresa = empresa;

                templateGeneral.Titulo = "Libro de IVA ";
                templateGeneral.Titulo1 = tipoLibro;
                templateGeneral.FechaDesde = pfecha;
                templateGeneral.FechaHasta = pfechaHasta;
                templateGeneral.ImagePath = strFilePath;
                doc.Open();
                doc.Add(Table);
                doc.Close();
                var file = stream.ToArray();
                var output = new MemoryStream();
                output.Write(file, 0, file.Length);
                output.Position = 0;
                string fileName = "LibroIva" + tipoLibro + ".pdf";
                return new FileStreamResult(output, "application/pdf") { FileDownloadName = fileName };
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
        private ActionResult HandleErrorCondition(string message)
        {
            throw new NotImplementedException();
        }
    }
    public class TemplateGeneral : ITemplateBase
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

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
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Hoja N°:", baseFontLite));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(writer.PageNumber.ToString("00000000"), baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            //Row 2
            cell = new PdfPCell(new Phrase(this.Titulo1, baseFontBig));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Colspan = 5;
            Table.AddCell(cell);
            // add a image
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(this.ImagePath + "logo.jpg");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell.Rowspan = 5;
            Table.AddCell(imageCell);            
            //Row 3
            cell = new PdfPCell(new Phrase(this.Empresa.Nombre, baseFontLite));
            cell.Border = 0;
            cell.Colspan = 7;
            Table.AddCell(cell);
            //Row 3
            cell = new PdfPCell(new Phrase("CUIT:" +this.Empresa.NumeroDocumento.ToString(), baseFontLite));
            cell.Border = 0;
            cell.Colspan = 7;
            Table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fecha Desde:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.FechaDesde.ToShortDateString(), baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Fecha Hasta:", baseFontLite));
            cell.Border = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(this.FechaHasta.ToShortDateString(), baseFontLite));
            cell.Border = 0;
            cell.Colspan = 4;
            Table.AddCell(cell);            
            Table.TotalWidth = document.PageSize.Width - 95f;
            Table.WidthPercentage = 70;
            
            Table.WriteSelectedRows(0, -1,65, document.PageSize.Height - 30, writer.DirectContent);
            
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
                headerTemplate = cb.CreateTemplate(400, 100);
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
    public class ITemplateBase: PdfPageEventHelper
    {
        public String Titulo { get; set; }
        public String Titulo1 { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string ImagePath { get; set; }
        public Empresa Empresa {get;set;}
        public Sujeto Cuenta { get; set; }
    }

}