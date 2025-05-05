using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Afip;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Tesoreria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BackEnd.Services.Services.Venta
{
    public interface IFacturaService : IService<Factura, Guid>
    {
        IEnumerable<CuentaMayor> MediosPagos();
        Factura UpdateAfip(Guid id,long cae, int pe, long numero);
        NumeradorDocumento NextNumber(string id, string letra, string tipo);
        
        int DigitosDecimal { get;set; }
    }
    public class FacturaService : ServiceBase<Factura,Guid>,IFacturaService
    {
        private IArticuloService articuloService;
        private IFamiliaService familiaService;
        private ICuentaMayorService cuentaMayorService;
        private IMovCtaCteService movCtaCteService;
        private IMayorService mayorService;
        private RepositoryBase<Mayor, Guid> mayorRepository;
        
        private IModeloAsientoFacturaService modeloAsientoFactura;
        private IGlobalService globalService;
        private ILibroIvaService libroIvaService;
        private ISujetoService sujetoService;
        private RepositoryBase<ReciboCtaCte, string> reciboCtaCteRepository;
        private IConfigFacturaService configFacturaService;
        private IAFIPHelperService afipHelperService;
        private IPuntoEmisionService puntoEmisionService;
        private INumeradorDocumentoService numeradorDocumentoService;

        private ITransaccionService transaccionService { get; set; }
        public int DigitosDecimal { get; set; } = 2;
        public FacturaService(UnitOfWorkGestionDb UnitOfWork,IArticuloService articuloService, IFamiliaService familiaService,
            ICuentaMayorService cuentaMayorService, IMovCtaCteService movCtaCteService, IMayorService mayorService, IModeloAsientoFacturaService modeloAsientoFactura,
            IGlobalService globalService, ITransaccionService transaccionService, IReciboCtaCteService reciboCtaCteService, ILibroIvaService libroIvaService, ISujetoService sujetoService
, IConfigFacturaService configFacturaService, IAFIPHelperService afipHelperService, INumeradorDocumentoService numeradorDocumentoService
, IPuntoEmisionService puntoEmisionService) : base(UnitOfWork)
        {
            this.cuentaMayorService = cuentaMayorService;
            this.articuloService = articuloService;
            this.familiaService = familiaService;
            this.movCtaCteService = movCtaCteService;
            this.mayorService = mayorService;
            this.modeloAsientoFactura = modeloAsientoFactura;
            this.globalService = globalService;
            this.transaccionService = transaccionService;
            this.reciboCtaCteRepository = new RepositoryBase<ReciboCtaCte, string>(UnitOfWork);
            this.libroIvaService = libroIvaService;
            //Configurar para Transaccion
            this.transaccionService.autoSave = false;
            this.transaccionService.UnitOfWork = UnitOfWork;
            this.libroIvaService.autoSave = false;
            this.libroIvaService.UnitOfWork = UnitOfWork;

            this.mayorService.autoSave = false;
            this.mayorService.UnitOfWork = UnitOfWork;
            this.mayorRepository = new RepositoryBase<Mayor, Guid>(UnitOfWork);

            this.movCtaCteService.autoSave = false;
            this.movCtaCteService.UnitOfWork = UnitOfWork;
            this.numeradorDocumentoService = numeradorDocumentoService;
            this.numeradorDocumentoService.autoSave = false;
            this.numeradorDocumentoService.UnitOfWork = UnitOfWork;
            this.sujetoService = sujetoService;
            this.configFacturaService = configFacturaService;
            this.afipHelperService = afipHelperService;
            this.puntoEmisionService = puntoEmisionService;
        }
        public override Factura GetOne(Guid id)
        {
            var result = this.GetAll().Where(w => w.Id == id).FirstOrDefault();
            return result;
        }
        public override IEnumerable<Factura> GetByName(string name)
        {
            var result = this.GetAll().Where(w => w.Total.ToString().Contains(name) || w.Numero.ToString().Contains(name)
            || w.Sujeto.Nombre.ToUpper().Contains(name.ToUpper()) || w.Sujeto.NumeroDocumento.ToString().Contains(name));
            return result;
        }
        public override IEnumerable<Factura> GetAll()
        {
            var result = this._Repository.GetAll()
                        .Include(i => i.Detalle)
                        .Include(i => i.MedioPago)
                        .Include(i=>i.Sujeto)
                        .Include(i=>i.Iva)
                        .Include(i=>i.Tributos)
                        .Include(i=>i.ComprobanteAsociado)
                        .OrderBy(o=>o.Fecha);
            return result;
        }
        public override Factura GetByTransaction(Guid id)
        {
            var result = this.GetAll().Where(w => w.IdTransaccion == id).FirstOrDefault();
            return result;
        }

        public override Factura Add(Factura Entity)
        {
            this.Calculate(Entity);                        
            //Fix Relation 
            //this.FixRelation(Entity);
            //set default values
            Entity = this.AddDefaultValues(Entity);
            //Generar Transaccion
            Transaccion tra = new Transaccion();
            tra.Tipo = "VENTAS.FACTURA";
            this.transaccionService.Add(tra);
            Entity.IdTransaccion = tra.Id;
            Entity.Origen = tra.Tipo;
            //Numerador
            var tmpNumerador = this.NextNumber(Entity.IdPuntoEmision, Entity.Letra, Entity.Tipo);
            Entity.Pe = tmpNumerador.PuntoEmision;
            Entity.Numero = tmpNumerador.Numero;            
            this.numeradorDocumentoService.Increment(tmpNumerador.Id);

            this.FixRelation(Entity);
            var entityResult = _Repository.Add(Entity);
            this.UpdateRelated(Entity);
            this.UnitOfWork.Commit();
            return entityResult;
        }
        public override Factura Update(Guid id,Factura entity)
        {            
            //Validar
            ValidationResults result = this.ValidateUpdate(entity);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }
            //Fix Relation 
            this.FixRelation(entity);
            //set default values
            entity = this.UpdateDefaultValues(entity);
            this.Calculate(entity);
            //Actualizar Related
            this.UpdateModelChild(id,entity);            
            this.UpdateRelated(entity);
            //Fix Relation            
            this.UnitOfWork.Commit();
            return entity;
        }
        private void UpdateModelChild(Guid id,Factura entity) 
        {
            var entityDB = this.GetOne(id);
            this.UnitOfWork.Context.Entry(entityDB).CurrentValues.SetValues(entity);
            // Actualizar Detalle
            List<DetalleFactura> itemDelete = new List<DetalleFactura>();
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
            // Actualizar Medio Pago
            List<MedioPago> itemDeleteMP = new List<MedioPago>();
            foreach (var item in entityDB.MedioPago)
                if (!entity.MedioPago.Any(s => s.Id == item.Id && s.Item == item.Item))
                    itemDeleteMP.Add(item);
            foreach (var item in itemDeleteMP)
            {
                entityDB.MedioPago.Remove(item);
            }
            foreach (var item in entity.MedioPago)
            {
                var dbItem = entityDB.MedioPago.SingleOrDefault(s => s.Id == item.Id & s.Item == item.Item);
                if (dbItem != null)
                    this.UnitOfWork.Context.Entry(dbItem).CurrentValues.SetValues(item);
                else
                    entityDB.MedioPago.Add(item);
            }
            // Actualizar Iva
            List<DetalleIva> itemDeleteIVA = new List<DetalleIva>();
            foreach (var item in entityDB.Iva)
                if (!entity.Iva.Any(s => s.Id == item.Id && s.Item == item.Item))
                    itemDeleteIVA.Add(item);
            foreach (var item in itemDeleteIVA)
            {
                entityDB.Iva.Remove(item);
            }
            foreach (var item in entity.Iva)
            {
                var dbItem = entityDB.Iva.SingleOrDefault(s => s.Id == item.Id & s.Item == item.Item);
                if (dbItem != null)
                    this.UnitOfWork.Context.Entry(dbItem).CurrentValues.SetValues(item);
                else
                    entityDB.Iva.Add(item);
            }
            // Actualizar Tributos
            List<DetalleTributos> itemDeleteT = new List<DetalleTributos>();
            foreach (var item in entityDB.Tributos)
                if (!entity.Tributos.Any(s => s.Id == item.Id && s.Item == item.Item))
                    itemDeleteT.Add(item);
            foreach (var item in itemDeleteT)
            {
                entityDB.Tributos.Remove(item);
            }
            foreach (var item in entity.Tributos)
            {
                var dbItem = entityDB.Tributos.SingleOrDefault(s => s.Id == item.Id & s.Item == item.Item);
                if (dbItem != null)
                    this.UnitOfWork.Context.Entry(dbItem).CurrentValues.SetValues(item);
                else
                    entityDB.Tributos.Add(item);
            }
            // Actualizar Compronante Asociado
            List<ComprobanteAsociado> itemDeleteCA = new List<ComprobanteAsociado>();
            foreach (var item in entityDB.ComprobanteAsociado)
                if (!entity.ComprobanteAsociado.Any(s => s.Id == item.Id && s.Item == item.Item))
                    itemDeleteCA.Add(item);
            foreach (var item in itemDeleteCA)
            {
                entityDB.ComprobanteAsociado.Remove(item);
            }
            foreach (var item in entity.ComprobanteAsociado)
            {
                var dbItem = entityDB.ComprobanteAsociado.SingleOrDefault(s => s.Id == item.Id & s.Item == item.Item);
                if (dbItem != null)
                    this.UnitOfWork.Context.Entry(dbItem).CurrentValues.SetValues(item);
                else
                    entityDB.ComprobanteAsociado.Add(item);
            }
            //this.FixRelation(entityDB);
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
            _Repository.Delete(id);
            this.DeleteRelated(entity.IdTransaccion);
            this.UnitOfWork.Commit();           

        }

        public int UpdateRelated(Factura Entity)
        {
            //Forma de pago
            //Cuenta Cte
            foreach (var item in Entity.MedioPago)
            {                
                var cta = cuentaMayorService.GetOne(item.IdCuentaMayor);
                if (cta.IdUso == "3")//Cta.Cte.
                {
                    var newMov = this.movCtaCteService.NewFrom(Entity);
                    newMov.FechaVenc = Entity.FechaVencimiento;
                    newMov.Importe = item.Importe;
                    newMov.IdCuentaMayor = item.IdCuentaMayor;
                    //borrar mov si existe
                    var tmpmov = this.movCtaCteService.GetByTransaction(Entity.IdTransaccion);
                    if (tmpmov != null)
                    {
                        foreach (var tmpitem in tmpmov)
                        {
                            this.movCtaCteService.Delete(tmpitem.Id);
                        }
                    }
                    this.movCtaCteService.Add(newMov);                    
                }
            }
            //Contabilidad
            var newMayor = this.Contabilice(Entity);
            //borra mov si existe            
            var tmpmayor = this.mayorRepository.GetAll().Where(w=>w.IdTransaccion==Entity.IdTransaccion);
            if (tmpmayor != null)
            {

                foreach (var tmpitem in tmpmayor)
                {                    
                    this.mayorRepository.Delete(tmpitem.Id);
                }
            }
            //insertar mayor           
            this.mayorRepository.Add(newMayor);
            //libro de iva
            //borra mov si existe
            var tmpLibroIva = this.libroIvaService.GetByTransaction(Entity.IdTransaccion);            
            if (tmpLibroIva != null)
            {               
                this.libroIvaService.Delete(tmpLibroIva.Id);               
            }
            var libroIva = libroIvaService.NewFrom(Entity);
            this.libroIvaService.Add(libroIva);            
           
            return 0;
        }
        public int DeleteRelated(Guid id)
        {

            int result = 0;
            //Borrar Mayor
            var tmpmayor = this.mayorService.GetByTransaction(id);
            if (tmpmayor != null)
            {

                foreach (var tmpitem in tmpmayor)
                {
                    this.mayorService.Delete(tmpitem.Id);
                }
            }
            //Borrar CtaCte
            var tmpCtaCte = this.movCtaCteService.GetByTransaction(id);
            if (tmpCtaCte != null)
            {

                foreach (var tmpitem in tmpCtaCte)
                {
                    this.movCtaCteService.Delete(tmpitem.Id);
                }
            }
            //Borrar Libro de Iva
            var tmpLibroIva = this.libroIvaService.GetByTransaction(id);
            if (tmpLibroIva != null)
            {               
                this.libroIvaService.Delete(tmpLibroIva.Id);                
            }
            return result;
        }       

        public override ValidationResults Validate(Factura Entity)
        {
            var result = base.Validate(Entity);
            result.AddAllResults(this.ValidateUpdate(Entity));
            return result;
        }
        public override ValidationResults ValidateUpdate(Factura entity)
        {            
            var result = base.ValidateUpdate(entity);
            var entityOrg = this.GetOne(entity.Id);
            if (entityOrg != null)
            {
                if (entityOrg.Cae != 0)
                {
                    result.AddResult(new ValidationResult("Factura con CAE no se puede actualizar", this, "Cae", "Cae", null));
                }
            }
            if (entity.IdCuenta == null)
            {
                result.AddResult(new ValidationResult("Valor Requerido", this, "IdCuenta", "IdCuenta", null));
            }
            if (entity.Detalle.Count() == 0)
            {
                result.AddResult(new ValidationResult("La factura debe tener almenos un item", this, "Detalle", "Detalle", null));
            }
            if (entity.MedioPago.Count() == 0)
            {
                result.AddResult(new ValidationResult("Valor requerido", this, "MedioPago", "MedioPago", null));
            }
            if (entity.MedioPago.Sum(p => p.Importe) != entity.Total)
            {
                result.AddResult(new ValidationResult("El total de la factura no coincide con el total de medio de pagos", this, "MedioPago", "MedioPago", null));
            }
            //Verificar Recibos Cta.Cte.
            var tmpmov = this.movCtaCteService.GetByTransaction(entity.IdTransaccion);
            foreach (var item in tmpmov)
            {
                var cant = this.reciboCtaCteRepository.GetAll().Where(w => w.IdCuenta == entity.IdCuenta).Where(w => w.DetalleRelacion.Any(a => a.IdMovCtaCte == item.Id)).Count();
                if (cant > 0)
                {
                    result.AddResult(new ValidationResult("Hay Recibos Cta. Cte. imputados a esta factura", this, "ReciboCtaCte", "ReciboCtaCte", null));
                }
            }
            //Validar Articulos
            foreach (var item in entity.Detalle)
            {
                var tmpExiste = articuloService.GetOne(item.IdArticulo);
                if (tmpExiste == null) 
                {
                    result.AddResult(new ValidationResult("Articulo no existe", item.IdArticulo, "Detalle", "Detalle", null));
                }
            }
            return result;

        }
        public override ValidationResults ValidateDelete(Factura entity)
        {
            var result =  base.ValidateDelete(entity);
            if (entity.Cae != 0) 
            {
                result.AddResult(new ValidationResult("Factura con Cae asignado no se puede eliminar",this, "CAE", "CAE",null));
            }
            //Verificar Recibos Cta.Cte.
            var tmpmov = this.movCtaCteService.GetByTransaction(entity.IdTransaccion);
            foreach (var item in tmpmov) 
            {   
               var cant = this.reciboCtaCteRepository.GetAll().Where(w => w.IdCuenta == entity.IdCuenta).Where(w=>w.DetalleRelacion.Any(a=>a.IdMovCtaCte == item.Id)).Count();
                if (cant > 0) 
                {
                    result.AddResult(new ValidationResult("Hay Recibos Cta. Cte. imputados a esta factura", this, "ReciboCtaCte", "ReciboCtaCte", null));
                }
            }
            return result;
        }
        public Factura Calculate(Factura entity)
        {

            //
            foreach (var item in entity.Detalle)
            {
                var tmpArt = articuloService.GetOne(item.IdArticulo);
                //item.Articulo = tmpArt;
                item.CondIva = tmpArt.CondIva;
                var tmpCondIva = globalService.CondIvaOperacionService.GetOne(item.CondIva);

                if (item.CondIva == "001")//No Gravado
                {
                    item.NoGravado = item.Total;
                }
                else if (item.CondIva == "002")
                {
                    item.Exento = item.Total;
                }
                else if (item.CondIva == "003" || item.CondIva == "004" || item.CondIva=="005" )
                {
                    item.Gravado = item.Total;
                }
                //Calcular Iva
                item.Iva = item.Gravado * tmpCondIva.Alicuota / 100;
                item.Total = item.Gravado + item.Iva;

            }
            //Poner en cero totales
            entity.TotalNeto = 0;
            entity.TotalDescuento = 0;
            entity.Total = 0;
            entity.TotalExento = 0;
            entity.TotalGravado = 0;
            entity.TotalNoGravado = 0;
            entity.TotalIva = 0;
            entity.TotalOTributos = 0;

            foreach (var item in entity.Detalle)
            {
                var tmpArticulo = articuloService.GetOne(item.IdArticulo);
                item.Bonificacion = (item.Cantidad * item.Precio * item.PorBonificacion / 100);
                
                //Si tiene impuesto interno
                if (tmpArticulo.ImpuestoVenta > 0) 
                {
                    item.OtroTributo = item.Cantidad * tmpArticulo.ImpuestoVenta;                
                }
                item.Total = Math.Round((item.Cantidad * item.Precio) - item.Bonificacion,this.DigitosDecimal) ;
                entity.TotalGravado += Math.Round(item.Gravado, this.DigitosDecimal);
                entity.TotalExento += Math.Round(item.Exento, this.DigitosDecimal);
                entity.TotalNoGravado += Math.Round(item.NoGravado, this.DigitosDecimal);
                entity.TotalIva += Math.Round(item.Iva, this.DigitosDecimal);
                entity.TotalNeto += Math.Round(item.Total, this.DigitosDecimal);
                entity.TotalOTributos += Math.Round(item.OtroTributo, this.DigitosDecimal);
                //entity.TotalNoGravado += item.NoGravado;
                //
             }
            //Agregar Otro Tributo si tiene impuestos internos en articulos            
            if (entity.TotalOTributos > 0) 
            {
                int item = entity.Tributos.Count() > 0 ? entity.Tributos.Count : 0;
                entity.Tributos.Add(new DetalleTributos { Id = entity.Id, Item = item, IdTributo = "01", Nombre = "IMPUESTOS NACIONALES", BaseImponible = entity.TotalOTributos, Tarifa = 100, Importe = entity.TotalOTributos });
            }

            //sumar Iva por condición
            var tmpQuery = from p in entity.Detalle group p by p.CondIva into g select new { CondIva = g.Key, Importe = g.Sum(p => p.Iva), Gravado = g.Sum(p => p.Gravado),NoGravado=g.Sum(p=>p.NoGravado), Exento = g.Sum(p => p.Exento) };
            //Borrar Iva
            entity.Iva.Clear();
            int i = 0;
            foreach (var item in tmpQuery)
            {
                var newItem = new DetalleIva();
                newItem.Id = entity.Id;
                newItem.CondIva = item.CondIva;
                newItem.Importe = Math.Round(item.Importe, this.DigitosDecimal);
                newItem.BaseImponible = Math.Round(item.Gravado + item.NoGravado + item.Exento, this.DigitosDecimal) ;
                newItem.Item = i;
                i += 1;
                entity.Iva.Add(newItem);
            }

            entity.TotalDescuento = Math.Round(entity.TotalNeto * entity.PorDescuento / 100, this.DigitosDecimal);
            entity.Total = Math.Round(entity.TotalGravado + entity.TotalNoGravado + entity.TotalExento + entity.TotalIva + entity.TotalOTributos - entity.TotalDescuento, this.DigitosDecimal);
            //Medio de Pago
            if (entity.MedioPago.Count == 1)
            {
                entity.MedioPago.FirstOrDefault().Importe = entity.Total;
            }
            return entity;
        }
        public override bool FixRelation(Factura Entity)
        {
            //Detalle
            if (Entity.Detalle != null)
            {
                int i = 0;
                foreach (var item in Entity.Detalle)
                {
                    item.Id = Entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            //Iva
            if (Entity.Iva != null)
            {
                int i = 0;
                foreach (var item in Entity.Iva)
                {
                    item.Id = Entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            //Tributos
            if (Entity.Tributos != null)
            {
                int i = 0;
                foreach (var item in Entity.Tributos)
                {
                    item.Id = Entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            //medio pago
            if (Entity.MedioPago != null)
            {
                int i = 0;
                foreach (var item in Entity.MedioPago)
                {
                    item.Id = Entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            //medio pago
            if (Entity.ComprobanteAsociado != null)
            {
                int i = 0;
                foreach (var item in Entity.ComprobanteAsociado)
                {
                    item.Id = Entity.Id;
                    item.Item = i;
                    i += 1;
                }
            }
            return true;

        }
        public override Factura AddDefaultValues(Factura entity)
        {
            entity.Fecha = entity.FechaComp;
            entity.IdTransaccion = Guid.NewGuid();
            if (entity.IdMoneda == null) 
            {
                entity.IdMoneda = "PES";
                entity.CotizacionMoneda = 1;
            }
            return entity;
        }
        public override Factura UpdateDefaultValues(Factura entity)
        {
            entity.Fecha = entity.FechaComp;
            return base.UpdateDefaultValues(entity);
        }
        public NumeradorDocumento NextNumber(string id,string letra,string tipo)
        {
            int idComprobante = this.afipHelperService.GetIdComprobanteAfip(letra, tipo);
            //var tmpConfig = this.configFacturaService.GetOne(id);
            var tmpPuntoEmision = this.puntoEmisionService.GetOne(id);
            //var tmpNumerador = tmpConfig.Numeradores.Where(w => w.IdComprobante == idComprobante).FirstOrDefault();
            var tmpNumerador = tmpPuntoEmision.Numeradores.Where(w=>w.NumeradorDocumento.IdComprobante==idComprobante).FirstOrDefault();
            var tmpresult = tmpNumerador.NumeradorDocumento;
            tmpresult.Numero += 1;
            return tmpresult;
        }
        public Mayor Contabilice(Factura entity)
        {
            Mayor result = new Mayor();
            result.Id = Guid.NewGuid(); ;
            result.IdArea = entity.IdArea;
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSeccion = entity.IdSeccion;
            result.IdSucursal = entity.IdSucursal;
            result.IdTransaccion = entity.IdTransaccion;
            result.Pe = entity.Pe;
            result.Origen = "VENTAS.FACTURA";
            result.Numero = entity.Numero;
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
            string ctaIngresoDefault = this.modeloAsientoFactura.GetOne(1).CtaIngresoDefault;
            string nombre = this.cuentaMayorService.GetOne(ctaIngresoDefault).Nombre;
            //Agrupo  
            var tmpTotalArticulo = (from d in entity.Detalle
                            group new { Total = d.Gravado + d.NoGravado +d.Exento, d.IdArticulo} by d.IdArticulo into g
                            select new TotalArticuloResult
                            {
                                IdArticulo = g.Key,
                                IdCuentaMayor=ctaIngresoDefault,
                                Total = g.Sum(x => x.Total)
                            }).ToList(); 
            foreach (var item in tmpTotalArticulo)
            {
                var tmpArticulo = articuloService.GetOne(item.IdArticulo);
                var tmpFamilia = familiaService.GetOne(tmpArticulo.IdFamilia);
                if (tmpFamilia != null) 
                {
                    if (tmpFamilia.CtaIngresoDefault != null) 
                    {
                        item.IdCuentaMayor = tmpFamilia.CtaIngresoDefault;
                    }
                }
            }
            //Agrupo por cuenta
            var tmpTotalPorCuentas = from t in tmpTotalArticulo
                                     group new { t.IdCuentaMayor, t.Total } by t.IdCuentaMayor into g
                                     select new
                                     {
                                         IdCuentaMayor = g.Key,
                                         Total = g.Sum(x => x.Total)
                                     };
            foreach (var item in tmpTotalPorCuentas) 
            {
                nombre = this.cuentaMayorService.GetOne(item.IdCuentaMayor).Nombre;
                result.AddDetalle(item.IdCuentaMayor, nombre, tipVenta, item.Total, entity.FechaVencimiento);
            }
            
            //Iva 
            foreach (var itemIva in entity.Iva) 
            {if (itemIva.Importe > 0)
                {
                    var modeloAsiento = this.modeloAsientoFactura.GetOne(1);
                    string ctaIva = itemIva.CondIva == "005" ? modeloAsiento.CtaIvaGenDefault : modeloAsiento.CtaIvaRedDefault;
                    nombre = this.cuentaMayorService.GetOne(ctaIva).Nombre;
                    result.AddDetalle(ctaIva, nombre, tipVenta, itemIva.Importe, entity.FechaVencimiento);
                }               
            }
            //Impuestos Internos, Ganancia , IB
            string ctaImpuestoDefault = this.modeloAsientoFactura.GetOne(1).CtaImpuestoDefault;
            string ctaPerIvaDefault = this.modeloAsientoFactura.GetOne(1).CtaPerIvaDefault;
            string ctaPerIBDefault = this.modeloAsientoFactura.GetOne(1).CtaPerIGDefault;
            foreach (var item in entity.Tributos) 
            {
                if (item.IdTributo == "01") 
                {                    
                    nombre = this.cuentaMayorService.GetOne(ctaImpuestoDefault).Nombre;
                    result.AddDetalle(ctaImpuestoDefault, nombre, tipVenta, item.Importe, entity.FechaVencimiento);
                }
            }
            //Acreditación
            foreach (var itemPago in entity.MedioPago)
            {
                nombre = this.cuentaMayorService.GetOne(itemPago.IdCuentaMayor).Nombre;
                result.AddDetalle(itemPago.IdCuentaMayor, nombre, tipAcred, itemPago.Importe, entity.FechaVencimiento, entity.IdCuenta);
            }
            return result;

        }
        public override Factura NewDefault()
        {
            var result =  base.NewDefault();
            result.Id = Guid.NewGuid();
            result.IdEmpresa = "001";
            result.IdSeccion = "001";
            result.IdSucursal = "001";
            result.IdArea = "001";
            result.Fecha = DateTime.Now;
            result.FechaComp = DateTime.Now;
            result.FechaVencimiento = DateTime.Now;
            result.Tipo = "1";
            result.IdMoneda = "PES";
            result.CotizacionMoneda = 1;
            result.IdTransaccion = Guid.NewGuid();
            return result;
        }

        public IEnumerable<CuentaMayor> MediosPagos()
        {
            List<CuentaMayor> result = new List<CuentaMayor>();
            result = this.cuentaMayorService.GetAll().Where(w => w.IdUso == "4").ToList();
            return result;
        }

        public Factura UpdateAfip(Guid id, long cae, int pe, long numero)
        {           
            var entity = this.GetOne(id);           
            if (entity== null)
            {
                throw new Exception("No Found");
            }
            entity.Cae = cae;
            entity.Pe = pe;
            entity.Numero = numero;
            //Actualizar Numero            
            var mayor = this.mayorService.GetByTransaction(entity.IdTransaccion).FirstOrDefault();           
            mayor.Pe = pe;
            mayor.Numero = numero;
            this.UnitOfWork.Context.Entry(mayor).State = EntityState.Modified;
            //Actualizar Numero Cta. Cte
            var ctaCte = this.movCtaCteService.GetByTransaction(entity.IdTransaccion);
                                               
            if (ctaCte != null) 
            {
                foreach (var item in ctaCte) 
                {
                    item.Pe = pe;
                    item.Numero = numero;
                    this.UnitOfWork.Context.Entry(item).State = EntityState.Modified;
                }
            }
            //Actualizar Numero Libro de Iva
            var tmpLibroIva = this.libroIvaService.GetByTransaction(entity.IdTransaccion);
            if (tmpLibroIva != null)
            {
                tmpLibroIva.Pe = pe;
                tmpLibroIva.Numero = numero;
                tmpLibroIva.Autorizado = true;
                this.UnitOfWork.Context.Entry(tmpLibroIva).State = EntityState.Modified;               
            }
            this.UnitOfWork.Commit();
            return entity;
        }
    }
    public class TotalArticuloResult
    {
        public string IdArticulo { get; set; }
        public string IdCuentaMayor { get; set; }
        public decimal Total { get; set; }
    }

}
