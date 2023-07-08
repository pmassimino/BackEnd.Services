using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Models.Ventas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface IMovCtaCteService : IService<MovCtaCte, Guid>
    {
        IEnumerable<MovCtaCte> GetByTransaction(Guid IdTransaccion);
        MovCtaCte NewFrom(Factura entity);
        MovCtaCte NewFrom(ReciboCtaCte entity);
        IList<MovCtaCte> GetBy(string id_cuenta, string id_cuentaMayor);        
        //saldo de todas las cuentas
        IList<MovSaldoCtaCte> ResumenSaldo(DateTime fecha, string id_cuenta="", string id_cuentaMayor="");
        IList<MovCtaCteView> Resumen(string id_cuenta, string id_cuentaMayor, DateTime fechaDesde, DateTime fechaHasta);

        decimal Saldo(string idCuenta, string idCuentaMayor, DateTime fecha);
        decimal SaldoVencido(string idCuenta, string idCuentaMayor, DateTime fecha);
        decimal SaldoAVencer(string idCuenta, string idCuentaMayor, DateTime fecha);
    }
    public class MovCtaCteService : ServiceBase<MovCtaCte, Guid>, IMovCtaCteService
    {
        public MovCtaCteService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<MovCtaCte> GetAll()
        {
            var result = this._Repository.GetAll().Include(s => s.Sujeto);
            return result;
        }

        public IEnumerable<MovCtaCte> GetByTransaction(Guid IdTransaccion)
        {
            var result = this.GetAll().Where(p => p.IdTransaccion == IdTransaccion);
            return result;
        }
        public override ValidationResults Validate(MovCtaCte Entity)
        {
            var result = base.Validate(Entity);
            if (string.IsNullOrEmpty(Entity.IdTipo))
            {
               result.AddResult(new ValidationResult("Valor Requerido", this, "IdTipo", "IdTipo", null));
            }           
            return result;
        }
        public override ValidationResults ValidateUpdate(MovCtaCte Entity)
        {
            var result = Validate(Entity);
            //Validar Registros Duplicados            
            return result;
        }
        public override Guid NextID()
        {
            return Guid.NewGuid();
        }

        public override IEnumerable<MovCtaCte> GetByName(string Name)
        {
            var result = this.GetAll().Where(p => p.Concepto.ToUpper().Contains(Name.ToUpper())).ToList();
            return result;
        }
       public MovCtaCte NewFrom(Factura entity)
        {
            MovCtaCte result = new MovCtaCte();
            result.Id = this.NextID();
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSucursal = entity.IdSucursal;
            result.IdArea = entity.IdArea;
            result.IdSeccion = entity.IdSeccion;
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
            result.IdCuenta = entity.IdCuenta;
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSucursal = entity.IdSucursal;
            result.FechaComp = entity.FechaComp;
            result.Fecha = entity.Fecha;
            result.FechaVenc = entity.FechaVencimiento;
            result.IdTransaccion = entity.IdTransaccion;
            result.Pe = entity.Pe;
            result.Numero = entity.Numero;
            result.Importe = entity.Total;
            string tipo = "0";
            //Factura o nota de denito
            if (entity.Tipo == "1" || entity.Tipo == "3")
            {
                tipo = "1";
            }
            else
            {
                tipo = "2";
            }
            result.IdTipo = tipo;
            return result;
        }
        public MovCtaCte NewFrom(ReciboCtaCte entity)
        {
            MovCtaCte result = new MovCtaCte();
            result.Id = this.NextID();
            result.IdArea = entity.IdArea;
            result.IdSeccion = entity.IdSeccion;
            result.IdTransaccion = entity.IdTransaccion;
            string concepto = "RECIBO CTA. CTE.";
            if (entity.IdTipo.Trim() == "2")
            {
                concepto = "RECIBO PAGO CTA. CTE.";
            }
           
            result.Concepto = concepto;
            result.Fecha = entity.Fecha;
            result.IdCuenta = entity.IdCuenta;
            result.IdEmpresa = entity.IdEmpresa;
            result.IdSucursal = entity.IdSucursal;           
            result.FechaVenc = entity.FechaVencimiento;
            
            result.IdTransaccion = entity.IdTransaccion;
            result.Pe = entity.Pe;
            result.Numero = entity.Numero;
            result.Importe = entity.Importe;
            string tipo = "0";
            //Factura o nota de debito
            if (entity.IdTipo == "1")
            {
                tipo = "2";
            }
            else
            {
                tipo = "1";
            }
            result.IdTipo = tipo;
            return result;
        }
              

        public IList<MovSaldoCtaCte> ResumenSaldo(DateTime fecha, string idCuenta = "", string idCuentaMayor = "")
        {           
            var result = this.GetAll()
                  .Where((w => w.IdCuenta == idCuenta || string.IsNullOrEmpty(idCuenta)&&(w.IdCuentaMayor==idCuentaMayor ||  string.IsNullOrEmpty(idCuentaMayor))))
                  .OrderBy(s=>s.Sujeto.Nombre)
                  .GroupBy(g => new { g.IdCuenta, g.IdCuentaMayor })            
                  .Select(s => new MovSaldoCtaCte(s.FirstOrDefault().IdCuenta,
                      s.FirstOrDefault().IdCuentaMayor, s.FirstOrDefault().Sujeto.Nombre,
                      s.Where(w => w.FechaVenc <= fecha).Sum(m => m.IdTipo == "1" ? m.Importe : m.IdTipo == "2" ? -m.Importe : 0),
                      s.Where(w => w.FechaVenc > fecha).Sum(m => m.IdTipo == "1" ? m.Importe : m.IdTipo == "2" ? -m.Importe : 0),
                      s.Sum(m => m.IdTipo == "1" ? m.Importe : m.IdTipo == "2" ? -m.Importe : 0))).ToList();
            return result;
        }

        public decimal Saldo(string id_cuenta, string id_cuentaMayor, DateTime fecha)
        {
            var result = this.GetAll().Where(w => w.Fecha <= fecha && w.IdCuenta == id_cuenta && w.IdCuentaMayor == id_cuentaMayor)
                .Sum(s => s.IdTipo == "1" ? s.Importe : s.IdTipo == "2" ? -s.Importe : 0);
            return result;
        }
        public decimal SaldoVencido(string idCuenta, string id_cuentaMayor, DateTime fecha)
        {
            var result = this.GetAll().Where(w => w.FechaVenc <= fecha && w.IdCuenta == idCuenta && w.IdCuentaMayor == id_cuentaMayor)
                .Sum(s => s.IdTipo == "1" ? s.Importe : s.IdTipo == "2" ? -s.Importe : 0);
            return result;
        }
        public decimal SaldoAVencer(string id_cuenta, string id_cuentaMayor, DateTime fecha)
        {
            var result = this.GetAll().Where(w => w.FechaVenc > fecha && w.IdCuenta == id_cuenta && w.IdCuentaMayor == id_cuentaMayor)
                .Sum(s => s.IdTipo == "1" ? s.Importe : s.IdTipo == "2" ? -s.Importe : 0);
            return result;
        }

        public IList<MovCtaCteView> Resumen(string id_cuenta, string id_cuentaMayor, DateTime fecha, DateTime fechaHasta)
        {
            
            var tmpMov = this.GetBy(id_cuenta, id_cuentaMayor)
                .Select(s => new MovCtaCteView()
                {
                    MovCtaCte = s,
                    Cuenta = s.Sujeto,
                    CuentaMayor = s.CuentaMayor,
                    Debe = s.IdTipo == "1" ? s.Importe : 0,
                    Haber = s.IdTipo == "2" ? s.Importe : 0,

                }).OrderBy(o => o.MovCtaCte.Fecha).ThenBy(o => o.MovCtaCte.IdTipo)
                .Where(w => w.MovCtaCte.IdCuenta == id_cuenta && w.MovCtaCte.IdCuentaMayor == id_cuentaMayor &&
                (w.MovCtaCte.Fecha >= fecha && w.MovCtaCte.Fecha <= fechaHasta))
                .ToList();
            decimal saldo = 0;
            //Saldo Anterior
            saldo = this.Saldo(id_cuenta, id_cuentaMayor, fecha.AddDays(-1));
            //Calcular Saldo
            foreach (var item in tmpMov)
            {
                saldo += item.Debe - item.Haber;
                item.Saldo = saldo;
                if (item.MovCtaCte.FechaVenc < DateTime.Now)
                {
                    item.Vencido = true;
                }
            }
            return tmpMov;
        }

        public IList<MovCtaCte> GetBy(string id_cuenta, string id_cuentaMayor)
        {
            var result = this.GetAll().Where(w => w.IdCuenta == id_cuenta && w.IdCuentaMayor == id_cuentaMayor).ToList();
            return result;
        }

    }
}
