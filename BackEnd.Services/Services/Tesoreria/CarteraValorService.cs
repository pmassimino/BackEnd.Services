using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BackEnd.Services.Services.Tesoreria
{
    public interface ICarteraValorService : IService<CarteraValor, Guid>
    {
        public IEnumerable<CarteraValorView> GetAllView(DateTime fecha,string estado = "ACTIVO");
        public CarteraValorView GetOneView(Guid id);
        public IEnumerable<CarteraValor> GetAllByTransaction(Guid id);

    }
    public class CarteraValorService : ServiceBase<CarteraValor, Guid>, ICarteraValorService
    {
       
       
        public ITransaccionService transaccionService { get; set; }
       

        public CarteraValorService(UnitOfWorkGestionDb UnitOfWork,
            ITransaccionService transaccionService) : base(UnitOfWork)
        {            
            this.transaccionService = transaccionService;          
          
        }
        public override IEnumerable<CarteraValor> GetAll()
        {
            var result = this._Repository.GetAll().Include(i=>i.MovCarteraValor).Include(i=>i.Sujeto).Include(i=>i.CuentaMayor);
            return result;
        }
        public IEnumerable<CarteraValorView> GetAllView(DateTime fecha,string estado = "ACTIVO") 
        {
            var result = from c in this.GetAll()
                         where  (c.Fecha.Date <= fecha ) && c.MovCarteraValor.LastOrDefault().Estado == estado 
                         select new CarteraValorView
                         { Id=c.Id,IdArea=c.IdArea,IdSeccion=c.IdSeccion,IdSucursal=c.IdSucursal,
                             IdEmpresa = c.IdEmpresa,IdCuenta = c.IdCuenta,NombreCuenta= c.Sujeto.Nombre,
                             Fecha=c.Fecha,FechaVencimiento=c.FechaVencimiento,Numero=c.Numero,Importe=c.Importe,Banco=c.Banco,Sucursal=c.Sucursal
                             ,IdCuentaMayor=c.IdCuentaMayor,NombreCuentaMayor=c.CuentaMayor.Nombre,IdTransaccion=c.IdTransaccion,Obs=c.Obs,Tipo=c.Tipo
                             ,Estado = c.MovCarteraValor.LastOrDefault().Estado };

            return result;
        }
        public CarteraValorView GetOneView(Guid id) 
        {
            var result = from c in this.GetAll()
                         where c.Id ==id
                         select new CarteraValorView
                         {
                             Id = c.Id,
                             IdArea = c.IdArea,
                             IdSeccion = c.IdSeccion,
                             IdSucursal = c.IdSucursal,
                             IdEmpresa = c.IdEmpresa,
                             IdCuenta = c.IdCuenta,
                             NombreCuenta = c.Sujeto.Nombre,
                             Fecha = c.Fecha,
                             FechaVencimiento = c.FechaVencimiento,
                             Numero = c.Numero,
                             Importe = c.Importe,
                             Banco = c.Banco,
                             Sucursal = c.Sucursal
                             ,
                             IdCuentaMayor = c.IdCuentaMayor,
                             NombreCuentaMayor = c.CuentaMayor.Nombre,
                             IdTransaccion = c.IdTransaccion,
                             Obs = c.Obs,
                             Tipo = c.Tipo
                             ,
                             Estado = c.MovCarteraValor.LastOrDefault().Estado
                         };
            return result.FirstOrDefault();
        }
        public IEnumerable<CarteraValor> GetAllByTransaction(Guid id)
        {
            var result = this.GetAll().Where(w => w.IdTransaccion == id);
            return result;
        }

        public override Guid NextID()
        {
            return  Guid.NewGuid();  
        }
        public override ValidationResults ValidateDelete(CarteraValor entity)
        {
            var result =  base.ValidateDelete(entity);
            if (result != null) 
            {
                var tmpMov = entity.MovCarteraValor.OrderByDescending(f=>f.Fecha).FirstOrDefault();
                if (tmpMov != null) 
                {
                    if (tmpMov.Estado != "ACTIVO" && tmpMov.Estado != "ANULADO") 
                    {
                        result.AddResult(new ValidationResult("No se puede emiminar Cartera en el actual estado",this,"CarteraValor", "CarteraValor",null));
                    }
                }
            }
            return result;
        }


    }
   
    
}

