
using BackEnd.Api.Core;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Venta;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Global
{
    [Route("api/ventas/[controller]")]
    [ApiController]
    public class ConfigFacturaController : ApiController<ConfigFactura, string>
    {
        public ConfigFacturaController(IConfigFacturaService service, IAuthService authService) : base(service, authService)
        {           
        }

    }
}