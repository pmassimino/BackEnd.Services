using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Contable;
using BackEnd.Services.Models.Tesoreria;
using BackEnd.Services.Models.Ventas;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using BackEnd.Services.Services.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BackEnd.Services.Services.Venta
{
    public interface IConfigFacturaService : IService<ConfigFactura, string>
    {
        NumeradorDocumento GetNumeradorDocumento(string idSeccion, int idTipo, string letra);
    }
    public class ConfigFacturaService : ServiceBase<ConfigFactura, string>, IConfigFacturaService
    {
        
        private IModeloAsientoFacturaService modeloAsientoFactura { get; set; }
        private IGlobalService globalService { get; set; }
        private  ICoreServices coreService { get; set; }
        private INumeradorDocumentoService numeradorDocumentoService { get; set; }

        private RepositoryBase<Factura, Guid> facturaRepository;

        public ConfigFacturaService(UnitOfWorkGestionDb UnitOfWork, IModeloAsientoFacturaService modeloAsientoFactura,
            IGlobalService globalService, INumeradorDocumentoService numeradorDocumentoService,
            ICoreServices coreService) : base(UnitOfWork)
        {
            this.globalService = globalService;
            this.modeloAsientoFactura = modeloAsientoFactura;
            this.numeradorDocumentoService = numeradorDocumentoService;
            this.coreService = coreService;
            this.facturaRepository = new RepositoryBase<Factura, Guid>(UnitOfWork);
        }
        public override IEnumerable<ConfigFactura> GetAll()
        {
            return this._Repository.GetAll().Include(i => i.Numeradores).ThenInclude(ti=>ti.NumeradorDocumento);
        }
        public override ConfigFactura GetOne(string id)
        {
            return this.GetAll().Where(w=>w.Id == id).FirstOrDefault();
        }

        public NumeradorDocumento GetNumeradorDocumento(string idSeccion, int idTipo, string letra)
        {
            NumeradorDocumento result = null;
            var tmpComp = this.globalService.ComprobanteService.GetBy(letra, idTipo);
            int idComp = 0;
            if (tmpComp != null) 
            {
                idComp = tmpComp.Id;
            }
            var tmpResult = this.GetOne(idSeccion).Numeradores.Where(w => w.IdComprobante == idComp).FirstOrDefault();
            if (tmpResult != null) 
            {
                result = numeradorDocumentoService.GetOne(tmpResult.Id);
            }
            return result;
        }
        public override ValidationResults ValidateDelete(ConfigFactura entity)
        {
            var result =  base.ValidateDelete(entity);
            var existeFactura = facturaRepository.GetAll().Where(w=>w.IdSeccion == entity.Id).Count() >0;
            if (existeFactura) 
            {
                result.AddResult(new ValidationResult("Hay facturas relacionadas con esta seccion, no se puede borrar", this, "IdSeccion", "IdSeccion", null));
            }
            return result;
        }
    }

}
