using BackEnd.Services.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Data
{
    public class UnitOfWorkGlobalDb : IUnitOfWork
    {
        public DbContext Context { get; }       

        public UnitOfWorkGlobalDb(GlobalDBContext context)
        {
            Context = context;
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
