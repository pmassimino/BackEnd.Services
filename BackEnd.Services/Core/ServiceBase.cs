using BackEnd.Services.Data;
using BackEnd.Services.Models.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;
using Validator = System.ComponentModel.DataAnnotations.Validator;

namespace BackEnd.Services.Core
{
    public interface IService<TEntity, TId> where TEntity : class
    {
        public bool autoSave { get; set; } 
        public char charFill { get; set; } 
        public int lenFill { get; set; }
        TEntity GetOne(TId id);
        TEntity GetByTransaction(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(int page);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetByName(string name);
        IEnumerable<TEntity> GetByName(string name,int page);
        TEntity Clone(TEntity Entity);
        TEntity NewDefault();
        TId NextID();
        TEntity Add(TEntity entity);
        TEntity AddDefaultValues(TEntity entity);
        void Delete(TId id);
        TEntity Update(TId id,TEntity entity);
        ValidationResults Validate(TEntity entity);
        ValidationResults ValidateUpdate(TEntity entity);
        ValidationResults ValidateDelete(TEntity entity);
        bool FixRelation(TEntity entity);
        public IUnitOfWork UnitOfWork { get; set; }

    }
    public class ServiceBase<TEntity, TId> : IService<TEntity, TId> where TEntity : class, IEntityModel<TId>, new()
    {
        public bool autoSave { get; set; } = true;
        public char charFill { get; set; } = Convert.ToChar("0");
        public int lenFill { get; set; } = 5;
        public int PageSize { get; set; } = 20;

        private IUnitOfWork _unitOfWork;
        public IUnitOfWork UnitOfWork { 
            get { return _unitOfWork; }
            set { _unitOfWork = value; this._Repository.UnitOfWork = value; } }
        //IAuthorizationService _AuthorizationService;
        ISessionService sessionService;
        public IRepository<TEntity, TId> _Repository { get; set ; }
        public string Name { get; set; }

        public ServiceBase() { }

        public ServiceBase(IUnitOfWork unitOfWork,bool autoSave = true)
        {
            this._Repository = new RepositoryBase<TEntity, TId>(unitOfWork);
            this.UnitOfWork = unitOfWork;            
            this.autoSave = autoSave;            
        }

        public IPrincipal User()
        {
            return System.Threading.Thread.CurrentPrincipal;
        }
       

       

        #region "Select"

        public virtual IEnumerable<TEntity> GetAll()
        {
            //Pedir Autorización
            //if (!this.CanExecute(AuthorizationConstants.Permision.add))
            //{
                //throw new AuthorizationException("Tarea no Autorizada para el actual usuario");
            //}

            return _Repository.GetAll();
        }

        public virtual IEnumerable<TEntity> GetAll(int page=1)
        {
            //Pedir Autorización
            //if (!this.CanExecute(AuthorizationConstants.Permision.add))
            //{
            //throw new AuthorizationException("Tarea no Autorizada para el actual usuario");
            //}

            return _Repository.GetAll().Skip(page-1).Take(this.PageSize);
        }


        public virtual TEntity GetOne(TId id)
        {           
            return _Repository.GetOne(id);
        }

        public IEnumerable<TEntity> GetAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _Repository.GetAll(predicate);
        }
        public virtual TEntity GetByTransaction(Guid id)
        {
            throw new NotImplementedException();
        }



        public virtual IEnumerable<TEntity> GetByName(string name)
        {           

            throw new NotImplementedException();
        }
        public virtual IEnumerable<TEntity> GetByName(string name,int page = 1)
        {

            throw new NotImplementedException();
        }

        #endregion

        #region "Update"
        public virtual TEntity Add(TEntity entity)
        {            
            //Validar
            ValidationResults result = this.Validate(entity);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }
            //Fix Relation 
            this.FixRelation(entity);
            //set default values
            entity = this.AddDefaultValues(entity);
            var entityResult = _Repository.Add(entity);
            //Grabar Cambios
            if (autoSave)
            UnitOfWork.Commit();

            return entityResult;
        }

        public virtual TEntity Update(TId id, TEntity entity)
        {           
            //Validar
            ValidationResults result = this.ValidateUpdate(entity);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }
            //Fix Relation 
            this.FixRelation(entity);
            //set default values
            entity = this.UpdateDefaultValues(entity);

            var entityResult = _Repository.Update(id, entity);
            //Grabar Cambios
            if (autoSave)
               UnitOfWork.Commit();
            return entityResult;
        }


        public virtual void Delete(TId id)
        {            
            //Validar
            var entity = this.GetOne(id);
            ValidationResults result = this.ValidateDelete(entity);
            if (!result.IsValid)
            {
                //throw new BusinessValidationExeption("Error de Validacion", result);
            }
            _Repository.Delete(id);
            if (autoSave)
                UnitOfWork.Commit();
        }
        #endregion


        #region "Validation"
        public virtual ValidationResults Validate(TEntity entity)
        {
            ValidationResults result = this.GetErrors(entity);
            //Validar id Repetido
            var exist = this.GetOne(entity.Id);
            if (exist != null)
            {
                result.AddResult(new ValidationResult("Valor repetido", this, "Id", "Id", null));
            }
            return result;
        }
        public virtual ValidationResults ValidateUpdate(TEntity entity)
        {
            ValidationResults result = this.GetErrors(entity);
            return result;
        }
        public virtual ValidationResults ValidateDelete(TEntity entity)
        {
            ValidationResults result = new ValidationResults();
            return result;
        }



        private ValidationResults GetErrors(TEntity entity)
        {
            var context = new ValidationContext(entity, null, null);
            var errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            bool valid = Validator.TryValidateObject(entity, context, errors);
            ValidationResults result = new ValidationResults();
            foreach (var item in errors)
            {
                var tmpResult = new ValidationResults();
                result.AddResult(new ValidationResult(item.ErrorMessage, this, "", "", null));
            }
            return result;
        }

        #endregion


        public TEntity Clone(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool FixRelation(TEntity entity)
        {
            return true;
        }




        public virtual TEntity AddDefaultValues(TEntity entity)
        {
            return entity;
        }

        public virtual TEntity UpdateDefaultValues(TEntity entity)
        {
            return entity;
        }

        public virtual TEntity NewDefault()

        {
            //return new TEntity();
            var newEntity = new TEntity();
            if (newEntity is IEntityModel<string>)
            {
                var count = this.GetAll().Count();
                do
                {
                    count += 1;
                    string newIdStr = (count).ToString().PadLeft(this.lenFill, this.charFill);
                    foreach (var property in typeof(TEntity).GetProperties())
                    {
                        if (property.Name == "Id") // setting value for x
                        {
                            property.SetValue(newEntity, newIdStr);
                        }
                    }
                }
                while (this.GetOne(newEntity.Id) != null);
            }
            
            return newEntity;
        }
        public virtual TId NextID() 
        {
            throw new NotImplementedException();
        }

        
    }
       
    public static class helper
    {
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
    }

}

