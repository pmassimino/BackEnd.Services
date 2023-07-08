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
    public interface ITransaccionService : IService<Transaccion, Guid>
    {

    }
    public class TransaccionService : ServiceBase<Transaccion, Guid>, ITransaccionService
    {
        ISessionService sessionService;
        public TransaccionService(UnitOfWorkGestionDb UnitOfWork,ISessionService sessionService) : base(UnitOfWork)
        {
            this.sessionService = sessionService;
        }        
       
        public override Transaccion AddDefaultValues(Transaccion entity)
        {
            entity.Owner = this.sessionService.IdAccount;
            return entity;
        }

    }
}
