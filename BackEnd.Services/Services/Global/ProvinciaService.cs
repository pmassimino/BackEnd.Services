using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IProvinciaService : IService<Provincia, string>
    {

    }
    public class ProvinciaService : ServiceBase<Provincia, string>, IProvinciaService
    {
        public ProvinciaService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
        public override IEnumerable<Provincia> GetAll()
        {
            return _Repository.GetAll().OrderBy(p => p.Nombre);
        }

    }
}
