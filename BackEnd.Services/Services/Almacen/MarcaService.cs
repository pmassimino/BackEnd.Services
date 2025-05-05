using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Contable;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Almacen
{
    public interface IMarcaService : IService<Marca, string>
    {

    }
    public class MarcaService : ServiceBase<Marca, string>, IMarcaService
    {

        public MarcaService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        
        }
        
        public override ValidationResults ValidateUpdate(Marca entity)
        {
            var result =  base.ValidateUpdate(entity);            
            return result;
        }
        public override Marca NewDefault()
        {
            this.lenFill = 5;
            return base.NewDefault();
        }
        public override ValidationResults ValidateDelete(Marca entity)
        {
            var result = base.ValidateDelete(entity);
            var articuloRepository = new RepositoryBase<Articulo, string>(UnitOfWork);
            var existeArticulo = articuloRepository.GetAll().Any(w => w.IdMarca == entity.Id);
            if (existeArticulo)
            {
                result.AddResult(new ValidationResult("Existen dependencias", this, "IdArticulo", "IdArticulo", null));
            }
            return result;

        }

    }
}
