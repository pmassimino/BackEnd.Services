using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface IComprobanteMayorService : IService<ComprobanteMayor, string>
    {

    }
    public class ComprobanteMayorService : ServiceBase<ComprobanteMayor, string>, IComprobanteMayorService
    {
        public ComprobanteMayorService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<ComprobanteMayor> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
    }
}
