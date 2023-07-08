using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface ITipoDocumentoService : IService<TipoDocumento, string>
    {

    }
    public class TipoDocumentoService : ServiceBase<TipoDocumento, string>, ITipoDocumentoService
    {
        public TipoDocumentoService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
    
    }
}
