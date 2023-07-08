using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Tesoreria
{
    public class CarteraValor: IEntityModel<Guid>, IStructuralEntity, ITransaccionalEntity
    {
        //Estructura
        [Key,Required]
        public Guid Id { get; set; }
        [Required,MaxLength(10)]
        public string IdEmpresa { get; set; }
        [Required,MaxLength(10)]
        public string IdSucursal { get; set; }
        [Required, MaxLength(10)]
        public string IdArea { get; set; }
        [Required, MaxLength(10)]
        public string IdSeccion { get; set; }
        public Guid IdTransaccion { get; set; }
        [MaxLength(20), ForeignKey("Sujeto")]
        public string IdCuenta { get; set; }
        [MaxLength(20), ForeignKey("CuentaMayor")]
        public string IdCuentaMayor { get; set; }
        public DateTime Fecha { get; set; }        
        public DateTime FechaVencimiento { get; set; }       
        public int Numero { get; set; }
        public string Tipo { get; set; }
        [MaxLength(60)]
        public string Banco { get; set; }
        [MaxLength(60)]
        public string Sucursal { get; set; }
        public decimal Importe { get; set; }
        [MaxLength(200)]
        public string Obs { get; set; }
        //Detalles        
        //Virtual
        public virtual IList<MovCarteraValor> MovCarteraValor { get; set; }
        public virtual CuentaMayor CuentaMayor { get; set; }
        public virtual Sujeto Sujeto { get; set; }
        //inicializar
        public CarteraValor()
        {
           this.MovCarteraValor = new List<MovCarteraValor>();
        }
    }

    public class MovCarteraValor
    {
        //Estructura
        [Key,Column(Order = 0),ForeignKey("CarteraValor")]
        public Guid Id { get; set; }
        [Key,Column(Order =1), Required]
        public int Item { get;set; }
        [MaxLength(1)]
        public string Tipo { get; set; } //1-Ingreso 2-Egreso
        public Guid IdTransaccion { get; set; }
        public DateTime Fecha { get; set;}
        [MaxLength(20)]
        public string Estado { get; set; } //Activo - Depositado  - Pago - Anulado
        [MaxLength(60)]
        public string Concepto { get; set;}
        public virtual CarteraValor CarteraValor { get; set;}

    }

    public class CarteraValorView 
    {
        public Guid Id { get; set; }
        
        public string IdEmpresa { get; set; }
        
        public string IdSucursal { get; set; }
        
        public string IdArea { get; set; }
        
        public string IdSeccion { get; set; }
        public Guid IdTransaccion { get; set; }        
        public string IdCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public string IdCuentaMayor { get; set; }
        public string NombreCuentaMayor { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }        
        public string Banco { get; set; }        
        public string Sucursal { get; set; }
        public decimal Importe { get; set; }        
        public string Obs { get; set; }
        public string Estado { get; set; }
    }

   
}
