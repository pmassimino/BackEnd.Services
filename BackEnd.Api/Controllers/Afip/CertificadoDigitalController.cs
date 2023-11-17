using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Afip;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BackEnd.Api.Controllers.Afip
{
    [Route("api/afip/[controller]")]
    [ApiController]
    public class CertificadoDigitalController : ApiController<CertificadoDigital, string>
    {
        ICertificadoDigitalService service;
        
        public CertificadoDigitalController(ICertificadoDigitalService service,IAuthService authService) : base(service, authService)
        {
            this.service = service;
        }

        

    }
}