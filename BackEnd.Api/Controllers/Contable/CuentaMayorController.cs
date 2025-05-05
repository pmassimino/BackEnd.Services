using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Contable
{
    [Route("api/contable/[controller]")]
    [ApiController]
    public class CuentaMayorController : ApiController<CuentaMayor, string> { 


        ICuentaMayorService _service;
        public CuentaMayorController(ICuentaMayorService service, IAuthService authService) : base(service, authService)
        {
            this._service= service;
        }

        [HttpGet("MediosPagos")]
        public IActionResult MediosPagos()
        {
            return Ok(this._service.GetCuentasMedioPago());
        }
        [HttpGet("CuentasSubdiario")]
        public IActionResult CuentasSubdiario()
        {
            return Ok(this._service.GetAll().Where(w=>w.IdUso=="3"));
        }
        [HttpGet("view")]
        public IActionResult GetAllView()
        {
            return Ok(this._service.GetAllViews());
        }
        [HttpGet("Imputables")]
        public IActionResult Imputables()
        {
            return Ok(this._service.GetAll().Where(w => w.IdUso != "1"));
        }

    }
}