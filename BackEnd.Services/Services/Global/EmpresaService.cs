using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace BackEnd.Services.Services.Global
{
    public interface IEmpresaService : IService<Empresa, Guid>
    {
        IEnumerable<Empresa> GetByIdAccount(string id);
    }
    public class EmpresaService : ServiceBase<Empresa, Guid>, IEmpresaService
    {
        public EmpresaService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
        {
        }

        public IEnumerable<Empresa> GetByIdAccount(string id)
        {
            //var result = this.GetAll().Where(w => w.Accounts.Count(a => a.IdAccount == id) > 0).ToList();
            var result = this.GetAll();
            return result;
        }
        public override IEnumerable<Empresa> GetAll()
        {
            return _Repository.GetAll().Include("Accounts");
        }
    }
}
