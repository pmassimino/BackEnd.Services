using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Afip;
using BackEnd.Services.Services.Comun;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface ILibroIvaService : IService<LibroIva, Guid>
    {
        IEnumerable<LibroIva> GetAll(string tipo);
        IEnumerable<LibroIva> GetAll(string tipo,DateTime fecha,DateTime fechaHasta);
        IEnumerable<LibroIvaView> GetAllView(string tipo, DateTime fecha, DateTime fechaHasta,bool filtrarAuto = true,bool autorizado = true);
        IEnumerable<LibroIva> GetByName(string tipo,string name);
        LibroIva NewFrom(Factura entity);
    }
    public class LibroIvaService : ServiceBase<LibroIva, Guid>, ILibroIvaService
    {
        ISujetoService sujetoService;
        IAFIPHelperService afipHelperService;
        public LibroIvaService(UnitOfWorkGestionDb UnitOfWork,ISujetoService sujetoService,IAFIPHelperService afipHelperService) : base(UnitOfWork)
        {
            this.sujetoService = sujetoService;
            this.afipHelperService = afipHelperService;
        }
        public override LibroIva Add(LibroIva entity)
        {
           
            this.Calculate(entity);
            var entityResult = base.Add(entity);
            return entityResult;
        }
        public override LibroIva GetByTransaction(Guid id)
        {
            var result = this.GetAll().Where(w => w.IdTransaccion == id).FirstOrDefault();
            return result;
        }
        public override IEnumerable<LibroIva> GetByName(string name)
        {           
            var result = this.GetAll().Where(p => p.Nombre.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        public override IEnumerable<LibroIva> GetAll()
        {            
            var result =  _Repository.GetAll().Include(d => d.DetalleIva).Include(d=>d.DetalleTributo).OrderByDescending(o => o.Fecha).ThenByDescending(o => o.Numero);
            return result;
        }
        public IEnumerable<LibroIva> GetAll(string tipo)
        {
            return this.GetAll().Where(w => w.Tipo == tipo);
        }
        public IEnumerable<LibroIva> GetAll(string tipo, DateTime fecha, DateTime fechaHasta)
        {            
            return this.GetAll().Where(w => w.Tipo == tipo && w.Fecha.Date >= fecha && w.Fecha.Date <= fechaHasta);
        }
        public IEnumerable<LibroIva> GetByName(string tipo, string name)
        {
            var result = this.GetByName(name).Where(w => w.Tipo == tipo);
            return result;
        }
        public IEnumerable<LibroIvaView> GetAllView(string tipo, DateTime fecha, DateTime fechaHasta,bool filtrarAuto = true,bool autorizado = true) 
        {
            var result = this.GetAll()
                 .Where((w => w.Tipo == tipo && w.Fecha.Date >= fecha && w.Fecha.Date <= fechaHasta 
                         && ((filtrarAuto == true && w.Autorizado == autorizado ) || filtrarAuto ==false) ))
                 .OrderBy(o => o.Fecha).ThenBy(o=>o.FechaComp).ThenBy(o => o.Pe).ThenBy(o => o.Numero)
                 .Select(s => new LibroIvaView
                 {
                     Id = s.Id,
                     IdTransaccion = s.IdTransaccion,
                     Fecha = s.Fecha,
                     FechaComprobante = s.FechaComp,
                     Pe = s.Pe,
                     Numero = s.Numero,
                     NumeroDocumento = s.NumeroDocumento,
                     Gravado = s.Gravado * (s.IdTipo=="1"?1:-1),
                     NoGravado = s.NoGravado*(s.IdTipo == "1" ? 1 : -1),
                     Exento = s.Exento * (s.IdTipo == "1" ? 1 : -1),
                     Iva105 = s.DetalleIva.Where(w => w.CondIva == "004").Sum(s => s.Importe) * (s.IdTipo == "1" ? 1 : -1),
                     Iva21 = s.DetalleIva.Where(w => w.CondIva == "005").Sum(s => s.Importe) * (s.IdTipo == "1" ? 1 : -1),
                     Iva27 = s.DetalleIva.Where(w => w.CondIva == "006").Sum(s => s.Importe) * (s.IdTipo == "1" ? 1 : -1),
                     IvaOtro = s.DetalleIva.Where(w => w.CondIva == "009" || w.CondIva == "008").Sum(s => s.Importe) * (s.IdTipo == "1" ? 1 : -1),
                     OtrosTributos = s.DetalleTributo.Sum(s => s.Importe) * (s.IdTipo == "1" ? 1 : -1),
                     Nombre = s.Nombre,
                     IdTipoDoc = s.IdTipoDoc,
                     IdCuenta = s.IdCuenta,
                     Total = s.Total * (s.IdTipo == "1" ? 1 : -1),
                     LibroIva = s
                 }); ;                 
                     
            return result;
        }
        public override ValidationResults Validate(LibroIva entity)
        {
            var result = base.Validate(entity);
            //Importes mayores a cero
            //Controlar balance
            if (entity.NumeroDocumento >= 0) 
            {
                result.AddResult(new ValidationResult("Numero de documento debe ser mayor a cero", this, "NumeroDocumento","NumeroDocumento",null));
            }
            if (string.IsNullOrEmpty(entity.Nombre)) 
            {
                result.AddResult(new ValidationResult("Nombre Requerido", this, "Nombre", "Nombre", null));
            }
            //validar cuenta
            if (entity.IdCuenta != null) 
            {
                var tmpSujeto = sujetoService.GetOne(entity.IdCuenta);
                if (tmpSujeto == null) 
                {
                    result.AddResult(new ValidationResult("Cuenta no existe", this, "IdCuenta", "IdCuenta", null));
                }
            }
            //Validar importes
            if (entity.Total != entity.Gravado + entity.Iva + entity.NoGravado + entity.Exento + entity.OtrosTributos) 
            {
                result.AddResult(new ValidationResult("Registro no balancea", this, "Total", "Total", null));
            }
            decimal importeTributos = entity.DetalleTributo.Sum(s => s.Importe);
            decimal importeNoGravado = entity.DetalleIva.Where(w => w.CondIva == "001").Sum(s => s.Importe);
            decimal importeExento = entity.DetalleIva.Where(w => w.CondIva == "002").Sum(s => s.Importe);
            decimal importeIva = entity.DetalleIva.Where(w => w.CondIva != "001" && w.CondIva != "002").Sum(s => s.Importe);
            if (importeTributos != entity.OtrosTributos) 
            {
                result.AddResult(new ValidationResult("Otros tributos no coincide con detalle de tributos", this, "OtrosTributos", "OtrosTributos", null));
            }
            if (importeNoGravado != entity.NoGravado)
            {
                result.AddResult(new ValidationResult("No Gravado no coincide con detalle iva", this, "NoGravado", "NoGravado", null));
            }
            if (importeExento != entity.Exento)
            {
                result.AddResult(new ValidationResult("Exento no coincide con detalle iva", this, "Exento", "Exento", null));
            }
            if (importeIva != entity.Iva)
            {
                result.AddResult(new ValidationResult("Exento no coincide con detalle iva", this, "Exento", "Exento", null));
            }

            if (entity.DetalleIva != null)
            {
                var cant = entity.DetalleIva.Where(p => p.Importe < 0).Count();
                if (cant > 0)
                {
                    result.AddResult(new ValidationResult("Importe Menor a Cero no Permitido", this, "Importe", "Importe", null));
                }
            }

            //Controlar balance
            
            return result;
        }
        public LibroIva Calculate(LibroIva entity) 
        {
            decimal importeTributos = entity.DetalleTributo.Sum(s => s.Importe);
            decimal importeNoGravado = entity.DetalleIva.Where(w => w.CondIva == "001").Sum(s => s.BaseImponible);
            decimal importeExento = entity.DetalleIva.Where(w => w.CondIva == "002").Sum(s => s.BaseImponible);
            decimal importeIva = entity.DetalleIva.Where(w => w.CondIva != "001" && w.CondIva != "002").Sum(s => s.Importe);
            decimal importeGravado = entity.DetalleIva.Where(w => w.CondIva != "001" && w.CondIva != "002").Sum(s => s.BaseImponible);
            decimal total = importeGravado + importeIva + importeNoGravado + importeExento + importeTributos;
            entity.Gravado = importeGravado;
            entity.NoGravado = importeNoGravado;
            entity.Exento = importeExento;
            entity.Iva = importeIva;
            entity.OtrosTributos = importeTributos;
            entity.Total = total;
            return entity;

        }
        public LibroIva NewFrom(Factura entity) 
        {
            LibroIva result = new LibroIva();
            result.Id = Guid.NewGuid(); ;
            result.IdArea = entity.IdArea;
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSeccion = entity.IdSeccion;
            result.IdSucursal = entity.IdSucursal;
            result.IdTransaccion = entity.IdTransaccion;
            result.IdMoneda = entity.IdMoneda;
            result.CotizacionMoneda = entity.CotizacionMoneda;
            result.Tipo = "V";
            string idTipo = "1"; //Positivo
            if (entity.Tipo == "2") //Notas de credito
            {
                 idTipo = "2"; //Negativo
            }
            result.IdTipo = idTipo;
            result.CodigoOperacion = "";
            string idComprobante = this.afipHelperService.GetIdComprobanteAfip(entity.Letra, entity.Tipo).ToString().PadLeft(3, '0');
            result.IdComprobante = idComprobante;
            result.Pe = entity.Pe;
            result.Numero = entity.Numero;
            result.Fecha = entity.Fecha;
            result.FechaComp = entity.FechaComp;
            result.FechaVenc = entity.FechaVencimiento;           
            //Sujeto
            var sujeto = sujetoService.GetOne(entity.IdCuenta);
            result.IdCuenta = entity.IdCuenta;
            result.Nombre = sujeto.Nombre;
            result.IdTipoDoc = sujeto.IdTipoDoc;
            result.NumeroDocumento = sujeto.NumeroDocumento;
            //Impuestos
            //Iva 
            foreach (var itemIva in entity.Iva)
            {               
                    result.AddDetalleIva(itemIva.CondIva,itemIva.BaseImponible,itemIva.Importe);
            }
            //Tributos
            foreach (var item in entity.Tributos)
            {                
                result.AddDetalleTributo(item.IdTributo,item.Nombre,item.BaseImponible,item.Tarifa,item.Importe);
            }
            this.Calculate(result);
            return result;
        }

       
    }
}
