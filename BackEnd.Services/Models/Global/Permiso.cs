
using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Models.Global
{
    public class Permiso : IEntityModel<string>
    {
        [Key, Required, MaxLength(100)]
        public string Id { get; set; }
        [MaxLength(10), ForeignKey("Accion")]
        public string IdAccion { get; set; }
        [MaxLength(100),ForeignKey("Recurso")]
        public string IdRecurso { get; set; }
        public virtual Accion Accion { get; set; }
        public virtual Recurso Recurso { get; set; }

    }
}
