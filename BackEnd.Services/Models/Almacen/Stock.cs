using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace BackEnd.Services.Models.Almacen
{
    public class Stock: IEntityModel<int>
    {        
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(20),ForeignKey("Articulo")]
        public string IdArticulo { get; set; }
        [Required, MaxLength(10), ForeignKey("Deposito")]
        public string IdDeposito { get; set; }        
        public int IdLote { get; set; }
        public int IdSerie { get; set; }
        public decimal Cantidad { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual Deposito Deposito { get; set; }

    }
    public class FormAjusteStock 
    {
        public Stock Stock { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
    }

}
