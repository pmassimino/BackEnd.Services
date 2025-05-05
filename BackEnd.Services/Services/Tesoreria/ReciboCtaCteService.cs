using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BackEnd.Services.Services.Tesoreria
{
    public interface IReciboCtaCteService : IService<ReciboCtaCte, Guid>
    {
        ValidationResults ValidateDetalleComp(DetalleComprobante item);
        ValidationResults ValidateDetalleValores(DetalleValores item);
        ValidationResults ValidateRelacion(DetalleRelacion item);
        IList<DetalleRelacion> AddRelacion(ReciboCtaCte entity,DetalleRelacion item);
        ReciboCtaCte Calculate(ReciboCtaCte entity);
        Mayor Contabilice(ReciboCtaCte entity);
        NumeradorDocumento NextNumber(string idSeccion);

        IList<ComprobantesDisponible> ComprobantesDisponibles(string idCuenta, string idCuentaMayor);

    }
    public class ReciboCtaCteService : ServiceBase<ReciboCtaCte, Guid>, IReciboCtaCteService
    {
        ITransaccionService transaccionService;
        INumeradorDocumentoService numDocService;
        ISessionService seccionService;
        IConfigReciboService configReciboService;
        IMovCtaCteService movCtaCteService;
        ICarteraValorService carteraValorService;
        IMayorService mayorService;
        private RepositoryBase<Mayor, Guid> mayorRepository;
        ICuentaMayorService cuentaMayorService;
        public ReciboCtaCteService(UnitOfWorkGestionDb UnitOfWork,
            IContableService contaService,ITransaccionService transaccionService,INumeradorDocumentoService numDocService,
            ISeccionService seccionService,IConfigReciboService configReciboService, IMovCtaCteService movCtaCteService,
            ICuentaMayorService cuentaMayorService,IMayorService mayorService,ICarteraValorService carteraValorService) : base(UnitOfWork)
        {   
            
            this.transaccionService = transaccionService;
            this.transaccionService.autoSave = false;
            this.transaccionService.UnitOfWork = UnitOfWork;
            this.numDocService = numDocService;
            this.numDocService.autoSave = false;
            this.numDocService.UnitOfWork = UnitOfWork;
            this.configReciboService = configReciboService;
            this.movCtaCteService = movCtaCteService;
            this.movCtaCteService.autoSave = false;
            this.movCtaCteService.UnitOfWork = UnitOfWork;
            this.carteraValorService = carteraValorService;
            this.carteraValorService.autoSave = false;
            this.carteraValorService.UnitOfWork = UnitOfWork;           
            this.mayorService = mayorService;
            this.mayorService.autoSave = false;
            this.mayorService.UnitOfWork = UnitOfWork;
            this.mayorRepository = new RepositoryBase<Mayor, Guid>(UnitOfWork);
            this.cuentaMayorService = cuentaMayorService;
         
        }
        public override IEnumerable<ReciboCtaCte> GetAll()
        {
            var result = this._Repository.GetAll().Include(i => i.Sujeto).Include(i => i.CuentaMayor).
                Include(i=>i.DetalleComprobante).Include(i=>i.DetalleValores).Include(i=>i.DetalleRelacion).OrderBy(o => o.Fecha).
                ThenBy(O=>O.Numero);
            return result;
        }
        public override ReciboCtaCte GetOne(Guid id)
        {
            return this.GetAll().Where(w=>w.Id == id).FirstOrDefault();
        }
        public override ReciboCtaCte GetByTransaction(Guid id)
        {
            var result = this.GetAll().Where(w => w.IdTransaccion == id).FirstOrDefault();
            return result;
        }

        public override IEnumerable<ReciboCtaCte> GetByName(string name)
        {
            var result = this.GetAll().Where(w => w.Sujeto.Nombre.Contains(name) || w.Numero.ToString().Contains(name) || w.Importe.ToString().Contains(name));
            return result;
        }
        public override ReciboCtaCte Add(ReciboCtaCte Entity)
        {
            this.Calculate(Entity);            
            //Validar
            ValidationResults result = this.Validate(Entity);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }            
            //set default values
            Entity = this.AddDefaultValues(Entity);
            Entity.Id = this.NextID();
                    
            //Generar Transaccion
            Transaccion tra = new Transaccion();
            tra.Tipo = "TESORERIA.RECIBO";
            this.transaccionService.Add(tra);
            Entity.IdTransaccion = tra.Id;
            //Numerar            
            var numerador = this.NextNumber(Entity.IdSeccion);
            Entity.Pe = numerador.PuntoEmision;
            Entity.Numero = numerador.Numero;
            this.numDocService.Increment(numerador.Id);
           

            this.FixRelation(Entity);
            var entityResult = _Repository.Add(Entity);
            //this.UnitOfWork.Commit();
            this.UpdateRelated(Entity);
            this.UnitOfWork.Commit();         
           
            return entityResult;           
            
        }
        public override Guid NextID()
        {
            return  Guid.NewGuid();  
         }
        public override void Delete(Guid id)
        {            
            //Validar
            var entity = this.GetOne(id);
            ValidationResults result = this.ValidateDelete(entity);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }

            this._Repository.Delete(id);
            //Elimnar children
            this.DeleteRelated(entity);
            this.UnitOfWork.Commit();
           
        }
        public override ValidationResults Validate(ReciboCtaCte entity)
        {
            var result = base.Validate(entity);
            //Valores requeridos
            if (string.IsNullOrEmpty(entity.IdCuenta))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "IdCuenta", "IdCuenta", null));
            }
            if (string.IsNullOrEmpty(entity.IdCuentaMayor))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "IdCuentaMayor", "IdCuentaMayor", null));
            }
            if (entity.DetalleComprobante == null)
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "DetalleComp", "DetalleComp", null));
            }
            if (entity.DetalleValores == null)
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "DetalleValores", "DetalleValores", null));
            }
            //balance
            if (entity.DetalleValores != null && entity.DetalleComprobante != null)
            {
                var totalValores = entity.DetalleValores.Sum(m => m.IdTipo == "1" ? m.Importe : m.IdTipo == "2" ? -m.Importe : 0);
                var totalComp = entity.DetalleComprobante.Sum(m => m.IdTipo == "1" ? m.Importe : m.IdTipo == "2" ? -m.Importe : 0);
                if (totalComp != totalValores)
                {
                    result.AddResult(new ValidationResult("No Balancea", this, "DetalleValores", "Importe", null));
                }
            }
            foreach (var item in entity.DetalleComprobante)
            {
                if (item.Importe < 0)
                {
                    result.AddResult(new ValidationResult("Comprobante N°:" + item.Numero.ToString() + "no puede ser menor a cero", this, "DetalleComprobante", "DetalleComprobante", null));
                }
            }
            foreach (var item in entity.DetalleValores)
            {
                if (item.Importe < 0)
                {
                    result.AddResult(new ValidationResult("Comprobante N°:" + item.Numero.ToString() + "no puede ser menor a cero", this, "DetalleComprobante", "DetalleComprobante", null));
                }
            }
            //Validar Cuentas cartera de Valores
            foreach (var item in entity.DetalleValores)
            {
                var tmpCuenta = cuentaMayorService.GetOne(item.IdCuentaMayor);
                if (tmpCuenta == null)
                {
                    result.AddResult(new ValidationResult("Cuenta contable no válida", this, "IdCuentaMayor", "DetalleValores", null));
                }
                if (tmpCuenta != null && tmpCuenta.IdUso == "6")
                {
                    if (string.IsNullOrEmpty(item.Banco))
                    {
                        result.AddResult(new ValidationResult("Nombre de banco requerido para cuenta cartera de valores ", this, "DetalleValores.Banco", "DetalleValores", null));
                    }
                    if (string.IsNullOrEmpty(item.Sucursal))
                    {
                        result.AddResult(new ValidationResult("Nombre sucursal requerido para cuenta cartera de valores ", this, "DetalleValores.Sucursal", "DetalleValores", null));
                    }
                    if (item.Numero == 0)
                    {
                        result.AddResult(new ValidationResult("Numero de cheque  requerido para cuenta cartera de valores ", this, "DetalleValores.Numero", "DetalleValores", null));
                    }
                }
            }
            return result;           
        }
        public override ValidationResults ValidateUpdate(ReciboCtaCte entity)
        {
            
            var result =  base.ValidateUpdate(entity);
            result.AddResult(new ValidationResult("No se puede actualizar un recibo", this, "Id", "Id", null));
            return result;

        }

        public ValidationResults ValidateDetalleComp(DetalleComprobante item)
        {
            ValidationResults result = new ValidationResults();
            if (item.Importe <= 0)
            {
                result.AddResult(new ValidationResult("Valor debe ser mayor a cero", this, "DetalleComp", "Importe", null));
            }
            if (string.IsNullOrEmpty(item.IdTipo))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "DetalleComp", "IdTipo", null));
            }
            return result;
        }
        public override ValidationResults ValidateDelete(ReciboCtaCte entity)
        {
            var result = base.ValidateDelete((entity));
            //Verificar Recibos Cta.Cte.
            var tmpmov = this.movCtaCteService.GetByTransaction(entity.IdTransaccion);
            foreach (var item in tmpmov)
            {
                var cant = this._Repository.GetAll().Where(w => w.IdCuenta == entity.IdCuenta && w.Id != entity.Id).Where(w => w.DetalleRelacion.Any(a => a.IdMovCtaCte == item.Id)).Count();
                if (cant > 0)
                {
                    result.AddResult(new ValidationResult("Hay Recibos Cta. Cte. imputados a este Recibo", this, "ReciboCtaCte", "ReciboCtaCte", null));
                }                
            }
            foreach (var item in entity.DetalleValores) 
            {
                if (item.IdCarteraValor != null) 
                {
                    var tmpCartera = this.carteraValorService.GetOneView(item.IdCarteraValor);
                    if (tmpCartera != null) 
                    {
                        if (tmpCartera.Estado != "ACTIVO") 
                        {
                            result.AddResult(new ValidationResult("Hay Valores en estado no habilitado para borrar", this, "CarteraValor", "CarteraValor", null));
                        }
                    }
                }
            }
            return result;
        }
        #region "Related"
        public void DeleteRelated(ReciboCtaCte entity)
        {
            //Mayor,
            var tmpMov = this.mayorService.GetByTransaction(entity.IdTransaccion);
            foreach (var item in tmpMov)
            {
                this.mayorService.Delete(item.Id);
            }
            //Cta. Cte
            var tmpMovCta = this.movCtaCteService.GetByTransaction(entity.IdTransaccion);
            foreach (var item in tmpMovCta)
            {
                this.movCtaCteService.Delete(item.Id);
            }
            //Carteran de Valor
            var tmpCarteraValor = this.carteraValorService.GetAllByTransaction(entity.IdTransaccion);
            foreach (var item in tmpCarteraValor)
            {
                this.carteraValorService.Delete(item.Id);
            }

        }
        #endregion
        public int UpdateRelated(ReciboCtaCte entity)
        {
            //Cuenta Cte
            var newMov = this.movCtaCteService.NewFrom(entity);
            newMov.Fecha = entity.Fecha;
            newMov.FechaVenc = entity.FechaVencimiento;
            newMov.FechaComp = entity.Fecha;
            newMov.Importe = entity.Importe;
            newMov.IdCuentaMayor = entity.IdCuentaMayor;
            //borrar mov si existe
            var tmpmov = this.movCtaCteService.GetByTransaction(entity.IdTransaccion);
            if (tmpmov != null)
            {
                foreach (var tmpitem in tmpmov)
                {
                    this.movCtaCteService.Delete(tmpitem.Id);
                }
            }
            newMov.Origen = "rec";
            this.movCtaCteService.Add(newMov);            
            //Asiento contable
            var newMayor = this.Contabilice(entity);
            //borra mov si existe
            var tmpmayor = this.mayorService.GetByTransaction(entity.IdTransaccion);
            if (tmpmayor != null)
            {
                foreach (var tmpitem in tmpmayor)
                {
                    this.mayorService.Delete(tmpitem.Id);
                }
            }
            //insertar mayor
            newMayor.Origen = "rec";
            if (entity.Importe != 0) //No contabilizar recibos de conciliación
            {
                this.mayorRepository.Add(newMayor);
            }
            //actualizar relacionRecibo
            //Restar a Cuenta
            decimal aCuenta = entity.DetalleComprobante.Where(p => p.IdTipoComp == "2").Sum(d => d.Importe);
            DetalleRelacion newRelacion = new DetalleRelacion();
            newRelacion.Id = entity.Id;
            newRelacion.IdMovCtaCte = newMov.Id;
            newRelacion.Importe = newMov.Importe - aCuenta;
            if (newRelacion.Importe > 0)
            {
                //this.AddRelacion(entity,newRelacion);
               entity.DetalleRelacion.Add(newRelacion);
            }
            //Actualizar 
            int i = 0;
            foreach (var item in entity.DetalleComprobante)
            {
                newRelacion = new DetalleRelacion();
                newRelacion.Id = entity.Id;
                newRelacion.Item = i;
                newRelacion.IdMovCtaCte = item.IdMovCtaCte;
                newRelacion.Importe = item.Importe;
                i += 1;
                if (this.ValidateRelacion(newRelacion).IsValid)
                {
                    //this.AddRelacion(entity,newRelacion);
                    entity.DetalleRelacion.Add(newRelacion);
                }                
            }
            //Acualizar Items
            i = 0;
            foreach (var item in entity.DetalleRelacion) 
            {
                item.Item = i;
                i += 1;
            }
            //Cartera de Valores
            foreach (var item in entity.DetalleValores) 
            {
                var tmpCuentaMayor = cuentaMayorService.GetOne(item.IdCuentaMayor);
                if (tmpCuentaMayor.IdUso == "6")
                {
                    CarteraValor tmpCartera = new CarteraValor();
                    tmpCartera.Id = carteraValorService.NextID();
                    item.IdCarteraValor = tmpCartera.Id;
                    tmpCartera.IdEmpresa = entity.IdEmpresa;
                    tmpCartera.IdTransaccion = entity.IdTransaccion;
                    tmpCartera.IdArea = entity.IdArea;
                    tmpCartera.IdSeccion = entity.IdSeccion;
                    tmpCartera.IdSucursal = entity.IdSucursal;
                    tmpCartera.IdCuenta = entity.IdCuenta;
                    tmpCartera.IdCuentaMayor = item.IdCuentaMayor;
                    tmpCartera.Banco = item.Banco;
                    tmpCartera.Sucursal = item.Sucursal;
                    tmpCartera.Fecha = item.Fecha;
                    tmpCartera.FechaVencimiento = item.FechaVencimiento;
                    tmpCartera.Importe = item.Importe;
                    tmpCartera.Numero = item.Numero;
                    tmpCartera.Tipo = "";
                    MovCarteraValor tmpMov = new MovCarteraValor();
                    tmpMov.Id = tmpCartera.Id;
                    tmpMov.Concepto = "RECIBO CTA. CTE.";
                    tmpMov.Tipo = "1"; //iNGRESO
                    tmpMov.Fecha = entity.Fecha;
                    tmpMov.Estado = "ACTIVO";
                    tmpMov.IdTransaccion = entity.IdTransaccion;
                    tmpCartera.MovCarteraValor.Add(tmpMov);
                    carteraValorService.Add(tmpCartera);
                } 

            }
            return 0;
        }
        public ValidationResults ValidateDetalleValores(DetalleValores item)
        {
            ValidationResults result = new ValidationResults();
            if (item.Importe <= 0)
            {
                result.AddResult(new ValidationResult("Valor debe ser mayor a cero", this, "DetalleValores", "Importe", null));
            }
            if (string.IsNullOrEmpty(item.IdTipo))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "DetalleValores", "IdTipo", null));
            }
            if (string.IsNullOrEmpty(item.IdCuentaMayor))
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "DetalleValores", "IdCuentaMayor", null));
            }
            //Validar Cuenta
            var tmpCuenta = this.cuentaMayorService.GetOne(item.IdCuentaMayor);
            if (tmpCuenta == null)
            {
                result.AddResult(new ValidationResult("Cuenta no válida", this, "DetalleValores", "IdCuentaMayor", null));
            }

            return result;
        }
        public ValidationResults ValidateRelacion(DetalleRelacion item)
        {
            ValidationResults result = new ValidationResults();           
            return result;
        }
        public override bool FixRelation(ReciboCtaCte entity)
        {
            //Detalle Comprobantes
            if (entity.DetalleComprobante != null)
            {
                int i = 0;
                foreach (var item in entity.DetalleComprobante)
                {
                    item.Id = entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            //Detalle Valores
            if (entity.DetalleValores != null)
            {
                int i = 0;
                foreach (var item in entity.DetalleValores)
                {
                    item.Id = entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            return true;
        }
        public override ReciboCtaCte AddDefaultValues(ReciboCtaCte entity)
        {
            entity.Fecha = DateTime.Today;
            entity.IdTransaccion = Guid.NewGuid();
            return entity;
        }
        public override ReciboCtaCte NewDefault()
        {
            var entity = base.NewDefault();
            entity.Fecha = DateTime.Today;
            entity.FechaVencimiento = DateTime.Today;
            entity.IdTransaccion = Guid.NewGuid();
            return entity;
        }

        public ReciboCtaCte Calculate(ReciboCtaCte entity)
        {
            //Calcular
            decimal importe = 0;
            foreach (var item in entity.DetalleComprobante)
            {
                importe += item.IdTipo == "1" ? item.Importe : -item.Importe;
            }
            entity.Importe = importe;
            return entity;
        }
        public Mayor Contabilice(ReciboCtaCte entity)
        {
            Mayor result = this.mayorService.NewDefault();
            result.Id = this.mayorService.NextID();
            result.IdArea = entity.IdArea;
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSeccion = entity.IdSeccion;
            result.IdSucursal = entity.IdSucursal;
            result.IdTransaccion = entity.IdTransaccion;
            result.Pe = entity.Pe;
            result.Numero = entity.Numero;
            string concepto = "RECIBO CTA. CTE.";
            if (entity.IdTipo.Trim() == "2")
            {
                concepto = "RECIBO PAGO CTA. CTE.";
            }
            result.Concepto = concepto;
            result.Fecha = entity.Fecha;            
            result.FechaVenc = entity.FechaVencimiento;
            //Predeterminar Recibo Cta Cte
            string tipCuenta = TipoAsiento.Haber;
            string tipAcred = TipoAsiento.Debe;
            //Recibo de Pago
            if (entity.IdTipo == "2")
            {
                tipCuenta = TipoAsiento.Debe;
                tipAcred = TipoAsiento.Haber;
            }
            //detalle

            //Cuenta Corriente
            string nombre = this.cuentaMayorService.GetOne(entity.IdCuentaMayor).Nombre;
            result.AddDetalle(entity.IdCuentaMayor, nombre, tipCuenta, entity.Importe, entity.FechaVencimiento);
            //Impuestos
            //Acreditación
            foreach (var item in entity.DetalleValores)
            {
                result.AddDetalle(item.IdCuentaMayor, item.Concepto, tipAcred, item.Importe, item.FechaVencimiento, entity.IdCuenta);
            }
            return result;

        }
        public IList<DetalleRelacion> AddRelacion(ReciboCtaCte entityNew,DetalleRelacion item)
        {            
            //Validar
            ValidationResults result = this.ValidateRelacion(item);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }
            //Fix Relation
            var entity = this.GetOne(item.Id);
            //Default values
            int count = 0;
            if (entity != null)
            {
                if (entity.DetalleRelacion != null)
                {
                    count = entity.DetalleRelacion.Count() == 0 ? 0 : entity.DetalleRelacion.Max(p => p.Item) + 1;
                }
            }
            else 
            {
                entity = entityNew;
            }
            item.Item = count;
            entity.DetalleRelacion.Add(item);
            this.FixRelation(entity);
            //base.Update(entity.Id,entity);
            return entity.DetalleRelacion;
        }

        public IList<ComprobantesDisponible> ComprobantesDisponibles(string idCuenta = "", string idCuentaMayor = "")
        {

            var tmpresult = this.movCtaCteService.GetAll().
                Where(p => p.IdCuenta == idCuenta && p.IdCuentaMayor == idCuentaMayor)
                .Select(s => new ComprobantesDisponible
                {
                    Id = s.Id,
                    IdMovCtaCte = s.Id,
                    Concepto = s.Concepto,
                    IdTipo = s.IdTipo,
                    Fecha = s.Fecha,
                    Pe = s.Pe,
                    Numero = s.Numero,
                    Importe = s.Importe,
                    ImporteDisponible = s.Importe,
                    ImporteAsignar = 0,
                    Select = false
                }).ToList();
            //Actualizar disponible
            foreach (var item in tmpresult)
            {
                var tmprecibos = this.GetAll().Where(p => p.IdCuenta == idCuenta && p.IdCuentaMayor == idCuentaMayor);
                decimal totalAcuenta = 0;
                foreach (var rec in tmprecibos)
                {
                    decimal ACuenta = rec.DetalleRelacion.Where(p => p.IdMovCtaCte == item.IdMovCtaCte).Sum(s => s.Importe);
                    totalAcuenta += ACuenta;
                }
               item.ImporteDisponible = item.Importe - totalAcuenta;
            }
            var result = tmpresult.Where(w => w.ImporteDisponible != 0).ToList();
            return result;

        }
             

        public NumeradorDocumento NextNumber(string idSeccion)
        {
            var tmpresult = this.configReciboService.GetNumeradorDocumento(idSeccion);
            tmpresult.Numero += 1;
            return tmpresult;         
        }
               
    }
    public class ComprobantesDisponible 
    {
        public Guid Id { get; set; }
        public Guid IdMovCtaCte { get; set; }
        public string Concepto { get; set; }
        public string IdTipo { get; set; }
        public DateTime Fecha { get; set; }
        public int Pe { get; set; }
        public long Numero { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteDisponible { get; set; }
        public decimal ImporteAsignar { get; set; }
        public bool Select { get; set; }
        

    }
    
}

