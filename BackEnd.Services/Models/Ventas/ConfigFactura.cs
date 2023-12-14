using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Ventas
{
    public class ConfigFactura : IEntityModel<string>
    {
        public ConfigFactura()
        {
            this.Numeradores = new List<ItemNumerador>();
            this.PuntosEmision = new List<ItemPuntoEmision>();
        }

        [Key, Required, MaxLength(10)]
        public String Id { get; set; }
        
        [MaxLength(60)]
        public string Nombre { get; set; }
        [MaxLength(10), ForeignKey("Seccion")]
        public String IdSeccion { get; set; }
        [MaxLength(60)]
        public string Reporte { get; set; }
        [MaxLength(60)]
        public string ReporteFiscal { get; set; }   

        public virtual IList<ItemNumerador> Numeradores { get; set; }
        public virtual IList<ItemPuntoEmision> PuntosEmision { get; set; }
        public virtual Seccion Seccion { get; set; }
       
    }
    public class ItemNumerador 
    {
        //Estructura
        [Key, Column(Order = 0), MaxLength(10), ForeignKey("ConfigFactura")]
        public string Id { get; set; }
        [Key,Column(Order = 1)]
        public int IdComprobante { get; set; }
        [MaxLength(10),ForeignKey("NumeradorDocumento")]
        public String IdNumeradorDocumento { get; set; }

        public virtual NumeradorDocumento NumeradorDocumento { get; set; }
        public virtual ConfigFactura ConfigFactura { get; set; }

    }
    public class ItemPuntoEmision
    {
        //Estructura
        [Key, Column(Order = 0), MaxLength(10), ForeignKey("ConfigFactura")]
        public string Id { get; set; }
        [Key, Column(Order = 1)]
        [MaxLength(10), ForeignKey("PuntoEmision")]
        public string IdPuntoEmision { get; set; }        
        public virtual PuntoEmision PuntoEmision { get; set; }
        public virtual ConfigFactura ConfigFactura { get; set; }

    }

}
