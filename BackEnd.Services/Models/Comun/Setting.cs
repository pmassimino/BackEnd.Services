using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Comun
{
    public class Setting: IEntityModel<string>
    {
        [Key,Required]
        public string Id { get; set; }
        public string Value { get; set; }        
    }
}
