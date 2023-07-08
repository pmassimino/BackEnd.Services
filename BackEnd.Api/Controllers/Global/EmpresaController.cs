using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Global
{
    [Route("api/global/[controller]")]
    [ApiController]
    public class EmpresaController : ApiController<Empresa, Guid>
    {
        private ISessionService sessionService;
        private IEmpresaService service;
        private ISettingGlobalService settingService;
        public EmpresaController(IEmpresaService service,ISessionService sessionService,ISettingGlobalService settingService, IAuthService authService) : base(service, authService)
        {
            this.service = service;
            this.sessionService = sessionService;
            this.settingService = settingService;
        }
        [HttpGet]
        public override IActionResult GetAll()
        {
            return Ok(this.service.GetByIdAccount(sessionService.IdAccount));
        }
        [HttpGet("LastOrDefault")]
        public IActionResult GetLastOrDefault() 
        {
            string key = "idEmpresaSelected." + sessionService.IdAccount;
            var value = settingService.GetValue(key);
            Empresa result = null;
            if (!string.IsNullOrEmpty(value)) 
            {
                Guid id =  Guid.Parse(value);                
                result = this.service.GetOne(id);
            }
            if (result == null) 
            {
                result = service.GetByIdAccount(sessionService.IdAccount).FirstOrDefault();
            }
            return Ok(result);
        }
    }
}