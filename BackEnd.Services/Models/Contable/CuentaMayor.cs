using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Contable
{
    public class CuentaMayor: IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(20)]
        public string IdSuperior { get; set; }
        [MaxLength(10), ForeignKey("TipoCuentaMayor")]
        public string IdTipo { get; set; }
        [MaxLength(10), ForeignKey("UsoCuentaMayor")]
        public string IdUso { get; set; }
        public virtual TipoCuentaMayor TipoCuentaMayor { get; set; }
        public virtual UsoCuentaMayor UsoCuentaMayor { get; set; }
        //public virtual CuentaMayor Superior { get; set; }

    }
    //1-General ; 2 - Cuenta Corriente ; 3 - Caja ; 4 - Banco ; 5 - Cartera de Valores  - Libro Iva 
    public class UsoCuentaMayor: IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
    //1-Activo 2 - Pasivo 3 - Patrimonio neto - 4 Resultado Ingresos - 5 Resultado Egresos - 6 Orden
    public class TipoCuentaMayor:IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
}
