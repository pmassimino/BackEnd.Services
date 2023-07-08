using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Services.Global
{
    public interface IAuthService
    {        
        bool Authorize(string idPermiso);
    }
    public class AuthService : IAuthService
    {
        ISessionService sessionService { get; set; }
        private RepositoryBase<Rol, int> rolRepository;

        public AuthService(ISessionService sessionService, UnitOfWorkGlobalDb unitOfWork)
        {
            this.sessionService = sessionService;
            this.rolRepository = new RepositoryBase<Rol, int>(unitOfWork);

        }
        bool IAuthService.Authorize(string idPermiso)
        {
            string idAccount = this.sessionService.IdAccount;
            Guid idEmpresa = this.sessionService.IdEmpresa;
            var roles = rolRepository.GetAll().Include("Accounts").Where(w => w.Accounts.Any(a => a.IdAccount == idAccount)).Include("Permisos");
            var tienePermiso = roles.Where(w => w.Permisos.Any(a => a.IdPermiso == idPermiso)).Count() > 0;
            return tienePermiso;
        }
    }
    public static class AuthoConstants
    {
        public static class Permision
        {
            public const string add = "add";
            public const string insert = "insert";
            public const string update = "update";
            public const string delete = "delete";
            public const string query = "query";

        }
    }
}
