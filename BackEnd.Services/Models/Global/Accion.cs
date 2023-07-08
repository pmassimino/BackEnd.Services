using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Models.Global
{
    public class Accion:IEntityModel<string>, IEntityNameModel
    {
    [Key, Required, MaxLength(10)]
    public string Id { get; set; }
    [Required, MaxLength(60)]
    public string Nombre { get; set; }
}
}
