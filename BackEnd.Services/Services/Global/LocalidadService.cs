using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface ILocalidadService : IService<Localidad, string>
    {
        IEnumerable<Localidad>  GetByProvincia(string id);

    }
    public class LocalidadService : ServiceBase<Localidad, string>, ILocalidadService
    {
        public LocalidadService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
    {
    }
        public override IEnumerable<Localidad> GetAll()
        {
            return _Repository.GetAll().OrderBy(p => p.Nombre).Include(p=>p.Provincia);
        }
        public override Localidad GetOne(string id)
        {
            var result = this.GetAll().Where(w => w.Id == id).FirstOrDefault();
            return result;
        }

        public IEnumerable<Localidad> GetByProvincia(string id)
        {
            return this.GetAll().Where(w => w.IdProvincia == id);
        }
    }
}
