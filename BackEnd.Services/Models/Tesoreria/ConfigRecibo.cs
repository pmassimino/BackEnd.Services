using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Tesoreria
{
    public class ConfigRecibo : IEntityModel<string>
    {
        [Key, Required, MaxLength(10), ForeignKey("Seccion")]
        public String Id { get; set; }
        [MaxLength(10), ForeignKey("NumeradorDocumento")]
        public String IdNumeradorDocumento { get; set; }
        public virtual NumeradorDocumento NumeradorDocumento { get; set; }
        public virtual Seccion Seccion { get; set; }
    }
}
