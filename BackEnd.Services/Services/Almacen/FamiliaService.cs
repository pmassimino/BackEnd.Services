using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Almacen
{
    public interface IFamiliaService : IService<Familia, string>
    {

    }
    public class FamiliaService:ServiceBase<Familia,string>,IFamiliaService
    {
        public FamiliaService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<Familia> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
    }
}
