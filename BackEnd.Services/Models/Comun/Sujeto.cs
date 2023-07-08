using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Comun
{
    public class Sujeto: IEntityModel<string>, IEntityNameModel
    {
        public Sujeto()
        {
            this.Contactos = new List<Contacto>();
            this.Domicilios = new List<Domicilio>();
            this.TipoRolSujeto = new List<TipoRolSujeto>();
            this.Vehiculos = new List<Vehiculo>();
            //this.sujetoCuentaMayor = new List<sujetoCuentaMayor>();
        }

        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
        [MaxLength(100)]        
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
        [MaxLength(10)]
        public string CodigoPostal { get; set; }
        [Required, MaxLength(60)]
        public string Domicilio { get; set; }
        public decimal Altura { get; set; }
        public decimal Piso { get; set; }
        public decimal Oficina { get; set; }
        [MaxLength(20)]
        public string Telefono1 { get; set; }
        [MaxLength(20)]
        public string Telefono2 { get; set; }
        [MaxLength(20)]
        public string Telefono3 { get; set; }
        [MaxLength(20)]
        public string Movil1 { get; set; }
        [MaxLength(20)]
        public string Movil2 { get; set; }
        [MaxLength(20)]
        public string Movil3 { get; set; }
        [MaxLength(20)]
        public string Fax1 { get; set; }
        [MaxLength(20)]
        public string Fax2 { get; set; }
        [MaxLength(20)]
        public string Fax3 { get; set; }
        [MaxLength(60)]
        public string email1 { get; set; }
        [MaxLength(60)]
        public string email2 { get; set; }
        [MaxLength(60)]
        public string email3 { get; set; }
        [MaxLength(10)]
        public string IdCondicionIva { get; set; }
        [MaxLength(10)]
        public string IdCondicionGanancia { get; set; }
        [MaxLength(10)]
        public string IdCondicionIB { get; set; }
        public decimal NumeroIB { get; set; }
        [MaxLength(10)]
        public string IdCondicionProductor { get; set; }
        [MaxLength(20)]
        public string Estado { get; set; }
        public virtual IList<Domicilio> Domicilios { get; set; }
        public virtual IList<TipoRolSujeto> TipoRolSujeto { get; set; }
        public virtual IList<Vehiculo> Vehiculos { get; set; }
        public virtual IList<Contacto> Contactos { get; set; }
       public virtual Localidad Localidad { get; set; }
       public virtual CondIva CondIva { get; set; }
        
    }
   
    public class Domicilio
    {
        [Key, Column(Order = 0), Required, MaxLength(20)]
        public string Id { get; set; }
        [Key, Column(Order = 1), Required, MaxLength(20), ForeignKey("Sujeto")]
        public string IdSujeto { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        [MaxLength(10)]
        public string idLocalidad { get; set; }
        [Required, MaxLength(60)]
        public string Direccion { get; set; }
        public decimal? Altura { get; set; }
        public string CodigoPostal { get; set; }
        public decimal? CodigoPlanta { get; set; }
        public virtual Sujeto Sujeto { get; set; }
        
    }
    public class Vehiculo
    {
        [Key, Column(Order = 0), Required, MaxLength(20)]
        public string Id { get; set; }
        [Key, Column(Order = 1), Required, MaxLength(20), ForeignKey("Sujeto")]
        public string IdSujeto { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        [MaxLength(60)]
        public string NombreChofer { get; set; }
        public decimal NumeroDocumento { get; set; }
        [MaxLength(10)]
        public string PatenteChasis { get; set; }
        [MaxLength(10)]
        public string PatenteAcoplado { get; set; }
        public virtual Sujeto Sujeto { get; set; }
    }
    public class Contacto
    {
        [Key, Column(Order = 1), Required, MaxLength(10)]
        public string Id { get; set; }
        [Key, Column(Order = 2), ForeignKey("Sujeto"), MaxLength(20)]
        public string IdSujeto { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Cargo { get; set; }
        [MaxLength(20)]
        public string Telefono1 { get; set; }
        [MaxLength(20)]
        public string Telefono2 { get; set; }
        [MaxLength(20)]
        public string Telefono3 { get; set; }
        [MaxLength(20)]
        public string Movil1 { get; set; }
        [MaxLength(20)]
        public string Movil2 { get; set; }
        [MaxLength(20)]
        public string Movil3 { get; set; }
        [MaxLength(60)]
        public string email1 { get; set; }
        [MaxLength(60)]
        public string email2 { get; set; }
        [MaxLength(60)]
        public string email3 { get; set; }
        public virtual Sujeto Sujeto { get; set; }
    }
    public class TipoRolSujeto
    {
        [Key, Column(Order = 0), Required, MaxLength(20), ForeignKey("Sujeto")]
        public string IdSujeto { get; set; }
        [Key, Column(Order = 1), Required, MaxLength(20), ForeignKey("TipoRol")]
        public string IdTipoRol { get; set; }
        public Nullable<System.DateTime> DateAdd { get; set; }
        public virtual TipoRol TipoRol { get; set; }
        public virtual Sujeto Sujeto { get; set; }
    }

    public class TipoRol : IEntityModel<string>, IEntityNameModel
    {

        [Key, Required, MaxLength(20)]
        public string Id { get; set; }
       
        [Required, MaxLength(100)]
        public string Nombre { get; set; }
    }

}
