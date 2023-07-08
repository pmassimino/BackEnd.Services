using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Data
{
   public interface IEntityModel<T>
    {
        public T Id { get; set; }

    }
    public interface IEntityNameModel
    {
        public string Nombre { get; set; }

    }

    public interface IStructuralEntity
    {
        string IdEmpresa { get; set; }
        string IdSucursal { get; set; }
        string IdSeccion { get; set; }
    }
    public interface ITransaccionalEntity
    {
        Guid IdTransaccion { get; set; }
    }

}
