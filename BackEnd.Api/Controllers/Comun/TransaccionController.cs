using System;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Comun
{
    [Route("api/comun/[controller]")]
    [ApiController]
    public class TransaccionController : ApiController<Transaccion,Guid>
    {
        ITransaccionService service;
        private ISessionService sessionService;
      
      
        private ISettingService settingService;
        public TransaccionController(ITransaccionService service, ISessionService sessionService, ISettingService settingService, IAuthService authService) : base(service, authService)
        {
            this.service = service;
            this.sessionService = sessionService;
            this.settingService = settingService;            
        }
        [HttpGet("{id}/Print")]
        public IActionResult Print(Guid id)
        {
            var result = this.service.GetOne(id);
            if (result == null)
            {
                return NotFound();
            }
            if (result.Tipo == "TESORERIA.RECIBO") 
            {               
                string url = "/api/tesoreria/reciboctacte/" + id + "/printByTransaccion";
                return new RedirectResult(url:url);
            }
            if (result.Tipo == "VENTAS.FACTURA")
            {                
                string url = "/api/ventas/factura/" + id + "/printByTransaccion";
                return new RedirectResult(url: url);
            }
            return NotFound();
        }

    }
}