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
    public class SettingController : ApiController<Setting,string>
    {
        ISettingService service;
        public SettingController(ISettingService service, IAuthService authService) : base(service, authService)
        {
            this.service = service;
        }
        [HttpGet("GetValue/{key}")]
        public IActionResult GetValue(string key)
        {
            return Ok(this.service.GetValue(key));
        }
        [HttpPut("SetValue/{key}")]
        public IActionResult SetValue(string key,string value)
        {
            return Ok(this.service.SetValue(key,value));
        }

    }
}