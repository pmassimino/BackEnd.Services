using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Comun
{
    public interface ISeccionService : IService<Seccion, string>
    {

    }
    public class SeccionService : ServiceBase<Seccion, string>, ISeccionService
    {
        public SeccionService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<Seccion> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
    }
}
