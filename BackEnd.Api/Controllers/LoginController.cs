using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Models.Global;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:BaseController
    {
        private readonly IConfiguration configuration;

        // TRAEMOS EL OBJETO DE CONFIGURACIÓN (appsettings.json)
        // MEDIANTE INYECCIÓN DE DEPENDENCIAS.
        private readonly IAccountService service;
        public LoginController(IConfiguration configuration,IAccountService service)
        {
            this.configuration = configuration;
            this.service = service;
        }

        // POST: api/Login

        [AllowAnonymous]
        [HttpPost()]       
        public IActionResult Login(UserLogin userLogin)
        {
            //UserLogin usuarioLogin = new UserLogin();
            var result = this.service.ValidateLogin(userLogin.username, userLogin.password);
            if (result.IsValid == false)
                //return Unauthorized("");
                return Unauthorized(this.errorToValidationError(result)); 

            var tmpaccount = service.GetByUserName(userLogin.username);          
            var _accountInfo = new Account();
            _accountInfo.Id = tmpaccount.Id;
            _accountInfo.Nombre = tmpaccount.Nombre;
            _accountInfo.Email = tmpaccount.Email;
            return Ok(new { token = GenerarTokenJWT(_accountInfo) });          
        }
        [HttpGet()]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok("Test OK");
        }

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private string GenerarTokenJWT(Account _accountInfo)
        {         
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["JWT:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id", _accountInfo.Id),
                    new Claim("nombre", _accountInfo.Nombre),
                    new Claim("email", _accountInfo.Email)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

    public class UserLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}