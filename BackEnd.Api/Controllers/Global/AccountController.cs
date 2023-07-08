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
    public class AccountController : ApiController<Account, string>
    {
        ISessionService sessionService;
        IAccountService accountService;
        public AccountController(IAccountService service, IAuthService authService,ISessionService sessionService) : base(service, authService)
        {
            this.accountService = service;
            this.sessionService = sessionService;
        }
        public override IActionResult Add([FromBody] Account entity)
        {
            return BadRequest("No Permitido");
        }
        public override IActionResult Update(string id, Account entity)
        {
            return BadRequest("No Permitido");
        }
        public override IActionResult Delete(string id)
        {
            return BadRequest("No Permitido");
        }
        [HttpGet("{id}/Roles")]
        public IActionResult GetRoles(string id)
        {
            string idAccount = sessionService.IdAccount;
            if (idAccount != id) 
            {
                return BadRequest("Permiso Denegado");
            }
            try
            {
                return Ok(this.accountService.GetRoles(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        }
}