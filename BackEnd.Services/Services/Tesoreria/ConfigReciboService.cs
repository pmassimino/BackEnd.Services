using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Services.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Services.Tesoreria
{
    public interface IConfigReciboService : IService<ConfigRecibo, string>
    {
        NumeradorDocumento GetNumeradorDocumento(string idSeccion);
    }
    public class ConfigReciboService : ServiceBase<ConfigRecibo, string>, IConfigReciboService
    {

       
        private ICoreServices coreService { get; set; }
        private INumeradorDocumentoService numeradorDocumentoService { get; set; }

        public ConfigReciboService(UnitOfWorkGestionDb UnitOfWork,
             INumeradorDocumentoService numeradorDocumentoService, ICoreServices coreService) : base(UnitOfWork)
        {
           
            this.numeradorDocumentoService = numeradorDocumentoService;
            this.coreService = coreService;
        }
       

        public NumeradorDocumento GetNumeradorDocumento(string idSeccion)
        {            
            var tmpResult = this.GetOne(idSeccion);
            var result = numeradorDocumentoService.GetOne(tmpResult.IdNumeradorDocumento);
            return result;
        }
    }
}
