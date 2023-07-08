using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BackEnd.Api.Controllers.Comun
{
    [Route("api/comun/[controller]")]
    [ApiController]
    public class AreaController : ApiController<Area,string>
    {
        IAreaService service;
        private ISessionService sessionService;
      
        private ISettingService settingService;
        public AreaController(IAreaService service, ISessionService sessionService, ISettingService settingService,IAuthService authService) : base(service, authService)
        {
            this.service = service;
            this.sessionService = sessionService;
            this.settingService = settingService;
        }

        [HttpGet("LastOrDefault")]
        public IActionResult GetLastOrDefault()
        {
            string key = "idAreaSelected." + sessionService.IdAccount;
            var value = settingService.GetValue(key);
            Area result = null;
            if (!string.IsNullOrEmpty(value))            {
                
                result = this.service.GetOne(value);
            }
            if (result == null)
            {
                result = service.GetAll().FirstOrDefault();
            }
            return Ok(result);
        }

    }
}