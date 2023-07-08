using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Comun
{
    public class Transaccion: IEntityModel<Guid>
    {
        public Transaccion() 
        {
            this.Id =  Guid.NewGuid();
            this.Fecha = DateTime.Now;
        }
        public Transaccion(string tipo ):this()
        {
            this.Tipo = tipo;
        }

        [Key, Required, MaxLength(10)]
        public Guid Id { get; set; }
        [Required, MaxLength(60)]
        public string Tipo { get; set; }
        [Required, MaxLength(60)]
        public string Owner { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

   
    }
}
