using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Almacen
{
    public class Familia: IEntityModel<string>, IEntityNameModel
    {        
        [Key, Required, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        public string IdFamilia { get; set; }
        [MaxLength(20)]
        public string CtaIngresoDefault { get; set; }
        [MaxLength(20)]
        public string CtaEgresoDefault { get; set; }
    }
}
