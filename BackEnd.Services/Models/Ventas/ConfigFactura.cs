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
        }

        [Key, Required, MaxLength(10)]
        public String Id { get; set; }
        public virtual IList<ItemNumerador> Numeradores { get; set; }
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
    
}
