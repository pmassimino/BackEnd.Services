using BackEnd.Api.Core;
using BackEnd.Services.Comun;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Comun
{
    [Route("api/comun/[controller]")]
    [ApiController]
    public class TipoRolController : ApiController<TipoRol,string>
    {
        public TipoRolController(ITipoRolService service, IAuthService authService) : base(service, authService)
        {           
        }

    }
}