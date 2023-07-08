using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Ventas;

namespace BackEnd.Services.Services.Venta
{
    public interface IModeloAsientoFacturaService : IService<ModeloAsientoFactura, int>
    {

    }
    public class ModeloAsientoFacturaService:ServiceBase<ModeloAsientoFactura, int>,IModeloAsientoFacturaService
    {
        public ModeloAsientoFacturaService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {        
        }

    }
}
