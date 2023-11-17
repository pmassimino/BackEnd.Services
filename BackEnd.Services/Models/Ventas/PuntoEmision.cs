using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Ventas
{
    public class PuntoEmision : IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        public int Numero { get;set; }
        [MaxLength(20)]
        public string IdAfipWsService { get; set; }
        [MaxLength(10)]
        public string IdProvincia { get; set; }        
        public string Localidad { get; set; }
        [MaxLength(10)]
        public string CodigoPostal { get; set; }
        [Required, MaxLength(60)]
        public string Domicilio { get; set; }
        public decimal Altura { get; set; }
        public virtual IList<NumeradorPuntoEmision> Numeradores { get; set; }
        public PuntoEmision() 
        {
            //Inicializar detalle
            this.Numeradores = new List<NumeradorPuntoEmision>();
        }

    }

    public class NumeradorPuntoEmision
    {
        //Estructura
        [Key, Column(Order = 0), MaxLength(10), ForeignKey("PuntoEmision")]
        public string Id { get; set; }
       
        [MaxLength(10), Column(Order = 1), ForeignKey("NumeradorDocumento")]
        public String IdNumeradorDocumento { get; set; }

        public virtual NumeradorDocumento NumeradorDocumento { get; set; }
        public virtual PuntoEmision PuntoEmision { get; set; }

    }
}
