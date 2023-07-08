using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IMonedaService : IService<Moneda, string>
    {

    }
    public class MonedaService : ServiceBase<Moneda, string>, IMonedaService
    {
        public MonedaService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
    
    }
}
