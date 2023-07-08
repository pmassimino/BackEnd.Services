using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Models.Contable
{
    public class Mayor : IEntityModel<Guid>,IStructuralEntity, ITransaccionalEntity
    {
        public Mayor()
        {
            List<DetalleMayor> Detalle = new List<DetalleMayor>();
            this.Detalle = Detalle;
        }
        //Estructura
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(10)]
        public string IdEmpresa { get; set; }
        [Required, MaxLength(10)]
        public string IdSucursal { get; set; }
        [Required, MaxLength(10)]
        public string IdArea { get; set; }
        [Required, MaxLength(10)]
        public string IdSeccion { get; set; }
        public Guid IdTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaComp { get; set; }
        public DateTime FechaVenc { get; set; }
        [MaxLength(60)]
        public string Concepto { get; set; }
        [MaxLength(10)]
        public string IdComprobante { get; set; }
        public int Pe { get; set; }
        public long Numero { get; set; }

        [MaxLength(20)]       
        public string Origen { get; set; }
        public virtual IList<DetalleMayor> Detalle { get; set; }
        public DetalleMayor AddDetalle(string IdCuentaMayor, string Concepto, string IdTipo, decimal Importe, DateTime FechaVenc, string IdCuenta = null)
        {
            DetalleMayor item = new DetalleMayor();
            item.Id = this.Id;
            item.Item = Detalle.Count();
            item.IdCuentaMayor = IdCuentaMayor;
            item.Concepto = Concepto;
            item.IdCuenta = IdCuenta;
            item.IdTipo = IdTipo;
            item.Importe = Importe;
            item.FechaVenc = FechaVenc;
            this.Detalle.Add(item);
            return item;
        }

    }
    public class DetalleMayor
    {
        //Estructura
        [Key, Required, Column(Order = 0), ForeignKey("Mayor")]
        public Guid Id { get; set; }
        [Key, Required, Column(Order = 1)]
        public int Item { get; set; }
        public DateTime FechaVenc { get; set; }
        [Required, MaxLength(20), ForeignKey("CuentaMayor")]
        public string IdCuentaMayor { get; set; }
        [MaxLength(60)]
        public string Concepto { get; set; }
        [MaxLength(20), ForeignKey("Sujeto")]
        public string IdCuenta { get; set; }
        [MaxLength(1)]
        public string IdTipo { get; set; }
        public decimal Importe { get; set; }
        public decimal Cantidad { get; set; }
        public virtual CuentaMayor CuentaMayor { get; set; }
        public virtual Mayor Mayor { get; set; }
        public virtual Sujeto Sujeto { get; set; }

    }
}
