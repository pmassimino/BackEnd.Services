using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Afip
{
    public interface IAFIPHelperService
    {
        public int GetIdComprobanteAfip(string letra, string idTipo);
    }
    public class AFIPHelperService : IAFIPHelperService
    {
        public int GetIdComprobanteAfip(string letra, string idTipo)
        {
            int result = 0;
            if (letra == "A")
            {
                if (idTipo == "1") //Factura
                {
                    result = 1;
                }
                else if (idTipo == "2") //Nota Credito
                {
                    result = 3;
                }
                else if (idTipo == "3") //Nota Debito
                {
                    result = 2;
                }
            }
            if (letra == "B")
            {
                if (idTipo == "1") //Factura
                {
                    result = 6;
                }
                else if (idTipo == "2") //Nota Credito
                {
                    result = 8;
                }
                else if (idTipo == "3") //Nota Debito
                {
                    result = 7;
                }
            }
            if (letra == "C")
            {
                if (idTipo == "1") //Factura
                {
                    result = 11;
                }
                else if (idTipo == "2") //Nota Credito
                {
                    result = 13;
                }
                else if (idTipo == "3") //Nota Debito
                {
                    result = 12;
                }
            }
            return result;
        }
        //Factura Electronica
        public IList<Concepto> Conceptos()
        {
            var result = new List<Concepto>();
            result.Add(new Concepto() { Id = 1, Nombre = "Productos" });
            result.Add(new Concepto() { Id = 2, Nombre = "Servicios" });
            result.Add(new Concepto() { Id = 3, Nombre = "Productos y Servicios" });
            return result;
        }
        public IList<Comprobante> Comprobantes() 
        {
            var result = new List<Comprobante>();
            result.Add(new Comprobante() { Id = "001", Nombre = "FACTURAS A" });
            result.Add(new Comprobante() { Id = "002", Nombre = "NOTAS DE DEBITO A" });
            result.Add(new Comprobante() { Id = "003", Nombre = "NOTAS DE CREDITO A" });
            result.Add(new Comprobante() { Id = "004", Nombre = "RECIBOS A" });
            result.Add(new Comprobante() { Id = "005", Nombre = "NOTAS DE VENTA AL CONTADO A" });
            result.Add(new Comprobante() { Id = "006", Nombre = "FACTURAS B" });
            result.Add(new Comprobante() { Id = "007", Nombre = "NOTAS DE DEBITO B" });
            result.Add(new Comprobante() { Id = "008", Nombre = "NOTAS DE CREDITO B" });
            result.Add(new Comprobante() { Id = "009", Nombre = "RECIBOS B" });
            result.Add(new Comprobante() { Id = "010", Nombre = "NOTAS DE VENTA AL CONTADO B" });
            result.Add(new Comprobante() { Id = "011", Nombre = "FACTURAS C" });
            result.Add(new Comprobante() { Id = "012", Nombre = "NOTAS DE DEBITO C" });
            result.Add(new Comprobante() { Id = "013", Nombre = "NOTAS DE CREDITO C" });
            result.Add(new Comprobante() { Id = "014", Nombre = "RECIBOS C" });
            result.Add(new Comprobante() { Id = "015", Nombre = "NOTAS DE VENTA AL CONTADO C" });
            result.Add(new Comprobante() { Id = "019", Nombre = "FACTURA EXPORTACION" });
            result.Add(new Comprobante() { Id = "020", Nombre = "NOTAS DE DEBITO POR OPERACIONES CON EL EXTERIOR" });
            result.Add(new Comprobante() { Id = "021", Nombre = "NOTAS DE CREDITO POR OPERACIONES CON EL EXTERIOR" });
            result.Add(new Comprobante() { Id = "051", Nombre = "FACTURAS M" });
            result.Add(new Comprobante() { Id = "052", Nombre = "NOTAS DE DEBITO M" });
            result.Add(new Comprobante() { Id = "053", Nombre = "NOTAS DE CREDITO M" });
            result.Add(new Comprobante() { Id = "060", Nombre = "CUENTAS DE VENTA Y LIQUIDO PRODUCTO A" });
            result.Add(new Comprobante() { Id = "061", Nombre = "CUENTAS DE VENTA Y LIQUIDO PRODUCTO B" });

            return result;
        }

    }
    //Factura
    public class Concepto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class Comprobante 
    {
        public string Id { get; set; }
        public String Nombre { get; set; }
    }

}
