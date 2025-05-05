using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
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
        IOrganizacionService organizacionService;
        public EmpresaService(UnitOfWorkGlobalDb UnitOfWork, IOrganizacionService organizacionService) : base(UnitOfWork)
        {
            this.organizacionService = organizacionService;
        }

        public IEnumerable<Empresa> GetByIdAccount(string id)
        {
            //var result = this.GetAll().Where(w => w.Accounts.Count(a => a.IdAccount == id) > 0).ToList();
            var result = this.GetAll();
            return result;
        }
        public override Empresa UpdateDefaultValues(Empresa entity)
        {
            entity.IdOrganizacion = organizacionService.GetAll().FirstOrDefault().Id;
            return base.UpdateDefaultValues(entity);
        }
        public override Empresa AddDefaultValues(Empresa entity)
        {
            entity.IdOrganizacion = organizacionService.GetAll().FirstOrDefault().Id;
            return base.AddDefaultValues(entity);
        }
        public override IEnumerable<Empresa> GetAll()
        {
            return _Repository.GetAll().Include("Accounts");
        }
        public override ValidationResults ValidateUpdate(Empresa entity)
        {
            var result = base.ValidateUpdate(entity);
            result.AddResult(new ValidationResult("Herror Forzado", this, "Nombre", "Nombre", null));
            return result;

        }
    }
}
