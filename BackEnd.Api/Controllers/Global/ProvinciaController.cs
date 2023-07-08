using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Global;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Global
{
    [Route("api/global/[controller]")]
    [ApiController]
    public class ProvinciaController : ApiController<Provincia, string>
    {
        public ProvinciaController(IProvinciaService service, IAuthService authService) : base(service, authService)
        {           
        }
    }
}