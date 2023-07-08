using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface ICondIvaOperacionService : IService<CondIvaOperacion, string>
    {

    }
    public class CondIvaOperacionService : ServiceBase<CondIvaOperacion, string>, ICondIvaOperacionService
    {
        public CondIvaOperacionService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
    
    }
}
