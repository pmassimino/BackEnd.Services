using BackEnd.Services.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Api.Core
{
   
    public class SessionService : ISessionService
    {
        public SessionService(IHttpContextAccessor httpContentAccessor) 
        {
            
            if (httpContentAccessor.HttpContext.Request.Headers.TryGetValue("IdEmpresa", out var idEmpresa))
            {
                this.IdEmpresa = Guid.Parse(idEmpresa);
            }
            if (httpContentAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var Authorization))
            {                
                this.Decodejws(Authorization);
            }
        }
        public Guid IdEmpresa { get ; set ; }
        public string IdAccount { get; set; }       
        
        private void Decodejws(string token) 
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = token;
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "id").Value;
            this.IdAccount = id;
        }

    }
}
