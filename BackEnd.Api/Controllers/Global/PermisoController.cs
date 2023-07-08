using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BackEnd.Api.Controllers.Global
{
    [Route("api/global/[controller]")]
    [ApiController]
    public class PermisoController : ApiController<Permiso, string>
    {
        ISessionService sessionService;
        public PermisoController(IPermisoService service, ISessionService sessionService, IAuthService authService) : base(service, authService)
        {
            this.sessionService = sessionService;
        }

    }
}