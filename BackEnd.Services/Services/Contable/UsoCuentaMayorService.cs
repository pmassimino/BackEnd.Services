using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface IUsoCuentaMayorService : IService<UsoCuentaMayor, string>
    {

    }
    public class UsoCuentaMayorService : ServiceBase<UsoCuentaMayor, string>, IUsoCuentaMayorService
    {
        public UsoCuentaMayorService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<UsoCuentaMayor> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        public override IEnumerable<UsoCuentaMayor> GetAll()
        {
            var result = base.GetAll().OrderBy(o => o.Id);
            return result;
        }
    }
}
