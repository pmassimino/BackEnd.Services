using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Global;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Comun
{
    public interface ISujetoService : IService<Sujeto, string>
    {

    }
    public class SujetoService : ServiceBase<Sujeto, string>, ISujetoService
    {
        ICoreServices coreService;
        ILocalidadService localidadService;
        ICondIvaService condIvaService;
        public SujetoService(UnitOfWorkGestionDb UnitOfWork, ICoreServices coreService,
            ICondIvaService condIvaService,ILocalidadService localidadService) : base(UnitOfWork) 
        {
            this.coreService = coreService;
            this.localidadService = localidadService;
            this.condIvaService = condIvaService;
        }
        
           
        
        public override Sujeto NewDefault()
        {
            var result =  base.NewDefault();
            result.IdTipoDoc = "96";
            result.IdCondicionIva = "03";
            result.NumeroDocumento = 0;
            return result;
        }
        public override IEnumerable<Sujeto> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        public override Sujeto GetOne(string id)
        {
            var result = this.GetAll().Where(w => w.Id == id).FirstOrDefault();
            if (result != null) 
            {                
                result.Localidad = result.IdLocalidad !=null ? localidadService.GetOne(result.IdLocalidad):null;
                result.CondIva = result.IdCondicionIva != null ? condIvaService.GetOne(result.IdCondicionIva):null ;
            }
            return result;
        }
        public override ValidationResults Validate(Sujeto entity)
        {
            var result = base.Validate(entity);
            result.AddAllResults(this.ValidateUpdate(entity));
            //Validar Domicilios
            result.AddAllResults(this.ValidateDomicilio(entity));
            return result;

        }
        public override ValidationResults ValidateUpdate(Sujeto entity)
        {
            var result =  base.ValidateUpdate(entity);
            if (string.IsNullOrEmpty(entity.Id))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "Id", "Id", null));
            }

            if (string.IsNullOrEmpty(entity.Nombre))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "Nombre", "Nombre", null));
            }
            if (string.IsNullOrEmpty(entity.Domicilio))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "Domicilio", "Domicilio", null));
            }
            if (string.IsNullOrEmpty(entity.IdCondicionIva))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "IdCondicionIva", "IdCondicionIva", null));
            }
            if (string.IsNullOrEmpty(entity.IdTipoDoc))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "IdTipoDoc", "IdTipoDoc", null));
            }
            //Validaciones de Cuit
            if ((entity.IdCondicionIva == "01" || entity.IdCondicionIva == "05") & (entity.IdTipoDoc != "80")) 
            {
                result.AddResult(new ValidationResult("Tipo Doc Debe ser CUIT o CUIL para el tipo responsable", this, "IdTipoDoc", "IdTipoDoc", null));
            }
            if (this.coreService.ValidarNumeroDocumento(entity.IdTipoDoc, entity.NumeroDocumento) == false)
            {
                result.AddResult(new ValidationResult("Numero de Documento vo válido", this, "NumeroDocumento", "NumeroDocumento", null));
            }

            //Validar Rol
            if (entity.TipoRolSujeto.Count() == 0)
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "TipoRolSujeto", "TipoRolSujeto", null));
            }
            foreach (var item in entity.TipoRolSujeto)
            {
                if (item.IdTipoRol == "001")
                {

                }
            }
            return result;
        }
        public ValidationResults ValidateDomicilio(Sujeto entity)
        {
            ValidationResults result = new ValidationResults();
            if (entity.Domicilios.Count() > 0) 
            {
                foreach (var item in entity.Domicilios) 
                {
                    if (string.IsNullOrEmpty(item.Direccion)) 
                    {
                        result.AddResult(new ValidationResult("Valor Requerido", this, "Domicilios", "Direccion", null));
                    }
                    if (string.IsNullOrEmpty(item.Nombre))
                    {
                        result.AddResult(new ValidationResult("Valor Requerido", this, "Domicilios", "Nombre", null));
                    }
                }

            }
            return result;
        }

    }
}
