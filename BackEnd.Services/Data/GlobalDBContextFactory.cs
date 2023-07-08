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
        public class GlobalDBContextFactory : IDesignTimeDbContextFactory<GlobalDBContext>
        {
            public GlobalDBContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<GlobalDBContext>();
                optionsBuilder.UseSqlite("Data Source=Global.db");                
                //optionsBuilder.UseSqlServer("Server=localhost;Database=Global;User Id=sa;Password=your_password;");

                return new GlobalDBContext(optionsBuilder.Options);
            }
        }
    }
}
