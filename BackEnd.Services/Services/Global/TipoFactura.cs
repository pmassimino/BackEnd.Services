using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface ITipoFacturaService : IService<TipoFactura, string>
    {

    }
    public class TipoFacturaService : ServiceBase<TipoFactura, string>, ITipoFacturaService
    {
        public TipoFacturaService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
    
    }
}
