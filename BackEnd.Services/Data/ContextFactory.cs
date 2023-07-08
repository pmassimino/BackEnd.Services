using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;


namespace BackEnd.Services.Data
{
    public interface ISessionService
    {
        Guid IdEmpresa { get; set; }
        string IdAccount { get; set; }
    }

    public interface IContextFactory
    {

        DbContext DbContext { get; }
    }
    public class ContextFactory : IContextFactory
    {
        private IDataBaseManager _dataBaseManager;
        private ISessionService _sessionService;

        private Guid _id;
        public DbContext DbContext
        {
            get
            {
                //var dbOptionsBuidler = this.ChangeDatabaseNameInConnectionString();
                // Add new (changed) database name to db options
                DbContextOptions<GestionDBContext> options = new DbContextOptions<GestionDBContext>();
                var bbContextOptionsBuilder = new DbContextOptionsBuilder<GestionDBContext>();
                var IdEmpresa = _sessionService.IdEmpresa;
                string databaseName = _dataBaseManager.GetDataBaseName(IdEmpresa);
                bbContextOptionsBuilder.UseSqlite("Data Source=" + databaseName.Trim() + ".db");                
                return new GestionDBContext(bbContextOptionsBuilder.Options);
            }
        }

        public ContextFactory(IDataBaseManager dataBaseManager, ISessionService sesionService)
        {
            this._dataBaseManager = dataBaseManager;
            this._sessionService = sesionService;
        }

    }
}

