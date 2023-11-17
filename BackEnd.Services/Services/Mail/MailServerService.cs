using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Afip;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Models.Mail;
using BackEnd.Services.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Venta
{
    public interface IMailServerService : IService<MailServer, int>
    {

    }
    public class MailServerService : ServiceBase<MailServer, int>, IMailServerService
    {
        public MailServerService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }        
    }
}
