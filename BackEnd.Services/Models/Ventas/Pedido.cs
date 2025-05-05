using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Services.Models.Ventas
{
    public class Pedido : IEntityModel<Guid>, IStructuralEntity, ITransaccionalEntity
    {
        //Estructura
        [Key, Required]
        public Guid Id { get; set; }
        [MaxLength(10)]
        public string IdEmpresa { get; set; }
        [MaxLength(10)]
        public string IdSucursal { get; set; }
        [Required, MaxLength(10)]
        public string IdArea { get; set; }
        [Required, MaxLength(10)]
        public string IdSeccion { get; set; }
        public Guid IdTransaccion { get; set; }
        //General
        [Required, MaxLength(2)]
        public string Tipo { get; set; }
        [Required, MaxLength(1)]
        public string Letra { get; set; }
        public string IdPuntoEmision { get; set; }
        public int Pe { get; set; }
        public long Numero { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public DateTime FechaComp { get; set; }
        [Required]
        public DateTime FechaEntrega { get; set; }
        [Required]
        public DateTime FechaVencimiento { get; set; }
        public string IdMoneda { get; set; }
        public decimal CotizacionMoneda { get; set; }
        [MaxLength(50)]
        public string Origen { get; set; }
        //Responsable
        [MaxLength(20), ForeignKey("Sujeto")]
        public string IdCuenta { get; set; }
        //Importes
        public decimal TotalNeto { get; set; }
        public decimal PorDescuento { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal Total { get; set; }
        public decimal TotalExento { get; set; }
        public decimal TotalGravado { get; set; }
        public decimal TotalNoGravado { get; set; }
        public decimal TotalIva { get; set; }
        public decimal TotalOTributos { get; set; }
        //Otros
        [Column(TypeName = "text")]
        public string Obs { get; set; }
        //Auditoria
        public string creadoPor { get; set; }
        //Detalles
        public virtual IList<DetallePedido> Detalle { get; set; }
        public virtual IList<DetalleIvaPedido> Iva { get; set; }        
        public virtual Sujeto Sujeto { get; set; }

        public Pedido()
        {
            //Inicializar detalle
            this.Detalle = new List<DetallePedido>();
            this.Iva = new List<DetalleIvaPedido>();         
        }

    }
    public class DetallePedido
    {
        //Estructura
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }

        [Key, Column(Order = 1)]
        public int Item { get; set; }
        //General
        [Required, MaxLength(20), ForeignKey("Articulo")]
        public string IdArticulo { get; set; }
        [MaxLength(10)]
        public string IdUnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        [MaxLength(200)]
        public string Concepto { get; set; }
        //Importes
        public decimal Precio { get; set; }
        public decimal PorBonificacion { get; set; }
        public decimal Bonificacion { get; set; }
        public decimal Gravado { get; set; }
        public string CondIva { get; set; }
        public decimal Iva { get; set; }
        public decimal NoGravado { get; set; }
        public decimal Exento { get; set; }
        public decimal OtroTributo { get; set; }
        public decimal Total { get; set; }
        //Adicionales 
        [MaxLength(30)]
        public string Lote { get; set; }
        [MaxLength(30)]
        public string Serie { get; set; }
        //
        public virtual Articulo Articulo { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
    public class DetalleIvaPedido
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
        public virtual Pedido Pedido { get; set; }
    } 
       
}
