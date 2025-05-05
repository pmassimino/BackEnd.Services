using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BackEnd.Api.Controllers.Almacen
{
    [Route("api/almacen/[controller]")]
    [ApiController]
    public class StockController : ApiController<Stock, int>
    {
        IMemoryCache memoryCache;
        IStockService service;
        public StockController(IStockService service,IAuthService authService,IMemoryCache memoryCache) : base(service, authService)
        {
            //this.NombreRecurso = "Almacen.Stock";
            this.service = service;
            this.memoryCache = memoryCache;
        }
        [HttpGet("UpdateFromMovStock")]        
        public IActionResult UpdateFromMovStock()
        {
            try
            {
                var result = service.UpdateFromMovStock();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new List<string[]>
                    {
                    new[] { "Error General", $"{ex.Message}{ex.InnerException?.Message}" }
                    };
                return BadRequest(result);
            }
        }
        [HttpPut("Ajustar")]
        public IActionResult Ajustar(FormAjusteStock form)
        {
            try
            {
                var result = service.Ajustar(form);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new List<string[]>
                    {
                    new[] { "Error General", $"{ex.Message}{ex.InnerException?.Message}" }
                    };
                return BadRequest(result);
            }
        }
    }



}
