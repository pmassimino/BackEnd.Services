using BackEnd.Services.Data;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Services.Models.Global
{
    public class Recurso : IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(100)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
    }
}
