using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Almacen
{
    public class Articulo:IEntityModel<string>,IEntityNameModel
    {
        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(10), ForeignKey("Familia")]
        public string IdFamilia { get; set; }
        [MaxLength(10)]
        public string IdUnidad { get; set; }
        [MaxLength(10)]
        public string Estado { get; set; }
        public decimal CostoVenta { get; set; }
        public decimal ImpuestoVenta { get; set; }
        public decimal PrecioVenta { get; set; }
        
        public decimal AlicuotaIva { get; set; }
        [MaxLength(10)]
        public string CondIva { get; set; }
        public decimal PrecioVentaFinal { get; set; }
        public decimal MargenVenta { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockActual { get; set; }
        public decimal StockReposicion { get; set; }
        public decimal StockMaximo { get; set; }
        public string Observacion { get; set; }
        public virtual Familia Familia { get; set; }
        
    }
}
