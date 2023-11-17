using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Afip;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Comun
{
    public interface IAfipWsService : IService<AfipWs, string>
    {

    }
    public class AfipWsService : ServiceBase<AfipWs, string>, IAfipWsService
    {
        public AfipWsService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }        
    }
}
