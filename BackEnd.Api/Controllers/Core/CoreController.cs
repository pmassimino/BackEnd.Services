using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreController : ControllerBase
    {
        ICoreServices _service;
        public CoreController(ICoreServices service)
        {
            this._service = service;
        }
        [HttpGet("ValidarNumeroDocumento")]
        public IActionResult ValidarNumeroDocumento(string tipo, long numero) 
        {
          return Ok(this._service.ValidarNumeroDocumento(tipo, numero));
        }
    }
}