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

namespace BackEnd.Api.Controllers.Almacen
{
    [Route("api/almacen/[controller]")]
    [ApiController]
    public class FamiliaController : ApiController<Familia,string>
    {
        public FamiliaController(IFamiliaService service, IAuthService authService) : base(service, authService)
        {
            //this.NombreRecurso = "Almacen.Familia";
        }

    }
}