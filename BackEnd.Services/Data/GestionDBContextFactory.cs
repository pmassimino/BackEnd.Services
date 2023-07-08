using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Infrastructure;


    namespace BackEnd.Services.Data
    {
        public class GestionDBContextFactory : IDesignTimeDbContextFactory<GestionDBContext>
        {
            public GestionDBContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<GestionDBContext>();
                optionsBuilder.UseSqlite("Data Source=HerediaDB.db");

                return new GestionDBContext(optionsBuilder.Options);
            }
        }
    }
}
