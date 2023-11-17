using BackEnd.Services.Data;
using BackEnd.Services.Models.Afip;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Soltec.Suscripcion.Code
{
    public class DbInitializer
    {
        public static void Seed(GestionDBContext context)
        {
            // context.Database.EnsureCreated();
            //Plan
            if (!context.AfipWs.Any())
            {
                List<AfipWs> list = new List<AfipWs>();
                AfipWs item = new AfipWs() {
                    Id= "wsaa",
                    Nombre = "Webservice de Autenticación y Autorización",
                    Url= "https://wsaahomo.afip.gov.ar/ws/services/LoginCms",
                    NombreServicio = "wsaa"
                };
                list.Add(item);
                item = new AfipWs()
                {
                    Id = "wsfev1",
                    Nombre = "Web Service de Factura Electrónica",
                    Url = "https://servicios1.afip.gov.ar/wsfexv1/service.asmx",
                    NombreServicio = "wsfev"
                };
                list.Add(item);
                item = new AfipWs()
                {
                    Id = "wsmtxca",
                    Nombre = "Web Service de Factura Electrónica (wsmtxca)",
                    Url = "https://serviciosjava.afip.gob.ar/wsmtxca/services/MTXCAService",
                    NombreServicio = "wsmtxca"
                };
                list.Add(item);
                item = new AfipWs()
                {
                    Id = "wsbfev1",
                    Nombre = "Web Service de Bonos Fiscales Electrónicos",
                    Url = "https://servicios1.afip.gov.ar/wsbfev1/service.asmx",
                    NombreServicio = "wsbfev1"
                };
                list.Add(item);
                item = new AfipWs()
                {
                    Id = "wsfexv1",
                    Nombre = "Web Service de Factura Electrónica de Exportación V1",
                    Url = "https://servicios1.afip.gov.ar/wsfexv1/service.asmx",
                    NombreServicio = "wsbfev1"
                };
                list.Add(item);
                context.AfipWs.AddRange(list);
                context.SaveChanges();
            }   
            }
        }
    }
