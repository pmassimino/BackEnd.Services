using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Global;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BackEnd.Api.Controllers.Global
{
    [Route("api/global/[controller]")]
    [ApiController]
    public class LocalidadController : ApiController<Localidad, string>
    {
        ILocalidadService service;
        private readonly IMemoryCache cacheService;
        public LocalidadController(ILocalidadService service, IMemoryCache cacheService, IAuthService authService) : base(service, authService)
        {
            this.service = service;
            this.cacheService = cacheService;
        }
        [HttpGet("provincia/{id}")]
        public IActionResult GetByProvincia(string id)
        {
            //Caching
            string key = "Localidad.ByProvincia." + id;
            if (!this.cacheService.TryGetValue(key, out IList<Localidad> result))
            {
                result = this.service.GetByProvincia(id).ToList();
                this.cacheService.Set(key, result, TimeSpan.FromMinutes(10));
            }
            return Ok(result);
        }
    }
}