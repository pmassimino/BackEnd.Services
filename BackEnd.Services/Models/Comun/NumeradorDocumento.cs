﻿using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Comun
{
    public class NumeradorDocumento: IEntityModel<string>, IEntityNameModel
    {        
        [Key, Required, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        public int PuntoEmision { get; set; }
        public int Numero { get; set; }

   
    }
}