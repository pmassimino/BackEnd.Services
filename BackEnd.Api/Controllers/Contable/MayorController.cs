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

namespace BackEnd.Api.Controllers.Contable
{
    [Route("api/contable/[controller]")]
    [ApiController]
    public class MayorController : ApiController<Mayor, Guid>
    {


        IMayorService _service;
        public MayorController(IMayorService service, IAuthService authService) : base(service, authService)
        {
            this._service = service;
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
            var result = _service.GetAll()
            .SelectMany(m => m.Detalle)
            .GroupBy(d => d.IdCuentaMayor)            
            .Select(g => new
            {
                IdCuentaMayor = g.Key,
                Nombre = g.FirstOrDefault().CuentaMayor.Nombre, // Suponiendo que hay una propiedad "Nombre" en la entidad CuentaMayor
                SaldoAnterior = g.Where(w=>w.Mayor.Fecha < fecha).Sum(d => d.IdTipo == "1" ? d.Importe : -d.Importe),
                Debitos = g.Where(w => w.Mayor.Fecha >= fecha && w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "1" ? d.Importe : 0),
                Creditos = g.Where(w => w.Mayor.Fecha >= fecha && w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "2" ? d.Importe : 0),
                SaldoPeriodo = g.Where(w => w.Mayor.Fecha >= fecha && w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "1" ? d.Importe : -d.Importe),
                Saldo = g.Where(w=>w.Mayor.Fecha <= fechaHasta).Sum(d => d.IdTipo == "1" ? d.Importe : -d.Importe)
            })
            .OrderBy(g => g.IdCuentaMayor)
            .ToList();
            return Ok(result);

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
            var result = _service.GetAll()
            .SelectMany(m => m.Detalle)
            .Select(m => new MayorView
            {
                Id=m.Mayor.Id,
                Fecha=m.Mayor.Fecha,
                FechaComp = m.Mayor.FechaComp,
                FechaVenc = m.Mayor.FechaVenc,
                IdCuentaMayor = m.IdCuentaMayor,
                Nombre = m.CuentaMayor.Nombre,
                Concepto = m.Mayor.Concepto,
                Pe = m.Mayor.Pe,
                Numero = m.Mayor.Numero,
                Debe = m.IdTipo == "1" ? m.Importe:0,
                Haber = m.IdTipo == "2" ? m.Importe : 0,
                Saldo = (decimal)0               
            })
            .OrderBy(o => o.Fecha)
            .Where(w=>w.IdCuentaMayor== IdCuentaMayor && w.Fecha >= fecha && w.Fecha <= fechaHasta)
            .ToList();

            var saldo = _service.GetAll()
                .SelectMany(m => m.Detalle)
                .Where(w => w.Mayor.Fecha < fecha && w.IdCuentaMayor==IdCuentaMayor)
                .Sum(s => s.IdTipo == "1" ? s.Importe : -s.Importe);
            
            foreach (var item in result) 
            {
                saldo += item.Debe - item.Haber;
                item.Saldo = saldo;
            }    
                               

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
            public decimal Saldo { get; set; }            

        }

    }
}