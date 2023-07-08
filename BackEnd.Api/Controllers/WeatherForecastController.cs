using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Services.Almacen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IArticuloService _ArticuloService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IArticuloService ArticuloService)
        {
            _logger = logger;
            _ArticuloService = ArticuloService;
        }

        [HttpGet]
        [Route("[controller]")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [Route("[controller]/[action]")]   
        public IEnumerable<Articulo> NewArticulo()
        {
            var Articulo = new Articulo();
            Articulo.Id = "001";
            Articulo.Nombre = "Articulo 1";
            Articulo.CostoVenta = 1;
            Articulo.MargenVenta = 100;
            Articulo.PrecioVenta = 2;
            Articulo.PrecioVentaFinal = 2;
            Articulo.AlicuotaIva = 0;
            //_ArticuloService.Add(Articulo);
            var artdefault = _ArticuloService.NewDefault();
            
            return _ArticuloService.GetAll();
        }

    }
}
