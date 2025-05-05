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
    public interface IDepositoService : IService<Deposito, string>
    {

    }
    public class DepositoService:ServiceBase<Deposito,string>,IDepositoService
    {

        public DepositoService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        
        }
        
        public override ValidationResults ValidateUpdate(Deposito entity)
        {
            var result =  base.ValidateUpdate(entity);            
            return result;
        }
        public override Deposito NewDefault()
        {
            this.lenFill = 3;
            var result = base.NewDefault();
            return result;
        }
    }
}
