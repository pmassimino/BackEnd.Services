using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Contable
{
    public interface IContableService
    {
        ICuentaMayorService CuentaMayorService { get; set; }
        IMovCtaCteService MovCtaCteService { get; set; }
        IMayorService MayorService { get; set; }
        ITipoCuentaMayorService TipoCuentaMayorService { get; set; }
        IUsoCuentaMayorService UsoCuentaMayorService { get; set; }

    }
    public class ContableService : IContableService
    {
        public ContableService(ICuentaMayorService serv1, IMovCtaCteService serv2, IMayorService serv3, ITipoCuentaMayorService serv4, IUsoCuentaMayorService serv5)
        {
            this.CuentaMayorService = serv1;
            this.MovCtaCteService = serv2;
            this.MayorService = serv3;
            this.TipoCuentaMayorService = serv4;
            this.UsoCuentaMayorService = serv5;
        }
        public ICuentaMayorService CuentaMayorService { get; set; }

        public IMayorService MayorService { get; set; }

        public IMovCtaCteService MovCtaCteService { get; set; }
        public ITipoCuentaMayorService TipoCuentaMayorService { get; set; }
        public IUsoCuentaMayorService UsoCuentaMayorService { get; set; }

    }
   
}
