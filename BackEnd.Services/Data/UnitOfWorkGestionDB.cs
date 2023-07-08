using BackEnd.Services.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Data
{
    public class UnitOfWorkGestionDb : IUnitOfWork
    {
        public DbContext Context { get; }

        //public UnitOfWorkGestionDb(GestionDBContext context)
        //{
        //   Context = context;
        //}
        public UnitOfWorkGestionDb(IContextFactory contextFactory)
        {
            Context = contextFactory.DbContext;
           
        }
        public void Commit()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();

        }

    }
}
