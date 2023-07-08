using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Global
{
    public class Account: IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
       
        
        [MaxLength(10)]
        public string IdGrupo { get; set; }
        [MaxLength(10)]
        public string Estado { get; set; }
        public virtual IList<RolAccount> Roles { get; set; }

    }
    public class AccountDto
    {
        
        public string Id { get; set; }
        
        public string Nombre { get; set; }
        
        public string Email { get; set; }       
        public string IdGrupo { get; set; }       
        public string Estado { get; set; }
        public virtual IList<Rol> Roles { get; set; }
    }
    
}
