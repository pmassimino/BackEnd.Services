using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services.Services.Global
{
    public interface IComprobanteService : IService<Comprobante, int>
    {
        Comprobante GetBy(string letra, int tipo);
    }
    public class ComprobanteService : ServiceBase<Comprobante, int>, IComprobanteService
    {
        public ComprobanteService(UnitOfWorkGlobalDb UnitOfWork) : base(UnitOfWork)
        { 
        }
       public Comprobante GetBy(string letra, int idTipo)
        {
           int id = 0;

           if (letra == "A")
             {
             if (idTipo == 1)
                id = 1;
             else if (idTipo == 2)
               id = 3;
             else if (idTipo == 3)
               id = 2;
              }

           if (letra == "B")
              {
              if (idTipo == 1)
                 id = 6;
              else if (idTipo == 2)
                 id = 8;
              else if (idTipo == 3)
                 id = 7;
               }

             if (letra == "C")
              {
              if (idTipo == 1)
                 id = 11;
              else if (idTipo == 2)
                 id = 13;
              else if (idTipo == 3)
                 id = 12;
              }
             if (letra == "M")
                {
                if (idTipo == 1)
                  id = 51;
                else if (idTipo == 2)
                  id = 53;
                else if (idTipo == 3)
                  id = 52;
              }
             if (letra == "E")
                {
                if (idTipo == 1)
                   id = 19;
                else if (idTipo == 2)
                   id = 21;
                else if (idTipo == 3)
                   id = 20;
              }
              var result = this.GetOne(id);
             return result;
       }
        }
    }

