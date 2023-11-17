using BackEnd.Api.Core;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Venta;

using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Venta
{
    [Route("api/Ventas/[controller]")]
    [ApiController]
    public class PuntoEmisionController : ApiController<PuntoEmision, string>
    {
        public PuntoEmisionController(IPuntoEmisionService service, IAuthService authService) : base(service, authService)
        {           
        }

    }
}