using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Models.Contable
{
    public class LibroIva : IEntityModel<Guid>,IStructuralEntity, ITransaccionalEntity
    {
        public LibroIva()
        {
            List<ItemIva> Detalle = new List<ItemIva>();
            this.DetalleIva = Detalle;
            List<ItemTributo> DetalleTributo = new List<ItemTributo>();
            this.DetalleTributo= DetalleTributo;

        }
        //Estructura
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(10)]
        public string IdEmpresa { get; set; }
        [Required, MaxLength(10)]
        public string IdSucursal { get; set; }
        [Required, MaxLength(10)]
        public string IdArea { get; set; }
        [Required, MaxLength(10)]
        public string IdSeccion { get; set; }
        public Guid IdTransaccion { get; set; }
        //C= Compras V=Ventas
        [Required, MaxLength(1)]
        public string Tipo { get; set; }
        //1-Positivo 2 - negativo
        [MaxLength(1)]
        public string IdTipo { get; set; }
        [MaxLength(2)]
        public string CodigoOperacion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaComp { get; set; }
        public DateTime FechaVenc { get; set; }
       
        [MaxLength(10)]
        public string IdComprobante { get; set; }
        [Required, MaxLength(10)]
        public string IdMoneda { get; set; }
        public decimal CotizacionMoneda { get; set; }
        public int Pe { get; set; }
        public long Numero { get; set; }
        [MaxLength(20)]
        public string IdCuenta { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required, MaxLength(10)]
        public string IdTipoDoc { get; set; }
        public long NumeroDocumento { get; set; }       

        [MaxLength(60)]       
        public string Origen { get; set; }
        //Importes
        public decimal Gravado { get; set; }        
        public decimal Iva { get; set; }
        public decimal NoGravado { get; set; }
        public decimal Exento { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal Total { get; set; }
        public bool Autorizado { get; set; }
        public virtual IList<ItemIva> DetalleIva { get; set; }
        public virtual IList<ItemTributo> DetalleTributo { get; set; }
        public ItemIva AddDetalleIva(string CondIva, decimal BaseImponible, decimal Importe)
        {
            ItemIva item = new ItemIva();
            item.Id = this.Id;
            item.Item = DetalleIva.Count();
            item.CondIva = CondIva;
            item.BaseImponible = BaseImponible;
            item.Importe = Importe;      
            this.DetalleIva.Add(item);
            return item;
        }
        public ItemTributo AddDetalleTributo(string idTributo,string nombre, decimal BaseImponible,decimal Tarifa, decimal Importe)
        {
            ItemTributo item = new ItemTributo();
            item.Id = this.Id;
            item.Nombre = nombre;
            item.Item = DetalleTributo.Count();
            item.IdTributo = idTributo;
            item.BaseImponible = BaseImponible;
            item.Tarifa = Tarifa;
            item.Importe = Importe;
            this.DetalleTributo.Add(item);
            return item;
        }

    }
    public class ItemIva
    {
        //Estructura
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }

        [Key, Column(Order = 1)]
        public int Item { get; set; }
        //General
        [Required, MaxLength(10)]
        public string CondIva { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal Importe { get; set; }
        public virtual LibroIva LibroIva { get; set; }
    }

    //Tributos 01 - Impuestos Nacionales ; 02 -Impuestos provinciales ; 03  - Impuestos municipales ; 04 - Impuestos internos ; 05 - Otros
    public class ItemTributo
    {     //Estructura
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }

        [Key, Column(Order = 1)]
        public int Item { get; set; }
        //General
        [Required, MaxLength(10)]
        public string IdTributo { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Importe { get; set; }
        public LibroIva LibroIva { get; set; }
    }
    public class LibroIvaView
    {
        public Guid Id { get; set; }
        public Guid IdTransaccion { get; set; }
        public LibroIva LibroIva { get; set; }  
        
        public Sujeto Cuenta { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaComprobante { get; set; }        
        public int Pe { get; set; }
        public long Numero { get; set; }        
        public string IdCuenta { get; set; }        
        public string Nombre { get; set; }       
        public string IdTipoDoc { get; set; }
        public long NumeroDocumento { get; set; }
        public decimal Gravado { get; set; }
        public decimal NoGravado { get; set; }
        public decimal Exento { get; set; }
        public decimal Iva21 { get; set; }
        public decimal Iva105 { get; set; }
        public decimal Iva27 { get; set; }
        public decimal IvaOtro { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal Total { get; set; }

    }
}
