using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Afip;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Contable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Venta
{
    public interface IPuntoEmisionService : IService<PuntoEmision, string>
    {

    }
    public class PuntoEmisionService : ServiceBase<PuntoEmision, string>, IPuntoEmisionService
    {
        public PuntoEmisionService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<PuntoEmision> GetAll()
        {
            var result = this._Repository.GetAll()
                        .Include(i => i.Numeradores) 
                        .ThenInclude(i=>i.NumeradorDocumento)
                        .OrderBy(o => o.Id);
            return result;
        }
        public override PuntoEmision GetOne(string id)
        {
            return this.GetAll().Where(w=>w.Id==id).FirstOrDefault(); 
        }
        public override PuntoEmision Update(string id, PuntoEmision entity)
        {   
            //Fix Relation 
            this.FixRelation(entity);
            //set default values
            entity = this.UpdateDefaultValues(entity);         
            //Actualizar Related
            this.UpdateModelChild(id, entity);
            //Fix Relation            
            this.UnitOfWork.Commit();
            return entity;
        }
        private void UpdateModelChild(String id, PuntoEmision entity)
        {
            var entityDB = this.GetOne(id);
            this.UnitOfWork.Context.Entry(entityDB).CurrentValues.SetValues(entity);
            // Actualizar Numeradores
            List<NumeradorPuntoEmision> itemDeleteMP = new List<NumeradorPuntoEmision>();
            foreach (var item in entityDB.Numeradores)
                if (!entity.Numeradores.Any(s => s.Id == item.Id && s.IdNumeradorDocumento == item.IdNumeradorDocumento))
                    itemDeleteMP.Add(item);
            foreach (var item in itemDeleteMP)
            {
                entityDB.Numeradores.Remove(item);
            }
            foreach (var item in entity.Numeradores)
            {
                var dbItem = entityDB.Numeradores.SingleOrDefault(s => s.Id == item.Id & s.IdNumeradorDocumento == item.IdNumeradorDocumento);
                if (dbItem != null)
                    this.UnitOfWork.Context.Entry(dbItem).CurrentValues.SetValues(item);
                else
                    entityDB.Numeradores.Add(item);
            }
            
        }
    }
}
