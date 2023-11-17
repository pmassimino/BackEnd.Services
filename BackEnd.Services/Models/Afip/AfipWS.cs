using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Afip
{
    public class AfipWs : IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string NombreServicio { get; set; }

    }
}
