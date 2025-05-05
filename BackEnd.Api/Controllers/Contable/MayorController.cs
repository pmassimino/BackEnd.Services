using System;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Services.Data;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using System.Collections.Generic;

namespace BackEnd.Api.Controllers.Contable
{
    [Route("api/contable/[controller]")]
    [ApiController]
    public class MayorController : ApiController<Mayor, Guid>
    {


        IMayorService _service;
        ISessionService sessionService;
        IEmpresaService empresaService;
        ICuentaMayorService cuentaMayorService;
        private IWebHostEnvironment hostingEnvironment { get; set; }
        public MayorController(IMayorService service, IAuthService authService, ISessionService sessionService, 
            IEmpresaService empresaService, IWebHostEnvironment hostingEnvironment,ICuentaMayorService cuentaMayorService) : base(service, authService)
        {
            this._service = service;
            this.sessionService = sessionService;
            this.empresaService = empresaService;
            this.cuentaMayorService = cuentaMayorService;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("Balance")]
        public IActionResult Balance(DateTime? fecha, DateTime? fechaHasta)
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
            var result = this.CalcularBalance(pfecha, pfechaHasta);
            return Ok(result);
        }
        [HttpGet("BalanceIntegrado")]
        public IActionResult BalanceIntegrado(DateTime? fecha, DateTime? fechaHasta)
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
            var tmpCuentas = cuentaMayorService.GetAllViews();
            var tmpBalance = this.CalcularBalance(pfecha, pfechaHasta);
            var tmpBalanceIntegrado = this.ConstruirJerarquia(tmpCuentas.ToList(), pfecha,pfechaHasta);
            var result = this.AsignarSaldos(tmpBalance.ToList(), tmpBalanceIntegrado.ToList());
            return Ok(result);
        }
        private IEnumerable<BalanceView> ConstruirJerarquia(List<CuentaMayorView> cuentas, DateTime fecha, DateTime fechaHasta)
        {
            var result = cuentas
                .OrderBy(c => c.Id)
                .Select(c =>
                {
                    return new BalanceView
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        IdTipo = c.IdTipo,
                        IdUso   = c.IdUso,
                        IdSuperior = c.IdSuperior,
                        Debitos = 0,
                        Creditos = 0,
                        SaldoPeriodo = 0,
                        Saldo = 0,
                        Cuentas = ConstruirJerarquia(c.Cuentas.ToList(), fecha, fechaHasta).ToList()
                    };
                });

            return result;
        }
        private IEnumerable<BalanceView> AsignarSaldos(List<BalanceView> balance, List<BalanceView> BalanceIntegrado) 
        {
            foreach (var item in BalanceIntegrado) 
            {   
                item.SaldoAnterior = balance.Where(w=>w.Id==item.Id).Sum(s=>s.SaldoAnterior);
                item.Debitos = balance.Where(w => w.Id == item.Id).Sum(s => s.Debitos);
                item.Creditos = balance.Where(w => w.Id == item.Id).Sum(s => s.Creditos);
                item.SaldoPeriodo = balance.Where(w => w.Id == item.Id).Sum(s => s.SaldoPeriodo);
                item.Saldo = balance.Where(w => w.Id == item.Id).Sum(s => s.Saldo);
                if (item.Cuentas != null) 
                {
                    this.AsignarSaldos(balance, item.Cuentas.ToList());
                }
                //Sumar Cuentas de Integración
                if (item.Cuentas.Any())
                {
                    item.SaldoAnterior += item.Cuentas.Sum(c => c.SaldoAnterior);
                    item.Debitos += item.Cuentas.Sum(c => c.Debitos);
                    item.Creditos += item.Cuentas.Sum(c => c.Creditos);
                    item.SaldoPeriodo += item.Cuentas.Sum(c => c.SaldoPeriodo);
                    item.Saldo += item.Cuentas.Sum(c => c.Saldo);
                }
            } 
            return BalanceIntegrado;
        }


        public IEnumerable<BalanceView> CalcularBalance(DateTime fecha, DateTime fechaHasta)
        {
            
            var result = _service.GetAll()
            .SelectMany(m => m.Detalle)
            .GroupBy(d => d.IdCuentaMayor)
            .Select(g => new BalanceView
            {
                Id = g.Key,
                Nombre = g.FirstOrDefault().CuentaMayor.Nombre,
                IdTipo = g.FirstOrDefault().CuentaMayor.IdTipo,
                IdUso = g.FirstOrDefault().CuentaMayor.IdUso,
                IdSuperior = g.FirstOrDefault().CuentaMayor.IdSuperior,
                SaldoAnterior = g.Where(w => w.Mayor.Fecha < fecha).Sum(d => d.IdTipo == "1" ? d.Importe : -d.Importe),
                Debitos = g.Where(w => w.Mayor.Fecha >= fecha && w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "1" ? d.Importe : 0),
                Creditos = g.Where(w => w.Mayor.Fecha >= fecha && w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "2" ? d.Importe : 0),
                SaldoPeriodo = g.Where(w => w.Mayor.Fecha >= fecha && w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "1" ? d.Importe : -d.Importe),
                Saldo = g.Where(w => w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "1" ? d.Importe : -d.Importe)
            })
            .OrderBy(g => g.Id)
            .ToList();
            return result;
        }


        [HttpGet("ListView")]
        public IActionResult ListView(string IdCuentaMayor,DateTime? fecha, DateTime? fechaHasta)
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
            if (string.IsNullOrEmpty(IdCuentaMayor))
            {
                return BadRequest("IdCuentaMayor Requerido");
            }
            if (fecha > fechaHasta) 
            {
                return BadRequest("Rango de fecha no válido");

            }
            var result = this.ListMayorView(IdCuentaMayor, fecha, fechaHasta);
                               

            return Ok(result);

        }
        [HttpGet("diario")]
        public IActionResult Diario(DateTime? fecha, DateTime? fechaHasta)
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
            if (fecha > fechaHasta)
            {
                return BadRequest("Rango de fecha no válido");

            }
            var result = _service.GetAll()
            .OrderBy(o => o.Fecha)
            .Where(w => w.Fecha >= fecha && w.Fecha <= fechaHasta)
            .ToList();

            return Ok(result);

        }
        [HttpGet("printbalance")]
        public async Task<IActionResult> PrintBalance(DateTime? fecha, DateTime? fechaHasta)
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
            if (fecha > fechaHasta)
            {
                return BadRequest("Rango de fecha no válido");

            }
           
            var result = this.CalcularBalance(pfecha, pfechaHasta);

            var empresa = this.empresaService.GetOne(this.sessionService.IdEmpresa);


            Font font = new Font();
            font.Size = 7;
            Font fontTitulo = new Font();
            fontTitulo.Size = 7;
            Font fontD = new Font();
            fontD.Size = 7;
            Font fontDes = new Font();
            fontDes.Size = 7;

            float[] columnWidths = new float[] { 1.8F, 4F, 1.8F, 1.8F, 1.8F, 1.8F, 1.8F};


            PdfPTable Table = new PdfPTable(columnWidths);
            // Conf
            // Table.SkipFirstHeader = True
            // Table.SkipLastFooter = True
            Table.HeaderRows = 1;
            // Header
            PdfPCell cell = new PdfPCell(new Phrase("Codigo", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Nombre", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Saldo Anterior", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Debitos", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Creditos", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Saldo Periodo", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Saldo", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            
            foreach (var item in result)
            {
                BaseColor colorBack;
                colorBack = BaseColor.WHITE;

                cell = new PdfPCell(new Phrase(item.Id, font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Nombre, font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.SaldoAnterior.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Debitos.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Creditos.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.SaldoPeriodo.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Saldo.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
            }
            // Agregar Totales
            // Linea en Blanco
            decimal totalDebitos = result.Sum(s => s.Debitos);
            decimal totalCreditos = result.Sum(s => s.Creditos);
            

            cell = new PdfPCell(new Phrase("Totales", font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            cell.Colspan = 3;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalDebitos.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalCreditos.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", font));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);

            var doc = new Document(PageSize.A4, -20f, 10f, 100f, 100f);


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
               
                templateGeneral.Empresa = empresa;
                
                templateGeneral.Titulo = "Balance";
                templateGeneral.Titulo1 = "Balance de saldos contables";
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
                string fileName = "BalanceSaldo.pdf";
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

        }
        [HttpGet("print")]
        public async Task<IActionResult> Print(string IdCuentaMayor, DateTime? fecha, DateTime? fechaHasta)
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
            if (string.IsNullOrEmpty(IdCuentaMayor))
            {
                return BadRequest("IdCuentaMayor Requerido");
            }
            CuentaMayor cuentaMayor = cuentaMayorService.GetOne(IdCuentaMayor);
            if (cuentaMayor == null)
            {
                return BadRequest("La cuenta no existe");
            }
            if (fecha > fechaHasta)
            {
                return BadRequest("Rango de fecha no válido");

            }

            var result = this.ListMayorView(IdCuentaMayor, fecha, fechaHasta);

            var empresa = this.empresaService.GetOne(this.sessionService.IdEmpresa);


            Font font = new Font();
            font.Size = 7;
            Font fontTitulo = new Font();
            fontTitulo.Size = 7;
            Font fontD = new Font();
            fontD.Size = 7;
            Font fontDes = new Font();
            fontDes.Size = 7;

            float[] columnWidths = new float[] { 1.8F, 2.4F, 4F, 1.8F, 1.8F, 1.8F, 1.8F };


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
            cell = new PdfPCell(new Phrase("Concepto", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Debe", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Haber", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Saldo Periodo", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Saldo", font));
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            Table.AddCell(cell);

            foreach (var item in result)
            {
                BaseColor colorBack;
                colorBack = BaseColor.WHITE;

                cell = new PdfPCell(new Phrase(item.Fecha.ToShortDateString(), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Pe.ToString().PadLeft(4, '0') + "-" + item.Numero.ToString().PadLeft(8, '0'), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Concepto, font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Debe.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Haber.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.SaldoPeriodo.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Saldo.ToString("N"), font));
                cell.BackgroundColor = colorBack;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                Table.AddCell(cell);
            }
            // Agregar Totales
            // Linea en Blanco
            decimal totalDebe = result.Sum(s => s.Debe);
            decimal totalHaber = result.Sum(s => s.Haber);


            cell = new PdfPCell(new Phrase("Totales", font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            cell.Colspan = 3;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalDebe.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalHaber.ToString("N"), font));
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", font));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.WHITE;
            cell.Border = 1;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderColor = BaseColor.BLACK;
            Table.AddCell(cell);

            var doc = new Document(PageSize.A4, -20f, 10f, 100f, 100f);


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

                templateGeneral.Empresa = empresa;

                templateGeneral.Titulo = "Mayor General ";
                templateGeneral.Titulo1 = IdCuentaMayor + "-" + cuentaMayor.Nombre;
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
                string fileName = "LibroMayor.pdf";
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

        }
        private ActionResult HandleErrorCondition(string message)
        {
            throw new NotImplementedException();
        }
        private IList<MayorView> ListMayorView( String IdCuentaMayor,DateTime? fecha,DateTime? fechaHasta) 
        {
            var result = _service.GetAll()
           .SelectMany(m => m.Detalle)
           .Select(m => new MayorView
           {
               Id = m.Mayor.Id,
               Fecha = m.Mayor.Fecha,
               FechaComp = m.Mayor.FechaComp,
               FechaVenc = m.Mayor.FechaVenc,
               IdCuentaMayor = m.IdCuentaMayor,
               Nombre = m.CuentaMayor.Nombre,
               Concepto = m.Mayor.Concepto,
               Pe = m.Mayor.Pe,
               Numero = m.Mayor.Numero,
               Debe = m.IdTipo == "1" ? m.Importe : 0,
               Haber = m.IdTipo == "2" ? m.Importe : 0,
               Saldo = (decimal)0
           })
           .OrderBy(o => o.Fecha).ThenBy(o => o.Fecha)
           .Where(w => w.IdCuentaMayor == IdCuentaMayor && w.Fecha >= fecha && w.Fecha <= fechaHasta)
           .ToList();
            var saldo = _service.GetAll()
             .SelectMany(m => m.Detalle)
             .Where(w => w.Mayor.Fecha < fecha && w.IdCuentaMayor == IdCuentaMayor)
             .Sum(s => s.IdTipo == "1" ? s.Importe : -s.Importe);
            decimal saldoPeriodo = 0;
            foreach (var item in result)
            {
                saldo += item.Debe - item.Haber;
                saldoPeriodo += item.Debe - item.Haber;
                item.Saldo = saldo;
                item.SaldoPeriodo = saldoPeriodo;
            }
            return result;
        }
        public class MayorView
        {
            public Guid Id { get; set; }
            public DateTime Fecha { get; set; }
            public DateTime FechaComp { get; set; }
            public DateTime FechaVenc { get; set; }
            public string IdCuentaMayor { get; set; }
            public string Nombre { get; set; }
            public string Concepto { get; set; }
            public int Pe { get; set; }
            public long Numero { get; set; }
            public decimal Debe { get; set; }
            public decimal Haber { get; set; }
            public decimal SaldoPeriodo { get; set; }
            public decimal Saldo { get; set; }            

        }
        public class BalanceView 
        {
            public string Id { get;set; }
            public string Nombre { get; set; }
            public string IdSuperior { get; set; }
            public string IdTipo { get; set; }
            public string IdUso { get; set; }
            public decimal SaldoAnterior { get; set; }
            public decimal Debitos { get; set; }
            public decimal Creditos { get;set; }
            public decimal SaldoPeriodo { get; set; }
            public decimal Saldo { get; set; }
            public IEnumerable<BalanceView> Cuentas { get; set; }
        }

    }
}