using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Acopio
{
    public class CartaPorte : IEntityModel<string>
    {
        [Key, Required, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(10)]
        public string IdEmpresa { get; set; }
        [Required, MaxLength(10)]
        public string IdSucursal { get; set; }
        [Required, MaxLength(10)]
        public string IdPlanta { get; set; }
        [Required, MaxLength(10)]
        public string IdSeccion { get; set; }
        public System.Guid IdTransaccion { get; set; }
        [Required, MaxLength(10)]
        public string IdTipo { get; set; }
        public System.DateTime FechaCarga { get; set; }
        public System.DateTime FechaDescarga { get; set; }
        public Nullable<decimal> Pe { get; set; }
        public Nullable<decimal> Numero { get; set; }
        
        public Nullable<decimal> Ctg { get; set; }
        [Required, MaxLength(10)]
        public string IdCosecha { get; set; }
        [Required, MaxLength(20)]
        public string IdTitular { get; set; }
        [MaxLength(20)]
        public string IdIntermediario { get; set; }
        [MaxLength(20)]
        public string IdRemitenteComercial { get; set; }
        [MaxLength(20)]
        public string IdCorredor { get; set; }
        [MaxLength(20)]
        public string IdRepresentante { get; set; }
        [Required, MaxLength(20)]
        public string IdDestinatario { get; set; }
        [Required, MaxLength(20)]
        public string IdDestino { get; set; }
        [Required, MaxLength(20)]
        public string IdTransporte { get; set; }
        [MaxLength(20)]
        public string IdChofer { get; set; }
        
        [MaxLength(20)]
        public string NumeroContrato { get; set; }
        [MaxLength(25)]
        public string NumeroCupo { get; set; }
        public int PesoEstimado { get; set; }
        public int PesoBrutoC { get; set; }
        public int PesoTaraC { get; set; }
        public int PesoNetoC { get; set; }
        public int PesoBrutoD { get; set; }
        public int PesoTaraD { get; set; }
        public int PesoNetoD { get; set; }
        [MaxLength(50)]
        public string DireccionProcedencia { get; set; }
        [MaxLength(10)]
        public string IdLocalidadPocedencia { get; set; }
        [MaxLength(10)]
        public string IdProvinciaProcedencia { get; set; }
        [Required, MaxLength(10)]
        public string IdLugarDestino { get; set; }
        [MaxLength(50)]
        public string DireccionDestino { get; set; }
        [MaxLength(10)]
        public string IdLocalidadDestino { get; set; }
        [MaxLength(10)]
        public string IdProvinciaDestino { get; set; }
        [MaxLength(10)]
        public string IdVehiculo { get; set; }
        [Required, MaxLength(10)]
        public string Patente_Chasis { get; set; }
        [MaxLength(20)]
        public string Patente_Acoplado { get; set; }
        public decimal Tarifa { get; set; }
        public decimal TarifaReferencia { get; set; }
        public decimal Distancia { get; set; }
        public string Observaciones { get; set; }
        [MaxLength(10)]
        public string id_Estado { get; set; }
    }
}
