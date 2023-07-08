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
    public class CarteraValorController : ApiController<CarteraValor, Guid>
    {
        private ICarteraValorService service;
        private IEmpresaService empresaService;
        ISessionService sessionService;
        IWebHostEnvironment hostingEnvironment;
        ILocalidadService localidadService;
        ISujetoService sujetoService;
        public CarteraValorController(ICarteraValorService service, IAuthService authService, ISessionService sessionService, IEmpresaService empresaService,
            ISujetoService sujetoService, ILocalidadService localidadService, IWebHostEnvironment hostingEnvironment) : base(service, authService)
        {
            this.service = service;
            this.empresaService = empresaService;
            this.sessionService = sessionService;
            this.hostingEnvironment = hostingEnvironment;
            this.localidadService = localidadService;
            this.sujetoService = sujetoService;

        }
        [HttpGet("ListView")]
        public IActionResult ListView(DateTime? fecha, string estado ="ACTIVO")
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            DateTime pfecha = DateTime.Now;           
            if (fecha.HasValue)
            {
                pfecha = (DateTime)fecha;
            }
            try
            {
                return Ok(this.service.GetAllView(pfecha, estado));
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

    }
}