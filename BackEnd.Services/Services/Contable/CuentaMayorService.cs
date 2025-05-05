using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface ICuentaMayorService : IService<CuentaMayor, string>
    {
        IEnumerable<CuentaMayor> GetCuentasMedioPago();
        IEnumerable<CuentaMayorView> GetAllViews();
    }
    public class CuentaMayorService : ServiceBase<CuentaMayor, string>, ICuentaMayorService
    {
        public CuentaMayorService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<CuentaMayor> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        public override IEnumerable<CuentaMayor> GetAll()
        {
            var result = base.GetAll().OrderBy(p=>p.Id);
            return result.ToList();
        }      
        public IEnumerable<CuentaMayorView> GetAllViews() 
        {
            var tmpresult = base.GetAll().OrderBy(p=>p.Id).Select(c => new CuentaMayorView
            {
                Id = c.Id,
                Nombre = c.Nombre,
                IdSuperior = c.IdSuperior,
                IdTipo = c.IdTipo,
                IdUso = c.IdUso,
                TipoCuentaMayor = c.TipoCuentaMayor,
                UsoCuentaMayor = c.UsoCuentaMayor,                
            }).ToList(); 
            var result = this.ConstruirJerarquia(tmpresult, null);
            return result;
        }
        private  IEnumerable<CuentaMayorView> ConstruirJerarquia(List<CuentaMayorView> cuentas, string idSuperior)
        {
            var result = cuentas
                .Where(c => c.IdSuperior == idSuperior)
                .OrderBy(c => c.Id)
                .Select(c => new CuentaMayorView
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    IdSuperior = c.IdSuperior,
                    IdTipo = c.IdTipo,
                    IdUso = c.IdUso,
                    TipoCuentaMayor = c.TipoCuentaMayor,
                    UsoCuentaMayor = c.UsoCuentaMayor,
                    Cuentas = ConstruirJerarquia(cuentas, c.Id).ToList()
                });
            return result;
        }

        public IEnumerable<CuentaMayor> GetCuentasMedioPago()
        {
            var result = this.GetAll().Where(w => w.IdUso == "3" || w.IdUso == "4" || w.IdUso == "4" || w.IdUso == "6");
            return result;
        }

        public override ValidationResults ValidateDelete(CuentaMayor entity)
        {
            var result = base.ValidateDelete(entity);
            //Validar Cuentas Herederas
            var tmpTieneHerederas = this.GetAll().Where(p => p.IdSuperior == entity.Id );
            if (tmpTieneHerederas.Count() > 0) 
            {
                result.AddResult(new ValidationResult("Hay cuentas que dependen de esta, no se puede eliminar", this, "IdSuperior", "IdSuperior", null));
            }
            return result;
        }
        public override ValidationResults ValidateUpdate(CuentaMayor entity)
        {
            var result =  base.ValidateUpdate(entity);
            var tmpTieneHerederas = this.GetAll().Where(p => p.IdSuperior == entity.Id);
            if (tmpTieneHerederas.Count() > 0)
            {
                //Si tiene Herederas solamente Integracion
                if (entity.IdUso != "1")
                {
                    result.AddResult(new ValidationResult("Esta Cuenta solo puede der de Integracion", this, "IdUso", "IdUso", null));
                }
            }
            return result;
        }
    }
}
