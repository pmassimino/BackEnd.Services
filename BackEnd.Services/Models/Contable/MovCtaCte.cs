using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Contable
{
    public class MovCtaCte : IEntityModel<Guid>,IStructuralEntity, ITransaccionalEntity
    {
        public MovCtaCte()
        { }
        //Estructura
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required,MaxLength(10)]
        public string IdEmpresa { get; set; }
        [Required, MaxLength(10)]
        public string IdSucursal { get; set; }
        [Required, MaxLength(10)]
        public string IdArea { get; set; }
        [Required, MaxLength(10)]
        public string IdSeccion { get; set; }
        public Guid IdTransaccion { get; set; }
        [MaxLength(20), ForeignKey("Sujeto")]
        public string IdCuenta { get; set; }
        [MaxLength(20), ForeignKey("CuentaMayor")]
        public string IdCuentaMayor { get; set; }
        public string IdComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaComp { get; set; }
        public DateTime FechaVenc { get; set; }
        public int Pe { get; set; }
        public long Numero { get; set; }
        public string Concepto { get; set; }
        [Required, MaxLength(1)]
        public string IdTipo { get; set; }
        public decimal Importe { get; set; }
        [MaxLength(20)]
        public string Origen { get; set; }
        public virtual CuentaMayor CuentaMayor { get; set; }
        public virtual Sujeto Sujeto { get; set; }
    }

    public class MovSaldoCtaCte
    {
        public MovSaldoCtaCte() { }
        public MovSaldoCtaCte(string idCuenta, string idCuentaMayor, string nombre, decimal saldoVencido, decimal saldoAVencer, decimal saldo)
        {
            this.IdCuenta = idCuenta;
            this.IdCuentaMayor = idCuentaMayor;
            this.Nombre = nombre;
            this.SaldoVencido = saldoVencido;
            this.SaldoAVencer = saldoAVencer;
            this.Saldo = saldo;
        }

        public string IdCuenta { get; set; }
        public string IdCuentaMayor { get; set; }
        public string Nombre { get; set; }
        public decimal SaldoVencido { get; set; }
        public decimal SaldoAVencer { get; set; }
        public decimal Saldo { get; set; }
    }
    public class MovCtaCteView 
    {

        public MovCtaCte MovCtaCte { get; set; }
        public CuentaMayor CuentaMayor { get; set; }
        public Sujeto Cuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public decimal Saldo { get; set; }
        public bool Vencido { get; set; }

    }
    public class CompPendView 
    {
        public MovCtaCte MovCtaCte { get; set; }
        public CuentaMayor cuentaMayor { get; set; }
        public Sujeto Cuenta { get; set; }
        public decimal Importe { get; set; }
        public decimal Pendiente { get; set; }

    }
}
