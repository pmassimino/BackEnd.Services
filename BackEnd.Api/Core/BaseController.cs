using BackEnd.Services.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackEnd.Api.Core
{

    
    public abstract class BaseController: ControllerBase 
    {
    
        public BaseController()
        {
        
        }
              
        public Dictionary<string, string> errorToValidationError(ValidationResults error)
        {            
            Dictionary<string, string> result = new Dictionary<string, string>();            
            foreach (var item in error)
            {                
                string key = item.Key;
                string message = item.Message;
                if (result.ContainsKey(key))
                {
                    result[key] = result[key] + ";" + message;
                }
                else 
                {
                    result.Add(item.Key, item.Message);
                }
            }
            return result;
        }
        public decimal GetIdUsuario()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string id = claimsIdentity.Claims.Where(c => c.Type == "IdAccount").FirstOrDefault().Value;
            decimal idUsuario = decimal.Parse(id);
            return idUsuario;
        }


    }
}
