using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface ICondIvaService : IService<CondIva, string>
    {

    }
    public class CondIvaService : ServiceBase<CondIva, string>, ICondIvaService
    {
        public CondIvaService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
    
    }
}
