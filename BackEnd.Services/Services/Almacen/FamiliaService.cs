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
    public interface IFamiliaService : IService<Familia, string>
    {

    }
    public class FamiliaService:ServiceBase<Familia,string>,IFamiliaService
    {
        ICuentaMayorService cuentaMayorService;
        public FamiliaService(UnitOfWorkGestionDb UnitOfWork,ICuentaMayorService cuentaMayorService) : base(UnitOfWork)
        {
            this.cuentaMayorService = cuentaMayorService;
        }
        public override IEnumerable<Familia> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        public override ValidationResults ValidateUpdate(Familia entity)
        {
            var result =  base.ValidateUpdate(entity);
            if (!string.IsNullOrEmpty(entity.CtaIngresoDefault)) 
            {
               var tmpCuenta = cuentaMayorService.GetOne(entity.CtaIngresoDefault);
                if (tmpCuenta == null)
                {
                    result.AddResult(new ValidationResult("Cuenta de Ingreso no existe", "Familia", "CtaIngresoDefault", "", null));
                }
                else if (tmpCuenta.IdTipo != "4") 
                {
                    result.AddResult(new ValidationResult("Cuenta de Ingreso no válida", "Familia", "CtaIngresoDefault", "", null));
                }
            }
            if (!string.IsNullOrEmpty(entity.CtaEgresoDefault))
            {
                var tmpCuenta = cuentaMayorService.GetOne(entity.CtaEgresoDefault);
                if (tmpCuenta == null)
                {
                    result.AddResult(new ValidationResult("Cuenta de Egreso no existe", "Familia", "CtaIngresoDefault", "", null));
                }
                else if (tmpCuenta.IdTipo != "5")
                {
                    result.AddResult(new ValidationResult("Cuenta de Egreso no válida", "Familia", "CtaIngresoDefault", "", null));
                }
            }
            return result;
        }
    }
}
