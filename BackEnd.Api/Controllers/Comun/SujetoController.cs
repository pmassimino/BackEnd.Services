using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Comun
{
    [Route("api/comun/[controller]")]
    [ApiController]
    public class SujetoController : ApiController<Sujeto,string>
    {
        public SujetoController(ISujetoService service, IAuthService authService) : base(service, authService)
        {           
        }
       

    }
}