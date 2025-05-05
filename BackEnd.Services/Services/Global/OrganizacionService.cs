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
    public interface IOrganizacionService : IService<Organizacion, Guid>
    {       
    }
    public class OrganizacionService : ServiceBase<Organizacion, Guid>, IOrganizacionService
    {
        public OrganizacionService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
        {
        }

     
     
    }
}
