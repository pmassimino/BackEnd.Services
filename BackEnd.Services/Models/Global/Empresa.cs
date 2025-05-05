using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Global
{
    public class Empresa: IEntityModel<Guid>, IEntityNameModel
    {
        public Empresa()
        {
            this.Accounts = new List<EmpresaAccount>();
        }
        [Key, Required]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string NombreComercial { get; set; }
        [Required, MaxLength(10)]
        public string IdTipoDoc { get; set; }

        public long NumeroDocumento { get; set; }
        [MaxLength(10)]
        public string IdProvincia { get; set; }
        [MaxLength(10)]
        public string IdLocalidad { get; set; }
        [Required, MaxLength(60)]
        public string Domicilio { get; set; }
        public decimal Altura { get; set; }
        public decimal Piso { get; set; }
        public decimal Oficina { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(20)]
        public string TelefonoSec { get; set; }
        [MaxLength(20)]
        public string Movil { get; set; }
        [MaxLength(20)]
        public string MovilSec { get; set; }
        [MaxLength(20)]
        public string Fax { get; set; }
       
        [MaxLength(60)]
        public string Email { get; set; }        
      
        [MaxLength(10)]
        public string IdCondicionIva { get; set; }
        [MaxLength(10)]
        public string IdCondicionGanancia { get; set; }
        [MaxLength(10)]
        public string IdCondicionIB { get; set; }
        public decimal NumeroIB { get; set; }

        [MaxLength(60)]
        public string DatabaseName { get; set; }
        [ForeignKey("Organizacion")]
        public Guid IdOrganizacion { get; set; }
        public string IdOwner { get; set; }
        public virtual IList<EmpresaAccount> Accounts { get; set; }
        public virtual Organizacion Organizacion { get; set; }

    }
    public class EmpresaAccount
    {
        public EmpresaAccount() { }
        public EmpresaAccount(Guid id, string idAccount)
        {
            this.Id = id;
            this.IdAccount = idAccount;
        }
        [Key, Column(Order = 0), Required, MaxLength(10)]
        public Guid Id { get; set; }
        [Key, Column(Order = 1), Required, MaxLength(10)]
        public string IdAccount { get; set; }

       
    }
}
