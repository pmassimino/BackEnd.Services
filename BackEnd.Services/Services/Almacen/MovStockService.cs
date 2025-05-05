using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Services.Contable;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Almacen
{
    public interface IMovStockService : IService<MovStock, int>
    {

    }
    public class MovStockService : ServiceBase<MovStock, int>, IMovStockService
    {

        public MovStockService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        
        }
        
        public override ValidationResults ValidateUpdate(MovStock entity)
        {
            var result =  base.ValidateUpdate(entity);            
            return result;
        }
        
    }
}
