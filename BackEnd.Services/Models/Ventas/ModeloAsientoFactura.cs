using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Ventas
{
    public class ModeloAsientoFactura: IEntityModel<int>, IEntityNameModel
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(20)]
        public string CtaIngresoDefault { get; set; }
        [MaxLength(20)]
        public string CtaGastoDefault { get; set; }
        [MaxLength(20)]
        public string CtaIvaGenDefault { get; set; }
        [MaxLength(20)]
        public string CtaIvaRedDefault { get; set; }
        [MaxLength(20)]
        public string CtaPerIGDefault { get; set; }
        [MaxLength(20)]
        public string CtaPerIvaDefault { get; set; }
        [MaxLength(20)]
        public string CtaCajaDefault { get; set; }
        [MaxLength(20)]
        public string CtaImpuestoDefault { get; set; }
    }
}
