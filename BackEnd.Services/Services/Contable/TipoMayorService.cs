using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface ITipoMayorService : IService<TipoMayor, string>
    {

    }
    public class TipoMayorService : ServiceBase<TipoMayor, string>, ITipoMayorService
    {
        public TipoMayorService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<TipoMayor> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
    }
}
