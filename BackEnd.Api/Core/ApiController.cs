using BackEnd.Services.Core;
using BackEnd.Services.Services.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    [Authorize]
    [Route("api/[controller]")]
    public abstract class ApiController<TEntity, Tid> : Controller where TEntity : class
    {
        public  IService<TEntity, Tid> _service;
        public string NombreRecurso { get; set; }
        public IAuthService authService { get; set; }
       
        public int pageSize { get; set; } = 20;

        public ApiController(IService<TEntity, Tid> service,IAuthService authService)
        {
            _service = service;
            this.authService = authService;            
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {            
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
           
            try
            {
               return Ok(_service.GetAll());
                    
            }
            catch (Exception ex)
            {
              return BadRequest(ex.Message);
            }
        }
        [HttpGet("page/{page}")]
        public virtual IActionResult GetAllPaged(int page =1)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }

            try
            {
                return Ok(_service.GetAll(page));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("totalItems")]
        public virtual IActionResult GetTotalItems()
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }

            try
            {
                int totalItems = _service.GetAll().Count();               
                return Ok(totalItems);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{name}")]
        public IActionResult ByName(string name)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                return Ok(_service.GetByName(name));
            }            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{name}/page/{page}")]
        public IActionResult ByNamePaged(string name, int page = 1)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                return Ok(_service.GetByName(name, page));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual IActionResult Find(Tid id)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                var record = _service.GetOne(id);
                if (record == null) return NotFound();
                return Ok(record);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        [HttpGet("ByTransaccion/{id}")]
        public virtual IActionResult FindByTransaccion(Guid id)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                var record = _service.GetByTransaction(id);
                if (record == null) return NotFound();
                return Ok(record);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        public virtual IActionResult NewDefault()
        {
            return Ok(_service.NewDefault());
        }

        [HttpPost]
        public virtual IActionResult Add([FromBody] TEntity entity)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                var result = _service.Validate(entity);
                if (result.IsValid == false)                    
                    return BadRequest(errorToValidationError(result));
                var entityResult = _service.Add(entity);
                return Ok(entityResult);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        [HttpPut("{id}")]
        public virtual IActionResult Update(Tid id, TEntity entity)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                var result = _service.ValidateUpdate(entity);
                if (result.IsValid == false)
                    return BadRequest(errorToValidationError(result));
                var entityResult = _service.Update(id, entity);
                return Ok(entityResult);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Tid id)
        {
            if (!string.IsNullOrEmpty(this.NombreRecurso))
            {
                string permiso = this.NombreRecurso + ".GetAll";
                if (!this.authService.Authorize(permiso))
                {
                    return BadRequest("Permiso Denegado");
                }
            }
            try
            {
                var entity = _service.GetOne(id);
                var result = _service.ValidateDelete(entity);
                if (result.IsValid == false)
                    return BadRequest(errorToValidationError(result));
                _service.Delete(id);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        private string errorToString(ValidationResults error)
        {
            string result = "Error Validacion \n";
            foreach (var item in error) 
            {
                result += item.Key + "-" +  item.Message + "\n";
                     
            }
            return result;
        }
        private BaddError errorToBadError(ValidationResults error)
        {
            BaddError result = new BaddError();
            foreach (var item in error)
            {
                BadErrorItem be = new BadErrorItem();
                be.Tags = item.Key;
                be.Message = item.Message;
                result.error.Add(be);
            }
            return result;
        }
              
        private Dictionary<string, string> errorToValidationError(ValidationResults error)
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
        private decimal GetIdUsuario()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string id = claimsIdentity.Claims.Where(c => c.Type == "IdAccount").FirstOrDefault().Value;
            decimal idUsuario = decimal.Parse(id);
            return idUsuario;
        }


    }
}
public class BadErrorItem
{
    public string Tags { get; set; }
    public string Message { get; set; }
}

public class BaddError
{
    public BaddError() 
    {
        this.error = new List<BadErrorItem>();
    }
    public IList<BadErrorItem> error { get; set; }
}