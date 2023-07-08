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
    public interface IRolService : IService<Rol, int>
    {
        IEnumerable<Rol> GetByIdAccount(string id);
    }
    public class RolService : ServiceBase<Rol, int>, IRolService
    {
        RepositoryBase<Rol, int> repository;
        ISessionService sessionService;
        public RolService(UnitOfWorkGlobalDb UnitOfWork,ISessionService sessionService) : base(UnitOfWork)
        {
            this.repository = new RepositoryBase<Rol, int>(UnitOfWork);
            this.sessionService = sessionService;
        }
        public override IEnumerable<Rol> GetAll()
        {
            return repository.GetAll().Include("Permisos").Include("Accounts");
        }
        public IEnumerable<Rol> GetByIdAccount(string id) 
        {
            return this.GetAll().Where(w => w.Accounts.Any(a => a.IdAccount == id));
        }
    }
   
}
