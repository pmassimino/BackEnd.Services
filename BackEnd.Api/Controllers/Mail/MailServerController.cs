using BackEnd.Api.Core;
using BackEnd.Services.Models.Mail;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Venta;

using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers.Venta
{
    [Route("api/mail/[controller]")]
    [ApiController]
    public class MailServerController : ApiController<MailServer, int>
    {
        public MailServerController(IMailServerService service, IAuthService authService) : base(service, authService)
        {           
        }

    }
}