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
    public interface INumeradorDocumentoService : IService<NumeradorDocumento, string>
    {
        int Increment(string id,int numero=1);
       
    }
    public class NumeradorDocumentoService : ServiceBase<NumeradorDocumento, string>, INumeradorDocumentoService
    {
        public NumeradorDocumentoService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }

        public int Increment(string id,int numero=1)
        {
            int result = 0;
            var tmpresult = this.GetOne(id);
            if (tmpresult != null) 
            {
                tmpresult.Numero += numero;
                this.Update(id, tmpresult);
                result = tmpresult.Numero;
            }
            return result;
        }

        public override IEnumerable<NumeradorDocumento> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }

        
    }
}
