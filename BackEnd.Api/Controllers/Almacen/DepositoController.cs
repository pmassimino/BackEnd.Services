using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BackEnd.Api.Controllers.Almacen
{
    [Route("api/almacen/[controller]")]
    [ApiController]
    public class DepositoController : ApiController<Deposito,string>
    {
        IMemoryCache memoryCache;
        public DepositoController(IDepositoService service,IAuthService authService,IMemoryCache memoryCache) : base(service, authService)
        {
            //this.NombreRecurso = "Almacen.Deposito";
            this.memoryCache = memoryCache;
        }    
        


    }
}