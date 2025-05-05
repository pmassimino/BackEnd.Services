using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace BackEnd.Services.Models.Almacen
{
    public class MovStock: IEntityModel<int>, ITransaccionalEntity
    {        
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]        
        public int Tipo { get; set; } //1-Ingreso 2-Egreso 
        public DateTime Fecha { get; set; } 
        [Required, MaxLength(60)]
        public string Concepto { get; set; }
        [MaxLength(20)]
        public string Numero { get; set; }
        public string IdArticulo { get; set; }
        [Required, MaxLength(10)]
        public string IdDeposito { get; set; }        
        public int IdLote { get; set; }
        public int IdSerie { get; set; }
        public decimal Cantidad { get; set; }
        public Guid IdTransaccion { get; set; }        

    }
}
