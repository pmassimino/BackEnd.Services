using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IUnidadMedidaService : IService<UnidadMedida, string>
    {

    }
    public class UnidadMedidaService : ServiceBase<UnidadMedida, string>, IUnidadMedidaService
    {
        public UnidadMedidaService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
    
    }
}
