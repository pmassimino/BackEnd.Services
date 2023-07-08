using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Global
{
    public class Organizacion:IEntityModel<Guid>, IEntityNameModel
    {
        [Key, Required]
        public Guid Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }        
    }
}
