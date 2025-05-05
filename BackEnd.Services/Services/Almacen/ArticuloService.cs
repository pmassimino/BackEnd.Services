using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Venta;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Almacen
{
    public interface IArticuloService:IService<Articulo,string>
    {
        
    }
    public class ArticuloService:ServiceBase<Articulo,string>,IArticuloService
    {
        private RepositoryBase<Factura,Guid> _facturaRepository;
        public ArticuloService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork) 
        {         
            this._facturaRepository = new RepositoryBase<Factura, Guid>(UnitOfWork);
        }
        public override IEnumerable<Articulo> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        public override IEnumerable<Articulo> GetByName(string name,int page)
        {
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()) || p.Id.ToUpper().Contains(name.ToUpper())).Skip(page - 1).Take(this.PageSize); ;
            return result;
        }
        public override ValidationResults ValidateDelete(Articulo entity)
        {
            var result =  base.ValidateDelete(entity);
            bool existeDetalle = _facturaRepository
                                 .GetAll()
                                 .AsNoTracking() // Opcional: mejora rendimiento si no necesitas modificar los datos
                                 .Any(factura => factura.Detalle.Any(detalle => detalle.IdArticulo == entity.Id));
            if (existeDetalle) 
            {
                result.AddResult(new ValidationResult("Existen dependencias", this, "IdArticulo", "IdArticulo", null));
            }
            return result;

        }
        public override Articulo NewDefault()
        {
            this.lenFill = 6;
            var result =   base.NewDefault();
            result.Estado = "ACTIVO";
            result.IdUnidad = "007";
            result.CondIva = "001";
            return result;
        }

    }
}
