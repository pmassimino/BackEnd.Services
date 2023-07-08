using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.Generic;
using System.Linq;


namespace BackEnd.Services.Services.Comun
{
    public interface ISettingService : IService<Setting, string>
    {
        public string GetValue(string key);
        public string SetValue(string key,string value);
    }
    public class SettingService : ServiceBase<Setting, string>, ISettingService
    {
        public SettingService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        }
        public override IEnumerable<Setting> GetByName(string name)
        {
            var result = this.GetAll().Where(p => p.Id.ToUpper().Contains(name.ToUpper()));
            return result;
        }
        string ISettingService.GetValue(string key)
        {
            string result = "";
            var tmpresult = this.GetOne(key);
            if (tmpresult != null) 
            {
                result = tmpresult.Value;
            }
            return result;
        }

        string ISettingService.SetValue(string key, string value)
        {
            var item = this.GetOne(key);
            if (item == null)
            {
                item = new Setting();
                item.Id = key;
                item.Value = value;
                this.Add(item);
            }
            else
            {
                item.Id = key;
                item.Value = value;
                this.Update(key, item);
            }
            return value;
        }
       

    }

}
