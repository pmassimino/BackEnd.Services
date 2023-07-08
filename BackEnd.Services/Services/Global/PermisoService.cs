using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IPermisoService : IService<Permiso, string>
    {
    }
    public class PermisoService : ServiceBase<Permiso, string>, IPermisoService
    {
        ISessionService sessionService;        
        public PermisoService(UnitOfWorkGlobalDb UnitOfWork,ISessionService sessionService) : base(UnitOfWork)
        {
            this.sessionService = sessionService;           
        }
        
    }
   
}
