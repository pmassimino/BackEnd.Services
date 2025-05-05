using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Almacen
{
    public class Lote: IEntityModel<int>
    {        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string IdArticulo { get; set; }
        [Required, MaxLength(60)]
        public string Numero { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public DateTime FechaVenc { get; set; }
        [MaxLength(20)]
        public string Estado { get; set; }

    }
}
