using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IAccountService : IService<Account, string>
    {
        ValidationResults ValidateLogin(string username, string pass);
        Account GetByUserName(string name);
        IEnumerable<Rol> GetRoles(string id);

    }
    public class AccountService : ServiceBase<Account, string>, IAccountService
    {

        IRolService rolService;
        public AccountService(UnitOfWorkGlobalDb UnitOfWork,IRolService rolService) : base(UnitOfWork)
        {
            this.rolService = rolService;
        }
        //Encripta el pass
        public string CreatePasswordHash(string password)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();           
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] MessageBytes = UE.GetBytes(password);

            dynamic hashedPwd = sha.ComputeHash(MessageBytes);
            return Convert.ToBase64String(hashedPwd);
        }

        public Account GetByUserName(string name)
        {
            var result = this.GetAll().Where(w => w.Nombre == name).FirstOrDefault();
            return result;
        }

        public ValidationResults ValidateLogin(string username, string pass)
        {
            ValidationResults result = new ValidationResults();
            Account tmpAccount = this._Repository.GetAll().Where(w=>w.Nombre==username).FirstOrDefault();
            //Si no existe Account - no valida
            if (tmpAccount == null)
            {                
                result.AddResult(new ValidationResult("Cuenta no existe", this, "Nombre", "Nombre", null));
                return result;
            }                        
            string currentpass = tmpAccount.Password.Trim();
            string passencrypt = CreatePasswordHash(pass);
            //Si son iguales  - válido
            if (currentpass != passencrypt) 
            {
                result.AddResult(new ValidationResult("Password Incorrecto", this, "Nombre", "Nombre", null));
            }
            return result;
        }
        public IEnumerable<Rol> GetRoles(string id) 
        {
            return rolService.GetByIdAccount(id);
        }

    }
   
}
