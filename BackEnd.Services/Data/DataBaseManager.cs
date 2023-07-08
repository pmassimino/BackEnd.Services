using BackEnd.Services.Services.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Data
{
    public interface IDataBaseManager
    {
        string GetDataBaseName(Guid id);
    }
    public class DataBaseManager : IDataBaseManager
    {
        IEmpresaService empresaService;
        public DataBaseManager(IEmpresaService empresaService) 
        {
            this.empresaService = empresaService;
        }
        public string GetDataBaseName(Guid id)
        {
            string databaseName = "HerediaDB";            
            var empresa = this.empresaService.GetOne(id);
            if (empresa != null) 
            {
                databaseName = empresa.DatabaseName;
            }
            return databaseName;
        }
    }
}
