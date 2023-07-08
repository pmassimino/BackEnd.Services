using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Services.Models.Global
{
    public class Rol : IEntityModel<int>, IEntityNameModel
    {
        [Key,Required]
        public int Id { get; set; }
        [Required,ForeignKey("Organizacion")]
        public Guid IdOrganizacion { get; set; }

        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        public virtual Organizacion Organizacion { get; set; }
        public virtual IList<RolPermiso> Permisos { get; set; }
        public virtual IList<RolAccount> Accounts { get; set; }
    }
    public class RolPermiso 
    {
        [Key, Column(Order = 0), Required, ForeignKey("Rol")]
        public int IdRol { get; set; }
        [Key, Column(Order = 1), Required, ForeignKey("Organizacion")]
        public Guid IdOrganizacion { get; set; }
        [Key, Column(Order = 2), Required, MaxLength(100), ForeignKey("Permiso")]
        public string IdPermiso { get; set; }
        public virtual Rol Rol { get; set; }       
    }
    public class RolAccount 
    {
        [Key, Column(Order = 0), Required, ForeignKey("Rol")]
        public int IdRol { get; set; }
        [Key, Required, MaxLength(10), ForeignKey("Account")]
        public string IdAccount { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual Account Account { get; set; }

    }
}
