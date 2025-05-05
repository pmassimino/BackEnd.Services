using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Afip;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Mail;
using BackEnd.Services.Models.Ventas;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BackEnd.Services.Services.Venta
{
    public interface IMailService : IService<Mail, int>
    {

    }
    public class MailService : ServiceBase<Mail, int>, IMailService
    {
        public MailService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override ValidationResults ValidateUpdate(Mail entity)
        {
            var result =  base.ValidateUpdate(entity);
            if (string.IsNullOrEmpty(entity.Subject)) 
            {
                result.AddResult(new ValidationResult("Asunto  no Valido", this, "Subject", "Subject", null));
            }
            if (!IsValidEmail(entity.From))
            {
                result.AddResult(new ValidationResult("Email no Valido", this, "From", "From", null));
            }
            if (!IsValidEmail(entity.To))
            {
                result.AddResult(new ValidationResult("Email no Valido", this, "To", "To", null));
            }   
            
            return result;
        }
        public override ValidationResults Validate(Mail entity)
        {
           var result = ValidateUpdate(entity);
            return result;
        }
        public override Mail AddDefaultValues(Mail entity)
        {
            entity.SentAt = DateTime.Now;
            return base.AddDefaultValues(entity);
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
