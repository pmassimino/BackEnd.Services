using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Ventas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface IMayorService : IService<Mayor, Guid>
    {
        Mayor newFrom(Factura entity);
        IEnumerable<Mayor> GetByTransaction(Guid IdTransaccion);


    }
    public class MayorService : ServiceBase<Mayor, Guid>, IMayorService
    {
        public ICuentaMayorService cuentaMayorService;

        public MayorService(UnitOfWorkGestionDb UnitOfWork, ICuentaMayorService serv1):base(UnitOfWork)
        {
            cuentaMayorService = serv1;
        }
        public override ValidationResults Validate(Mayor Entity)
        {
            var result = base.Validate(Entity);
            result.AddAllResults(this.ValidateUpdate(Entity));
            return result;

        }
        public override ValidationResults ValidateUpdate(Mayor Entity)
        {
            //Importes mayores a cero
            //Controlar balance
            var result = base.ValidateUpdate(Entity);
            if (Entity.Detalle != null)
            {
                var cant = Entity.Detalle.Where(p => p.Importe < 0).Count();
                if (cant > 0)
                {
                    result.AddResult(new ValidationResult("Importe Menor a Cero no Permitido", this, "Importe", "Importe", null));
                }
            }
            //Controlar balance
            if (Entity.Detalle != null)
            {
                var debe = Entity.Detalle.Where(p => p.IdTipo.Trim() == "1").Sum(p => p.Importe);
                var haber = Entity.Detalle.Where(p => p.IdTipo.Trim() == "2").Sum(p => p.Importe);
                if (debe != haber)
                {
                    result.AddResult(new ValidationResult("Asiento no Balancea", this, "Importe", "Importe", null));
                }
            }
            
            return result;
        }
        public override Guid NextID()
        {
            return Guid.NewGuid();
        }
        
        public override bool FixRelation(Mayor entity)
        {
            if (entity.Detalle != null)
            {
                int i = 0;
                foreach (var item in entity.Detalle)
                {
                    item.Id = entity.Id;
                    item.Item = i;
                    i += 1;
                }         
            }
            return base.FixRelation(entity);
        }
        public override Mayor GetOne(Guid id)
        {
            return _Repository.GetAll().Include(d=>d.Detalle).Where(w=>w.Id==id).FirstOrDefault();
        }
        public override IEnumerable<Mayor> GetAll()
        {            
            return _Repository.GetAll().Include(d => d.Detalle.OrderBy(o=>o.IdTipo)).ThenInclude(c => c.CuentaMayor).OrderByDescending(o=>o.Fecha).ThenByDescending(o=>o.Numero);
        }
        public override Mayor Update(Guid id, Mayor entity)
        {            
            //Fix Relation 
            this.FixRelation(entity);
            //set default values
            entity = this.UpdateDefaultValues(entity);
            //this.Calculate(entity);
            //Actualizar Related
            this.UpdateModelChild(id, entity);
            //this.UpdateRelated(entity);
            //Fix Relation            
            this.UnitOfWork.Commit();
            return entity;
        }
        private void UpdateModelChild(Guid id, Mayor entity)
        {
            var entityDB = this.GetOne(id);
            this.UnitOfWork.Context.Entry(entityDB).CurrentValues.SetValues(entity);
            // Actualizar Detalle
            List<DetalleMayor> itemDelete = new List<DetalleMayor>();
            foreach (var item in entityDB.Detalle)
                if (!entity.Detalle.Any(s => s.Id == item.Id && s.Item == item.Item))
                    itemDelete.Add(item);
            foreach (var item in itemDelete)
            {
                entityDB.Detalle.Remove(item);
            }
            foreach (var item in entity.Detalle)
            {
                var dbItem = entityDB.Detalle.SingleOrDefault(s => s.Id == item.Id & s.Item == item.Item);
                if (dbItem != null)
                    this.UnitOfWork.Context.Entry(dbItem).CurrentValues.SetValues(item);
                else
                    entityDB.Detalle.Add(item);
            }
            
        }
        public IEnumerable<Mayor> GetByTransaction(Guid id_Transaction)
        {
            var result = this.GetAll().Where(p => p.IdTransaccion == id_Transaction).ToList();
            return result;
        }
        public override Mayor NewDefault()
        {
            Mayor entity = new Mayor();
            entity.Fecha = DateTime.Now;
            entity.FechaComp = DateTime.Now;
            entity.FechaVenc = DateTime.Now;

            return entity;

        }
        public override IEnumerable<Mayor> GetByName(string name)
        {
            var result = this.GetAll().Where(w => w.Numero.ToString().Contains(name));
            return result;
        }

        public Mayor newFrom(Factura entity)
        {
            Mayor result = new Mayor();
            result.Id = this.NextID();
            result.IdArea = entity.IdArea;
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSeccion = entity.IdSeccion;
            result.IdSucursal = entity.IdSucursal;
            result.IdTransaccion = entity.IdTransaccion;
            string concepto = "FACTURA";
            if (entity.Tipo.Trim() == "2")
            {
                concepto = "NOTA DE CREDITO";
            }
            else if (entity.Tipo.Trim() == "3")
            {
                concepto = "NOTA DE DEBITO";
            }
            result.Concepto = concepto;
            result.Fecha = entity.Fecha;
            result.FechaComp = entity.FechaComp;
            result.FechaVenc = entity.FechaVencimiento;
            //Predeterminar Factura
            string tipVenta = TipoAsiento.Haber;
            string tipAcred = TipoAsiento.Debe;
            //Nota de Credito
            if (entity.Tipo == "2")
            {
                tipVenta = TipoAsiento.Debe;
                tipAcred = TipoAsiento.Haber;
            }
            //detalle

            //Venta
            result.AddDetalle("4111", "VENTA INDUMENTARIA", tipVenta, entity.TotalNeto, entity.FechaVencimiento);
            //Impuestos
            //Acreditación
            foreach (var itemPago in entity.MedioPago)
            {
                result.AddDetalle(itemPago.IdCuentaMayor, itemPago.Concepto, tipAcred, itemPago.Importe, entity.FechaVencimiento, entity.IdCuenta);
            }
            return result;

        }

        
    }    
}
