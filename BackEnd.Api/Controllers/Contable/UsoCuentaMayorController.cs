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
    public class UsoCuentaMayorController : ApiController<UsoCuentaMayor, string>
    {
        public UsoCuentaMayorController(IUsoCuentaMayorService service, IAuthService authService) : base(service, authService)
        {           
        }
       

    }
}