using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Tesoreria
{
    public class ReciboCtaCte : IEntityModel<Guid>, IStructuralEntity, ITransaccionalEntity
    {
        //Estructura
        [Key,Required]
        public Guid Id { get; set; }
        [Required,MaxLength(10)]
        public string IdEmpresa { get; set; }
        [Required,MaxLength(10)]
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
        public DateTime Fecha { get; set; }        
        public DateTime FechaVencimiento { get; set; }
        public int Pe { get; set; }
        public int Numero { get; set; }
        public string IdTipo { get; set; }
        public decimal Importe { get; set; }
        [MaxLength(200)]
        public string Obs { get; set; }
        //Detalles
        public virtual IList<DetalleComprobante> DetalleComprobante { get; set; }
        public virtual IList<DetalleValores> DetalleValores { get; set; }
        public virtual IList<DetalleRelacion> DetalleRelacion { get; set; }
        //Virtual
        public virtual CuentaMayor CuentaMayor { get; set; }
        public virtual Sujeto Sujeto { get; set; }
        //inicializar
        public ReciboCtaCte()
        {
            this.DetalleComprobante = new List<DetalleComprobante>();
            this.DetalleValores = new List<DetalleValores>();
            this.DetalleRelacion = new List<DetalleRelacion>();
        }
    }
    public class DetalleComprobante
    {
        //Estructura
        [Key, Column(Order = 0), Required]
        public Guid Id { get; set; }
        [Key, Required, Column(Order = 1)]
        public int Item { get; set; }
        //general
        [MaxLength(2)]
        public string IdTipo { get; set; } //1 - debito 2 - crédito
        [MaxLength(2)]
        public string IdTipoComp { get; set; } //1-normal 2 - a cta.
       
        public Guid IdMovCtaCte { get; set; }
        public DateTime Fecha { get; set; }
        public int Pe { get; set; }
        public int Numero { get; set; }
        [MaxLength(60)]
        public string Concepto { get; set; }
        public decimal Importe { get; set; }
        public virtual ReciboCtaCte ReciboCtaCte { get; set; }
    }
    public class DetalleValores
    {
        //Estructura
        [Key, Column(Order = 0), Required]
        public Guid Id { get; set; }
        [Key, Required, Column(Order = 1)]
        public int Item { get; set; }
        //general
        [MaxLength(2)]
        public string IdTipo { get; set; } //1 - debito 2 - crédito
        [MaxLength(20), ForeignKey("CuentaMayor")]
        public string IdCuentaMayor { get; set; }
        [MaxLength(10)]
        public string IdComp { get; set; }
        public Guid IdCarteraValor { get; set; }

        public DateTime Fecha { get; set; }
        public DateTime FechaVencimiento { get; set; }


        public int Numero { get; set; }
        [MaxLength(60)]
        public string Concepto { get; set; }
        public decimal Importe { get; set; }
        [MaxLength(30)]
        public string Banco { get; set; }
        [MaxLength(30)]
        public string Sucursal { get; set; }
        //virtual
        public virtual CuentaMayor CuentaMayor { get; set; }
        public virtual ReciboCtaCte ReciboCtaCte { get; set; }
    }

    public class DetalleRelacion
    {
        //Estructura
        [Key, Column(Order = 0), Required]
        public Guid Id { get; set; }
        [Key, Required, Column(Order = 1)]
        public int Item { get; set; }
        [MaxLength(10)]
        public Guid IdMovCtaCte { get; set; }
        public decimal Importe { get; set; }
        public virtual ReciboCtaCte  ReciboCtaCte {get;set;}
    }
}
