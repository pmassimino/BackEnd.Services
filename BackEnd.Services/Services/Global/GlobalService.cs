using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IGlobalService
    {
        ICondIvaOperacionService CondIvaOperacionService { get; set; }
        IComprobanteService ComprobanteService { get; set; }

    }
    public class GlobalService : IGlobalService
    {
        public ICondIvaOperacionService CondIvaOperacionService { get; set; }
        public IComprobanteService ComprobanteService { get; set; }
        public GlobalService(ICondIvaOperacionService service1,IComprobanteService comprobanteService)
        {
            this.CondIvaOperacionService = service1;
            this.ComprobanteService = comprobanteService;
        }
       
    }
}
